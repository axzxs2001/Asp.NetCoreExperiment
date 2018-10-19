

using Microsoft.Extensions.Configuration;
using Orleans;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace ConsulLib
{

    public interface IConsulGrain : IGrainWithGuidKey
    {
        Task Method1();
    }

    public class ConsulGrain : Grain, IConsulGrain
    {    

        public Task Method1()
        {          
            Console.WriteLine("这里是Grain的一个类，ConsulGrain.Method1");
            return Task.CompletedTask;
        }

    }
}