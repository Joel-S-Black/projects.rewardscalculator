using Dapper;
using Microsoft.Data.Sqlite;
using RewardsCalculator.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RewardsCalculator.Api.Data
{
    public class CustomerRepository
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

        public IEnumerable<Customer> GetAllCustomers()
        {
            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();

                var result = conn.Query<Customer>(this.GetAllCustomersSql);

                return result;
            }
        }
    }
}
