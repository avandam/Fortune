﻿using System.Collections.Generic;
using Fortune.Logic.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fortune.Logic.Tests.Fields
{
    [TestClass()]
    public class TelexTests
    {
        [TestMethod()]
        public void CreateTextFixedFeeNoResourceTest()
        {
            Telex field = new Telex(null, 1, 3000000, false, "You sold a book, gain {0}.");
            Assert.AreEqual("You sold a book, gain 3 million.", field.CreateText());
        }

        [TestMethod()]
        public void CreateTextFixedFeeWithResourceTest()
        {
            Telex field = new Telex(null, 1, new List<Resource> { new Resource(ResourceType.Aluminum, 500000) }, 3000000, false, "Problem in a mine, pay {0} if you have {1}.");
            Assert.AreEqual("Problem in a mine, pay 3 million if you have Aluminium.", field.CreateText());
        }

        [TestMethod()]
        public void CreateTextVariableFeeTest()
        {
            Telex field = new Telex(null, 1, new List<Resource> { new Resource(ResourceType.Aluminum, 500000)},3000000, 2000000, false, "Problem in an mine, pay {0} if you have {1}, else pay {2}.");
            Assert.AreEqual("Problem in an mine, pay 3 million if you have Aluminium, else pay 2 million.", field.CreateText());
        }
    }
}