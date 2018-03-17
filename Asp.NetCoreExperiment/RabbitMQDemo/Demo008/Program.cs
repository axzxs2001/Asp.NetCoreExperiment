using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace Demo008
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                {
                    Console.WriteLine("1、kern.*  2、* .critical");
                    switch (Console.ReadLine())
                    {
                        case "1":

                            using (var channel = connection.CreateModel())
                            {
                                channel.ExchangeDeclare(exchange: "topic_logs",
                                                        type: "topic");

                                var routingKey = "kern.*";
                                var message = "你好kern.*";
                                var body = Encoding.UTF8.GetBytes(message);
                                channel.BasicPublish(exchange: "topic_logs",
                                                     routingKey: routingKey,
                                                     basicProperties: null,
                                                     body: body);
                                Console.WriteLine(" [x] Sent '{0}':'{1}'", routingKey, message);
                                break;
                            }

                        case "2":

                            using (var channel = connection.CreateModel())
                            {
                                channel.ExchangeDeclare(exchange: "topic_logs",
                                                        type: "topic");

                                var routingKey = "* .critical";
                                var message = "你好* .critical";
                                var body = Encoding.UTF8.GetBytes(message);
                                channel.BasicPublish(exchange: "topic_logs",
                                                     routingKey: routingKey,
                                                     basicProperties: null,
                                                     body: body);
                                Console.WriteLine(" [x] Sent '{0}':'{1}'", routingKey, message);
                                break;
                            }
                    }
                }
            }
        }
    }
}
