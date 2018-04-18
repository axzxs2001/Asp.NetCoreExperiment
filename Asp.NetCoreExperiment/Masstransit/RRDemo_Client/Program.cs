using GreenPipes;
using MassTransit;
using RRDemo_Entity;
using System;
using System.Threading.Tasks;

namespace RRDemo_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "请求方";

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
                {
                    hst.Username("guest");
                    hst.Password("guest");
                });
                //重试
                cfg.UseRetry(ret =>
                {
                    ret.Interval(3, 10);
                });
                //限流
                cfg.UseRateLimit(1000, TimeSpan.FromSeconds(100));
                //熔断
                cfg.UseCircuitBreaker(cb =>
                {
                    cb.TrackingPeriod = TimeSpan.FromSeconds(60);
                    cb.TripThreshold = 15;
                    cb.ActiveThreshold = 10;
                    cb.ResetInterval = TimeSpan.FromMinutes(5);

                });
            });
            bus.Start();

            var serviceAddress = new Uri($"rabbitmq://localhost/reqresgsw");
            var client = bus.CreateRequestClient<IRequestEntity, IResponseEntity>(serviceAddress, TimeSpan.FromHours(10));

            while (true)
            {
                Console.WriteLine("请出请按q,否则请按其他键！");
                string value = Console.ReadLine();
                if (value.ToLower() == "q")
                {
                    break;
                }

                Task.Run(async () =>
                {
                    var request = new RequestEntity() { ID = 1, Name = "张三" };
                    var response = await client.Request(request);

                    Console.WriteLine($"请求ID={request.ID},Name={request.Name}");
                    Console.WriteLine($"应签ID={response.ID},Name={response.Name},RequestID={response.RequestID}");
                }).Wait();
            }

        }
    }
}
