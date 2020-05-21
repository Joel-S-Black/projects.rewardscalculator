using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using RewardsCalculator.Api.Data;
using RewardsCalculator.Api.Services;

namespace RewardsCalculator.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore().AddFormatterMappings().AddJsonFormatters();

            services.Configure<ConnectionString>(conn => 
            {
                string separator = Path.DirectorySeparatorChar.ToString();
                conn.Value = $"Data Source='{Environment.CurrentDirectory}{separator}Data{separator}Database{separator}transactions.db'";
            });

            services.AddSingleton<IRewardPointsCalculator, RewardPointsCalculator>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();
            services.AddScoped<IRewardsService, RewardsService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}
