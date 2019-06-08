using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace NpvCalculator.Api.Configuration.Startup
{
    public static partial class ConfigurationExtensions
    {
        public static IServiceCollection ConfigureMvc(this IServiceCollection services)
        {
            services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services;
        }
    }
}
