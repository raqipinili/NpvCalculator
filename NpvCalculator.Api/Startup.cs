using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NpvCalculator.Api.Configuration.Startup;
using Security.Core.Helpers;

namespace NpvCalculator.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string secretKey = Configuration.GetSection("AppSettings:SecretKey").Value;
            var signingCredentials = AuthHelper.GetSigningCredentials(secretKey);

            services.ConfigureDependencyInjection();
            services.ConfigureDatabase(Configuration);
            services.ConfigureCors();
            services.ConfigureJwtOptions(Configuration, signingCredentials);
            services.ConfigureAuthentication(Configuration, signingCredentials.Key);
            services.ConfigureAuthorization();
            services.ConfigureContextAccessor();
            services.ConfigureMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            app.UseCors("localhost_4200");
            // app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
