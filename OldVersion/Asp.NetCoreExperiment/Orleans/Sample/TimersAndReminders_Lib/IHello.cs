using Orleans;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace TimersAndReminders_Lib
{
    public interface IHello : IGrainWithGuidKey
    {
        Task<string> SayHello(string greeting);
    }
    public class HelloGrain : Grain, IHello, IRemindable
    {
        public Task ReceiveReminder(string reminderName, TickStatus status)
        {
            Console.WriteLine($"{reminderName},谢谢提醒，差点忘了！FirstTickTime:{status.FirstTickTime},CurrentTickTime:{status.CurrentTickTime},Period:{status.Period.TotalSeconds}");
            return Task.CompletedTask;
        }

        public Task<string> SayHello(string greeting)
        {
            //CreateTimer();
            CreateRemider();
            Console.WriteLine($"收到: greeting = '{greeting}'");
            return Task.FromResult($"回复: '{greeting}'，{DateTime.Now}");
        }
        void CreateRemider()
        {
            var reminder = this.RegisterOrUpdateReminder("testremider", TimeSpan.FromSeconds(3), TimeSpan.FromMinutes(1));
        }

        void CreateTimer()
        {
            object obj = 123;
            var dis = this.RegisterTimer((obje) =>
            {
                Console.WriteLine($"定时器：{obje}");
                return Task.CompletedTask;
            }, obj, TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(2));
        }
    }
}
