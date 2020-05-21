using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using RewardsCalculator.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RewardsCalculator.Api.Data
{
    public class CustomerRepository: ICustomerRepository
    {
        private readonly ConnectionString _connectionString;

        public CustomerRepository(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value;
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
            using (var conn = new SqliteConnection(_connectionString.Value))
            {
                conn.Open();

                var result = await conn.QueryAsync<Customer>(this.GetAllCustomersSql);

                return result;
            }
        }
    }
}
