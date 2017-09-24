using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            LoadServer();
        }

        void LoadServer()
        {
            var config = Orleans.Runtime.Configuration.ClusterConfiguration.LocalhostPrimarySilo(2234, 1234);
            var silohost = new Orleans.Runtime.Host.SiloHost("Ba", config);
            silohost.InitializeOrleansSilo();
            silohost.StartOrleansSilo();

            if (silohost.IsStarted)
            {
                Console.WriteLine("silohost 启动成功");
            }
            else
            {
                Console.WriteLine("启动失败");
            }
            Console.ReadKey();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
