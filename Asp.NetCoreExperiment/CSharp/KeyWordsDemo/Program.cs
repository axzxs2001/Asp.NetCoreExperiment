using System;
using static System.Console;

namespace KeyWordsDemo
{
    class Program
    {
        static  void Main(string[] args)
        {

            //Demo nulldemo = new NullDemo();
            //nulldemo.Run();
            //WriteLine("=====================");
            Demo stringdemo = new StringDemo();
            stringdemo.Run();

        }
    }

    interface Demo
    {
        void Run();
    }
}
