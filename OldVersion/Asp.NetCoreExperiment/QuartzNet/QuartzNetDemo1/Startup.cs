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
            var cronJobs = new Dictionary<string,string>();
            Configuration.GetSection("CronJob").Bind(cronJobs);
            
            services.AddSingleton(cronJobs);
            services.AddSingleton<IBackgroundRepository, BackgroundRepository>();
            // services.UseQuartz(typeof(MigrationJob), typeof(MonthOneTimeJob), typeof(MonthTwoTimeJob), typeof(WeekOneTimeJob));
             services.UseQuartz(typeof(BackgroundJob));
            services.AddMvc();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IScheduler scheduler, Dictionary<string, string> cronJobs)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            foreach(var key in cronJobs.Keys)
            {
                QuartzServicesUtilities.StartJob<BackgroundJob>(scheduler, cronJobs[key], key);
            }      
            app.UseMvc();
        }
    }
}
