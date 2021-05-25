using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPIThreadPoolDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //ab -n 100 -c 100 http://localhost:5000/syncdata
            //ab -n 100 -c 100 http://localhost:5000/asyncdata
           // ThreadPool.SetMinThreads(50, 8);
            ThreadPool.GetMinThreads(out int worker, out int io);
            Console.WriteLine($"worker:{worker},IO:{io}");
            ThreadPool.GetMaxThreads(out int worker1, out int io1);

            Console.WriteLine($"worker:{worker1},IO:{io1}");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {                   
                    webBuilder.UseStartup<Startup>();
                });
    }
}
