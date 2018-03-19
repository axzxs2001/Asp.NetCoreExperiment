using MassTransit;
using System;
using System.Threading.Tasks;

namespace Demo0002
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "客户端";
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
                {
                    hst.Username("guest");
                    hst.Password("guest");
                });
                
            });

            var uri = new Uri("rabbitmq://localhost/gsw");
            while (Console.ReadLine() != null)
            {
                Task.Run(() => SendCommand(bus, uri)).Wait();
               
            }

            Console.ReadLine();
        }
        private static async void SendCommand(IBusControl bus, Uri sendToUri)
        {
            var endPoint = await bus.GetSendEndpoint(sendToUri);
            var command = new Demo001.ABC()
            {
                Name = "张三",
                Time = DateTime.Now
            };

            await endPoint.Send(command);

            Console.WriteLine($"send command:id={command.Name},{command.Time}");
        }

    }
  
 
}
