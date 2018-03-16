using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;

namespace Demo006
{
    class Program
    {
        #region fanout
        //public static void Main()
        //{

        //    var factory = new ConnectionFactory() { HostName = "localhost" };
        //    using (var connection = factory.CreateConnection())
        //    using (var channel = connection.CreateModel())
        //    {
        //        channel.ExchangeDeclare(exchange: "logs", type: "fanout");

        //        var queueName = channel.QueueDeclare().QueueName;
        //        channel.QueueBind(queue: queueName,
        //                          exchange: "logs",
        //                          routingKey: "");

        //        Console.WriteLine(" [*] Waiting for logs.");

        //        var consumer = new EventingBasicConsumer(channel);
        //        consumer.Received += (model, ea) =>
        //        {
        //            var body = ea.Body;
        //            var message = Encoding.UTF8.GetString(body);
        //            Console.WriteLine(" [x] {0}", message);
        //        };
        //        channel.BasicConsume(queue: queueName,
        //                             autoAck: true,
        //                             consumer: consumer);

        //        Console.WriteLine(" Press [enter] to exit.");
        //        Console.ReadLine();
        //    }
        //}
        #endregion

        #region direct
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1、info  2、error  3、warning");
                switch(Console.ReadLine())
                {
                    case "1":
                        args = new string[] { "info", "Run. Run. 这是一个info" };
                        break;
                    case "2":
                        args = new string[] { "error", "Run. Run. 这是一个error" };
                        break;
                    case "3":
                        args = new string[] { "warning", "Run. Run. 这是一个warning" };
                        break;
                }
               
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: "direct_logs",
                                            type: "direct");

                    var severity = (args.Length > 0) ? args[0] : "info";
                    var message = (args.Length > 1)
        ? string.Join(" ", args.Skip(1).ToArray())
                                  : "Hello World!";
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "direct_logs",
                                         routingKey: severity,
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent '{0}':'{1}'", severity, message);
                }

                Console.WriteLine(" Press [enter] to exit.");
            }


    
        }
        #endregion
    }
}
