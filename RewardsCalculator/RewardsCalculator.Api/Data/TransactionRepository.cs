using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using RewardsCalculator.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RewardsCalculator.Api.Data
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ConnectionString _connectionString;

        public TransactionRepository(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value;
        }

        private string GetTransactionsInRangeSql
        {
            get
            {
                return @"
                            SELECT  product_id      AS ProductId,
                                    customer_id     AS CustomerId,
                                    amount          AS Amount,
                                    purchase_date   AS PurchaseDate
                            FROM purchases
                            WHERE purchase_date <= @endDate
                            AND purchase_date >= @startDate;
                        ";
            }
        }

        public async Task<IEnumerable<Transaction>> GetTransactionInRange(DateTime endDate, DateTime startDate)
        {
            using(var conn = new SqliteConnection(_connectionString.Value))
            {
                conn.Open();

                var transactions = await conn.QueryAsync<Transaction>(this.GetTransactionsInRangeSql,
                    new
                    {
                        endDate = endDate.ToShortDateString(),
                        startDate = startDate.ToShortDateString()
                    }
                );

                return transactions;
            }
        }
    }
}
