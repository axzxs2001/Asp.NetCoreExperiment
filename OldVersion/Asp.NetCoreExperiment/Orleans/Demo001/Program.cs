using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Orleans.Hosting;
using Orleans.Configuration;
using System.Net;
using Microsoft.Extensions.Logging;

namespace Demo001
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.Title = "Server";
            return RunMainAsync().Result;
        }
        private static async Task<int> RunMainAsync()
        {
            try
            {
                var host = await StartSilo();
                Console.WriteLine("Press Enter to terminate...");
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

            // define the cluster configuration
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "HelloWorldApp";
                })
                .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
                .ConfigureLogging(logging => logging.AddConsole());

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }


    public interface IHello : IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);
    }
    public class HelloGrain : Grain, IHello
    {
        public Task<string> SayHello(string greeting)
        {
            Console.WriteLine($"SayHello message received: greeting = '{greeting}'");
            return Task.FromResult($"You said: '{greeting}', I say: Hello!");
        }
    }
}

