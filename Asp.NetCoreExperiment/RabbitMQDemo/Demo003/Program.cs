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
                channel.QueueDeclare(queue: "task_queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var args = new string[] { "First message.", "Second message..", "Third message...", "Fourth message....", "Fifth message....." };
                foreach (var arg in args)
                {
                    var message = GetMessage(arg);
                    var body = Encoding.UTF8.GetBytes(message);

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    //channel.BasicAcks += Channel_BasicAcks;
                    //channel.BasicNacks += Channel_BasicNacks;
                    //channel.BasicRecoverOk += Channel_BasicRecoverOk;
                    //channel.BasicReturn += Channel_BasicReturn;
                   
                    channel.BasicPublish(
                        exchange: "", 
                        routingKey: "task_queue", 
                        basicProperties: properties, 
                        body: body);

                    Console.WriteLine(" [x] Sent {0}", message);
                }
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private static void Channel_BasicReturn(object sender, RabbitMQ.Client.Events.BasicReturnEventArgs e)
        {
            Console.WriteLine(e.ReplyText);
        }

        private static void Channel_BasicRecoverOk(object sender, EventArgs e)
        {
            Console.WriteLine("Channel_BasicRecoverOk");
        }

        private static void Channel_BasicNacks(object sender, RabbitMQ.Client.Events.BasicNackEventArgs e)
        {
            Console.WriteLine(e.DeliveryTag);
        }

        private static void Channel_BasicAcks(object sender, RabbitMQ.Client.Events.BasicAckEventArgs e)
        {
            Console.WriteLine(e.DeliveryTag);
        }

      

        private static string GetMessage(string args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello World!");
        }
    }
}
