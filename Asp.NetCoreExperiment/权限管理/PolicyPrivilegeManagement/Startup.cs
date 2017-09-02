using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace PolicyPrivilegeManagement
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
            services.AddMvc();
            services.AddAuthorization(options =>
            {
                //基于角色的策略
                //options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("admin", "system"));
                //基于用户名
                //options.AddPolicy("RequireAdministratorRole", policy => policy.RequireUserName("桂素伟"));
                //基于Claim
                //options.AddPolicy("RequireAdministratorRole", policy => policy.RequireClaim(ClaimTypes.Country,"中国"));

                options.AddPolicy("RequireAdministratorRole", policy => policy.RequireClaim("date",""));

            }).AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>{
                options.LoginPath = new PathString("/login");
                options.AccessDeniedPath = new PathString("/denied");
            });
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            //验证中间件
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
