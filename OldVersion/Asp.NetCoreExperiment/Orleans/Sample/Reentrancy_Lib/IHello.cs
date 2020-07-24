
using Orleans;
using Orleans.Concurrency;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Reentrancy_Lib
{
    public interface IHello : IGrainWithGuidKey
    {
        [AlwaysInterleave]
        Task Method1();
        Task Method2();
    }
    //Reentrant是Grain里的所有方法交错
    //[Reentrant]
    public class HelloGrain : Grain, IHello
    {
        public async Task Method1()
        {

            await Task.Delay(TimeSpan.FromSeconds(5));
            Console.WriteLine($"方法：Method1  执行,{DateTime.Now.ToString("HH:mm:ss.fff")}");
        }

        public async Task Method2()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
            Console.WriteLine($"方法：Method2  执行,{DateTime.Now.ToString("HH:mm:ss.fff")}");
        }
    }
}
