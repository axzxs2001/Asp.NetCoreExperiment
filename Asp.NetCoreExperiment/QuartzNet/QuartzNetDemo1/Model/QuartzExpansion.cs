using Microsoft.Extensions.DependencyInjection;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzNetDemo1.Model
{
    public static class QuartzExpansion
    {
        public static void UseQuartz(this IServiceCollection services, params Type[] jobs)
        {
            services.AddSingleton<IJobFactory, QuartzJonFactory>();
            foreach (var serviceDescriptor in jobs.Select(jobType => new ServiceDescriptor(jobType, jobType, ServiceLifetime.Singleton)))
            {
                services.Add(serviceDescriptor);
            }

            services.AddSingleton(provider =>
            {
                var props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                var schedulerFactory = new StdSchedulerFactory(props);
                var scheduler = schedulerFactory.GetScheduler().Result;
                scheduler.JobFactory = provider.GetService<IJobFactory>();
                scheduler.Start();
                return scheduler;
            });

        }
    }
}
