using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Jaeger;
using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTracing;
using OpenTracing.Propagation;
using OpenTracing.Util;

namespace JaegerDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static ITracer tracer;
        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {       //命名客户端
            services.AddHttpClient("nameclient5000", c =>
            {
                c.BaseAddress = new Uri("http://localhost:5001/");
                var dir = CreateInjectTracingSpanHeader();
                foreach (var item in dir)
                {
                    c.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            });
            services.AddControllers();
            // OpenTracing Tracer
            IReporter reporter = new NoopReporter();
            if (Configuration.GetSection("OpenTracing:Agent").Exists())
            {
                string agentHost = Configuration.GetSection("OpenTracing:Agent").GetValue<string>("Host");
                int agentPort = Configuration.GetSection("OpenTracing:Agent").GetValue<int>("Port");
                int agentMaxPacketSize = Configuration.GetSection("OpenTracing:Agent").GetValue<int>("MaxPacketSize");
                reporter = new RemoteReporter.Builder()
                    .WithSender(new UdpSender(string.IsNullOrEmpty(agentHost) ? UdpSender.DefaultAgentUdpHost : agentHost,
                                              agentPort <= 0 ? UdpSender.DefaultAgentUdpCompactPort : agentPort,
                                              agentMaxPacketSize <= 0 ? 0 : agentMaxPacketSize))
                    .Build();
            }
            tracer = new Tracer.Builder(Assembly.GetEntryAssembly().GetName().Name)  // service name
               .WithReporter(reporter)
               .WithSampler(new ConstSampler(true))  // always send the span
               .Build();
            GlobalTracer.Register(tracer);

            // 注入Tracer
            services.AddSingleton(tracer);
        }
        Dictionary<string, string> CreateInjectTracingSpanHeader()
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            TextMapInjectAdapter carrier = new TextMapInjectAdapter(headers);  // get the calling headers
            GlobalTracer.Instance.Inject(GlobalTracer.Instance.ActiveSpan.Context, BuiltinFormats.HttpHeaders, carrier);
            return headers;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
