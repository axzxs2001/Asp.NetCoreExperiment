using Orleans;
using System;
using System.Threading.Tasks;

namespace HelloWorld_Lib
{

    public interface IHelloWorldGrain : IGrainWithGuidKey
    {
        Task Method1();
    }

    public class HelloWorldGrain : Grain, IHelloWorldGrain
    {
        public Task Method1()
        {
            Console.WriteLine("这里是Grain的一个类，IHelloWorld.Method1");
            return Task.CompletedTask;
        }

    }
}