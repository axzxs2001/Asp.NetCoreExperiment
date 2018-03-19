using MassTransit;
using MEDemo_Entity;
using System;

namespace MEDemo_Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Hierarchy message producer";

            var bus =   Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost"), hst =>
                {
                    hst.Username("guest");
                    hst.Password("guest");
                });
              
            });
            bus.Start();

            do
            {
                Console.WriteLine("Enter message (or quit to exit)");
                Console.Write("> ");
                string value = Console.ReadLine();

                if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                    break;
                bus.Publish(new MyEntity() {ID=1,  Age=10, Name="张三" });          
            }
            while (true);


            Console.WriteLine("Publish Hierarchy events.. Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }
}
