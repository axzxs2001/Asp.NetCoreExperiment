using ModifiersDemo;
using System;

namespace ModifiersDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            new TestModifiers().Test();
            Console.WriteLine("-------------");
            var mTest = new ModifiersLibDemo.TestModifiers();
            mTest.Test();
            Console.WriteLine("-------------");
            mTest.Test2();
            Console.WriteLine("Hello World!");
        }
    }

    public class TestModifiers : ModifiersLibDemo.ModifiersClass
    {
        public void Test()
        {
            PublicMethod();
            ProtectedMethod();
            ProtectedInternalMethod();
        }
    }
}
