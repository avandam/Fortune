using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Fortune.Logic.Exceptions;
// ReSharper disable StringLiteralTypo

namespace Fortune.Logic.Tests
{
    [TestClass]
    public class TelexCardTests
    {

        [TestMethod]
        public void CreateTextFixedFeeNoResourceTest()
        {
            TelexCard telex = new TelexCard("Je hebt een boek verkocht, je krijgt {0}.", TelexType.Gain, 3000000);
            Assert.AreEqual("Je hebt een boek verkocht, je krijgt 3 miljoen.", telex.CreateText());
        }

        [TestMethod]
        public void CreateTextFixedFeeWithResourceTest()
        {
            TelexCard telex = new TelexCard("Problemen in een mijn. Betaal {0} als je {1} bezit.", TelexType.Loss, 3000000, new List<Resource> { new Resource(ResourceType.Aluminum, 500000) });
            Assert.AreEqual("Problemen in een mijn. Betaal 3 miljoen als je Aluminium bezit.", telex.CreateText());
        }

        [TestMethod]
        public void CreateTextVariableFeeTest()
        {
            TelexCard telex = new TelexCard("Problemen in een mijn. Betaal {0} als je {1} bezit, anders betaal je {2}.", TelexType.Loss, 3000000, 2000000, new List<Resource> { new Resource(ResourceType.Aluminum, 500000) });
            Assert.AreEqual("Problemen in een mijn. Betaal 3 miljoen als je Aluminium bezit, anders betaal je 2 miljoen.", telex.CreateText());
        }

        [TestMethod]
        public void TelexCardStaticFeeTest()
        {
            TelexCard card = new TelexCard("Krijg {0}", TelexType.Gain, 1000000);
            Assert.AreEqual("Krijg 1 miljoen",card.CreateText());
            Assert.AreEqual(1000000, card.MaxFee);
            Assert.AreEqual(int.MinValue, card.MinFee);
        }

        [TestMethod]
        public void TelexCardVariableFeeTest()
        {
            TelexCard card = new TelexCard("Krijg {0} indien je {1} bezit, anders {2}", TelexType.Gain, 2000000, 1000000, new List<Resource> {new Resource(ResourceType.Aluminum, 1000000)});
            Assert.AreEqual("Krijg 2 miljoen indien je Aluminium bezit, anders 1 miljoen", card.CreateText());
            Assert.AreEqual(2000000, card.MaxFee);
            Assert.AreEqual(1000000, card.MinFee);
        }

        [TestMethod]
        public void TelexCardResourceAndOtherPlayerTypeNotAllowed()
        {
            Assert.ThrowsException<TelexException>(() => new TelexCard("Je bent jarig, iedere speler geeft je {0}", TelexType.GainFromPlayers, 1000000, new List<Resource> { new Resource(ResourceType.Aluminum, 1000000) }));
        }
    }
}