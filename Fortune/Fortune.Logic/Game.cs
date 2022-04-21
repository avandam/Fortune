namespace Fortune.Logic
{
    public class Game
    {
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
            field.DoAction(currentPlayer, redDie, whiteDie);

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
                        currentPlayer.UpdateCash(0 - fee);
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
                currentPlayer.UpdateCash(redDie * -1000000);
            }

            int newFieldNumber = (currentPlayer.GetLocation().Number + redDie + whiteDie) % fields.Count;
            Field field = fields.First(field => field.Number == newFieldNumber);
            currentPlayer.MoveTo(field);
            return field;
        }

        public List<Certificate> GetPossibleCertificates(List<Continent> continents, Player player)
        {
            List<Resource> allowedResources = player.GetResourcesOwned();
            List<Certificate> possibleCertificates = new List<Certificate>();
            foreach (Area area in fields.Where(field => field is Area && continents.Contains((field as Area).Continent)))
            {
                List<Certificate> certificates = area.GetCertificates();
                possibleCertificates.AddRange(certificates.Where(certificate => allowedResources.Contains(certificate.Resource)));
            }

            return possibleCertificates;
        }

        public List<Certificate> GetPossibleCertificates(Player player)
        {
            List<Resource> allowedResources = player.GetResourcesOwned();
            List<Certificate> possibleCertificates = new List<Certificate>();
            foreach (Area area in fields.Where(field => field is Area))
            {
                List<Certificate> certificates = area.GetCertificates();
                possibleCertificates.AddRange(certificates.Where(certificate => allowedResources.Contains(certificate.Resource)));
            }

            return possibleCertificates;
        }

        public void BuyCertificate(Player currentPlayer, Certificate certificate)
        {
            if (certificate.Area.GetCertificates().Contains(certificate))
            {
                if (currentPlayer.Cash >= certificate.Price)
                {
                    currentPlayer.BuyCertificate(certificate);
                    certificate.Area.GetCertificates().Remove(certificate);
                }
            }
        }
    }
}
