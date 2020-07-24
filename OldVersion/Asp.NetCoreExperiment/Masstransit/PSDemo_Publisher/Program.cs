using MassTransit;
using MassTransit.Logging;
using MassTransit.NLogIntegration;
using System;

namespace PSDemo_Publisher
{
    class Program
    {
        //启动Docker 中的RabbitMQ
        //docker run -d --name rabbitmq --publish 5671:5671  --publish 5672:5672 --publish 4369:4369 --publish 25672:25672 --publish 15671:15671 --publish 15672:15672 rabbitmq:management
        //docker rabbitmq启动UI http://localhost:15672  用户名：guest   密码：guest
        static void Main(string[] args)
        {
           var bus= Bus.Factory.CreateUsingRabbitMq(cfg =>
           {
               cfg.UseNLog();
               var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
                {
                    hst.Username("gsw");
                    hst.Password("gsw790622");
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

                var log = Logger.Get("log");
                log.Info("publish gsw");
                

                bus.Publish(new PSDemo_Entity.ChildEntity() { Name = "李四", Time = DateTime.Now,Age=22 });
            }
            while (true);        

            bus.Stop();
        }
    }
}
