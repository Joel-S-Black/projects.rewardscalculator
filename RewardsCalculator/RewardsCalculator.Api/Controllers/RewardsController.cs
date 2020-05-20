using Microsoft.AspNetCore.Mvc;
using RewardsCalculator.Api.Services;
using System;

namespace RewardsCalculator.Api.Controllers
{
    [Produces("application/json")]
    [Route("rewards")]
    public class RewardsController:Controller
    {
        private readonly IRewardsService _service;
        public RewardsController(IRewardsService service)
        {
            _service = service;
        }

        [HttpGet("default")]
        public IActionResult GetDefaultRewardsForAllCustomers()
        {
            var results = _service.GetAllRewardsInTimeRange(DateTime.Now, DateTime.Now.AddDays(-90));

            return Ok(results);
        }

        [HttpGet("all/byrange")]
        public IActionResult GetRewardsByDatesForAllCustomers(DateTime startDate, DateTime endDate)
        {
            var results = _service.GetAllRewardsInTimeRange(endDate, startDate);

            return Ok(results);
        }
    }
}
