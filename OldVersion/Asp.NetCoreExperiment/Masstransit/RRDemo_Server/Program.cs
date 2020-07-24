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
            Console.Title = "应答方";
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
                {
                    hst.Username("guest");
                    hst.Password("guest");
                });
                cfg.ReceiveEndpoint(host, "reqresgsw", e =>
                {
                    e.Consumer<RequestConsumer>();
                });
            });
            bus.Start();     
            Console.ReadLine();
            bus.Stop();
        }
    }

    public class RequestConsumer : IConsumer<IRequestEntity>
    {
        public async Task Consume(ConsumeContext<IRequestEntity> context)
        {
            await Console.Out.WriteLineAsync($"收到请求id={context.Message.ID} name={context.Message.Name}");
            var response = new ResponseEntity
            {
                ID = 22,
                Name = $"李四",
                RequestID = context.Message.ID
            };
            Console.WriteLine($"应答ID={response.ID},Name={response.Name},RequestID={response.RequestID}");
            context.Respond(response);
        }
    }
}
