using System.Collections.Generic;
using Fortune.Logic.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fortune.Logic.Tests.Fields
{
    [TestClass()]
    public class TelexCardTests
    {
        [TestMethod()]
        public void CreateTextFixedFeeNoResourceTest()
        {
            TelexCard telex = new TelexCard("You sold a book, gain {0}.", true, 3000000);
            Assert.AreEqual("You sold a book, gain 3 million.", telex.CreateText());
        }

        [TestMethod()]
        public void CreateTextFixedFeeWithResourceTest()
        {
            TelexCard telex = new TelexCard("Problem in a mine, pay {0} if you have {1}.", false, 3000000, new List<Resource> { new Resource(ResourceType.Aluminum, 500000) });
            Assert.AreEqual("Problem in a mine, pay 3 million if you have Aluminium.", telex.CreateText());
        }

        [TestMethod()]
        public void CreateTextVariableFeeTest()
        {
            TelexCard telex = new TelexCard("Problem in an mine, pay {0} if you have {1}, else pay {2}.", false, 3000000, 2000000, new List<Resource> { new Resource(ResourceType.Aluminum, 500000)});
            Assert.AreEqual("Problem in an mine, pay 3 million if you have Aluminium, else pay 2 million.", telex.CreateText());
        }
    }
}