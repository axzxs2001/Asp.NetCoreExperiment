using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace APIHTTPSDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel(options =>
                    {
                        //options.Listen(IPAddress.Any, 80);
                        options.Listen(IPAddress.Any, 8443, listenOptions =>
                        {
                            listenOptions.UseHttps("server.pfx", "gsw123");
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
