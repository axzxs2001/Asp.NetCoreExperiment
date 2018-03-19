using MassTransit;
using System;

namespace PSDemo_Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
           var bus= Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
                {
                    hst.Username("guest");
                    hst.Password("guest");
                });               
            });
            do
            {
                Console.WriteLine("Enter message (or quit to exit)");
                Console.Write("> ");
                string value = Console.ReadLine();
                if ("quit".Equals(value, StringComparison.OrdinalIgnoreCase))
                    break;


                bus.Publish(new PSDemo_Entity.Entity() { Name="张三", Time = DateTime.Now });
                bus.Publish(new PSDemo_Entity.ChildEntity() { Name = "李四", Time = DateTime.Now,Age=22 });
            }
            while (true);

            Console.WriteLine("Publish Greeting events.. Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }
}
