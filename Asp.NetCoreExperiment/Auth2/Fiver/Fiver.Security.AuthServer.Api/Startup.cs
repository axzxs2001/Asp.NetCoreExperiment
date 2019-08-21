using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Fiver.Security.AuthServer.Api
{
    public class Startup
    {
        public void ConfigureServices(
            IServiceCollection services)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.JwtAuthenticationScheme)
                    .AddIdentityServerAuthentication(options =>
                     {
                         options.Authority = "http://localhost:5000"; // Auth Server
                         options.RequireHttpsMetadata = false;
                         options.ApiName = "fiver_auth_api"; // API Resource Id
                     });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


            // app.UseDeveloperExceptionPage();
            var path = Directory.GetCurrentDirectory();
           // app.UsePathBase("{Fiver.Security.AuthServer.Api}");
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
