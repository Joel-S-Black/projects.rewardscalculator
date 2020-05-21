using Microsoft.AspNetCore.Mvc;
using RewardsCalculator.Api.Services;
using System;
using System.Threading.Tasks;

namespace RewardsCalculator.Api.Controllers
{
    [Route("rewards")]
    [Produces("application/json")]
    public class RewardsController:ControllerBase
    {
        private readonly IRewardsService _service;
        public RewardsController(IRewardsService service)
        {
            _service = service;
        }

        [HttpGet("default")]
        public async Task<IActionResult> GetDefaultRewardsForAllCustomers()
        {
            var rewards = await _service.GetAllRewardsInTimeRange(DateTime.Now, DateTime.Now.AddDays(-90));

            return Ok(rewards);
        }

        [HttpGet("all/byrange")]
        public async Task<IActionResult> GetRewardsByDatesForAllCustomers([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var rewards = await _service.GetAllRewardsInTimeRange(endDate, startDate);

            return Ok(rewards);
        }
    }
}
