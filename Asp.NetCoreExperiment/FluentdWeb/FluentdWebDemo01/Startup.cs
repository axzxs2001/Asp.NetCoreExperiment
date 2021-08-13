using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Exceptionless;

namespace FluentdWebDemo01
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
            var elcfgs = new List<ExceptionlessCfg>();
            Configuration.GetSection("Exceptionlesses").Bind(elcfgs);
            var eldir = new Dictionary<string, ExceptionlessClient>();
            foreach (var elcfg in elcfgs)
            {
                eldir.Add(elcfg.AppName,new ExceptionlessClient(cfg=> { cfg.ApiKey = elcfg.ApiKey;cfg.ServerUrl = elcfg.ServerUrl; }));
            }
            services.AddSingleton(eldir);

         
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FluentdWebDemo01", Version = "v1" });
            });
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FluentdWebDemo01 v1"));
            }
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public class ExceptionlessCfg
    {
        public string AppName { get; set; }
        public string ApiKey { get; set; }
        public string ServerUrl { get; set; }
    }


}
