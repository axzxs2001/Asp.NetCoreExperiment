using MassTransit;
using RRDemo_Entity;
using System;
using System.Threading.Tasks;

namespace RRDemo_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Request Service :");
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
                {
                    hst.Username("guest");
                    hst.Password("guest");
                });
                cfg.ReceiveEndpoint(host, "rrgsw", e =>
                {
                    e.Consumer<RequestConsumer>();
                });
            });

            

            bus.Start();

            Console.WriteLine("Listening for Request.. Press enter to exit");
            Console.ReadLine();

            bus.Stop();
        }
    }

    public class RequestConsumer : IConsumer<IRequestEntity>
    {
        public async Task Consume(ConsumeContext<IRequestEntity> context)
        {
            await Console.Out.WriteLineAsync($"recieved request id:{context.Message.ID} name={context.Message.Name}");
            context.Respond(new ResponseEntity
            {
                ID =22,
                Name=$"李四",
                RequestID=context.Message.ID
            });
        }
    }
}
