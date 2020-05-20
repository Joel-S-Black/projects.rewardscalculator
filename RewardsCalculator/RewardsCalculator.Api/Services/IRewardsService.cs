using RewardsCalculator.Api.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RewardsCalculator.Api.Services
{
    public interface IRewardsService
    {
        Task<IEnumerable<RewardPointsResult>> GetAllRewardsInTimeRange(DateTime now, DateTime dateTime);
    }
}
