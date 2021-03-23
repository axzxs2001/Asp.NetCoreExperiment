using System;

namespace KeyWordsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo demo = new NullDemo();
            demo.Run();
        }
    }

    interface Demo
    {
        void Run();
    }
}
