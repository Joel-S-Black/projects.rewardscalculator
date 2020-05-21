using Moq;
using NUnit.Framework;
using RewardsCalculator.Api.Data;
using RewardsCalculator.Api.Models;
using RewardsCalculator.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RewardsCalculator.Api.Tests
{
    public class RewardsService_UT
    {
        private RewardsService _testObject;
        private Mock<ITransactionRepository> _transactionRepo;
        private Mock<ICustomerRepository> _customerRepo;

        [SetUp]
        public void Setup()
        {
            _transactionRepo = new Mock<ITransactionRepository>();
            _customerRepo = new Mock<ICustomerRepository>();
            _testObject = new RewardsService(_transactionRepo.Object, new RewardPointsCalculator(), _customerRepo.Object);
        }

        [Test]
        public async Task GetAllRewardsInTimeRange_ShouldCalculate_180RewardPoints_For_Two_120Purchases_On_DifferentDays()
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

            IEnumerable<Customer> customers = new List<Customer>
            {
                new Customer
                {
                    CustomerId = 1,
                    FirstName = "Fake",
                    LastName = "Customer"
                }
            };

            _transactionRepo.Setup(r => r.GetTransactionInRange(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(Task.FromResult(transactions));
            _customerRepo.Setup(r => r.GetAllCustomers()).Returns(Task.FromResult(customers));

            var expectedRewards = 180;

            // Act
            var actual = await _testObject.GetAllRewardsInTimeRange(new DateTime(2020, 5, 6), new DateTime(2020, 4, 3));

            // Assert
            Assert.AreEqual(expectedRewards, actual.FirstOrDefault().TotalRewardsPoints);
        }

        [Test]
        public async Task GetAllRewardsInTimeRange_ShouldCalculate_90RewardPoints_For_One_120Purchases_And_One_30Purchase_On_DifferentDays()
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
                    Amount = 30,
                    PurchaseDate = new DateTime(2020, 4, 4)
                }
            };

            IEnumerable<Customer> customers = new List<Customer>
            {
                new Customer
                {
                    CustomerId = 1,
                    FirstName = "Fake",
                    LastName = "Customer"
                }
            };

            _transactionRepo.Setup(r => r.GetTransactionInRange(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(Task.FromResult(transactions));
            _customerRepo.Setup(r => r.GetAllCustomers()).Returns(Task.FromResult(customers));

            var expectedRewards = 90;

            // Act
            var actual = await _testObject.GetAllRewardsInTimeRange(new DateTime(2020, 5, 6), new DateTime(2020, 4, 3));

            // Assert
            Assert.AreEqual(expectedRewards, actual.FirstOrDefault().TotalRewardsPoints);
        }
    }
}
