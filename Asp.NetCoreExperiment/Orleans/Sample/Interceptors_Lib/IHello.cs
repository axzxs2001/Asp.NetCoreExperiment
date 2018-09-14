using Orleans;
using System;
using System.Threading.Tasks;

namespace Interceptors_Lib
{
    public interface IHello : IGrainWithGuidKey
    {
        Task<string> SayHello(string greeting);
    }
    public class HelloGrain : Grain, IHello
    {

        public Task<string> SayHello(string greeting)
        {
            Console.WriteLine($"收到: greeting = '{greeting}'");
            return Task.FromResult($"回复: '{greeting}'，{DateTime.Now}");
        }
    }
}
