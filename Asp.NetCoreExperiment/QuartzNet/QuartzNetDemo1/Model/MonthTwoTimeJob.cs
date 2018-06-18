
using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzNetDemo1.Model
{
    /// <summary>
    /// 每月执行两次的任务
    /// </summary>
    public class MonthTwoTimeJob : IJob
    {

        static int _times = 0;
        static object obj=new object();
        IBackgroundRepository _backgroundRepository;

        public MonthTwoTimeJob(IBackgroundRepository backgroundRepository)
        {
            _backgroundRepository = backgroundRepository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            lock (obj)
            {
                try
                {
                    _backgroundRepository.FeeTwoTimePerMonth();
                    _times = 0;
                }
                catch (Exception exc)
                {

                    if (_times < 3)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(exc.Message);
                        Console.ForegroundColor = ConsoleColor.White;
                        _times++;
                        this.Execute(context);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}:【每月执行二次】发生三次异常警报");
                        Console.ForegroundColor = ConsoleColor.White;
                        _times = 0;
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
