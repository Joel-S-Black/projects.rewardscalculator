using RewardsCalculator.Api.Models;
using RewardsCalculator.Api.Services;
using System.Collections.Generic;
using System.Linq;

namespace RewardsCalculator.Api.ViewModels
{
    public class RewardPointsResult
    {
        public RewardPointsResult()
        {
            MonthlyTotals = new List<MonthlyRewardPoints>();
        }

        public RewardPointsResult(Customer customer, IEnumerable<Transaction> transactions, IRewardPointsCalculator calculator) : this()
        {
            CustomerId = customer.CustomerId;
            FirstName = customer.FirstName;
            LastName = customer.LastName;

            TotalRewardsPoints = transactions.Sum(t => calculator.CalculatePoints(t));

            var monthGroups = transactions.GroupBy(t => t.PurchaseDate.Month);

            foreach (var month in monthGroups)
            {
                MonthlyTotals.Add(new MonthlyRewardPoints(calculator, month));
            }
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<MonthlyRewardPoints> MonthlyTotals { get; set; } 
        public int TotalRewardsPoints { get; set; }
    }
}
