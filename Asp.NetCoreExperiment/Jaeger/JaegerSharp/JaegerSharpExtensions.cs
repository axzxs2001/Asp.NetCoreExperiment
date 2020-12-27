using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Util;
using System.Reflection;


namespace JaegerSharp
{
    public static class JaegerSharpExtensions
    {
        /// <summary>
        /// 注入Jaeger
        /// </summary>
        /// <param name="services">服务容器</param>
        /// <param name="host">Jaeger agent host</param>
        /// <param name="port">Jaeger agent port</param>
        /// <param name="maxPacketSize">Jaeger agent maxpacketsize</param>
        public static void AddJaegerSharp(this IServiceCollection services, string host, int port, int maxPacketSize)
        {     
            services.AddSingleton<ITracer>(serviceProvider =>
            {
                var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();              
                var reporter = new RemoteReporter.Builder()
                    .WithLoggerFactory(loggerFactory)
                    .WithSender(new UdpSender(string.IsNullOrEmpty(host) ? UdpSender.DefaultAgentUdpHost : host,
                                                    port <= 100 ? UdpSender.DefaultAgentUdpCompactPort : port,
                                                    maxPacketSize <= 0 ? 0 : maxPacketSize))
                          .Build();
                ITracer tracer = new Tracer.Builder(Assembly.GetEntryAssembly().GetName().Name)
                   .WithReporter(reporter)
                   .WithLoggerFactory(loggerFactory)
                   .WithSampler(new ConstSampler(true))
                   .Build();
                GlobalTracer.Register(tracer);
                return tracer;
            });
        }
    }
}
