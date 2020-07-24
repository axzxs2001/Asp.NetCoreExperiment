using System;
using System.Threading.Tasks;
using MassTransit;


namespace Demo001
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "服务端";
            var bus= Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
                {
                    hst.Username("guest");
                    hst.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "gsw", e =>
                {
                    e.Consumer<ConsumerABC>();
                    e.Consumer<ConsumerABC1>();

                });
            });
          
            bus.Start();
            Console.WriteLine("按任意键退出！");
            Console.ReadLine();
            bus.Stop();
        }
    }

    public class ConsumerABC : IConsumer<ABC>
    {
        public async Task Consume(ConsumeContext<ABC> context)
        {
            await Console.Out.WriteLineAsync($"收到信息: {context.Message.Name},{context.Message.Birthday},{context.Message.Message}");
        }
    
    }
    public class ConsumerABC1 : IConsumer<ABC>
    {
        public async Task Consume(ConsumeContext<ABC> context)
        {
            await Console.Out.WriteLineAsync($"收到信息1: {context.Message.Name},{context.Message.Birthday},{context.Message.Message}");
        }

    }
    public class ABC
    {
        public DateTime Birthday { get; set; }
        public string Name { get; set; }

        public string Message { get; set; }
    }

}
