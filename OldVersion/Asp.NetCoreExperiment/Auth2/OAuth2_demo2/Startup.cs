using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;

namespace OAuth2_demo2
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "GitHub";
            })
   .AddCookie()
   .AddOAuth("GitHub", options =>
   {
       options.ClientId = Configuration["GitHub:ClientId"];
       options.ClientSecret = Configuration["GitHub:ClientSecret"];
       options.CallbackPath = new PathString("/signin-github");

       options.AuthorizationEndpoint = "https://github.com/login/oauth/authorize";
       options.TokenEndpoint = "https://github.com/login/oauth/access_token";
       options.UserInformationEndpoint = "https://api.github.com/user";

       options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
       options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
       options.ClaimActions.MapJsonKey("urn:github:login", "login");
       options.ClaimActions.MapJsonKey("urn:github:url", "html_url");
       options.ClaimActions.MapJsonKey("urn:github:avatar", "avatar_url");

       options.Events = new OAuthEvents
       {
           OnCreatingTicket = async context =>
           {
               var request = new HttpRequestMessage(HttpMethod.Get, context.Options.UserInformationEndpoint);
               request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
               request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);

               var response = await context.Backchannel.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, context.HttpContext.RequestAborted);
               response.EnsureSuccessStatusCode();

               var user = JObject.Parse(await response.Content.ReadAsStringAsync());

               context.RunClaimActions(user);
           }
       };
   });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
