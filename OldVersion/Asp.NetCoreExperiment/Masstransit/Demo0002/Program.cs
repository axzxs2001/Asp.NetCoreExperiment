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
            var mes = Console.ReadLine();
            while (null!= mes)
            {
                Task.Run(() => SendCommand(bus, uri,mes)).Wait();   
                mes = Console.ReadLine();
                
            }
            Console.ReadLine();
        }
        private static async void SendCommand(IBusControl bus, Uri sendToUri,string mes)
        {
            var endPoint = await bus.GetSendEndpoint(sendToUri);
            var command = new Demo001.ABC()
            {
                Name = "张三",
                Birthday = DateTime.Now,
                Message = mes
            };

            await endPoint.Send(command);

            Console.WriteLine($"发送的实体 Name={command.Name},Birthday={command.Birthday},Message={command.Message}");
        }

    }
  
 
}
