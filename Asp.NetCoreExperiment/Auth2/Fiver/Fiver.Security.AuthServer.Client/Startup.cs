using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace Fiver.Security.AuthServer.Client
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        IConfiguration Configuration;
        public void ConfigureServices(
            IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // cookie middle setup above
                options.Authority = "http://localhost:5000"; // Auth Server
                options.RequireHttpsMetadata = false; // only for development 
                options.ClientId = "fiver_auth_client"; // client setup in Auth Server
                options.ClientSecret = "secret";
                options.ResponseType = "code id_token"; // means Hybrid flow (id + access token)
                options.Scope.Add("fiver_auth_api");
                options.Scope.Add("offline_access");
                options.GetClaimsFromUserInfoEndpoint = true;
                options.SaveTokens = true;
            });

            services.AddMvc();
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
