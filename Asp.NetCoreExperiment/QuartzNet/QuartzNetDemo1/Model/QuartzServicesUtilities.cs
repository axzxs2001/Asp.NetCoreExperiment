
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzNetDemo1.Model
{
    public static class QuartzServicesUtilities
    {
        public async static void StartJob<TJob>(IScheduler scheduler,string cronExpression)
            where TJob : IJob
        {
            var jobName = typeof(TJob).FullName;

            var job = JobBuilder.Create<TJob>()
                .WithIdentity(jobName)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}.trigger")
                .StartNow()//每天23点执行一次
                .WithCronSchedule(cronExpression)
                .Build();
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
