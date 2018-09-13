using Orleans;
using System;

namespace Orleans004_Observers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
    interface IMyGrain : IGrainObserver
    {
        void ReceiveMessage(string message);
    }
    public class MyGrain : IMyGrain
    {
        public void ReceiveMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
