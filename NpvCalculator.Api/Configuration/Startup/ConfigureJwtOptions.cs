using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NpvCalculator.Security.Helpers;

namespace NpvCalculator.Api.Configuration.Startup
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureJwtOptions(this IServiceCollection services, IConfiguration config, SigningCredentials signingCredentials)
        {
            var jwtSection = config.GetSection(nameof(JwtOptions));

            services.AddOptions();
            services.Configure<JwtOptions>(options =>
            {
                options.Issuer = jwtSection[nameof(JwtOptions.Issuer)];
                options.Audience = jwtSection[nameof(JwtOptions.Audience)];
                options.SigningCredentials = signingCredentials;
            });

            services.AddScoped(o => o.GetService<IOptionsSnapshot<JwtOptions>>().Value);

            return services;
        }
    }
}
