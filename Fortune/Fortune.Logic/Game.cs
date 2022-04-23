using System.Runtime.Versioning;
using Fortune.Logic.Exceptions;
using Fortune.Logic.Fields;

namespace Fortune.Logic
{
    public class Game
    {
        private const int jokerPrice = 3000000;
        private const int bonusValue = 500000;
        private const int doubleFee = -1000000;

        private readonly Random random = new Random();
        private readonly List<Player> players;
        private Player currentPlayer;
        private readonly List<Field> fields = new List<Field>();

        public Game(List<Player> players)
        {
            this.players = players;
            this.currentPlayer = players.First();
        }

        public void DoTurn()
        {
            if (currentPlayer.IsBankrupt)
            {
                EndTurn();
                return;
            }

            Field field = Move(out var redDie, out var whiteDie);
            PayResourceFee(field);
            field.DoAction(redDie, whiteDie);
        }

        public void EndTurn()
        {
            currentPlayer = players.First(player => player.Number == (currentPlayer.Number + 1) % players.Count);
        }

        private void PayResourceFee(Field field)
        {
            if (field.HasResource())
            {
                foreach (Player player in players)
                {
                    if (player != currentPlayer)
                    {
                        int percentageOfResource = player.GetPercentageForResource(field.Resource);
                        int fee = field.Resource.GetFee(percentageOfResource);
                        player.UpdateCash(fee);
                        currentPlayer.UpdateCash(-fee);
                    }
                }
            }
        }

        private Field Move(out int redDie, out int whiteDie)
        {
            redDie = random.Next(1, 6);
            whiteDie = random.Next(1, 6);

            if (redDie == whiteDie)
            {
                currentPlayer.UpdateCash(redDie * doubleFee);
            }

            int newFieldNumber = (currentPlayer.GetLocation().Number + redDie + whiteDie) % fields.Count;
            Field field = fields.First(field => field.Number == newFieldNumber);
            currentPlayer.MoveTo(field);
            return field;
        }

        public List<Certificate> GetPossibleCertificates(List<string> zones)
        {
            List<Resource> allowedResources = currentPlayer.GetResourcesOwned();
            List<Certificate> possibleCertificates = new List<Certificate>();
            foreach (Area area in fields.Where(field => field is Area area && zones.Contains(area.Zone.Continent)))
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
            foreach (Area area in fields.Where(field => field is Area))
            {
                List<Certificate> certificates = area.GetCertificates();
                possibleCertificates.AddRange(certificates.Where(certificate => allowedResources.Contains(certificate.Resource)));
            }

            return possibleCertificates;
        }

        public void BuyCertificate(Certificate certificate)
        {
            Field certificateField = fields.First(field => field.Zone == certificate.Zone);

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
        }


        #region PlayerActions

        #endregion PlayerActions

        #region FieldActions
        public void HandleJoker()
        {
            if (currentPlayer.Cash < jokerPrice)
            {
                // Handle UI action to inform no Joker is allowed to be bought
            }
            currentPlayer.OfferJoker();
        }

        public void HandleBonus(int diceValue)
        {
            currentPlayer.UpdateCash(diceValue * bonusValue);
        }

        public void HandleTelex(string text, List<Resource> resources, int maxFee, int minFee, bool isGain)
        {
            // Handle UI action to show the telex information
            if (resources == null || resources.Count == 0)
            {
                currentPlayer.UpdateCash(isGain ? maxFee : -maxFee);
                return;
            }

            if (resources.Any(resource => currentPlayer.HasResource(resource)))
            {
                currentPlayer.UpdateCash(isGain ? maxFee : -maxFee);
                return;
            }

            currentPlayer.UpdateCash(isGain ? minFee : -minFee);
        }

        public void HandleChoiceWorld()
        {
            currentPlayer.OfferCertificates(GetPossibleCertificates());
        }

        public void HandleChoiceContinent(List<string> continents)
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
        #endregion FieldActions
    }
}
