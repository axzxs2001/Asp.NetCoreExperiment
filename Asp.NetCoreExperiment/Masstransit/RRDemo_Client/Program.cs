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
            Console.WriteLine("Press 'Enter' to send a message.To exit, Ctrl + C");

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
                    
                });
            });
            bus.Start();

            var serviceAddress = new Uri($"rabbitmq://localhost/rrgsw");
            var client = bus.CreateRequestClient<IRequestEntity, IResponseEntity>(serviceAddress, TimeSpan.FromSeconds(10));

            for (; ; )
            {
                Console.Write("Enter customer id (quit exits): ");
                string customerId = Console.ReadLine();
                if (customerId == "quit")
                    break;

                // this is run as a Task to avoid weird console application issues
                Task.Run(async () =>
                {
                    var request = new RequestEntity() { ID = 1, Name = "张三" };
                    var response = await client.Request(request);

                    Console.WriteLine($"请求ID={request.ID},Name={request.Name}  应签ID={response.ID},Name={response.Name},RequestID={response.RequestID}");
                }).Wait();
            }

        }
    }
}
