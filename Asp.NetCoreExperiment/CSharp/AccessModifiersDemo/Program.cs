using AccessModifiersLib;
using System;

namespace AccessModifiersDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }


    class DerivedClass2 : BaseClass
    {
        void Access()
        {     //与AccessModifiersLib联动

            // 下面错误，虽然继承，但是私有的，只能当前程序集内访问
            // myValue = 10;
        }
    }
}
