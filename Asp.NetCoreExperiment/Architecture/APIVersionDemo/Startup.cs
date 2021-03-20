using APIVersionDemo.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIVersionDemo
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

            services.AddApiVersioning();

            //services.AddApiVersioning(opt =>
            //{
            //    //默认1.0
            //    opt.AssumeDefaultVersionWhenUnspecified = true;
            //    opt.DefaultApiVersion = new ApiVersion(1, 0);
            //    opt.ApiVersionReader = ApiVersionReader.Combine(
            //        new MediaTypeApiVersionReader("version"),
            //        new HeaderApiVersionReader("api-version")
            //        );
            //    opt.ReportApiVersions = true;
            //});

            //services.AddApiVersioning(opt =>
            //{
            //    //替换在ProductionController上的特性
            //    opt.Conventions.Controller<ProductController>()
            //    .HasApiVersion(2, 0)
            //    .HasDeprecatedApiVersion(1, 0)
            //    .Action(typeof(ProductController)
            //    .GetMethod(nameof(ProductController.QueryProductv2))!)
            //    .MapToApiVersion(2, 0);
            //});

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIVersionDemo", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIVersionDemo v1"));
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
