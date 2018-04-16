using MassTransit;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ObserverSubscriber
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "订阅方";
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
                {
                    hst.Username("guest");
                    hst.Password("guest");
                });
                cfg.ReceiveEndpoint(host, "ObserverTest", e =>
                {
                    e.Consumer<EntityConsumer>();
                });
            });
            var observer = new ReceiveObserver();
            var handle = bus.ConnectReceiveObserver(observer);
            bus.Start();
            Console.ReadLine();
            bus.Stop();
        }
    }
    public class ReceiveObserver : IReceiveObserver
    {
        public Task PreReceive(ReceiveContext context)
        {
    
            Console.WriteLine("------------------PreReceive--------------------");
            Console.WriteLine(Encoding.Default.GetString(context.GetBody()));
            Console.WriteLine("--------------------------------------");
            return Task.CompletedTask;
        }

        public Task PostReceive(ReceiveContext context)
        {
        
            Console.WriteLine("-----------------PostReceive---------------------");
            Console.WriteLine(Encoding.Default.GetString(context.GetBody()));
            Console.WriteLine("--------------------------------------");
            return Task.CompletedTask;
        }

        public Task PostConsume<T>(ConsumeContext<T> context, TimeSpan duration, string consumerType)
            where T : class
        {
       
            Console.WriteLine("------------------PostConsume--------------------");
            Console.WriteLine($"ID={ (context.Message as Entity).ID},Name={(context.Message as Entity).Name},Time={(context.Message as Entity).Time}");
            Console.WriteLine("--------------------------------------");
            return Task.CompletedTask;
        }

        public Task ConsumeFault<T>(ConsumeContext<T> context, TimeSpan elapsed, string consumerType, Exception exception) where T : class
        {
         
            Console.WriteLine("-----------------ConsumeFault---------------------");
            Console.WriteLine($"ID={ (context.Message as Entity).ID},Name={(context.Message as Entity).Name},Time={(context.Message as Entity).Time}");
            Console.WriteLine("--------------------------------------");
            return Task.CompletedTask;
        }

        public Task ReceiveFault(ReceiveContext context, Exception exception)
        {            
            Console.WriteLine("----------------ReceiveFault----------------------");
            Console.WriteLine(Encoding.Default.GetString(context.GetBody()));
            Console.WriteLine("--------------------------------------");
            return Task.CompletedTask;
        }
    }


    public class EntityConsumer : IConsumer<Entity>
    {
        public async Task Consume(ConsumeContext<Entity> context)
        {
            await Console.Out.WriteLineAsync($"IEntityConsumer 类型 {context.Message.GetType()} {context.Message.ID} {context.Message.Age} {context.Message.Name} {context.Message.Time}");

        }
    }

    public class Entity
    {
        public int ID { get; set; }

        public int Age { get; set; }
        public string Name { get; set; }
        public DateTime Time { get; set; }

    }

}
