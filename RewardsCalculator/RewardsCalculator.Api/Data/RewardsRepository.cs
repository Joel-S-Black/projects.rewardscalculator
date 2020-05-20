using Dapper;
using Microsoft.Data.Sqlite;
using RewardsCalculator.Api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace RewardsCalculator.Api.Data
{
    public class RewardsRepository
    {
        private static string separator = Path.DirectorySeparatorChar.ToString();
        private string _connectionString = $"Data Source='{Environment.CurrentDirectory}{separator}Data{separator}Database{separator}transactions.db'";

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
            using(var conn = new SqliteConnection(_connectionString))
            {
                conn.Open();

                var result = conn.Query<Product>(this.GetAllProductsSql);

                return result;
            }
        }
    }
}
