using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using QuartzNetDemo1.Model;

namespace QuartzNetDemo1
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
            services.AddSingleton<IMigrationRepository, MigrationRepository>();
            services.UseQuartz(typeof(MigrationJob));
           // services.AddMvc();
        }

    
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IScheduler scheduler)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //每天23点执行一次OrderEventJob的Execute方法   "0 0 23 * * ?"
            QuartzServicesUtilities.StartJob<MigrationJob>(scheduler, "*/3 * * * * ?");
            //app.UseMvc();
        }
    }
}
