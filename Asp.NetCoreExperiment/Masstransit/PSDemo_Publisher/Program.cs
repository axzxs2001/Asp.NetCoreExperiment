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
                Console.WriteLine("请出请按q,否则请按其他键！");
                string value = Console.ReadLine();
                if (value.ToLower() == "q")
                {
                    break;
                }

                bus.Publish(new PSDemo_Entity.Entity() { Name="张三", Time = DateTime.Now });
                bus.Publish(new PSDemo_Entity.ChildEntity() { Name = "李四", Time = DateTime.Now,Age=22 });
            }
            while (true);        

            bus.Stop();
        }
    }
}
