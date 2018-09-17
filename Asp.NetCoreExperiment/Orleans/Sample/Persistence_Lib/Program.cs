


using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Persistence_Lib
{
    public interface IHello : IGrainWithGuidKey
    {      
        Task Method1();
      
    }

    public class HelloGrain : Grain, IHello
    {
        public async Task Method1()
        {

            await Task.Delay(TimeSpan.FromSeconds(5));
            Console.WriteLine($"方法：Method1  执行,{DateTime.Now.ToString("HH:mm:ss.fff")}");
        }

     
    }
}
