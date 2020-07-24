using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ENVIRONMENT_Demo01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Console.WriteLine($"-------------------Program中env={env}----------------");
            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.FullName;
            switch (env)
            {
                //本地开发环境
                case "Development":
                    return WebHost.CreateDefaultBuilder(args)
                        .UseUrls("http://*:5000")
                        .UseStartup(assemblyName);
                //正式环境
                case "Production":
                    return WebHost.CreateDefaultBuilder(args)
                    .UseStartup(assemblyName)
                    .UseKestrel(options =>
                    {
                        options.Listen(IPAddress.Any, 80);
                        options.Listen(IPAddress.Any, 443, listenOptions =>
                        {
                            listenOptions.UseHttps("my.pfx", "password");
                        });
                    });
                //测试环境
                case "Staging":
                    return WebHost.CreateDefaultBuilder(args)
                      .UseUrls("http://*:5678")
                      .UseStartup(assemblyName);
                //默认
                default:
                    return WebHost.CreateDefaultBuilder(args)
                           .UseStartup("");
            }
        }
    }
}
