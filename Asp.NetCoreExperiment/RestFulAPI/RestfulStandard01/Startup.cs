using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore;
using Microsoft.Extensions.PlatformAbstractions;

namespace RestfulStandard01
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("RestfulStandard01", new Info { Title = "API接口", Version = "v1", Contact = new Contact { Email = "", Name = "NetStars", Url = "" }, Description = "医API" });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "RestfulStandard01.xml");
                options.IncludeXmlComments(xmlPath);

                //var api = new ApiKeyScheme { In = "header", Description = "请输入带有Bearer的Token", Name = "Authorization", Type = "apiKey" };
                //options.AddSecurityDefinition("Bearer", api);
                //options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                //{ "Bearer", Enumerable.Empty<string>() },
            //});

            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc().UseSwagger(options =>
            {
                options.RouteTemplate = "{documentName}/swagger.json";
            })
                .UseSwaggerUI(options =>
                {
                    options.DocumentTitle = "RestfulStandard01.API";
                    options.SwaggerEndpoint("/RestfulStandard01/swagger.json", "RestfulStandard01");
                }); ;
        }
    }
}
