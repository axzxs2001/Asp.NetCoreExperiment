using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using PolicyPrivilegeManagement.Models;

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
                // options.AddPolicy("RequireClaim", policy => policy.RequireRole("admin", "system"));
                //基于用户名
                //options.AddPolicy("RequireClaim", policy => policy.RequireUserName("桂素伟"));
                //基于Claim
                //options.AddPolicy("RequireClaim", policy => policy.RequireClaim(ClaimTypes.Country,"中国"));
                //自定义值
                // options.AddPolicy("RequireClaim", policy => policy.RequireClaim("date","2017-09-02"));
                //自定义Requirement,userPermission可从数据库中获得
   
                var userPermission = new List<UserPermission> {
                              new UserPermission {  Url="/", UserName="gsw"},
                              new UserPermission {  Url="/home/permissionadd", UserName="gsw"},
                              new UserPermission {  Url="/", UserName="aaa"},
                              new UserPermission {  Url="/home/contact", UserName="aaa"}
                          };

                options.AddPolicy("Permission",
                          policy => policy.Requirements.Add(new PermissionRequirement("/denied", userPermission)));

            }).AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>{
                options.LoginPath = new PathString("/login");
                options.AccessDeniedPath = new PathString("/denied");

            });
            //注入授权Handler
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
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
