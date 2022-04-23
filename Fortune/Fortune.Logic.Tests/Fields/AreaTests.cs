using System.Collections.Generic;
using System.Drawing;
using Fortune.Logic.Exceptions;
using Fortune.Logic.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fortune.Logic.Tests.Fields
{
    [TestClass()]
    public class AreaTests
    {
        [TestMethod()]
        public void GetCertificatesTest()
        {
            Zone zone = new Zone("Benelux", "Europe", Color.LimeGreen);
            Resource resource1 = new Resource("Aluminum", 500000);
            Resource resource2 = new Resource("Gold", 500000);
            Resource resource3 = new Resource("Tea", 500000);
            Certificate certificate1 = new Certificate(resource1, 20, 2000000, zone, "Belgium");
            Certificate certificate2 = new Certificate(resource1, 5, 500000, zone, "Netherlands");
            Certificate certificate3 = new Certificate(resource2, 10, 1000000, zone, "Belgium");
            Certificate certificate4 = new Certificate(resource3, 5, 500000, zone, "Luxemburg");
            List<Certificate> certificates = new List<Certificate> { certificate1, certificate2, certificate3, certificate4 };
            Area field = new Area(null, 1, resource3, certificates);

            List<Certificate> certificatesRetrieved = field.GetCertificates();

            Assert.AreEqual(4, certificatesRetrieved.Count);
            CollectionAssert.Contains(certificatesRetrieved, certificate1);
            CollectionAssert.Contains(certificatesRetrieved, certificate2);
            CollectionAssert.Contains(certificatesRetrieved, certificate3);
            CollectionAssert.Contains(certificatesRetrieved, certificate4);
        }

        [TestMethod()]
        public void GetCertificatesWithResourcesTest()
        {
            Zone zone = new Zone("Benelux", "Europe", Color.LimeGreen);
            Resource resource1 = new Resource("Aluminum", 500000);
            Resource resource2 = new Resource("Gold", 500000);
            Resource resource3 = new Resource("Tea", 500000);
            Certificate certificate1 = new Certificate(resource1, 20, 2000000, zone, "Belgium");
            Certificate certificate2 = new Certificate(resource1, 5, 500000, zone, "Netherlands");
            Certificate certificate3 = new Certificate(resource2, 10, 1000000, zone, "Belgium");
            Certificate certificate4 = new Certificate(resource3, 5, 500000, zone, "Luxemburg");
            List<Certificate> certificates = new List<Certificate> { certificate1, certificate2, certificate3, certificate4};
            Area field = new Area(null, 1, resource3, certificates);

            List<Certificate> certificatesRetrieved = field.GetCertificatesWithResources(new List<Resource> {resource1});

            Assert.AreEqual(2, certificatesRetrieved.Count);
            CollectionAssert.Contains(certificatesRetrieved, certificate1);
            CollectionAssert.Contains(certificatesRetrieved, certificate2);
        }

        [TestMethod()]
        public void GetCertificatesWithMultipleResourcesTest()
        {
            Zone zone = new Zone("Benelux", "Europe", Color.LimeGreen);
            Resource resource1 = new Resource("Aluminum", 500000);
            Resource resource2 = new Resource("Gold", 500000);
            Resource resource3 = new Resource("Tea", 500000);
            Certificate certificate1 = new Certificate(resource1, 20, 2000000, zone, "Belgium");
            Certificate certificate2 = new Certificate(resource1, 5, 500000, zone, "Netherlands");
            Certificate certificate3 = new Certificate(resource2, 10, 1000000, zone, "Belgium");
            Certificate certificate4 = new Certificate(resource3, 5, 500000, zone, "Luxemburg");
            List<Certificate> certificates = new List<Certificate> { certificate1, certificate2, certificate3, certificate4 };
            Area field = new Area(null, 1, resource3, certificates);

            List<Certificate> certificatesRetrieved = field.GetCertificatesWithResources(new List<Resource> { resource1, resource2 });

            Assert.AreEqual(3, certificatesRetrieved.Count);
            CollectionAssert.Contains(certificatesRetrieved, certificate1);
            CollectionAssert.Contains(certificatesRetrieved, certificate2);
            CollectionAssert.Contains(certificatesRetrieved, certificate3);
        }

        [TestMethod()]
        public void ReturnCertificateTest()
        {
            Zone zone = new Zone("Benelux", "Europe", Color.LimeGreen);
            Resource resource1 = new Resource("Aluminum", 500000);
            Resource resource2 = new Resource("Gold", 500000);
            Resource resource3 = new Resource("Tea", 500000);
            Certificate certificate1 = new Certificate(resource1, 20, 2000000, zone, "Belgium");
            Certificate certificate2 = new Certificate(resource1, 5, 500000, zone, "Netherlands");
            Certificate certificate3 = new Certificate(resource2, 10, 1000000, zone, "Belgium");
            Certificate certificate4 = new Certificate(resource3, 5, 500000, zone, "Luxemburg");
            List<Certificate> certificates = new List<Certificate> { certificate1, certificate2, certificate3 };
            Area field = new Area(null, 1, resource3, certificates);

            field.ReturnCertificate(certificate4);
            List<Certificate> certificatesRetrieved = field.GetCertificates();

            Assert.AreEqual(4, certificatesRetrieved.Count);
            CollectionAssert.Contains(certificatesRetrieved, certificate1);
            CollectionAssert.Contains(certificatesRetrieved, certificate2);
            CollectionAssert.Contains(certificatesRetrieved, certificate3);
            CollectionAssert.Contains(certificatesRetrieved, certificate4);
        }

        [TestMethod()]
        public void ReturnCertificateExistingTest()
        {
            Zone zone = new Zone("Benelux", "Europe", Color.LimeGreen);
            Resource resource1 = new Resource("Aluminum", 500000);
            Resource resource2 = new Resource("Gold", 500000);
            Resource resource3 = new Resource("Tea", 500000);
            Certificate certificate1 = new Certificate(resource1, 20, 2000000, zone, "Belgium");
            Certificate certificate2 = new Certificate(resource1, 5, 500000, zone, "Netherlands");
            Certificate certificate3 = new Certificate(resource2, 10, 1000000, zone, "Belgium");
            Certificate certificate4 = new Certificate(resource3, 5, 500000, zone, "Luxemburg");
            List<Certificate> certificates = new List<Certificate> { certificate1, certificate2, certificate3, certificate4 };
            Area field = new Area(null, 1, resource3, certificates);

            Assert.ThrowsException<CertificateActionNotAllowedException>(() => field.ReturnCertificate(certificate4));
        }

        [TestMethod()]
        public void ReturnCertificateWrongZoneTest()
        {
            Zone zone = new Zone("Benelux", "Europe", Color.LimeGreen);
            Zone zone2 = new Zone("Germany", "Europe", Color.LimeGreen);
            Resource resource1 = new Resource("Aluminum", 500000);
            Resource resource2 = new Resource("Gold", 500000);
            Resource resource3 = new Resource("Tea", 500000);
            Certificate certificate1 = new Certificate(resource1, 20, 2000000, zone, "Belgium");
            Certificate certificate2 = new Certificate(resource1, 5, 500000, zone, "Netherlands");
            Certificate certificate3 = new Certificate(resource2, 10, 1000000, zone, "Belgium");
            Certificate certificate4 = new Certificate(resource3, 5, 500000, zone2, "Germany");
            List<Certificate> certificates = new List<Certificate> { certificate1, certificate2, certificate3};
            Area field = new Area(null, 1, resource3, certificates);

            Assert.ThrowsException<CertificateActionNotAllowedException>(() => field.ReturnCertificate(certificate4));
        }

        [TestMethod()]
        public void BuyCertificateTest()
        {
            Zone zone = new Zone("Benelux", "Europe", Color.LimeGreen);
            Resource resource1 = new Resource("Aluminum", 500000);
            Resource resource2 = new Resource("Gold", 500000);
            Resource resource3 = new Resource("Tea", 500000);
            Certificate certificate1 = new Certificate(resource1, 20, 2000000, zone, "Belgium");
            Certificate certificate2 = new Certificate(resource1, 5, 500000, zone, "Netherlands");
            Certificate certificate3 = new Certificate(resource2, 10, 1000000, zone, "Belgium");
            Certificate certificate4 = new Certificate(resource3, 5, 500000, zone, "Luxemburg");
            List<Certificate> certificates = new List<Certificate> { certificate1, certificate2, certificate3, certificate4 };
            Area field = new Area(null, 1, resource3, certificates);

            field.BuyCertificate(certificate3);
            List<Certificate> certificatesRetrieved = field.GetCertificates();

            Assert.AreEqual(3, certificatesRetrieved.Count);
            CollectionAssert.Contains(certificatesRetrieved, certificate1);
            CollectionAssert.Contains(certificatesRetrieved, certificate2);
            CollectionAssert.Contains(certificatesRetrieved, certificate4);
        }

        [TestMethod()]
        public void BuyCertificateNotInAreaTest()
        {
            Zone zone = new Zone("Benelux", "Europe", Color.LimeGreen);
            Resource resource1 = new Resource("Aluminum", 500000);
            Resource resource2 = new Resource("Gold", 500000);
            Resource resource3 = new Resource("Tea", 500000);
            Certificate certificate1 = new Certificate(resource1, 20, 2000000, zone, "Belgium");
            Certificate certificate2 = new Certificate(resource1, 5, 500000, zone, "Netherlands");
            Certificate certificate3 = new Certificate(resource2, 10, 1000000, zone, "Belgium");
            Certificate certificate4 = new Certificate(resource3, 5, 500000, zone, "Luxemburg");
            List<Certificate> certificates = new List<Certificate> { certificate1, certificate2, certificate3 };
            Area field = new Area(null, 1, resource3, certificates);

            Assert.ThrowsException<CertificateActionNotAllowedException>(() => field.BuyCertificate(certificate4));
        }

    }
}