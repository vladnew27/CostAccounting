
using CostAccounting.Core;

namespace CostAccounting.Tests
{
    [TestFixture]
    public class ExtensionsTests
    {

        [Test]
        public void WeightedAverage_ShouldReturnCorrectValue()
        {
            // Arrange
            var records = new List<TestRecord>
            {
                new TestRecord { Value = 1, Weight = 1 },
                new TestRecord { Value = 20, Weight = 2 },
                new TestRecord { Value = 30, Weight = 3 }
            };

            // Act
            var result = records.WeightedAverage(r => r.Value, r => r.Weight);

            // Assert
            Assert.AreEqual(21.833, result, 0.001);
        }

        [Test]
        public void WeightedAverage_ShouldThrowArgumentException_WhenEmptyCollection()
        {
            // Arrange
            var records = new List<TestRecord>();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => records.WeightedAverage(r => r.Value, r => r.Weight));
        }

        [Test]
        public void WeightedAverage_ShouldReturnValueOfSingleRecord_WhenSingleRecord()
        {
            // Arrange
            var records = new List<TestRecord>
            {
                new TestRecord { Value = 42, Weight = 1 }
            };

            // Act
            var result = records.WeightedAverage(r => r.Value, r => r.Weight);

            // Assert
            Assert.AreEqual(42, result, 0.001);
        }

        [Test]
        public void WeightedAverage_ShouldThrowDivideByZeroException_WhenAllWeightsZero()
        {
            // Arrange
            var records = new List<TestRecord>
            {
                new TestRecord { Value = 10, Weight = 0 },
                new TestRecord { Value = 20, Weight = 0 }
            };

            // Act & Assert
            Assert.Throws<DivideByZeroException>(() => records.WeightedAverage(r => r.Value, r => r.Weight));
        }

        private class TestRecord
        {
            public double Value { get; set; }
            public double Weight { get; set; }
        }
    }
}
