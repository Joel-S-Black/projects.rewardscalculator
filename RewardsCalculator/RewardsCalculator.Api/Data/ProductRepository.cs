using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using RewardsCalculator.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RewardsCalculator.Api.Data
{
    public class ProductRepository: IProductRepository
    {
        private readonly ConnectionString _connectionString;

        public ProductRepository(IOptions<ConnectionString> connectionString)
        {
            _connectionString = connectionString.Value;
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

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            using (var conn = new SqliteConnection(_connectionString.Value))
            {
                conn.Open();

                var result = await conn.QueryAsync<Product>(this.GetAllProductsSql);

                return result;
            }
        }
    }
}
