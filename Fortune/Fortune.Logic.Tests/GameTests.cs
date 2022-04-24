using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fortune.Logic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fortune.Logic.Exceptions;
using Fortune.Logic.Fields;

namespace Fortune.Logic.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void BuyJokerTest()
        {
            Player player = new Player("Test", 1, 10000000);
            Game game = new Game(new List<Player> { player });

            game.BuyJoker();
            Assert.AreEqual(7000000, player.Cash);
            Assert.AreEqual(1, player.NumberOfJokers);
        }

        [TestMethod()]
        public void BuyJokerNotEnoughMoneyTest()
        {
            Player player = new Player("Test", 1, 1000000);
            Game game = new Game(new List<Player> { player });

            Assert.ThrowsException<JokerException>(() => game.BuyJoker());
        }

        [TestMethod()]
        public void SellJokerTest()
        {
            Player player = new Player("Test", 1, 10000000);
            Game game = new Game(new List<Player> { player });

            game.BuyJoker();
            Assert.AreEqual(1, player.NumberOfJokers);
            game.SellJoker();
            Assert.AreEqual(10000000, player.Cash);
            Assert.AreEqual(0, player.NumberOfJokers);
        }

        [TestMethod()]
        public void SellJokerNotAllowedTest()
        {
            Player player = new Player("Test", 1, 10000000);
            Game game = new Game(new List<Player> { player });

            Assert.ThrowsException<JokerException>(() => game.SellJoker());
        }

        [TestMethod()]
        public void BuyCertificateTest()
        {
            Zone zone = new Zone("Benelux", ContinentType.Europe, Color.LimeGreen);
            Certificate certificate1 = new Certificate(new Resource("Aluminum", 100000), 5, 500000, zone, "Netherlands");
            Player player = new Player("Test", 1, 10000000);
            
            Game game = new Game(new List<Player> {player});
            Area area = new Area(game, 0, new Resource("Aluminum", 100000), new List<Certificate> {certificate1});
            game.AddBoard(new List<Field> { area });

            game.DoTurn();

            game.BuyCertificate(certificate1);
            Assert.IsTrue(player.HasCertificate(certificate1));
            Assert.AreEqual(9500000, player.Cash);
            Assert.AreEqual(0, area.GetCertificates().Count);
        }

        [TestMethod()]
        public void BuyCertificateNoCertificateAtFieldTest()
        {
            Zone zone = new Zone("Benelux", ContinentType.Europe, Color.LimeGreen);
            Certificate certificate1 = new Certificate(new Resource("Aluminum", 100000), 5, 500000, zone, "Netherlands");
            Certificate certificate2 = new Certificate(new Resource("Aluminum", 200000), 5, 500000, zone, "Belgium");
            Player player = new Player("Test", 1, 10000000);

            Game game = new Game(new List<Player> { player });
            Area area = new Area(game, 0, new Resource("Aluminum", 100000), new List<Certificate> { certificate1 });
            game.AddBoard(new List<Field> { area });

            game.DoTurn();

            Assert.ThrowsException<CertificateActionNotAllowedException>(() => game.BuyCertificate(certificate2));
        }

        [TestMethod()]
        public void BuyCertificateNotEnoughMoneyTest()
        {
            Zone zone = new Zone("Benelux", ContinentType.Europe, Color.LimeGreen);
            Certificate certificate1 = new Certificate(new Resource("Aluminum", 100000), 5, 500000, zone, "Netherlands");
            Player player = new Player("Test", 1, 0);

            Game game = new Game(new List<Player> { player });
            Area area = new Area(game, 0, new Resource("Aluminum", 100000), new List<Certificate> { certificate1 });
            game.AddBoard(new List<Field> { area });

            game.DoTurn();

            Assert.ThrowsException<CertificateActionNotAllowedException>(() => game.BuyCertificate(certificate1));
        }

        [TestMethod()]
        public void BuyCertificateTooManyBuysTest()
        {
            Zone zone = new Zone("Benelux", ContinentType.Europe, Color.LimeGreen);
            Certificate certificate1 = new Certificate(new Resource("Aluminum", 100000), 5, 500000, zone, "Netherlands");
            Certificate certificate2 = new Certificate(new Resource("Aluminum", 200000), 5, 500000, zone, "Belgium");
            Certificate certificate3 = new Certificate(new Resource("Aluminum", 200000), 5, 500000, zone, "Luxemburg");
            Certificate certificate4 = new Certificate(new Resource("Iron", 200000), 5, 500000, zone, "Belgium");
            Player player = new Player("Test", 1, 10000000);

            Game game = new Game(new List<Player> { player });
            Area area = new Area(game, 0, new Resource("Aluminum", 100000), new List<Certificate> { certificate1, certificate2, certificate3, certificate4 });
            game.AddBoard(new List<Field> { area });

            game.DoTurn();

            game.BuyCertificate(certificate1);
            game.BuyCertificate(certificate2);
            game.BuyCertificate(certificate3);
            Assert.ThrowsException<CertificateNotAllowedToBuyException>(() => game.BuyCertificate(certificate4));
            Assert.IsTrue(player.HasCertificate(certificate1));
            Assert.IsTrue(player.HasCertificate(certificate2));
            Assert.IsTrue(player.HasCertificate(certificate3));
            Assert.IsFalse(player.HasCertificate(certificate4));
            Assert.AreEqual(8500000, player.Cash);
        }
    }
}