using MassTransit;
using System;
using System.Threading.Tasks;

namespace PSDemo_SubscriberA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title="订阅者A";

            var bus= Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
                {
                    hst.Username("guest");
                    hst.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "gswPSA", e =>
                {
                    e.Consumer<ConsumerA>();
                    e.Consumer<ConsumerB>();
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
            await Console.Out.WriteLineAsync($"订阅者A  ConsumerA收到信息: {context.Message.Name}  {context.Message.Time} 类型：{context.Message.GetType()}");
        }
    }
    public class ConsumerB : IConsumer<PSDemo_Entity.ChildEntity>
    {
        public async Task Consume(ConsumeContext<PSDemo_Entity.ChildEntity> context)
        {
            await Console.Out.WriteLineAsync($"订阅者A  ConsumerB收到信息: {context.Message.Name}  {context.Message.Time} 类型：{context.Message.GetType()}");
        }
    }
}
