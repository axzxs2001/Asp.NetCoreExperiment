using MassTransit;
using MEDemo_Entity;
using System;

namespace MEDemo_Producer
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
            bus.Start();
            do
            {
                Console.WriteLine("请出请按q,否则请按其他键！");
              
                string value = Console.ReadLine();

                if (value.ToLower() == "q")
                {
                    break;
                }

                bus.Publish(new Entity() { ID = 1, Name = "张三", Time = DateTime.Now });
            }
            while (true);       
       
            bus.Stop();
        }
    }
}
