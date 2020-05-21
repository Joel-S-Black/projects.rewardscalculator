using RewardsCalculator.Api.Models;
using RewardsCalculator.Api.Services;
using System.Globalization;
using System.Linq;

namespace RewardsCalculator.Api.ViewModels
{
    public class MonthlyRewardPoints
    {
        public MonthlyRewardPoints()
        {

        }

        public MonthlyRewardPoints(IRewardPointsCalculator calculator, IGrouping<int, Transaction> month)
        {
            MonthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month.Key);
            RewardPoints = month.Sum(t => calculator.CalculatePoints(t));
        }

        public string MonthName { get; set; }
        public int RewardPoints { get; set; }
    }
}