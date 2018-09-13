using Orleans;
using Orleans.Concurrency;
using System;
using System.Threading.Tasks;

namespace Orleans006_Reentrancy
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new ClientBuilder().Build();
            var grain = client.GetGrain<IMyGrain>(new Guid());

            Console.WriteLine("Hello World!");
        }
    }
    interface IMyGrain : IGrainWithGuidKey
    {
        Task A();

        [AlwaysInterleave]
        Task B();
    }
    class MyGrain : Grain, IMyGrain
    {
        public Task A()
        {
            throw new NotImplementedException();
        }

        public Task B()
        {
            throw new NotImplementedException();
        }
    }
}
