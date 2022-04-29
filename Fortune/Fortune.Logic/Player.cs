using Fortune.Logic.Fields;

namespace Fortune.Logic
{
    public class Player
    {
        public string Name { get; }
        public int Number { get; }
        public int Cash { get; private set; }
        
        private Field location;
        public bool IsBankrupt;
        public int NumberOfJokers { get; private set; }
        private readonly List<Certificate> certificates;

        public Player(string name, int number, int cash)
        {
            Name = name;
            Number = number;
            Cash = cash;
            NumberOfJokers = 0;
            certificates = new List<Certificate>();
            location = new Start();
        }

        public void UpdateCash(int value)
        {
            Cash += value;
            if (Cash < 0)
            {
                IsBankrupt = true;
                // Handle bankruptcy
            }
        }

        public void MoveTo(Field field)
        {
            location = field;
        }

        public Field GetLocation()
        {
            return location;
        }

        public int GetPercentageForResource(Resource resource)
        {
            return certificates.Where(certificate => certificate.Resource == resource).Sum(certificate => certificate.Percentage);
        }

        public bool HasResource(Resource resource)
        {
            return certificates.Any(certificate => certificate.Resource == resource);
        }

        public List<Resource> GetResourcesOwned()
        {
            return certificates.Select(certificate => certificate.Resource).Distinct().ToList();
        }

        public void Auction(int numberOfCertificates)
        {
            // Implement auction behavior, including Joker behavior
        }

        public void OfferCertificates(List<Certificate> allowedCertificates)
        {
            // Event to ui to offer cards
        }
        
        public void AddCertificate(Certificate certificate)
        {
            certificates.Add(certificate);
        }

        public void OfferJoker()
        {
            // Event to UI to offer a Joker
        }

        public void BuyJoker()
        {
            NumberOfJokers++;
        }

        public void SellJoker()
        {
            NumberOfJokers--;
        }


        public bool HasCertificate(Certificate certificate)
        {
            return certificates.Contains(certificate);
        }

        public void RemoveCertificate(Certificate certificate)
        {
            certificates.Remove(certificate);
        }
    }
}
