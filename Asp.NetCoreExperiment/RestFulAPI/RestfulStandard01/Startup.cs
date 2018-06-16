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
using RestfulStandard01.Model;
using System.Text;

namespace RestfulStandard01
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            env.EnvironmentName = EnvironmentName.Production;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.Use(async (context, next) =>
                {
                    try
                    {
                        await next.Invoke();
                    }
                    catch (Exception exc)
                    {
                        var logger = app.ApplicationServices.GetService(typeof(ILogger<Startup>)) as ILogger;
                        logger.LogCritical(exc, exc.Message);
                        var result = "一个错误发生";
                        var data = Encoding.UTF8.GetBytes(result);
                        context.Response.StatusCode = 500;
                        context.Response.ContentType = "application/json";
                        context.Response.Body.Write(data, 0, data.Length);
                    }
                });

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
