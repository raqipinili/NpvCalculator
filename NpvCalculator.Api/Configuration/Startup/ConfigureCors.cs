using Microsoft.Extensions.DependencyInjection;

namespace NpvCalculator.Api.Configuration.Startup
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("localhost_4200", builder =>
                {
                    builder.WithOrigins(new[] { "http://localhost:4200", "https://localhost:4200" })
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });
            });

            return services;
        }
    }
}
