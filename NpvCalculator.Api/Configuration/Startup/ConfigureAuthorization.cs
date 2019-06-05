using Microsoft.Extensions.DependencyInjection;

namespace NpvCalculator.Api.Configuration.Startup
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("TestPolicy", policy => policy.RequireClaim("test.claim"));
            });

            return services;
        }
    }
}
