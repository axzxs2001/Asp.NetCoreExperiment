using MassTransit;
using ObserverSubscriber;
using System;
using System.Threading.Tasks;

namespace ObserverPublish
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "发布方";

            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost"), hst =>
                {
                    hst.Username("guest");
                    hst.Password("guest");
                });

            });
            var observer = new SendObserver();
            var handle = bus.ConnectSendObserver(observer);

            var observer1 = new PublishObserver();
            var handle1 = bus.ConnectPublishObserver(observer1);
            bus.Start();
            do
            {
                Console.WriteLine("请出请按q,否则请按其他键！");

                string value = Console.ReadLine();

                if (value.ToLower() == "q")
                {
                    break;
                }

                bus.Publish(new Entity() { ID = 1, Age = 10, Name = "张三", Time = DateTime.Now });
            }
            while (true);

            bus.Stop();
        }
    }

    public class PublishObserver : IPublishObserver
    {
        public Task PrePublish<T>(PublishContext<T> context)
            where T : class
        {
            Console.WriteLine("------------------PrePublish--------------------");
            Console.WriteLine($"ID={ (context.Message as Entity).ID},Name={(context.Message as Entity).Name},Time={(context.Message as Entity).Time}");
            Console.WriteLine("--------------------------------------");
            return Task.CompletedTask;
        }

        public Task PostPublish<T>(PublishContext<T> context)
            where T : class
        {
            Console.WriteLine("------------------PostPublish--------------------");
            Console.WriteLine($"ID={ (context.Message as Entity).ID},Name={(context.Message as Entity).Name},Time={(context.Message as Entity).Time}");
            Console.WriteLine("--------------------------------------");
            return Task.CompletedTask;
        }

        public Task PublishFault<T>(PublishContext<T> context, Exception exception)
            where T : class
        {
            Console.WriteLine("------------------PublishFault--------------------");
            Console.WriteLine($"ID={ (context.Message as Entity).ID},Name={(context.Message as Entity).Name},Time={(context.Message as Entity).Time}");
            Console.WriteLine("--------------------------------------");
            return Task.CompletedTask;
        }
    }

    public class SendObserver : ISendObserver
    {
        public Task PreSend<T>(SendContext<T> context)
            where T : class
        {
            Console.WriteLine("------------------PreSend--------------------");
            Console.WriteLine($"ID={ (context.Message as Entity).ID},Name={(context.Message as Entity).Name},Time={(context.Message as Entity).Time}");
            Console.WriteLine("--------------------------------------");
            return Task.CompletedTask;
        }

        public Task PostSend<T>(SendContext<T> context)
            where T : class
        {
            Console.WriteLine("------------------PostSend--------------------");
            Console.WriteLine($"ID={ (context.Message as Entity).ID},Name={(context.Message as Entity).Name},Time={(context.Message as Entity).Time}");
            Console.WriteLine("--------------------------------------");
            return Task.CompletedTask;
        }

        public Task SendFault<T>(SendContext<T> context, Exception exception)
            where T : class
        {
            Console.WriteLine("------------------SendFault--------------------");
            Console.WriteLine($"ID={ (context.Message as Entity).ID},Name={(context.Message as Entity).Name},Time={(context.Message as Entity).Time}");
            Console.WriteLine("--------------------------------------");
            return Task.CompletedTask;
        }
    }
}
