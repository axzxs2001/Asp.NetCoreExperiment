

using ConsulLib;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace ConsulClient
{
    class Program
    {
        const int initializeAttemptsBeforeFailing = 5;
        private static int attempt = 0;

        static void Main(string[] args)
        {
            Console.Title = "Client，回车开始"; while (true)
            {
                RunMainAsync().Wait();
            }
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                using (var client = await StartClientWithRetries())
                {

                    await DoClientWork(client);
                    Console.ReadKey();
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                return 1;
            }
        }

        private static async Task<IClusterClient> StartClientWithRetries()
        {
            attempt = 0;
            IClusterClient client = new ClientBuilder()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "TestApp";
                })
                .UseConsulClustering(opt =>
                {
                    opt.Address = new Uri("http://127.0.0.1:8500");
                    opt.KvRootFolder = "orleans";
                })
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await client.Connect(RetryFilter);
            Console.WriteLine("Client成功连接服务端");
            return client;
        }
        /// <summary>
        /// 重连
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static async Task<bool> RetryFilter(Exception exception)
        {
            if (exception.GetType() != typeof(SiloUnavailableException))
            {
                Console.WriteLine($"集群客户端失败连接，返回unexpected error.  Exception: {exception}");
                return false;
            }
            attempt++;
            Console.WriteLine($"集群客户端试图 {attempt}/{initializeAttemptsBeforeFailing} 失败连接.  Exception: {exception}");
            if (attempt > initializeAttemptsBeforeFailing)
            {
                return false;
            }
            await Task.Delay(TimeSpan.FromSeconds(4));
            return true;
        }
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private static async Task DoClientWork(IClusterClient client)
        {
            while (true)
            {
                Console.WriteLine("回车开始下一个请求");
                var content = Console.ReadLine();
                var hello = client.GetGrain<IConsulGrain>(Guid.NewGuid());
                await hello.Method1(content);
            }

        }
    }
}
