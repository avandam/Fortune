using Fortune.Logic.Exceptions;

namespace Fortune.Logic
{
    public abstract class Field
    {
        protected Game Game { get; }
        public Resource Resource { get; }
        protected List<Certificate> Certificates { get; }
        public Continent Continent { get; } // TODO: Consider removing this and get it from certificates -> What if all certificates are bought?
        public int Number { get; }
        public string Name { get; }

        protected Field(Game game, int number, string name) : this(game, number, name, null, null)
        {
        }

        protected Field(Game game, int number, string name, Resource resource) : this(game, number, name, resource, null)
        {
        }

        protected Field(Game game, int number, string name, List<Certificate> certificates) : this(game, number, name, null, certificates)
        {
        }

        protected Field(Game game, int number, string name, Resource resource, List<Certificate> certificates)
        {
            Game = game;
            Number = number;
            Name = name;
            Resource = resource;
            Certificates = certificates;
            if (Certificates != null && Certificates.Count > 0)
            {
                Continent = Certificates[0].Area.Continent; // TODO: Fix code smell
            }
        }

        public abstract void DoAction(Player currentPlayer, int redDiceValue, int whiteDiceValue);

        public bool HasResource()
        {
            return Resource != null;
        }

        public virtual void ReturnCertificate(Certificate certificate)
        {
            throw new CertificateNotSupportedException($"Field {Name} does not support certificate actions");
        }

        public virtual void BuyCertificate(Player player, Certificate certificate)
        {
            throw new CertificateNotSupportedException($"Field {Name} does not support certificate actions");
        }

        public virtual List<Certificate> GetCertificates()
        {
            throw new CertificateNotSupportedException($"Field {Name} does not support certificate actions");
        }

        public virtual List<Certificate> GetCertificatesWithResources(List<Resource> resources)
        {
            throw new CertificateNotSupportedException($"Field {Name} does not support certificate actions");
        }

        public override string ToString()
        {
            return Name;
        }
    }
}