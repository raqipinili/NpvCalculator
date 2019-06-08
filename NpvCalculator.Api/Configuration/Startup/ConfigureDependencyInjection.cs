using Microsoft.Extensions.DependencyInjection;
using NpvCalculator.Core.Services;
using Security.Core.Services;

namespace NpvCalculator.Api.Configuration.Startup
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IFinancialCalculator, FinancialCalculator>();
            services.AddTransient<IFinancialService, FinancialService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<ITokenService, TokenService>();
            return services;
        }
    }
}
