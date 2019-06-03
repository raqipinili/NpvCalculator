using Microsoft.Extensions.DependencyInjection;
using NpvCalculator.Core;

namespace NpvCalculator.Api.Configuration.Startup
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IFinancialCalculator, FinancialCalculator>();
            return services;
        }
    }
}
