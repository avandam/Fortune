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
    public class FieldTests
    {
        [TestMethod()]
        public void HasResourceTest()
        {
            Field field = new Joker(null, 1);
            Assert.AreEqual("Joker", field.ToString());
        }
    }
}