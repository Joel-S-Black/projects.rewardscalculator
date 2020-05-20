using RewardsCalculator.Api.ViewModels;
using System;
using System.Collections.Generic;

namespace RewardsCalculator.Api.Services
{
    public interface IRewardsService
    {
        IEnumerable<RewardPointsResult> GetAllRewardsInTimeRange(DateTime now, DateTime dateTime);
    }
}
