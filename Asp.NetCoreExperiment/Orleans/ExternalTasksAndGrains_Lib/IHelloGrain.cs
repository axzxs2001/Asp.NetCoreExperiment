
using Orleans;
using Orleans.LogConsistency;
using Orleans.Providers;
using Orleans.Runtime;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ExternalTasksAndGrains_Lib
{
    public interface IHelloGrain:IGrainWithGuidKey
    {
        Task Method1();
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
    }

}
