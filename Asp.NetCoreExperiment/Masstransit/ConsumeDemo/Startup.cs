using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GreenPipes;
using MassTransit;
using MassTransitEntity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;

namespace ConsumeDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMassTransit(x =>
            {
                x.AddConsumer<ConsumerClass1>();
                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    //test为虚拟host  vhost
                    //var host = cfg.Host("localhost","test", hc =>

                    //var host = cfg.Host(new Uri("rabbitmq://localhost/test"), hc =>
                    var host = cfg.Host(new Uri("rabbitmq://localhost"), hc =>
                      {

                          hc.Username("guest");
                          hc.Password("guest");
                      });
                    cfg.ReceiveEndpoint(host, "submit-order" + Program.Name, e =>
                      {
                          e.PrefetchCount = 16;
                          // e.UseMessageRetry(x => x.Interval(2, 100));
                          e.ConfigureConsumer<ConsumerClass1>(provider);

                          //绑定参数
                          //e.Bind("exchange-name", x =>
                          //{
                          //    x.Durable = false;                              
                          //    x.AutoDelete = true;
                          //    x.ExchangeType = "direct";
                          //    x.RoutingKey = "8675309";
                          //});
                      });
                   // cfg.UseDelayedExchangeMessageScheduler();
                    // cfg.UseMessageScheduler(new Uri("rabbitmq://localhost/quartz"));
                }));
            });
            services.AddSingleton<IHostedService, BusService>();
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
    public class ConsumerClass1 : IConsumer<Class1>
    {
        public async Task Consume(ConsumeContext<Class1> context)
        {
            try
            {
                if (DateTime.Now.Second % 2 == 0)
                {
                    throw new Exception("这是抛出来的一个异常");
                }
                else
                {
                    await Console.Out.WriteLineAsync($"订阅者  ConsumerEnterprise收到信息: \r\n{ Newtonsoft.Json.JsonConvert.SerializeObject(context.Message)} \r\n类型：{context.Message.GetType()}");
                }
            }
            catch (Exception e)
            {
                await Console.Out.WriteLineAsync($"订阅者 {e.Message}");
                //await context.Redeliver(TimeSpan.FromSeconds(5));
               // await context.Defer(TimeSpan.FromSeconds(50));
            }
            finally
            {
                await Console.Out.WriteLineAsync("---------------------------------------");
            }

        }
    }

    public class BusService : IHostedService
    {
        private readonly IBusControl _busControl;

        public BusService(IBusControl busControl)
        {
            _busControl = busControl;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _busControl.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _busControl.StopAsync(cancellationToken);
        }
    }
}
