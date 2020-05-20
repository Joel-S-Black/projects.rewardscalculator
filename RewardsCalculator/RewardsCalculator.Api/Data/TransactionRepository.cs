using Dapper;
using Microsoft.Data.Sqlite;
using RewardsCalculator.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RewardsCalculator.Api.Data
{
    public class TransactionRepository: ITransactionRepository
    {
        private readonly string _connectionString;

        public TransactionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
