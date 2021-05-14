using System;
using static System.Console;

namespace KeyWordsDemo
{
    class RangDemo : IDemo
    {
        public void Run()
        {
            var arr = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

            var arr38 = arr[3..8];
            foreach (var i in arr38)
            {
                WriteLine(i);
            }
            WriteLine("=========");
            var arr3End = arr[3..];
            arr3End[4] = 1000;
            foreach (var i in arr3End)
            {
                WriteLine(i);
            }

            WriteLine("========="); 
            var arrStart7 = arr[..10];
            foreach (var i in arrStart7)
            {
                WriteLine(i);
            }

            WriteLine("=========");
            Index start = 3;
            Index end = new Index(10);
            Range range = new Range(start, end);
            var arr310 = arr[range];
            
            foreach (var i in arr310)
            {
                WriteLine(i);
            }
            WriteLine("=========");


        }
    }
}
