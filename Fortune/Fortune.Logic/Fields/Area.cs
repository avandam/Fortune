using Fortune.Logic.Exceptions;

namespace Fortune.Logic.Fields
{
    public class Area : Field
    {
        public Area(Game game, int number, Resource resource, List<Certificate> certificates) : base(game, number, certificates[0].Zone.Country, resource, certificates)
        {
        }

        public override void OnEntry(int redDiceValue, int whiteDiceValue)
        {
            Game.HandleResourceFee(Resource);
        }

        public override void DoAction(int redDiceValue, int whiteDiceValue)
        {
            Game.HandleArea(Certificates);
        }

        public override List<Certificate> GetCertificates()
        {
            return Certificates;
        }

        public override List<Certificate> GetCertificatesWithResources(List<Resource> resources)
        {
            return Certificates.Where(cert => resources.Contains(cert.Resource)).ToList();
        }

        public override void ReturnCertificate(Certificate certificate)
        {
            if (Certificates.Any(cert => cert == certificate))
            {
                throw new CertificateActionNotAllowedException($"Certificate {certificate} is already in this area");
            }

            if (certificate.Zone != Zone)
            {
                throw new CertificateActionNotAllowedException($"Certificate {certificate} cannot be returned to a different area");
            }

            Certificates.Add(certificate);
        }

        public override void BuyCertificate(Certificate certificate)
        {
            if (!Certificates.Contains(certificate))
            {
                throw new CertificateActionNotAllowedException($"Certificate {certificate} is not available in this area");
            }

            Certificates.Remove(certificate);
        }
    }
}
