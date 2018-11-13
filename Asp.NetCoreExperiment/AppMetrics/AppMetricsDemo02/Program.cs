using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.Metrics.AspNetCore.Health;
using App.Metrics.Health;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AppMetricsDemo02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            //#if INLINE_CHECKS
            //.ConfigureHealthWithDefaults(
            //    builder =>
            //    {
            //        builder.HealthChecks.AddCheck("DatabaseConnected", () => new ValueTask<HealthCheckResult>(HealthCheckResult.Healthy("Database Connection OK")));                  
            //        builder.HealthChecks.AddHttpGetCheck("AppMetricsDemo01", new Uri("http://localhost:5000/health"), TimeSpan.FromSeconds(10));
            //    })
         //#endif
         //#if HOSTING_OPTIONS
         //.ConfigureAppHealthHostingConfiguration(options =>
         //{
         //    options.HealthEndpoint = "/health";
         //    options.HealthEndpointPort = 5001;
         //})
         //#endif
        // .UseHealth()
            .UseStartup<Startup>();
    }
}
