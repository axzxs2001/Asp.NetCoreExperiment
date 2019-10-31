using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace HostTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("init main");          
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {             
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {         
                NLog.LogManager.Shutdown();
            }
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureAppConfiguration((context, builder) =>
                {
                    builder.AddJsonFile("host.json");
                });
                webBuilder.UseStartup<Startup>();
            })
            .UseNLog();
    }
}
