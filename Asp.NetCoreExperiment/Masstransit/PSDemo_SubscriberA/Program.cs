using MassTransit;
using System;
using System.Threading.Tasks;

namespace PSDemo_SubscriberA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SubscriberA");

            var bus= Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
                {
                    hst.Username("guest");
                    hst.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "gswPSA", e =>
                {
                    e.Consumer<GreetingEventConsumerA>();
                    e.Consumer<GreetingEventConsumerB>();
                });
            });        

            bus.Start();
            Console.WriteLine("Listening for Greeting events.. Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }
    public class GreetingEventConsumerA : IConsumer<PSDemo_Entity.Entity>
    {
        public async Task Consume(ConsumeContext<PSDemo_Entity.Entity> context)
        {
            await Console.Out.WriteLineAsync($"receive PSDemo_SubscriberA GreetingEventConsumerA: {context.Message.Name}  {context.Message.Time}");
        }
    }
    public class GreetingEventConsumerB : IConsumer<PSDemo_Entity.ChildEntity>
    {
        public async Task Consume(ConsumeContext<PSDemo_Entity.ChildEntity> context)
        {
            await Console.Out.WriteLineAsync($"receive PSDemo_SubscriberA GreetingEventConsumerB: {context.Message.Name}  {context.Message.Time}");
        }
    }
}
