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
            Console.Title="订阅方";
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
                cfg.ReceiveEndpoint(host, "megsw1", e =>
                {
                    e.Consumer<YouEntityConsumer>();                
                });
            });

            bus.Start();        
            Console.ReadLine();
            bus.Stop();
        }
    }

    public class IEntityConsumer : IConsumer<IEntity>
    {
        public async Task Consume(ConsumeContext<IEntity> context)
        {           
            await Console.Out.WriteLineAsync($"IEntityConsumer 类型 {context.Message.GetType()} {context.Message.ID}");

        }
    }
    public class EntityConsumer : IConsumer<Entity>
    {
        public async Task Consume(ConsumeContext<Entity> context)
        {
            await Console.Out.WriteLineAsync($"EntityConsumer  类型 {context.Message.GetType()}  {context.Message.ID} {context.Message.Name} {context.Message.Time}");
        }
    }
    public class MyEntityConsumer : IConsumer<MyEntity>
    {
        public async Task Consume(ConsumeContext<MyEntity> context)
        {
            await Console.Out.WriteLineAsync($"MyEntityConsumer 类型 {context.Message.GetType()}  {context.Message.ID} {context.Message.Name} {context.Message.Time} {context.Message.Age}");
        }
    }

    

    public class YouEntityConsumer : IConsumer<YouEntity>
    {
        public async Task Consume(ConsumeContext<YouEntity> context)
        {
            await Console.Out.WriteLineAsync($"YouEntityConsumer 类型 {context.Message.GetType()}  {context.Message.ID} {context.Message.Name} {context.Message.Time} {context.Message.Age}");
        }
    }
}
