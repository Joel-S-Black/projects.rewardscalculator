using Dapper;
using Microsoft.Data.Sqlite;
using RewardsCalculator.Api.Models;
using System.Collections.Generic;

namespace RewardsCalculator.Api.Data
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetAllProductsSql
        {
            get
            {
                return @"
                            SELECT  product_id  AS ProductId,
                                    description AS Description,
                                    short_name  AS ShortName,
                                    unit_price  AS UnitPrice
                            FROM products
                        ";
            }
        }

        public IEnumerable<Product> GetAllProducts()
        {
            using (var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();

                var result = conn.Query<Product>(this.GetAllProductsSql);

                return result;
            }
        }
    }
}
