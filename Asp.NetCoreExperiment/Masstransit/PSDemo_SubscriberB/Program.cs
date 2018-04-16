using MassTransit;
using System;
using System.Threading.Tasks;

namespace PSDemo_SubscriberB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title="订阅者B";

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
                {
                    hst.Username("guest");
                    hst.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "gswPSB", e =>
                {
                    e.Consumer<ConsumerA>();
                });
            });

            bus.Start();     
            Console.ReadLine();
            bus.Stop();
        }
    }
    public class ConsumerA : IConsumer<PSDemo_Entity.Entity>
    {
        public async Task Consume(ConsumeContext<PSDemo_Entity.Entity> context)
        {
            await Console.Out.WriteLineAsync($"订阅者B  ConsumerA收到信息:  {context.Message.Name}  {context.Message.Time}  类型：{context.Message.GetType()}");
        }
    }
}
