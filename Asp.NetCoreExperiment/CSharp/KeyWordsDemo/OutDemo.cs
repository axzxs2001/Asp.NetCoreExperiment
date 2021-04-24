using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace KeyWordsDemo
{
    class OutDemo : IDemo
    {
        public void Run()
        {
            //out 1
            Computer(out int result);
            WriteLine(result);

            WriteLine("------------");
            IParent<object> p1 = new Child<object>();
            IParent<object> p2 = new Child<string>();
            WriteLine(p1.GetType());
            WriteLine(p2.GetType());
            p1.Print();
            p2.Print();
            p1 = p2;
            p1.Print();

        }
        //out 1
        public void Computer(out int result)
        {
            result = 10;
        }

        //out 2
        interface IParent<out R>
        {
            R Print();
        }
        class Child<R> : IParent<R>
        {
            public R Print()
            {
                var r = default(R);
                WriteLine($"{typeof(R).Name}");
                return r;
            }
        }
    }
}
