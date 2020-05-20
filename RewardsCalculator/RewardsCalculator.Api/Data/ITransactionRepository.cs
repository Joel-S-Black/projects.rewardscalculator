using RewardsCalculator.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RewardsCalculator.Api.Data
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionInRange(DateTime endDate, DateTime startDate);
    }
}