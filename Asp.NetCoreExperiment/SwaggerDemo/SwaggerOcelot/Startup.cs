using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.JwtAuthorize;
using Ocelot.Middleware;
using Swashbuckle.AspNetCore.Swagger;

namespace SwaggerOcelot
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
            services.AddOcelotJwtAuthorize();
            //注入Ocelot
            services.AddOcelot(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("ApiGateway", new Info { Title = "网关服务", Version = "v1", Contact = new Contact { Email = "285130205@qq.com", Name = "SwaggerOcelot", Url = "http://10.10.10.10" }, Description = "网关平台" });
            });
        }

        public async void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var apis = new Dictionary<string, string>(
                new KeyValuePair<string, string>[] {
                    KeyValuePair.Create("SwaggerAuthorize", "Authorize"),
                    KeyValuePair.Create("SwaggerAPI01", "API01"),
                    KeyValuePair.Create("SwaggerAPI02", "API02")
                });

            app.UseMvc()
               .UseSwagger()
               .UseSwaggerUI(options =>
               {
                   apis.Keys.ToList().ForEach(key =>
                   {
                       options.SwaggerEndpoint($"/{key}/swagger.json", $"{apis[key]} -【{key}】");
                   });
                   options.DocumentTitle = "Swagger测试平台";
               });
            await app.UseOcelot();
        }
    }
}
