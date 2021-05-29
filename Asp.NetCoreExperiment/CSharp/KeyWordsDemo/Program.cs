using System;
using static System.Console;

namespace KeyWordsDemo
{
    interface IDemo
    {
        void Run();
    }
    class Program
    {
        static void Main(string[] args)
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

            IDemo fexcdemo = new FilterExceptionDemo();
            fexcdemo.Run();
        }
    }


}
