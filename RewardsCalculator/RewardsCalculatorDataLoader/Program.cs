using Dapper;
using Microsoft.Data.Sqlite;
using RewardsCalculator.Api.Data;
using RewardsCalculator.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.IO;
using System.Linq;

namespace RewardsCalculatorDataLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            //using(var conn = new SqliteConnection("Data Source='transactions.db'"))
            //{
            //    var res = conn.Query("select SQLITE_VERSION() AS Version");
            //    Console.WriteLine(res);
            //    Console.ReadLine();
            //}

            string separator = Path.DirectorySeparatorChar.ToString();
            string _connectionString = $"Data Source='{Environment.CurrentDirectory}{separator}Data{separator}Database{separator}transactions.db'";
            var repo = new ProductRepository(_connectionString);

            var results = repo.GetAllProducts();

            Console.WriteLine("Products: ");
            foreach(var item in results)
            {
                Console.WriteLine($"{item.ProductId} - {item.Description} - {item.ShortName} - {item.UnitPrice}");
            }

            Console.WriteLine("Customers: ");
            var customers = new CustomerRepository(_connectionString);

            var custResults = customers.GetAllCustomers();
            foreach(var item in custResults)
            {
                Console.WriteLine($"{item.CustomerId} - {item.FirstName} - {item.LastName}");
            }

            var date = new DateTime(2020, 1, 1);
            int counter = 1;

            var transactions = new List<Transaction>();

            while (date < DateTime.Now)
            {
                var transactionDate = date.ToShortDateString();

                var random = new Random();

                foreach (var cust in custResults)
                {
                    var randomProduct = random.Next(1, results.Count());
                    var randomPrice = random.Next(1, counter);
                    var actualProduct = results.FirstOrDefault(p => p.ProductId == randomProduct);
                    Console.WriteLine($"Insert into purchases ( customer_id,product_id, amount, purchase_date) Values({cust.CustomerId},{actualProduct.ProductId},{actualProduct.UnitPrice * randomPrice},'{transactionDate}');");

                    var purchase = new Transaction
                    {
                        CustomerId = cust.CustomerId,
                        ProductId = actualProduct.ProductId,
                        Amount = actualProduct.UnitPrice * randomPrice,
                        PurchaseDate = date
                    };

                    transactions.Add(purchase);
                }

                counter++;
                date = date.AddDays(1);
            }

            Console.ReadLine();
        }
    }
}
