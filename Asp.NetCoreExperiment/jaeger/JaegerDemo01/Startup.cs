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
using OpenTracing.Util;

namespace JaegerDemo01
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static ITracer tracer;

        public void ConfigureServices(IServiceCollection services)
        {
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

            // ×¢ÈëTracer
            services.AddSingleton(tracer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
