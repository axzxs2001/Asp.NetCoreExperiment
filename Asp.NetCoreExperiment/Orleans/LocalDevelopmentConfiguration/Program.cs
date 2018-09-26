

using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Runtime;
using System;
using System.Net;
using System.Threading.Tasks;



namespace LocalDevelopmentConfiguration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "SiloHost";
            RunSiloAsync().Wait();


            Console.Title = "Client";
            RunClientAsync().Wait();
            Console.ReadLine();
        }
        #region silo
        private static async Task<int> RunSiloAsync()
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
            var host = new SiloHostBuilder()
                //localhost集群用于单个本地Silo
                .UseLocalhostClustering()//.UseDevelopmentClustering(new IPEndPoint(IPAddress.Parse(""), 30000))
                                         //配置ClusterId 和 ServiceId
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "AccountTransferApp";
                })
                // 配置连接
                .Configure<EndpointOptions>(options =>
                {
                    //Silo到Silo通信端口
                    options.SiloPort = 11111;
                    //client到silo通信端口
                    options.GatewayPort = 30000;
                    //集群中通知端口
                    options.AdvertisedIPAddress = IPAddress.Parse("172.16.0.42");
                    //网关使用的套接字将绑定到此端点
                    options.GatewayListeningEndpoint = new IPEndPoint(IPAddress.Any, 40000);
                    //用于silo-to-silo的套接字将绑定到此端点 
                    options.SiloListeningEndpoint = new IPEndPoint(IPAddress.Any, 50000);
                })
                //配置引用的Grain
                .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(MyGrain).Assembly).WithReferences())
                //配置日志
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await host.StartAsync();
            return host;
        }
        #endregion
        #region client
        private static async Task<int> RunClientAsync()
        {
            try
            {
                using (var client = await StartClientWithRetries())
                {
                    while (true)
                    {
                        await DoClientWork(client);
                        Console.WriteLine("回车重新开始");
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 1;
            }
        }

        private static async Task<IClusterClient> StartClientWithRetries(int initializeAttemptsBeforeFailing = 5)
        {
            int attempt = 0;
            IClusterClient client;
            while (true)
            {
                try
                {
                    int gatewayPort = 30000;
                    var siloAddress = IPAddress.Loopback;
                    var gateway = new IPEndPoint(siloAddress, gatewayPort);

                    client = new ClientBuilder()
                        //localhost集群用于单个本地Silo
                        .UseLocalhostClustering()
                        //配置ClusterId 和 ServiceId
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "dev";
                            options.ServiceId = "AccountTransferApp";
                        })
                        //配置引用的Grain
                        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(IMyGrain).Assembly).WithReferences())
                        //配置日志
                        .ConfigureLogging(logging => logging.AddConsole())
                        .Build();

                    await client.Connect();
                    Console.WriteLine("Client successfully connect to silo host");
                    break;
                }
                catch (SiloUnavailableException)
                {
                    attempt++;
                    Console.WriteLine($"Attempt {attempt} of {initializeAttemptsBeforeFailing} failed to initialize the Orleans client.");
                    if (attempt > initializeAttemptsBeforeFailing)
                    {
                        throw;
                    }
                    await Task.Delay(TimeSpan.FromSeconds(4));
                }
            }

            return client;
        }

        private static async Task DoClientWork(IClusterClient client)
        {
            var myGrain = client.GetGrain<IMyGrain>(0);
            await myGrain.Method1();
        }
        #endregion
    }

    public interface IMyGrain : IGrainWithIntegerKey
    {
        Task Method1();
    }
    public class MyGrain : Grain, IMyGrain
    {
        public Task Method1()
        {
            return Task.CompletedTask;
        }
    }

}
