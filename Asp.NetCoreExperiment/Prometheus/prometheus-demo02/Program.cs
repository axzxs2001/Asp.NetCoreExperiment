using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Prometheus.DotNetRuntime;

namespace prometheus_demo02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enabling prometheus-net.DotNetStats...");
            //DotNetRuntimeStatsBuilder.Default().StartCollecting();
            DotNetRuntimeStatsBuilder.Customize()
                .WithThreadPoolSchedulingStats()
                .WithContentionStats()
                .WithGcStats()
                .WithJitStats()
                .WithThreadPoolStats()
                .WithErrorHandler(ex => Console.WriteLine("ERROR: " + ex.ToString()))
                // .WithDebuggingMetrics(true)
                .StartCollecting();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(opts =>
                    {
                        opts.AllowSynchronousIO = true;
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
