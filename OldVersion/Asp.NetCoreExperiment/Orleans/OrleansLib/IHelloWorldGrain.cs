using Orleans;
using System;
using System.Threading.Tasks;

namespace ConsulLib
{

    public interface IConsulGrain : IGrainWithGuidKey
    {
        Task Method1(string content);
    }

    public class ConsulGrain : Grain, IConsulGrain
    {

        public Task Method1(string content)
        {
            Console.WriteLine($"{DateTime.Now},这里是Grain的一个类，ConsulGrain.Method1({content})");
            return Task.CompletedTask;
        }

    }
}