using System;
using System.Threading.Tasks;
using static System.Console;

namespace KeyWordsDemo
{
    interface IDemo
    {
        void Run();
    }
    interface IDemoAsync
    {
        Task RunAsync();
    }
    class Program
    {
        static async Task Main(string[] args)
        {
            //Demo nulldemo = new NullDemo();
            //nulldemo.Run();
            //WriteLine("=====================");
            //Demo stringdemo = new StringDemo();
            //stringdemo.Run();
            ////WriteLine("=====================");
            //IDemo defaultDemo = new DefaultDemo();
            //defaultDemo.Run();

            //IDemo newDemo = new NewDemo();
            //newDemo.Run();

            //IDemo outDemo = new OutDemo();
            //outDemo.Run();


            //IDemo imp = new ImplicitAndExplicitDemo();
            //imp.Run();

            //IDemo rangdemo = new RangDemo();
            //rangdemo.Run();

            //IDemo fexcdemo = new FilterExceptionDemo();
            //fexcdemo.Run();

            //IDemo testDis = new TestIDisposable();
            //testDis.Run();

            //IDemo enu = new TestIEnumerable();
            // enu.Run();

            IDemoAsync asyncStream = new AsyncStreamDemo();
            await asyncStream.RunAsync();
        }
    }


}
