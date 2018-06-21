
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Quartz.Impl;
using Microsoft.Extensions.Options;

namespace QuartzNetDemo3.Model
{
    public class BackgroundJob : IJob
    {
        IBackgroundRepository _backgroundRepository;
        Dictionary<string, string> _cronJob;

        public BackgroundJob(IBackgroundRepository backgroundRepository, IOptionsSnapshot<Dictionary<string, string>> cronJobs)
        {
            _cronJob = cronJobs.Value;
            _backgroundRepository = backgroundRepository;
        }
        public Task Execute(IJobExecutionContext context)
        {

            try
            {
                if (context.JobDetail is JobDetailImpl)
                {
                    var names = (context.JobDetail as Quartz.Impl.JobDetailImpl)?.Name.Split('_');
                    var method = names.Length > 1 ? names[1] : "";
                    if (!string.IsNullOrEmpty(method))
                    {
                        var methodInfo = typeof(IBackgroundRepository).GetMethod(method);
                        methodInfo.Invoke(_backgroundRepository, new object[0]);

                    }
                }
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}:【数据迁移】发生三次异常警报,{exc.Message}");
                Console.ForegroundColor = ConsoleColor.White;

            }

            return Task.CompletedTask;
        }
    }
}
