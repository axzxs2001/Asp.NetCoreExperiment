using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Runtime;
using System;
using System.Net;
using System.Threading.Tasks;
using Transactions_Lib;

namespace Transactions_Client
{
    /// <summary>
    /// Orleans test silo client
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Client";
            RunMainAsync().Wait();
            Console.ReadLine();
        }

        private static async Task<int> RunMainAsync()
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
                        .UseLocalhostClustering()
                        .Configure<ClusterOptions>(options =>
                        {
                            options.ClusterId = "dev";
                            options.ServiceId = "AccountTransferApp";
                        })
                        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(IAccountGrain).Assembly).WithReferences())
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

            var atm = client.GetGrain<IATMGrain>(0);
            Guid from = Guid.NewGuid();
            Guid to = Guid.NewGuid();
            try
            {
                await atm.Transfer(from, to, 100);
                await atm.Transfer(from, to, 50);
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"发生异常：{exc.Message}");
                Console.ResetColor();
            }
            uint fromBalance = await client.GetGrain<IAccountGrain>(from).GetBalance();
            uint toBalance = await client.GetGrain<IAccountGrain>(to).GetBalance();
            Console.WriteLine($"\n\nWe transfered 100 credits from {from} to {to}.\n{from} balance: {fromBalance}\n{to} balance: {toBalance}\n\n");

        }
    }
}
