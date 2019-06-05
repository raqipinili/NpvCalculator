using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NpvCalculator.Security.Helpers;
using System;

namespace NpvCalculator.Api.Configuration.Startup
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureAuthentication(this IServiceCollection services, IConfiguration config, SecurityKey securityKey)
        {
            var jwtSection = config.GetSection(nameof(JwtOptions));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.ClaimsIssuer = jwtSection[nameof(JwtOptions.Issuer)];
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtSection[nameof(JwtOptions.Issuer)],

                    ValidateAudience = true,
                    ValidAudience = jwtSection[nameof(JwtOptions.Audience)],

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = securityKey,

                    ValidateLifetime = true,
                    RequireExpirationTime = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            return services;
        }
    }
}
