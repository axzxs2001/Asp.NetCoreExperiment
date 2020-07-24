using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using System;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace QuartzNetDemo2
{
    class Program
    {
        private static void Main(string[] args)
        {
            Demo2();
        }

        #region demo2
        static void Demo2()
        {
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            RunProgramRunExample().GetAwaiter().GetResult();

            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }
        private static async Task RunProgramRunExample()
        {
            try
            {
                //定义配置参数集合
                var props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                var factory = new StdSchedulerFactory(props);
                //生成调度器
                var scheduler = await factory.GetScheduler();

                //开始调度器
                await scheduler.Start();

                //创建作业任务
                var job = JobBuilder.Create<HelloJob>()
                    .WithIdentity("job1", "group1")
                    .Build();

                //创建触发器
                var trigger = TriggerBuilder.Create()
                    .WithIdentity("trigger1", "group1")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever())
                    .Build();

                //用调度器关联触发器和作业任务
                await scheduler.ScheduleJob(job, trigger);

                // 延迟60秒
                await Task.Delay(TimeSpan.FromSeconds(60));

                // 关闭调度器
                await scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }

        }
        #endregion
        #region demo1
        static void Demo1()
        {
            // trigger async evaluation
            RunProgram().GetAwaiter().GetResult();
        }
        private static async Task RunProgram()
        {
            try
            {
                // Grab the Scheduler instance from the Factory
                var props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };
                var factory = new StdSchedulerFactory(props);
                var scheduler = await factory.GetScheduler();

                // and start it off
                await scheduler.Start();

                // some sleep to show what's happening
                await Task.Delay(TimeSpan.FromSeconds(60));

                // and last shut down the scheduler when you are ready to close your program
                await scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                await Console.Error.WriteLineAsync(se.ToString());
            }
        }
        #endregion
    }

    public class HelloJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
        
            await Console.Out.WriteLineAsync($"{DateTime.Now.ToString()}:Greetings from HelloJob!");
        }
    }

    public class ConsoleLogProvider : ILogProvider
    {
        public Logger GetLogger(string name)
        {
            return (level, func, exception, parameters) =>
            {
                if (level >= LogLevel.Info && func != null)
                {
                    Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
                }
                return true;
            };
        }

        public IDisposable OpenNestedContext(string message)
        {
            throw new NotImplementedException();
        }

        public IDisposable OpenMappedContext(string key, string value)
        {
            throw new NotImplementedException();
        }
    }
}
