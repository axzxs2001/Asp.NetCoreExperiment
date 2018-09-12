using Orleans;
using Orleans.Runtime;
using System;
using System.Threading.Tasks;

namespace Demo004
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new TestGrain();
            test.MyTest();
            Console.ReadLine();

             IGrainFactory
        }
    }

    interface ITestGrain
    {
        Task MyTest();
    }
    class TestGrain : Grain, ITestGrain
    {
        public Task MyTest()
        {
            Console.WriteLine("mytest");
            return Task.CompletedTask;
        }
        public override Task OnActivateAsync()
        {
            Console.WriteLine("OnActivateAsync");
            return base.OnActivateAsync();
        }
        public override Task OnDeactivateAsync()
        {
            Console.WriteLine("OnDeactivateAsync");
            return base.OnDeactivateAsync();
        }
        public override void Participate(IGrainLifecycle lifecycle)
        {
            Console.WriteLine("Participate");
            base.Participate(lifecycle);
        }
    }
}
