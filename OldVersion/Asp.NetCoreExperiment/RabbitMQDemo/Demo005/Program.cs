using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Demo005
{
    class Program
    {
        #region fanout
        //public static void Main(string[] args)
        //{
        //    var factory = new ConnectionFactory() { HostName = "localhost" };
        //    using (var connection = factory.CreateConnection())
        //    using (var channel = connection.CreateModel())
        //    {
        //        channel.ExchangeDeclare(exchange: "logs", type: "fanout");

        //        var message = GetMessage(args);
        //        var body = Encoding.UTF8.GetBytes(message);
        //        channel.BasicPublish(exchange: "logs",
        //                             routingKey: "",
        //                             basicProperties: null,
        //                             body: body);
        //        Console.WriteLine(" [x] Sent {0}", message);
        //    }

        //    Console.WriteLine(" Press [enter] to exit.");
        //    Console.ReadLine();
        //}

        //private static string GetMessage(string[] args)
        //{
        //    return ((args.Length > 0)
        //           ? string.Join(" ", args)
        //           : "info: Hello World!");
        //}
        #endregion

        public static void Main(string[] args)
        {
            Console.Title = "消费端：warning error";
            args = new string[] {"info","warning", "error" };
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "direct_logs",
                                        type: "direct");
                var queueName = channel.QueueDeclare().QueueName;

                if (args.Length < 1)
                {
                    Console.Error.WriteLine("Usage: {0} [info] [warning] [error]",
                                            Environment.GetCommandLineArgs()[0]);
                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                    Environment.ExitCode = 1;
                    return;
                }

                foreach (var severity in args)
                {
                    channel.QueueBind(queue: queueName,
                                      exchange: "direct_logs",
                                      routingKey: severity);
                }

                Console.WriteLine(" [*] Waiting for messages.");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;
                    Console.WriteLine(" 控制台输出 Received '{0}':'{1}'",
                                      routingKey, message);
                };
                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}
