using Microsoft.Extensions.DependencyInjection;
using NpvCalculator.Core.Services;
using NpvCalculator.Security.Services;

namespace NpvCalculator.Api.Configuration.Startup
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IFinancialCalculator, FinancialCalculator>();
            services.AddTransient<IFinancialService, FinancialService>();
            return services;
        }
    }
}
