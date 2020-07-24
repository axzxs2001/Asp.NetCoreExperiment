using GrainHub;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using WebSiloHost.Repository;

namespace WebSiloHost
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



            Console.WriteLine(IPAddress.Loopback);
            //注入配置文件
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            var invariant = "System.Data.SqlClient";
            var connectionString = config.GetConnectionString("DefaultConnectionString");

            var assambly = typeof(ISettlementGrain).Assembly;

            var builder = new SiloHostBuilder()
                   .Configure<ClusterOptions>(options =>
                   {
                       options.ClusterId = config.GetSection("Cluster").GetSection("ClusterID").Value;
                       options.ServiceId = config.GetSection("Cluster").GetSection("ServiceID").Value;
                   })
                   //设置端口
                   .Configure<EndpointOptions>(options =>
                   {
                       //配置本地套节字
                       options.SiloPort = 11111;
                       options.GatewayPort = 30000;
                       options.AdvertisedIPAddress = IPAddress.Parse(config.GetSection("IP").Value);
                       options.SiloListeningEndpoint = new IPEndPoint(IPAddress.Any, 11111);
                       options.GatewayListeningEndpoint = new IPEndPoint(IPAddress.Any, 30000);

                   })

                   .ConfigureApplicationParts(parts => parts.AddApplicationPart(assambly).WithReferences())
                   .ConfigureLogging(logging => logging.AddConsole())
                   .AddLogStorageBasedLogConsistencyProvider("LogStorage")
                   .AddStateStorageBasedLogConsistencyProvider("StateStorage")
                   .ConfigureAppConfiguration(context =>
                   {
                       context.AddConfiguration(config);
                   })
                   // .UseInMemoryReminderService()
                   //依赖注入
                   .UseServiceProviderFactory(opt =>
                   {
                       opt.AddTransient<ISettlementRepository, SettlementRepository>();
                       return opt.BuildServiceProvider();
                   })
                   //用AdoNet集群 
                   .UseAdoNetClustering(options =>
                   {
                       options.Invariant = invariant;
                       options.ConnectionString = connectionString;
                   })
                   //用AdoNet作为提醒服务
                   .UseAdoNetReminderService(options =>
                   {
                       options.Invariant = invariant;
                       options.ConnectionString = connectionString;
                   })
                   //用AdoNet持久化
                   .AddAdoNetGrainStorage("SettlementStore", options =>
                   {
                       options.UseJsonFormat = true;
                       options.Invariant = invariant;
                       options.ConnectionString = connectionString;
                   })
                   .AddSimpleMessageStreamProvider(name: "SMSProvider");



            var host = builder.Build();

            await host.StartAsync();
            return host;
        }
    }
}
