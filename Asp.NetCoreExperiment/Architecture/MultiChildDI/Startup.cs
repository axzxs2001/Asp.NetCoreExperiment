using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MultiChildDI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MultiChildDI
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
            //services.AddSingleton<IDemoService, DemoService01>();
            //services.AddSingleton<IDemoService, DemoService02>();
            //services.AddSingleton<IDemoService, DemoService03>();

            services.AddScoped<IDemoService, DemoService01>();
            services.AddScoped<IDemoService, DemoService02>();
            services.AddScoped<IDemoService, DemoService03>();

            //services.AddScoped<DemoService01>();
            //services.AddScoped<DemoService02>();
            //services.AddScoped<DemoService03>();
            //services.AddScoped<IDemoServiceFactory, DemoServiceFactory>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MultiChildDI", Version = "v1" });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MultiChildDI v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public interface IDemoServiceFactory
    {
        IDemoService Create(string name);
    }
    public class DemoServiceFactory : IDemoServiceFactory
    {
        private readonly IServiceProvider _provider;
        public DemoServiceFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public IDemoService Create(string name)
        {
            var type = Assembly.GetAssembly(typeof(DemoServiceFactory)).GetType($"MultiChildDI.Services.DemoService{name}");
            var instance = _provider.GetService(type);
            return instance as IDemoService;
        }
    }
}
