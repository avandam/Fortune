using Microsoft.VisualStudio.TestTools.UnitTesting;
using Fortune.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortune.Logic.Tests
{
    [TestClass()]
    public class EnumExtenderTests
    {
        [TestMethod()]
        public void CountryTypeToTextTest()
        {
            Assert.AreEqual("Oceanië", CountryType.Oceania.ToText());
        }

        [TestMethod()]
        public void ResourceTypeToTextTest()
        {
            Assert.AreEqual("Aardgas", ResourceType.NaturalGas.ToText());
        }

        [TestMethod()]
        public void ContinentTypeToTextTest()
        {
            Assert.AreEqual("Oceanië", ContinentType.Oceania.ToText());
        }
    }
}