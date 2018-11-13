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
            .ConfigureHealthWithDefaults(
                builder =>
                {
                    builder.Configuration.Configure(opt => { opt.ReportingEnabled = true; opt.ApplicationName = "aaaa"; opt.Enabled = true; });
                    const int threshold = 100;
                    builder.HealthChecks.AddCheck("DatabaseConnected", () => new ValueTask<HealthCheckResult>(HealthCheckResult.Healthy("Database Connection OK")));
                    builder.HealthChecks.AddProcessPrivateMemorySizeCheck("Private Memory Size", threshold);
                    builder.HealthChecks.AddProcessVirtualMemorySizeCheck("Virtual Memory Size", threshold);
                    builder.HealthChecks.AddProcessPhysicalMemoryCheck("Working Set", threshold);
                    builder.HealthChecks.AddPingCheck("google ping", "google.com", TimeSpan.FromSeconds(10));
                    builder.HealthChecks.AddHttpGetCheck("github", new Uri("https://github.com/"), TimeSpan.FromSeconds(10));
                })
            //#endif
            //#if HOSTING_OPTIONS
            .ConfigureAppHealthHostingConfiguration(options =>
            {
                options.PingEndpoint = "/ping";
                options.PingEndpointPort = 5001;
                options.HealthEndpoint = "/health";
                options.HealthEndpointPort = 5001;
            })
            //#endif
            .UseHealth().UseHealthEndpoints(opt => { opt.HealthEndpointEnabled = true; })
            .UseStartup<Startup>();
    }
}
