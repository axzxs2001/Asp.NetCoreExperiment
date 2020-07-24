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
using Quartz;
using QuartzNetDemo3.Model;

namespace QuartzNetDemo3
{

    /// <summary>
    /// 实现不了，状态有冲突
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Dictionary<string, string>>(Configuration.GetSection("CronJob"));

            services.AddSingleton<IBackgroundRepository, BackgroundRepository>();
            services.UseQuartz(typeof(BackgroundJob));
            services.AddMvc();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IScheduler scheduler, IOptionsSnapshot<Dictionary<string, string>> cronJobsOpt)
        {
            var cronJobs = cronJobsOpt.Value;
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            foreach (var key in cronJobs.Keys)
            {
                QuartzServicesUtilities.StartJob<BackgroundJob>(scheduler, cronJobs[key], key);
            }
            app.UseMvc();
        }
    }
}
