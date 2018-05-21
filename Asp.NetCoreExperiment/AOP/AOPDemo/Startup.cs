using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AOPDemo.Models.Repository;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AOPDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddTransient<IItemManageRepository, ItemManageRepository>();
        //    services.AddMvc();
        //    services.AddDynamicProxy();
        //    var sp = services.BuildAspectCoreServiceProvider();
        //}
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            //使用Session,支持内存，Redis、SqlServer存储方式
            services.AddDistributedMemoryCache();

            services.AddSession(options => {
                //options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromMinutes(20);
               // options.Cookie.HttpOnly = true;              
            });

            //cookie验证
            services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
            {
                opt.LoginPath = "/login";
                opt.Cookie.Path = "/";
            });

            services.AddTransient<IItemManageRepository, ItemManageRepository>();
            services.AddMvc();
            services.AddDynamicProxy();
            return services.BuildAspectCoreServiceProvider();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();
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
