using NUnit.Framework;
using RewardsCalculator.Api.Models;
using RewardsCalculator.Api.Services;

namespace RewardsCalculator.Api.Tests
{
    public class RewardPointsCalculator_UT
    {
        private RewardPointsCalculator _testObject;

        [SetUp]
        public void Setup()
        {
            _testObject = new RewardPointsCalculator();
        }

        [Test]
        public void Should_ReturnZeroPoints_When_Transaction_IsUnder50()
        {
            // Arrange
            var expected = 0;

            var test = new Transaction { Amount = 0 };

            // Act
            var actual = _testObject.CalculatePoints(test);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Should_Return1Point_When_Transaction_AmountIs51()
        {
            // Arrange
            var expected = 1;

            var test = new Transaction { Amount = 51 };

            // Act
            var actual = _testObject.CalculatePoints(test);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Should_Return50Points_When_Transaction_AmountIs100()
        {
            // Arrange
            var expected = 50;

            var test = new Transaction { Amount = 100 };

            // Act
            var actual = _testObject.CalculatePoints(test);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Should_Return90Points_When_Transaction_AmountIs120()
        {
            // Arrange
            var expected = 90;

            var test = new Transaction { Amount = 120 };

            // Act
            var actual = _testObject.CalculatePoints(test);

            //Assert
            Assert.AreEqual(expected, actual);
        }
    }
}