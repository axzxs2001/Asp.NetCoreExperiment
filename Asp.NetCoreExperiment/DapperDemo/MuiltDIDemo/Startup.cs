using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MuiltDIDemo
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
            services.AddTransient<IJK, JK1>();      
            services.AddTransient<IJK, JK2>();       

            //第一种
            //services.AddSingleton(factory =>
            //{

            //    Func<string, IJK> accesor = key =>
            //    {
            //        if (key.Equals("JK1"))

            //        {
            //            return factory.GetService<JK1>();

            //        }
            //        else if (key.Equals("JK2"))
            //        {
            //            return factory.GetService<JK2>();

            //        }
            //        else
            //        {
            //            throw new ArgumentException($"Not Support key : {key}");
            //        }
            //    };
            //    return accesor;
            //});

            //第二种方式
            services.AddTransient<IJK, JK1>(ser =>
            {
                return new JK1() { Version = "V2" };
            });
            services.AddTransient(typeof(IJK), ser =>
            {
                return new JK1() { Version = "V3" };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }

    public interface IJK
    {
        string Version { get; set; }
    }
    public class JK1 : IJK
    {
        public string Version { get; set; } = "V1";
    }

    public class JK2 : IJK
    {
        public string Version { get; set; } = "V1";
    }
}
