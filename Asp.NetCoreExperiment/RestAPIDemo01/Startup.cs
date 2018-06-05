using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http;

namespace RestAPIDemo01
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
            services.AddMvc(options =>
            {
                //请求响应的格式，支持xml，默认json，想返回xml，需要在请求的header中添加accept application/xml，请求不支持的格式返加4.6 not acceptable
                options.ReturnHttpNotAcceptable = true;
                options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());
                options.InputFormatters.Add(new XmlSerializerInputFormatter(new MvcOptions() {  }));
            }
         
            ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
          //  if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }
            //else
            {
                app.UseHsts();
                app.UseExceptionHandler(builder =>
                {
                    builder.Run(async context =>
                    {
                        var arr = System.Text.Encoding.UTF8.GetBytes("错了");
                        context.Response.StatusCode = 500;
                        await context.Response.Body.WriteAsync(arr, 0,arr.Length);
                    });
                });
            }

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
