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
            if (args.Length < 2)
            {
                Console.WriteLine("必需要参数1 端口，参数2 消费者名称");
                Console.ReadLine();
                return;
            }
            Name = args[1];
            CreateHostBuilder(args).Build().Run();
        }
        public static string Name;
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls($"http://*:{args[0]}");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
