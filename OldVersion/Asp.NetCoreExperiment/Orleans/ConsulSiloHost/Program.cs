
using ConsulLib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HelloWorld_SiloHost
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.Title = "Server";
            var result = RunMainAsync().Result;
            Console.ReadLine();
            return result;
        }
        private static async Task<int> RunMainAsync()
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("回车结束...");
                Console.ReadLine();
                await host.StopAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }


        private static async Task<ISiloHost> StartSilo()
        {


            //Grain的程序集
            var assambly = typeof(IConsulGrain).Assembly;
            var builder = new SiloHostBuilder()                  
                   .Configure<ClusterOptions>(options =>
                   {
                       options.ClusterId = "dev";
                       options.ServiceId = "TestApp";

                   })
                   .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                   .ConfigureApplicationParts(parts => parts.AddApplicationPart(assambly).WithReferences())
                   .ConfigureLogging(logging => logging.AddConsole())
                   .UseConsulClustering(opt =>
                   {
                       opt.Address = new Uri("http://127.0.0.1:8500");
                       opt.KvRootFolder = "orleans";

                   });

            var host = builder.Build();

            await host.StartAsync();
            return host;
        }



    }




}
