using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRDemo
{
    public static class Program
    {
        public static Task Main(string[] args)
        {
            var writer = Console.Out;

            IServiceCollection services = new ServiceCollection();
            services.AddScoped<SingleInstanceFactory>(p => p.GetRequiredService);
            services.AddScoped<MultiInstanceFactory>(p => p.GetRequiredServices);

            services.AddSingleton<TextWriter>(writer);

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IMediator), typeof(QingQiu))
                .AddClasses()
                .AsImplementedInterfaces());

            var provider = services.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();
            return Run(mediator, writer);
        }
        static async Task Run(IMediator mediator, TextWriter writer)
        {
            writer.WriteLine("请求：QingQiu");
            var yindDa = await mediator.Send(new QingQiu { Message = "QingQiu" });
            writer.WriteLine("应答: " + yindDa.Message);
            writer.WriteLine("------------------------------");
            await mediator.Publish(new Pinged());
            Console.ReadLine();
        }

        private static IEnumerable<object> GetRequiredServices(this IServiceProvider provider, Type serviceType)
        {
            return (IEnumerable<object>)provider.GetRequiredService(typeof(IEnumerable<>).MakeGenericType(serviceType));
        }
    }


    public class QingQiu : IRequest<YindDa>
    {
        public string Message
        { get; set; }
    }

    public class YindDa
    {
        public string Message { get; set; }

    }
    public class PingHandler : IRequestHandler<QingQiu, YindDa>
    {
        private readonly TextWriter _writer;

        public PingHandler(TextWriter writer)
        {
            _writer = writer;
        }

        public async Task<YindDa> Handle(QingQiu request, CancellationToken cancellationToken)
        {
            await _writer.WriteLineAsync($"--- 这里收到请求： {request.Message}");
            return new YindDa { Message = request.Message + " YindDa" };
        }
    }
    


    public class PingedHandler : INotificationHandler<Pinged>
    {
        private readonly TextWriter _writer;

        public PingedHandler(TextWriter writer)
        {
            _writer = writer;
        }

        public Task Handle(Pinged notification, CancellationToken cancellationToken)
        {
            return _writer.WriteLineAsync("INotification pinged async.");
        }
    }
    public class Pinged : INotification
    {

    }
}
