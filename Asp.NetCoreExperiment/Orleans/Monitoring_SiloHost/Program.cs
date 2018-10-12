


using Microsoft.Extensions.Logging;
using Monitoring_Lib;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.TelemetryConsumers.AI;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Monitoring_SiloHost
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
            var assambly = typeof(IHello).Assembly;
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "TestApp";
                })
                //重入的配置参数
                .Configure<SchedulingOptions>(options =>
                {
                    options.AllowCallChainReentrancy = false;

                })
                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(assambly).WithReferences())
                .ConfigureLogging(logging => logging.AddConsole())
                .UseInMemoryReminderService()
                .UseLocalhostClustering()
                .Configure<ApplicationInsightsTelemetryConsumerOptions>(opt => {
                    opt.InstrumentationKey = ""; 
                   
                })
                //.UseConsulClustering(opt =>
                //{
                //    opt.Address = new Uri("");
                //    opt.KvRootFolder = "";
                //    opt.AclClientToken = "";
                //})
                .Configure<TelemetryOptions>(options =>
                {
                    options.AddConsumer<AITelemetryConsumer>();              
                });


            var host = builder.Build();
            await host.StartAsync();
            return host;
        }



    }



}

