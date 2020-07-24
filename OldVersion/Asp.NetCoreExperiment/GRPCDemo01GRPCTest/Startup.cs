using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GRPCDemo01Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GRPCDemo01GRPCTest
{
    public class Startup
    {
  
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpcClient<Goodser.GoodserClient>(o =>
            {
                o.Address = new Uri("https://localhost:5001");
            });
            services.AddGrpc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<OrderService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
