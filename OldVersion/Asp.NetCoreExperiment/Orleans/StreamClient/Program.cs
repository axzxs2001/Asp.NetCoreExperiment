

using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Providers.Streams.Common;
using Orleans.Runtime;
using Orleans.Serialization;
using StreamLib;
using System;
using System.Threading.Tasks;
using System.Linq;
namespace StreamClient
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
            var connectionString = "";
            attempt = 0;
            IClusterClient client = new ClientBuilder()
                 .UseLocalhostClustering()
                //.UseAzureStorageClustering(options => options.ConnectionString = connectionString)
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "TestApp";
                })
                //.Configure<SerializationProviderOptions>(opt =>
                //{
                //    opt.SerializationProviders.Add(typeof(MySerializer).GetTypeInfo());
                //})
                .ConfigureLogging(logging => logging.AddConsole())
                //简单通知
                .AddSimpleMessageStreamProvider("SMSProvider")
                //RabbitMq实现队列订阅通知
                //.AddRabbitMqStream("SMSProvider", configurator =>
                //{
                //    configurator.ConfigureRabbitMq(host: "localhost", port: 5672, virtualHost: "/",
                //                                   user: "guest", password: "guest", queueName: "SMSProvider");
                //})
                //AzureQueue实现通知
                //.AddAzureQueueStreams<AzureQueueDataAdapterV2>("SMSProvider", b => b.Configure(opt =>
                //{
                //    opt.ConnectionString = connectionString;
                //    opt.QueueNames = new List<string>() { "orleantest" };
                //}))
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
            Console.WriteLine("输入客户端标识");
            var grain = client.GetGrain<IReceiver>(Console.ReadLine());

            var streamProvider = client.GetStreamProvider("SMSProvider");
            var guid =await grain.GetGuid();
            Console.WriteLine(guid);
            var stream = streamProvider.GetStream<Message>(guid, "StreamLib");
            
            await stream.SubscribeAsync(new AsyncObserver());
            // await stream.SubscribeAsync<string>(async (data, token) => Console.WriteLine(data));
            while (true)
            {
                Console.WriteLine("输入发送的值：");
                var content = Console.ReadLine();
                await grain.Method1(new Message { Content = content });
            }

        }
    }


    public class MySerializer : IExternalSerializer
    {
        public object DeepCopy(object source, ICopyContext context)
        {
            var fooCopy = SerializationManager.DeepCopyInner(source, context);
            return fooCopy;
        }

        public object Deserialize(Type expectedType, IDeserializationContext context)
        {
            //if (expectedType.Name == nameof(EventSequenceTokenV2))
            if (expectedType.Name == nameof(EventSequenceToken))
            {
                var n = Convert.ToInt32(context.DeserializeInner(expectedType));
                Console.WriteLine($"--------------n={n}-------------------");
                var num = Convert.ToInt64(context.GetSerializationManager().Deserialize(context.StreamReader));
                Console.WriteLine($"--------------num={num}-------------------");
                // return new EventSequenceTokenV2(num);
                return new EventSequenceToken(num, n);
            }
            else
            {
                return context.DeserializeInner(expectedType);
            }
        }

        public bool IsSupportedType(Type itemType)
        {
            //if (itemType.Name == nameof(EventSequenceTokenV2))
            if (itemType.Name == nameof(EventSequenceToken))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Serialize(object item, ISerializationContext context, Type expectedType)
        {
            context.SerializeInner(item, expectedType);
        }
    }

}
