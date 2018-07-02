using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


using QuartzNetDemo4.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using QuartzNetDemo4.Model.DataModel;
using Microsoft.Extensions.Options;

namespace QuartzNetDemo4
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

            //var cronJobs = new List<CronMethod>();
            //Configuration.GetSection("CronJob").Bind(cronJobs);
            //services.AddSingleton(cronJobs);

            services.Configure<List<CronMethod>>(Configuration.GetSection("CronJob"));

            services.AddTransient<IBackgroundRepository, BackgroundRepository>();

            services.UseQuartz(typeof(BackgroundJob));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IScheduler scheduler, IOptionsSnapshot<List<CronMethod>> cronJobs)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            foreach (var cronJob in cronJobs.Value)
            {
                QuartzServicesUtilities.StartJob<BackgroundJob>(scheduler, cronJob.CronExpression, cronJob.MethodName);
            }

            app.UseMvc();
        }
    }
}
