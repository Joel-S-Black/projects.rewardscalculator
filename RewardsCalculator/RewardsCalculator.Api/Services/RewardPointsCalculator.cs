using RewardsCalculator.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RewardsCalculator.Api.Services
{
    public class RewardPointsCalculator
    {
        const int lowerThreshold = 50;
        const int lowerThresholdPoints = 1;
        const int upperThreshold = 100;
        const int upperThresholdPoints = 2;

        public int CalculatePoints(Transaction purchase)
        {
            if(purchase.Amount <= lowerThreshold)
            {
                return 0;
            }
            else if(purchase.Amount <= upperThreshold)
            {
                var difference = purchase.Amount - lowerThreshold;
                return difference * lowerThresholdPoints;
            }
            else
            {
                var upperDifference = purchase.Amount - upperThreshold;
                var lowerDifference = upperThreshold - lowerThreshold;
                return (upperDifference * upperThresholdPoints) + (lowerDifference * lowerThresholdPoints);
            }
        }
    }
}
