using System.Collections.Generic;
using Fortune.Logic.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fortune.Logic.Tests.Fields
{
    /// <summary>
    /// Tests for simple fields
    /// Start
    /// Joker
    /// Bonus
    /// Auction
    /// ChoiceWorld
    /// ChoiceContinent
    /// </summary>
    [TestClass()]
    public class GenericFieldTests
    {
        #region Start tests
        [TestMethod()]
        public void StartHasNoResourcesTest()
        {
            Start field = new Start(null);
            Assert.IsFalse(field.HasResource());
        }
        #endregion Start Tests

        #region Joker tests
        [TestMethod()]
        public void JokerHasNoResourcesTest()
        {
            Joker field = new Joker(null, 1);
            Assert.IsFalse(field.HasResource());
        }
        #endregion Joker tests

        #region Bonus tests
        [TestMethod]
        public void BonusHasResourceTest()
        {
            Bonus field = new Bonus(null, 1, new Resource("TestResource", 1000000));
            Assert.IsTrue(field.HasResource());
        }
        #endregion Bonus tests

        #region Auction tests
        [TestMethod]
        public void AuctionHasNoResourceTest()
        {
            Auction field = new Auction(null, 1);
            Assert.IsFalse(field.HasResource());
        }
        #endregion Auction tests

        #region ChoiceWorld tests
        [TestMethod]
        public void ChoiceWorldHasNoResourceTest()
        {
            ChoiceWorld field = new ChoiceWorld(null, 1);
            Assert.IsFalse(field.HasResource());
        }
        #endregion ChoiceWorld tests

        #region ChoiceContinent tests
        [TestMethod]
        public void ChoiceContinentHasResourceTest()
        {
            ChoiceContinent field = new ChoiceContinent(null, 1,  new Resource("TestResource", 1000000), new List<string> {"Asia", "Oceania" });
            Assert.IsTrue(field.HasResource());
        }
        #endregion ChoiceContinent tests
    }
}