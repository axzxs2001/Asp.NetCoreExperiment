

using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Monitoring_Lib
{
    public interface IHello : IGrainWithGuidKey
    {
     
        Task Method1();
    }

    public class HelloGrain : Grain, IHello
    {
        public async Task Method1()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
            //throw new Exception("这里发生错误");
            Console.WriteLine($"方法：Method1  执行,{DateTime.Now.ToString("HH:mm:ss.fff")}");
        }
        
    }
}
