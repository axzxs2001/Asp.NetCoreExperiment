using MassTransit;
using MEDemo_Entity;
using System;
using System.Threading.Tasks;

namespace MEDemo_ConsumerA
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hierarchy message subscriber");
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
                {
                    hst.Username("guest");
                    hst.Password("guest");
                });
                cfg.ReceiveEndpoint(host, "megsw", e =>
                {
                    e.Consumer<IEntityConsumer>();
                    e.Consumer<EntityConsumer>();
                    e.Consumer<MyEntityConsumer>();
                });
            });

            bus.Start();
            Console.WriteLine("Listening for Hierarchy events.. Press enter to exit");
            Console.ReadLine();
            bus.Stop();
        }
    }

    public class IEntityConsumer : IConsumer<IEntity>
    {
        public async Task Consume(ConsumeContext<IEntity> context)
        {
           
            await Console.Out.WriteLineAsync($"consumer type is {context.Message.GetType()} {context.Message.ID}");

        }
    }
    public class EntityConsumer : IConsumer<Entity>
    {
        public async Task Consume(ConsumeContext<Entity> context)
        {
            await Console.Out.WriteLineAsync($"consumer  type is {context.Message.GetType()}  {context.Message.ID} {context.Message.Name}");
        }
    }
    public class MyEntityConsumer : IConsumer<MyEntity>
    {
        public async Task Consume(ConsumeContext<MyEntity> context)
        {
            await Console.Out.WriteLineAsync($"consumer type is {context.Message.GetType()}  {context.Message.ID} {context.Message.Name}  {context.Message.Age}");
        }
    }
}
