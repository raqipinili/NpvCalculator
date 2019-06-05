using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NpvCalculator.Data;
using System;

namespace NpvCalculator.Api.Configuration.Startup
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration config)
        {
            // SQL Server
            string sqlServerConnection = config.GetConnectionString("SqlServerConnection");
            services.AddDbContext<CalculatorDbContext>(options =>
                options.UseSqlServer(sqlServerConnection, sqlServerOptionsAction: opt =>
                    opt.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)));

            // Sqlite
            //string sqliteConnection = config.GetConnectionString("SqliteConnection");
            //services.AddDbContext<CalculatorDbContext>(options => options.UseSqlite(sqliteConnection));

            return services;
        }
    }
}
