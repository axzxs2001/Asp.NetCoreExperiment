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

namespace ConfigurationDemo004
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
            var settings = Configuration.GetSection("settings");
            var abc = Configuration.GetSection("abc");

            var sets = new AppSetting();
            Configuration.GetSection("settings").Bind(sets);
            var abcs = new ABC();
            Configuration.GetSection("abc").Bind(abcs);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
    public class AppSetting
    {
        public string A { get; set; }
        public string B { get; set; }

    }

    public class ABC
    {
        public string ABC1 { get; set; }
        public string ABC2 { get; set; }
    }
}
