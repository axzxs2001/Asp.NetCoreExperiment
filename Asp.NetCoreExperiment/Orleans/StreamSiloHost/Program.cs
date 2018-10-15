using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using StreamLib;
using System;
using System.Net;
using System.Threading.Tasks;

namespace StreamSiloHost
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
            var assambly = typeof(IRandomReceiver).Assembly;
            var builder = new SiloHostBuilder()
                   .UseLocalhostClustering()
                   .Configure<ClusterOptions>(options =>
                   {
                       options.ClusterId = "dev";
                       options.ServiceId = "TestApp";
                   })
                   .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                   .ConfigureApplicationParts(parts => parts.AddApplicationPart(assambly).WithReferences())
                   .ConfigureLogging(logging => logging.AddConsole())
                   .UseInMemoryReminderService()
                   .UseLocalhostClustering()
                   //.AddPersistentStreams("SMSProvider",
                   //GeneratorAdapterFactory.Create,
                   // providerConfigurator => providerConfigurator.Configure<HashRingStreamQueueMapperOptions>(ob => ob.Configure( options => { options.TotalQueueCount = 1;})))
                   .AddSimpleMessageStreamProvider("SMSProvider")
                   //RabbitMq实现队列订阅通知
                   //.AddRabbitMqStream("RMQProvider", configurator =>
                   //{
                   //    configurator.ConfigureRabbitMq(host: "localhost", port: 5672, virtualHost: "/",
                   //                                   user: "guest", password: "guest", queueName: "test");
                   //})
                   .AddMemoryGrainStorage("PubSubStore");


            var host = builder.Build();

            await host.StartAsync();
            return host;
        }



    }




}
