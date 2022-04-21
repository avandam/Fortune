using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fortune.Logic.Exceptions;

namespace Fortune.Logic
{
    public class Area : Field
    {
        public Area(Game game, int number, string name, Resource resource, List<Certificate> certificates) : base(game, number, name, resource, certificates)
        {
        }

        public override void DoAction(Player currentPlayer, int redDiceValue, int whiteDiceValue)
        {
            currentPlayer.OfferCertificates(Certificates);
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
                throw new CertificateActionNotAllowedException($"Certificate {certificate.ToString()} is already in this area");
            }

            if (certificate.Area != this)
            {
                throw new CertificateActionNotAllowedException($"Certificate {certificate.ToString()} cannot be returned to a different area");
            }

            Certificates.Add(certificate);
        }

        public override void BuyCertificate(Player player, Certificate certificate)
        {
            if (!Certificates.Contains(certificate))
            {
                throw new CertificateActionNotAllowedException($"Certificate {certificate.ToString()} is not available in this area");
            }

            if (player.Cash < certificate.Price)
            {
                throw new CertificateActionNotAllowedException($"Certificate {certificate.ToString()} can not be afforded");
            }

            player.BuyCertificate(certificate);
            Certificates.Remove(certificate);
        }
    }
}
