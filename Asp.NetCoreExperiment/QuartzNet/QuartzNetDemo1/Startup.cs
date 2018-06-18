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
            services.AddSingleton<IBackgroundRepository, BackgroundRepository>();
            services.UseQuartz(typeof(MigrationJob), typeof(MonthOneTimeJob), typeof(MonthTwoTimeJob), typeof(WeekOneTimeJob));
            services.AddMvc();
        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IScheduler scheduler)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //每天23点执行一次OrderEventJob的Execute方法   "0 0 23 * * ?"
            QuartzServicesUtilities.StartJob<MigrationJob>(scheduler, "0 0 0 * * ?");

            //每月1号凌晨2点，5点，8点执行一次：0 0 2,5,8 1 * ?
            QuartzServicesUtilities.StartJob<MonthOneTimeJob>(scheduler, "0 0 2,5,8 1 * ?");

            //每月1号，16号凌晨1点，4点，7点执行一次：0 0 1,4,7 1,16 * ?
            QuartzServicesUtilities.StartJob<MonthTwoTimeJob>(scheduler, "0 0 1,4,7 1,16 * ?");

            //每周星期一凌晨3点，6点，9点执行一次：0 0 3,6,9 ? * 2
            QuartzServicesUtilities.StartJob<WeekOneTimeJob>(scheduler, "0 0 3,6,9 ? * 2");
            app.UseMvc();
        }
    }
}
