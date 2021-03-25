using System;
using static System.Console;

namespace KeyWordsDemo
{
    class StringDemo : Demo
    {
        public unsafe void Run()
        {
            string a = "abcd";
            string b = "abcd";
            fixed (char* p = a)
            {
                WriteLine("原a字符串地址= 0x{0:x}", (int)p);

            }
            fixed (char* p = b)
            {
                WriteLine("原b字符串地址= 0x{0:x}", (int)p);

            }
            b = b + "e";
            fixed (char* p = b)
            {
                WriteLine("新b字符串地址= 0x{0:x}", (int)p);

            }
            WriteLine("------------------");
            Console.WriteLine("IsNullOrWhiteSpace(null):" + string.IsNullOrWhiteSpace(null));
            Console.WriteLine("IsNullOrWhiteSpace(\"\"):" + string.IsNullOrWhiteSpace(""));
            Console.WriteLine("IsNullOrWhiteSpace(\" \"):" + string.IsNullOrWhiteSpace(" "));
            Console.WriteLine("IsNullOrEmpty(null):" + string.IsNullOrEmpty(null));
            Console.WriteLine("IsNullOrEmpty(\"\"):" + string.IsNullOrEmpty(""));
            Console.WriteLine("IsNullOrEmpty(\" \"):" + string.IsNullOrEmpty(" "));

            ReadLine();
        }
    }

}
