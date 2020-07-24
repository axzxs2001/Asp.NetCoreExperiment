using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ConsumeDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args == null || args.Length < 2)
            {            
                Name = "";
                Port = 5003;
            }
            else
            {
                Name = args[1];
                Port = int.Parse(args[0]);
            }
            CreateHostBuilder(args).Build().Run();
        }
        public static string Name;
        public static int Port;
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls($"http://*:{Port}");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
