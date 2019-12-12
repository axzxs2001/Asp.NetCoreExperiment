using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");
            //var worker = new Worker1();

            var worker = new Worker2();
            //等待
            //worker.DoWork().Wait();
            //使用丢弃
            //_ = worker.DoWork();
            //需要方法改成异步
            //await worker.DoWork();
            //使用GetAwaiter
            worker.DoWork().GetAwaiter();


            while (!worker.IsComplete)
            {
                Console.Write(".");
                Thread.Sleep(100);
            }
            Console.WriteLine("Done");
            Console.ReadLine();
        }
    }
    //异步类
    class Worker2
    {
        public bool IsComplete { get; private set; }

        public async Task DoWork()
        {
            this.IsComplete = false;
            Console.WriteLine("Doing work");
            await LongOperation();
            Console.WriteLine("work completed");
            this.IsComplete = true;
        }

        private async Task LongOperation()
        {
            await Task.Factory.StartNew(() =>
             {
                 Console.WriteLine("Working!");
                 Thread.Sleep(2000);
             });
        }
    }
    class Worker1
    {
        public bool IsComplete { get; private set; }

        public void DoWork()
        {
            this.IsComplete = false;
            Console.WriteLine("Doing work");
            LongOperation();
            Console.WriteLine("work completed");
            this.IsComplete = true;
        }

        private void LongOperation()
        {
            Console.WriteLine("Working!");
            Thread.Sleep(2000);
        }
    }
}
