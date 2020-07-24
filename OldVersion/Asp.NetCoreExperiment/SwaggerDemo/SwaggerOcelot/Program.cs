using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SwaggerOcelot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration(conbuilder =>
                 {
                     conbuilder.AddJsonFile("appsettings.json");
                     conbuilder.AddJsonFile("configuration.json");
                 })
                  .UseUrls("http://*:9000")
                .UseStartup<Startup>();
    }
}
