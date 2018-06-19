
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzNetDemo1.Model
{
    public static class QuartzServicesUtilities
    {
        public async static void StartJob<TJob>(IScheduler scheduler, string cronExpression, string name)
            where TJob : IJob
        {
            var jobName = $"{typeof(TJob).FullName}_{name}";
            var job = JobBuilder.Create<TJob>()
                .WithIdentity(jobName)
                .Build();
            var trigger = TriggerBuilder.Create()
                .WithIdentity($"{jobName}.trigger")
                .StartNow()
                .WithCronSchedule(cronExpression)
                .Build();
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
