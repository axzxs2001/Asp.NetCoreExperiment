using ADONETProviders_Lib;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

using System;
using System.Net;
using System.Threading.Tasks;


namespace ADONETProviders_SiloHost001
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.Title = "Server001";
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
            var invariant = "System.Data.SqlClient"; // for Microsoft SQL Server
            var connectionString = "Data Source=.;Initial Catalog=orleansdb;Persist Security Info=True;User ID=sa;Password=sa;";
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
                //use AdoNet for clustering 
                .UseAdoNetClustering(options =>
                {
                    options.Invariant = invariant;
                    options.ConnectionString = connectionString;
                })
                //use AdoNet for reminder service
                .UseAdoNetReminderService(options =>
                {
                    options.Invariant = invariant;
                    options.ConnectionString = connectionString;
                })
                //use AdoNet for Persistence
                .AddAdoNetGrainStorage("GrainStorageForTest", options =>
                {
                    options.Invariant = invariant;
                    options.ConnectionString = connectionString;
                });


            var host = builder.Build();
            await host.StartAsync();
            return host;
        }



    }



}
