using RewardsCalculator.Api.Models;

namespace RewardsCalculator.Api.Services
{
    public interface IRewardPointsCalculator
    {
        int CalculatePoints(Transaction purchase);
    }
}
