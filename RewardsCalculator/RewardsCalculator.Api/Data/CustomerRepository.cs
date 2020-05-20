using Dapper;
using Microsoft.Data.Sqlite;
using RewardsCalculator.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RewardsCalculator.Api.Data
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly string _connectionString;

        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public string GetAllCustomersSql
        {
            get
            {
                return @"
                            SELECT  customer_id AS CustomerId,
                                    first_name  AS FirstName,
                                    last_name   AS LastName
                            FROM customers
                        ";
            }
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();

                var result = await conn.QueryAsync<Customer>(this.GetAllCustomersSql);

                return result;
            }
        }
    }
}
