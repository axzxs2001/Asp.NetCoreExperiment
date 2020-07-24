using System;
using System.Collections.Generic;
using B;
using CSharpDemo01;
using CSharpDemo01_Lib;

namespace CSharpDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(nameof(System.String));
            int j = 5;
            Console.WriteLine(nameof(j));
            List<string> names = new List<string>();
            Console.WriteLine(nameof(names));
        }
    }

    class TestA
    {
        private protected void A()
        {
            Console.WriteLine("TestA.A");
        }
        public void B()
        {
            A();
        }
    }
}
namespace B
{
    class TestB : TestA
    {
        public void BB()
        {
            A();
        }
    }
    class TestB_Lib : TestA_Lib
    {
        public void BB()
        {
            //A();
        }
    }
}
