using RewardsCalculator.Api.Data;
using RewardsCalculator.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RewardsCalculator.Api.Services
{
    public class RewardsService : IRewardsService
    {
        private readonly ITransactionRepository _transactionData;
        private readonly IRewardPointsCalculator _calculator;
        private readonly ICustomerRepository _customerData;

        public RewardsService(ITransactionRepository transactionRepo, IRewardPointsCalculator calculator, ICustomerRepository customerData)
        {
            _transactionData = transactionRepo;
            _calculator = calculator;
            _customerData = customerData;
        }
        public async Task<IEnumerable<RewardPointsResult>> GetAllRewardsInTimeRange(DateTime endDate, DateTime startDate)
        {
            var points = new List<RewardPointsResult>();

            var allTransactions = await _transactionData.GetTransactionInRange(endDate, startDate);
            var allCustomers = await _customerData.GetAllCustomers();

            var customersInRange = allTransactions.GroupBy(t => t.CustomerId);

            foreach(var cust in customersInRange)
            {
                var customer = allCustomers.FirstOrDefault(c => c.CustomerId == cust.Key);

                points.Add(new RewardPointsResult(customer, cust, _calculator));
            }

            return points.OrderBy(p => p.CustomerId);
        }
    }
}
