
using Orleans;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Observer_Lib
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
            Console.WriteLine($"收到: greeting = '{greeting}'");
            return Task.FromResult($"回复: '{greeting}'，{DateTime.Now}");
        }
    
    }
}
