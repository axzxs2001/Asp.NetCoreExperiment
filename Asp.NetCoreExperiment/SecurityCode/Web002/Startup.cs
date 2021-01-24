using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web002.Permission;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PolicyPrivilegeManagement.Models;

namespace Web002
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
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthorization(options =>
            {
                //自定义Requirement，userPermission可从数据库中获得
                var userPermission = new List<UserPermission> {
                              new UserPermission {  Url="home/index", UserName="gsw"},
                              new UserPermission {  Url="adminpage", UserName="gsw"},
                              new UserPermission {  Url="logout", UserName="gsw"},
                              new UserPermission {  Url="home/index", UserName="aaa"},
                              new UserPermission {  Url="systempage", UserName="aaa"},
                              new UserPermission {  Url="logout", UserName="aaa"}

                          };
                options.AddPolicy("Permission", policy =>
                {
                    policy.Requirements.Add(new PermissionRequirement("/denied", userPermission));
                });
            })
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/login");
                    options.AccessDeniedPath = new PathString("/denied");
                });
            //注入授权Handler
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddControllersWithViews(opt =>
            {
                opt.EnableEndpointRouting = false;
               // opt.EnableEndpointRouting = true;
            })
            .AddNewtonsoftJson();
            services.AddRazorPages();

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //添加验证
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
            //普通模式
            app.UseMvcWithDefaultRoute();
            //终端点模式
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Home}/{action=Index}/{id?}");
            //    endpoints.MapRazorPages();
            //});

            app.UseCookiePolicy();
        }
    }
}
