using Fortune.Logic.Exceptions;

namespace Fortune.Logic.Fields
{
    public abstract class Field
    {
        protected Game Game { get; }
        protected List<Certificate> Certificates { get; }
        public Zone Zone { get; } 
        public int Number { get; }
        public string Name { get; }

        protected Field(Game game, int number, string name) : this(game, number, name, null)
        {
        }

        protected Field(Game game, int number, string name, List<Certificate> certificates)
        {
            Game = game;
            Number = number;
            Name = name;
            Certificates = certificates;
            if (Certificates != null && Certificates.Count > 0)
            {
                Zone = Certificates[0].Zone; 
            }
        }

        public virtual void OnEntry(int redDiceValue, int whiteDiceValue)
        {
            // Default: Do Nothing
        }

        public virtual void DoAction(int redDiceValue, int whiteDiceValue)
        {
            // Default: Do Nothing
        }

        public virtual void ReturnCertificate(Certificate certificate)
        {
            throw new CertificateNotSupportedException($"Field {Name} does not support certificate actions");
        }

        public virtual void BuyCertificate(Certificate certificate)
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