using RewardsCalculator.Api.ViewModels;
using System;
using System.Collections.Generic;

namespace RewardsCalculator.Api.Services
{
    public class RewardsService : IRewardsService
    {
        public IEnumerable<RewardPointsResult> GetAllRewardsInTimeRange(DateTime now, DateTime dateTime)
        {
            throw new NotImplementedException();
        }
    }
}
