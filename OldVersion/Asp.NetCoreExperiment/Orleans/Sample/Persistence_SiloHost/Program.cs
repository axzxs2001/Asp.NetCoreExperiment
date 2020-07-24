


using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Persistence_Lib;
using System;
using System.Net;
using System.Threading.Tasks;


namespace Persistence_SiloHost
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
            var assambly = typeof(IHelloGrain).Assembly;
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
                //.UseInMemoryReminderService()         
                //.AddMemoryGrainStorage("OrleansStorage", options => options.NumStorageGrains = 10);

                .AddAdoNetGrainStorage("OrleansStorage", options =>
                {

                    options.UseJsonFormat = true;
                    options.Invariant = "System.Data.SqlClient";
                    options.ConnectionString = "Data Source=.;Initial Catalog=orleansdb;Persist Security Info=True;User ID=sa;Password=sa;";

                });
            // .UseLocalhostClustering();

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }



    }



}
