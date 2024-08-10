using CostAccounting.Core;
using CostAccounting.Core.AccountingStrategy;
using CostAccounting.Models;
using Moq;

namespace CostAccounting.Tests
{
    [TestFixture]
    public class ShareCalculatorTests
    {
        private Mock<IStockData> _mockStockData;
        private Mock<IAccountingStrategy> _mockStrategy;
        private ShareCalculator _shareCalculator;

        [SetUp]
        public void SetUp()
        {
            _mockStockData = new Mock<IStockData>();
            _mockStrategy = new Mock<IAccountingStrategy>();
            _shareCalculator = new ShareCalculator(_mockStockData.Object);
        }

        private IEnumerable<Lot> GetSampleLots()
        {
            return new List<Lot>
            {
                new Lot(100, 50.0, DateTime.Now),
                new Lot(50, 60.0, DateTime.Now.AddMonths(-3)),
                new Lot(5, 30.0, DateTime.Now.AddMonths(-1)),
                new Lot(50, 44.0, DateTime.Now.AddMonths(-2)),
            };
        }

        [TestCase(100, 105)]
        [TestCase(200, 5)]
        [TestCase(205, 0)]
        public void GetRemainingShares_ShouldReturnExpectedRemainingShares(int amountForSale, int expectedRemaining)
        {
            // Arrange
            _mockStockData.Setup(sd => sd.GetStockLots()).Returns(GetSampleLots());

            var calculator = new ShareCalculator(_mockStockData.Object);

            // Act
            int remainingShares = calculator.GetRemainingShares(amountForSale);

            // Assert
            Assert.AreEqual(expectedRemaining, remainingShares);
        }

        [TestCase(123400)]
        [TestCase(206)]
        public void GetRemainingShares_ShouldThrowTransactionException_WhenNotEnoughShares(int amountForSale)
        {
            // Arrange
            _mockStockData.Setup(s => s.GetStockLots()).Returns(GetSampleLots());

            // Act & Assert
            var ex = Assert.Throws<TransactionException>(() => _shareCalculator.GetRemainingShares(amountForSale));
            Assert.AreEqual($"Unable to sell {amountForSale} shares. Current amount 205", ex.Message);
        }

        [TestCase(10, 60.0)]   
        [TestCase(55, 58.5454)]     
        [TestCase(100, 52.0)]       
        [TestCase(105, 50.95238)]   
        [TestCase(200, 50.5)]       
        [TestCase(205, 50.4878)]   
        public void GetCostBasisOfSoldShares_ShouldReturnCorrectWeightedAverage(int amountForSale, double expectedCostBasisOfSoldShares)
        {
            // Arrange
            _mockStockData.Setup(sd => sd.GetStockLots()).Returns(GetSampleLots());

           _shareCalculator.SetStrategy(new FIFOStrategy());

            // Act
            var costBasis = _shareCalculator.GetCostBasisOfSoldShares(amountForSale);

            // Assert
            double delta = 0.01;
            Assert.AreEqual(expectedCostBasisOfSoldShares, costBasis, delta);
        }

        [TestCase(10, 50.0)]   
        [TestCase(105, 50)]    
        [TestCase(200, 50)]    
        [TestCase(205, 0)]    
        public void GetCostBasisOfRemainingShares_ShouldReturnCorrectWeightedAverage(int amountForSale, double expectedCostBasisOfRemainingdShares)
        {
            // Arrange
            _mockStockData.Setup(sd => sd.GetStockLots()).Returns(GetSampleLots());

            _shareCalculator.SetStrategy(new FIFOStrategy());

            // Act
            var costBasis = _shareCalculator.GetCostBasisOfRemainingShares(amountForSale);

            // Assert
            double delta = 0.01;
            Assert.AreEqual(expectedCostBasisOfRemainingdShares, costBasis, delta);
        }

        [TestCase(10, 50.0, -100.0 )]          
        [TestCase(55, 50.0, -469.997)]   
        [TestCase(100, 75.5, 2350)]     
        [TestCase(105, 75.5, 2577.5)] 
        [TestCase(200, 75.5, 5000)]     
        [TestCase(205, 100.0, 10150)] 
        public void GetProfitOrLoss_ShouldReturnCorrectProfitOrLoss(int amountForSale, double price, double expectedProfit)
        {
            // Arrange
            _mockStockData.Setup(sd => sd.GetStockLots()).Returns(GetSampleLots());

            _shareCalculator.SetStrategy(new FIFOStrategy());

            // Act
            var profitOrLoss = _shareCalculator.GetProfitOrLoss(amountForSale, price);

            // Assert
            var delta = 0.01;
            Assert.AreEqual(expectedProfit, profitOrLoss, delta);
        }
        
    }
}
