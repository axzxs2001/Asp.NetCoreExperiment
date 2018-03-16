using RabbitMQ.Client;
using System;
using System.Text;

namespace Demo003
{
    class Program
    {
        public static void Main()
        {
            Console.Title = "Client";
            Console.WriteLine("按任意键开始！");
            Console.ReadKey();

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "task_queue_persistent",
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var args = new string[] { "5 message........", "1 message.", "2 message..", "3 message...", "4 message...."};
                foreach (var arg in args)
                {
                    var message = GetMessage(arg);
                    var body = Encoding.UTF8.GetBytes(message);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                   
                    channel.BasicPublish(
                        exchange: "", 
                        routingKey: "task_queue_persistent", 
                        basicProperties: properties, 
                        body: body);

                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }      
      

        private static string GetMessage(string args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}
