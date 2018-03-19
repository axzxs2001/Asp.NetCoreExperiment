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

                });
            });
          
            bus.Start();
            Console.WriteLine("Listening for Greeting commands.. Press enter to exit");
            Console.ReadLine();
            bus.Stop();
        }
    }

    public class ConsumerABC : IConsumer<ABC>
    {
        public async Task Consume(ConsumeContext<ABC> context)
        {
            await Console.Out.WriteLineAsync($"receive greeting commmandB: {context.Message.Name},{context.Message.Time}");
        }
    
    }
    public class ABC
    {
        public DateTime Time { get; set; }
        public string Name { get; set; }
    }

}
