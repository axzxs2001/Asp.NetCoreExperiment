
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzNetDemo4.Model
{
    public static class QuartzServicesUtilities
    {
        public async static void StartJob<TJob>(IScheduler scheduler, string cronExpression, string methodName)
            where TJob : IJob
        {

            var jobName = $"{typeof(TJob).FullName}_{methodName}_{cronExpression}";
            var job = JobBuilder.Create<TJob>()
                .WithIdentity(jobName)
                .Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}.trigger")
                .WithCronSchedule(cronExpression)
                .StartNow()
                .Build();
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
