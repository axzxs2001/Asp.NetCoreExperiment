using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Hangfire.Server;
using Hangfire;
using Hangfire.Storage;

namespace HangfireTest
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHangfire(x => x.UseSqlServerStorage("Data Source=.;Initial Catalog=ExperimentPageDb;Persist Security Info=True;User ID=sa;Password=gsw123"));
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseHangfireServer();//启动Hangfire服务
            app.UseHangfireDashboard();//启动hangfire面板

            using (var connection = JobStorage.Current.GetConnection())
            {
                var storageConnection = connection as JobStorageConnection;
                if (storageConnection != null)
                {
                    //立即启动
                    var jobId = BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));
                }
            }
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring!"), Cron.Minutely, queue: "test");

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
