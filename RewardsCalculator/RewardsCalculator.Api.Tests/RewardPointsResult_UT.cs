using NUnit.Framework;
using RewardsCalculator.Api.Models;
using RewardsCalculator.Api.Services;
using RewardsCalculator.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RewardsCalculator.Api.Tests
{
    public class RewardPointsResult_UT
    {
        IRewardPointsCalculator _calculator;

        public RewardPointsResult_UT()
        {
            _calculator = new RewardPointsCalculator();
        }

        [Test]
        public void RewardPointsResult_ShouldSetup_2_Months_For_Transactions_In_2_Different_Months()
        {
            // Arrange
            IEnumerable<Transaction> transactions = new List<Transaction>
            {
                new Transaction
                {
                    CustomerId = 1,
                    ProductId = 1,
                    Amount = 120,
                    PurchaseDate = new DateTime(2020, 5, 5)
                },
                new Transaction
                {
                    CustomerId = 1,
                    ProductId = 1,
                    Amount = 120,
                    PurchaseDate = new DateTime(2020, 4, 4)
                }
            };

            var customer = new Customer { CustomerId = 1, FirstName = "Fake", LastName = "Customer" };
            var expected = 2;

            // Act
            var actual = new RewardPointsResult(customer, transactions, _calculator);

            // Assert
            Assert.AreEqual(expected, actual.MonthlyTotals.Count);
        }

        [TestCase(1, "January")]
        [TestCase(2, "February")]
        [TestCase(3,"March")]
        [TestCase(4,"April")]
        [TestCase(5,"May")]
        [TestCase(6,"June")]
        [TestCase(7,"July")]
        [TestCase(8,"August")]
        [TestCase(9,"September")]
        [TestCase(10,"October")]
        [TestCase(11,"November")]
        [TestCase(12,"December")]
        public void RewardPointsResult_ShouldSet_MonthlyTotals_MonthName_To_January_For_Value_1_In_Transaction_Month(int monthNumber, string monthName)
        {
            // Arrange
            IEnumerable<Transaction> transactions = new List<Transaction>
            {
                new Transaction
                {
                    CustomerId = 1,
                    ProductId = 1,
                    Amount = 120,
                    PurchaseDate = new DateTime(2020, monthNumber, 5)
                }
            };

            var customer = new Customer { CustomerId = 1, FirstName = "Fake", LastName = "Customer" };
            var expected = monthName;

            // Act
            var actual = new RewardPointsResult(customer, transactions, _calculator);

            // Assert
            Assert.AreEqual(expected, actual.MonthlyTotals.Single().MonthName);
        }

        [Test]
        public void RewardPointsResult_ShouldSetup_TotalPoints_Equal_ToSumOfAllMonths()
        {
            // Arrange
            IEnumerable<Transaction> transactions = new List<Transaction>
            {
                new Transaction
                {
                    CustomerId = 1,
                    ProductId = 1,
                    Amount = 51,
                    PurchaseDate = new DateTime(2020, 5, 5)
                },
                new Transaction
                {
                    CustomerId = 1,
                    ProductId = 1,
                    Amount = 51,
                    PurchaseDate = new DateTime(2020, 4, 4)
                }
            };

            var customer = new Customer { CustomerId = 1, FirstName = "Fake", LastName = "Customer" };

            // Act
            var actual = new RewardPointsResult(customer, transactions, _calculator);

            // Assert
            Assert.AreEqual(actual.TotalRewardsPoints, actual.MonthlyTotals.Sum(t => t.RewardPoints));
        }
    }
}
