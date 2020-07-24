
using Orleans;
using Orleans.LogConsistency;
using Orleans.Providers;
using Orleans.Runtime;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExternalTasksAndGrains_Lib
{
    public interface IHelloGrain : IGrainWithGuidKey
    {
        Task Method1();
        Task Method2();
    }

    public class HelloGrain : Grain, IHelloGrain
    {
        public async Task Method1()
        {
            // Grab the Orleans task scheduler
            var orleansTs = TaskScheduler.Current;
            await Task.Delay(3000);
            // Current task scheduler did not change, the code after await is still running in the same task scheduler.
            Console.WriteLine($"1、外部1 orleansTs == TaskScheduler.Current结果:{ orleansTs == TaskScheduler.Current}");

            Task t1 = Task.Run(() =>
            {
                // This code runs on the thread pool scheduler, not on Orleans task scheduler
                Console.WriteLine($"1、t1内部 orleansTs == TaskScheduler.Current结果： {orleansTs == TaskScheduler.Current}");
                Console.WriteLine($"1、t1内部 TaskScheduler.Default== TaskScheduler.Current结果：{TaskScheduler.Default == TaskScheduler.Current}");
            });
            await t1;
            // We are back to the Orleans task scheduler. 
            // Since await was executed in Orleans task scheduler context, we are now back to that context.

            Console.WriteLine($"1、外部2 orleansTs == TaskScheduler.Current结果:{ orleansTs == TaskScheduler.Current}");

            // Example of using ask.Factory.StartNew with a custom scheduler to escape from the Orleans scheduler
            //Task t2 = Task.Factory.StartNew(() =>
            //{
            //    // This code runs on the MyCustomSchedulerThatIWroteMyself scheduler, not on the Orleans task scheduler
            //    Console.WriteLine($"1、t2内部 orleansTs == TaskScheduler.Current结果： {orleansTs == TaskScheduler.Current}");

            //    Assert.AreEqual(MyCustomSchedulerThatIWroteMyself, TaskScheduler.Current);
            //},
            //CancellationToken.None, TaskCreationOptions.None,
            //scheduler: MyCustomSchedulerThatIWroteMyself);
            //await t2;
            // We are back to Orleans task scheduler.
            //Assert.AreEqual(orleansTS, TaskScheduler.Current)
        }


        public async Task Method2()
        {
            // Grab the Orleans task scheduler
            var orleansTs = TaskScheduler.Current;
            Task<int> t1 = Task.Run(async () =>
            {
                // This code runs on the thread pool scheduler, not on Orleans task scheduler
                Console.WriteLine($"1、内部 t1 begin orleansTs == TaskScheduler.Current结果{orleansTs == TaskScheduler.Current}");
                // You can do whatever you need to do here. Now let's say you need to make a grain call.
                Task<Task<int>> t2 = Task.Factory.StartNew(() =>
               {
                   // This code runs on the Orleans task scheduler since we specified the scheduler: orleansTs.
                   Console.WriteLine($"1、内部 t2 orleansTs == TaskScheduler.Current结果{orleansTs == TaskScheduler.Current}");
                   return GrainFactory.GetGrain<IFooGrain>(0).MakeGrainCall();
               }, CancellationToken.None, TaskCreationOptions.None, scheduler: orleansTs);

                int res = await (await t2); // double await, unrelated to Orleans, just part of TPL APIs.
                                            // This code runs back on the thread pool scheduler, not on the Orleans task scheduler
                Console.WriteLine($"1、内部 t1 end  orleansTs == TaskScheduler.Current结果{orleansTs == TaskScheduler.Current}");
              
                return res;
            });

            int result = await t1;
            // We are back to the Orleans task scheduler.
            // Since await was executed in the Orleans task scheduler context, we are now back to that context.
      
            Console.WriteLine($"1、外部1 orleansTs == TaskScheduler.Current结果:{ orleansTs == TaskScheduler.Current}");

        }


    }
    public interface IFooGrain : IGrainWithIntegerKey
    {
        Task<int> MakeGrainCall();
    }

    public class FooGrain : Grain, IFooGrain
    {
        public Task<int> MakeGrainCall()
        {
            Console.WriteLine("MakeGrainCall调用");
            return Task.FromResult<int>(100);
        }
    }

}
