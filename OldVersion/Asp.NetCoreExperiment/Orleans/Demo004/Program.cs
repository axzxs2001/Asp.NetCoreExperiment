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
          
            var client = new ClientBuilder()
                .UseLocalhostClustering()
                .Build();

            var test = client.GetGrain<TestGrain>(new Guid());
            test.MyTest();

            Console.ReadLine();
        }
    }

    interface ITestGrain : IGrainWithGuidKey
    {
        Task MyTest();
    }
    class TestGrain : Grain, ITestGrain
    {
        IDisposable _dis;
        public Task MyTest()
        {
            Console.WriteLine("mytest");
            object o = "123";
            _dis = this.RegisterTimer((obj) =>
             {
                 Console.WriteLine(obj);
                 return null;
             }, o, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(1));

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
