
using Quartz;
using System;
using System.Threading.Tasks;

namespace QuartzNetDemo1.Model
{
    public class MigrationJob : IJob
    {

        static int _times = 0;
        static object obj=new object();
        IMigrationRepository _migrationRepository;

        public MigrationJob(IMigrationRepository migrationRepository)
        {
            _migrationRepository = migrationRepository;
        }
        public Task Execute(IJobExecutionContext context)
        {
            lock (obj)
            {
                try
                {
                    _migrationRepository.Migration();
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
                        Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}:开始发警报");
                        Console.ForegroundColor = ConsoleColor.White;
                        _times = 0;
                    }
                }
            }
            return Task.CompletedTask;
        }
    }
}
