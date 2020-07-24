using System;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace PublishDemo
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
            services.AddMassTransit(config =>
            {
                config.AddBus(provider =>
                {
                    return Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        var host = cfg.Host(new Uri("rabbitmq://localhost"), hc =>
                        {  
                            hc.Username("guest");
                            hc.Password("guest");
                        });
                        //cfg.UseMessageScheduler(new Uri("rabbitmq://localhost/quartz"));
                    });
                }); 
            });
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
