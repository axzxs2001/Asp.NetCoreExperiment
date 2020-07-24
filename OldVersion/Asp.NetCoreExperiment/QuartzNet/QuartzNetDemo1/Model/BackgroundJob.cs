
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Quartz.Impl;

namespace QuartzNetDemo1.Model
{
    public class BackgroundJob : IJob
    {

        static int _times = 0;
        static object obj = new object();
        IBackgroundRepository _backgroundRepository;
        Dictionary<string, string> _cronJob;

        public BackgroundJob(IBackgroundRepository backgroundRepository, Dictionary<string, string> cronJobs)
        {
            _cronJob = cronJobs;
            _backgroundRepository = backgroundRepository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            lock (obj)
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
                            _times = 0;
                        }
                    }
                }
                catch (Exception exc)
                {

                    if (_times < 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(exc.Message);
                        if(exc.InnerException!=null)
                        {
                            Console.WriteLine(exc.InnerException.Message);
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        _times++;
                        this.Execute(context);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}:【数据迁移】发生三次异常警报");
                        Console.ForegroundColor = ConsoleColor.White;
                        _times = 0;
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
