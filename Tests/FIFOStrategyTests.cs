using CostAccounting.Core.AccountingStrategy;
using CostAccounting.Models;

namespace CostAccounting.Tests
{
    [TestFixture]
    public class FIFOStrategyTests
    {
        private FIFOStrategy _fifoStrategy;

        [SetUp]
        public void Setup()
        {
            _fifoStrategy = new FIFOStrategy();
        }

        private IEnumerable<Lot> GetSampleLots()
        {
            return new List<Lot>
            {
                new Lot(100, 50.0, DateTime.Now),          
                new Lot(5, 30.0, DateTime.Now.AddMonths(-1)),
                new Lot(50, 44.0, DateTime.Now.AddMonths(-2)),
                new Lot(50, 60.0, DateTime.Now.AddMonths(-3)) 
            };
        }

        [Test]
        public void WrapLots_ShouldReturnCorrectLots_WhenAmountForSaleIsLessThanTotal()
        {
            // Arrange
            var lots = GetSampleLots();
            var amountForSale = 55;

            // Act
            var result = _fifoStrategy.WrapLots(amountForSale, lots).ToList();

            // Assert
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(50, result[0].AmountSold);
            Assert.AreEqual(5, result[1].AmountSold);
            Assert.AreEqual(0, result[2].AmountSold);
        }

        [Test]
        public void WrapLots_ShouldHaveSameReferencel()
        {
            // Arrange
            var lots = GetSampleLots();
            var amountForSale = 55;

            // Act
            var result = _fifoStrategy.WrapLots(amountForSale, lots);

            // Assert
            Assert.AreSame(lots.OrderBy(lot => lot.PurchaseDate).First(), result.First().Lot);
        }

        [Test]
        public void WrapLots_ShouldReturnCorrectLots_WhenAmountForSaleMatchesTotal()
        {
            // Arrange
            var lots = GetSampleLots();
            var amountForSale = 205;

            // Act
            var result = _fifoStrategy.WrapLots(amountForSale, lots).ToList();

            // Assert
            Assert.AreEqual(4, result.Count);
            Assert.AreEqual(50, result[0].AmountSold);
            Assert.AreEqual(50, result[1].AmountSold);
            Assert.AreEqual(5, result[2].AmountSold);
            Assert.AreEqual(100, result[3].AmountSold);
        }

        [Test]
        public void WrapLots_ShouldHandleZeroAmountForSale()
        {
            // Arrange
            var lots = GetSampleLots();
            var amountForSale = 0;

            // Act
            var result = _fifoStrategy.WrapLots(amountForSale, lots).ToList();

            // Assert
            Assert.AreEqual(0, result[0].AmountSold);
            Assert.AreEqual(0, result.Sum(l => l.AmountSold));
        }

        [Test]
        public void WrapLots_ShouldThrowArgumentException_WhenAmountForSaleIsNegative()
        {
            // Arrange
            var lots = GetSampleLots();
            var amountForSale = -10;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _fifoStrategy.WrapLots(amountForSale, lots).ToList());
        }
    }
}
