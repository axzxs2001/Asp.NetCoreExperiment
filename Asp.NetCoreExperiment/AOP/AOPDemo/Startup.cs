using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AOPDemo.Models.Repository;
using AspectCore.Extensions.DependencyInjection;
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
