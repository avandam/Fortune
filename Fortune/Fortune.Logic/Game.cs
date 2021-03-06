using Fortune.Logic.Exceptions;
using Fortune.Logic.Fields;

namespace Fortune.Logic
{
    public class Game
    {
        private const int jokerPrice = 3000000;
        private const int bonusValue = 500000;
        private const int doubleFee = -1000000;

        private const int maxNumberOfCertificatesToBuy = 3;
        public int NumberOfCertificatesBoughtThisTurn { get; private set; }

        private readonly Random random = new Random();
        private readonly List<Player> players;
        private Player currentPlayer;

        public Game(List<Player> players)
        {
            this.players = players;
            this.currentPlayer = players.First();
            GameData.InitializeData(this);
        }

        public void AddBoard(List<Field> boardFields)
        {
            GameData.SetFields(boardFields);
        }

        public void DoTurn()
        {
            if (currentPlayer.IsBankrupt)
            {
                EndTurn();
                return;
            }

            Field field = Move(out var redDie, out var whiteDie);
            field.OnEntry(redDie, whiteDie);
            
            if (redDie == whiteDie)
            {
                currentPlayer.UpdateCash(redDie * doubleFee);
                if (currentPlayer.IsBankrupt)
                {
                    EndTurn();
                    return;
                }
            }
            field.DoAction(redDie, whiteDie);
        }

        public void EndTurn()
        {
            NumberOfCertificatesBoughtThisTurn = 0;
            currentPlayer = players.First(player => player.Number == (currentPlayer.Number + 1) % players.Count);
        }

        private Field Move(out int redDie, out int whiteDie)
        {
            redDie = random.Next(1, 6);
            whiteDie = random.Next(1, 6);

            int newFieldNumber = (currentPlayer.GetLocation().Number + redDie + whiteDie) % GameData.GetNumberOfFields();
            Field field = GameData.GetField(newFieldNumber);
            currentPlayer.MoveTo(field);
            return field;
        }

        public List<Certificate> GetPossibleCertificates(List<ContinentType> continents)
        {
            List<Resource> allowedResources = currentPlayer.GetResourcesOwned();
            List<Certificate> possibleCertificates = new List<Certificate>();
            foreach (Area area in GameData.GetFieldsForContinents(continents))
            {
                List<Certificate> certificates = area.GetCertificates();
                possibleCertificates.AddRange(certificates.Where(certificate => allowedResources.Contains(certificate.Resource)));
            }

            return possibleCertificates;
        }

        public List<Certificate> GetPossibleCertificates()
        {
            List<Resource> allowedResources = currentPlayer.GetResourcesOwned();
            List<Certificate> possibleCertificates = new List<Certificate>();
            foreach (Area area in GameData.GetAreas())
            {
                List<Certificate> certificates = area.GetCertificates();
                possibleCertificates.AddRange(certificates.Where(certificate => allowedResources.Contains(certificate.Resource)));
            }

            return possibleCertificates;
        }

        #region PlayerActions
        public void BuyJoker()
        {
            if (currentPlayer.Cash < jokerPrice)
            {
                throw new JokerException("Player can not afford a Joker");
            }

            currentPlayer.UpdateCash(-jokerPrice);
            currentPlayer.BuyJoker();
        }

        public void SellJoker()
        {
            if (currentPlayer.NumberOfJokers == 0)
            {
                throw new JokerException("Can not sell a Joker if you do not have one");
            }

            currentPlayer.UpdateCash(jokerPrice);
            currentPlayer.SellJoker();
        }

        public void BuyCertificate(Certificate certificate)
        {
            if (NumberOfCertificatesBoughtThisTurn >= maxNumberOfCertificatesToBuy)
            {
                throw new CertificateNotAllowedToBuyException("Already purchased the maximum number of certificates this turn");
            }

            Field certificateField = GameData.GetFieldByZone(certificate.Zone);

            if (!certificateField.GetCertificates().Contains(certificate))
            {
                throw new CertificateActionNotAllowedException($"Field {certificateField} does not contain certificate {certificate}");
            }

            if (currentPlayer.Cash <= certificate.Price)
            {
                throw new CertificateActionNotAllowedException($"Player {currentPlayer} does not have enough cash to buy this certificate");
            }

            certificateField.BuyCertificate(certificate);
            currentPlayer.UpdateCash(-certificate.Price);
            currentPlayer.AddCertificate(certificate);
            NumberOfCertificatesBoughtThisTurn++;
        }

        public void AuctionCertificate(Certificate certificate, Player buyingPlayer, int price)
        {
            if (!currentPlayer.HasCertificate(certificate))
            {
                throw new CertificateSaleInvalidException("You cannot sell a certificate you don't own");
            }

            if (currentPlayer == buyingPlayer)
            {
                throw new CertificateSaleInvalidException("You can not buy your own certificate");
            }

            if (buyingPlayer.Cash < certificate.Price)
            {
                throw new CertificateSaleInvalidException("Buying player can not afford this certificate");
            }

            if (price < certificate.Price / 2)
            {
                throw new CertificateSaleInvalidException("It is not allowed to sell a certificate below half the original price");
            }

            buyingPlayer.UpdateCash(-price);
            currentPlayer.UpdateCash(price);
            currentPlayer.RemoveCertificate(certificate);
            buyingPlayer.AddCertificate(certificate);
        }

        public void AuctionCertificateToBank(Certificate certificate)
        {
            if (!currentPlayer.HasCertificate(certificate))
            {
                throw new CertificateSaleInvalidException("You cannot sell a certificate you don't own");
            }

            Field area = GameData.GetFieldByZone(certificate.Zone);
            currentPlayer.RemoveCertificate(certificate);
            currentPlayer.UpdateCash(certificate.Price / 2);
            area.ReturnCertificate(certificate);
        }
        #endregion PlayerActions

        #region FieldActions
        public void HandleBuyJoker()
        {
            if (currentPlayer.Cash < jokerPrice)
            {
                // Handle UI action to inform no Joker is allowed to be bought
            }
            currentPlayer.OfferJoker();
        }

        public void HandleBonus(int diceValue)
        {
            // Handle UI action to inform that user gets a bonus
            
            currentPlayer.UpdateCash(diceValue * bonusValue);
        }

        public void HandleTelex(TelexCard telexCard)
        {
            int fee = 0;
            // Handle UI action to show the telex information
            if (telexCard.Resources == null || telexCard.Resources.Count == 0)
            {
                if (telexCard.Type == TelexType.GainFromPlayers)
                {
                    foreach (var player in players.Where(player => player != currentPlayer && !player.IsBankrupt))
                    {
                        fee += telexCard.MaxFee;
                        player.UpdateCash(-telexCard.MaxFee);
                    }
                    currentPlayer.UpdateCash(fee);
                    return;
                }
                currentPlayer.UpdateCash(telexCard.Type == TelexType.Gain ? telexCard.MaxFee : -telexCard.MaxFee);
                return;
            }

            if (telexCard.Resources.Any(resource => currentPlayer.HasResource(resource)))
            {
                currentPlayer.UpdateCash(telexCard.Type == TelexType.Gain ? telexCard.MaxFee : -telexCard.MaxFee);
                return;
            }

            currentPlayer.UpdateCash(telexCard.Type == TelexType.Gain ? telexCard.MinFee : -telexCard.MinFee);
        }

        public void HandleChoiceWorld()
        {
            currentPlayer.OfferCertificates(GetPossibleCertificates());
        }

        public void HandleChoiceContinent(List<ContinentType> continents)
        {
            currentPlayer.OfferCertificates(GetPossibleCertificates(continents));
        }

        public void HandleAuction(int numberOfCertificates)
        {
            currentPlayer.Auction(numberOfCertificates);
        }

        public void HandleArea(List<Certificate> certificates)
        {
            currentPlayer.OfferCertificates(certificates);
        }
        public void HandleResourceFee(Resource resource)
        {
            foreach (Player player in players)
            {
                if (player != currentPlayer)
                {
                    int percentageOfResource = player.GetPercentageForResource(resource);
                    int fee = resource.GetFee(percentageOfResource);
                    player.UpdateCash(fee);
                    currentPlayer.UpdateCash(-fee);
                }
            }
        }

        #endregion FieldActions

    }
}
