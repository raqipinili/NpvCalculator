using Microsoft.Extensions.DependencyInjection;
using Security.Core.Enums;

namespace NpvCalculator.Api.Configuration.Startup
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("NPV", policy => policy.RequireClaim("permissions", new[] { ((int)Permissions.NetPresentValue).ToString() }));
                options.AddPolicy("PV", policy => policy.RequireClaim("permissions", new[] { ((int)Permissions.PresentValue).ToString() }));
                options.AddPolicy("FV", policy => policy.RequireClaim("permissions", new[] { ((int)Permissions.FutureValue).ToString() }));
            });

            return services;
        }
    }
}
