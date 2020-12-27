using JaegerSharp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

namespace WebAPI01
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
            //命名客户端
            services.AddHttpClient("WebAPI02", client =>
            {
                client.BaseAddress = new Uri(Configuration.GetSection("DownStreamUrl").Value);
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI01", Version = "v1" });
            });
            //添加OpenTracing
            services.AddOpenTracing();
            //注入Jaeger
            if (Convert.ToBoolean(Configuration.GetSection("OpenTracing:Enable")?.Value))
            {
                var agentHost = Configuration.GetSection("OpenTracing:Agent").GetValue<string>("Host");
                var agentPort = Configuration.GetSection("OpenTracing:Agent").GetValue<int>("Port");
                var agentMaxPacketSize = Configuration.GetSection("OpenTracing:Agent").GetValue<int>("MaxPacketSize");
                services.AddJaegerSharp(agentHost, agentPort, agentMaxPacketSize);
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI01 v1"));
            }

            app.UseHttpsRedirection();
            if (Convert.ToBoolean(Configuration.GetSection("OpenTracing:Enable")?.Value))
            {
                app.UseJaegerSharp();
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
