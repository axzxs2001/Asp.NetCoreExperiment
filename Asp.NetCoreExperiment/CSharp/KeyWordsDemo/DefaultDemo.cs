using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace KeyWordsDemo
{
    class DefaultDemo : IDemo
    {
        public void Run()
        {
            //default 1
            WriteLine(default(int));
            WriteLine(default(bool));
            WriteLine(default(DateTime));
            WriteLine(default(string));

            switch (default(string))
            {
                case "":
                    WriteLine("空字符串");
                    break;
                case null:
                    WriteLine("null");
                    break;
                //default 2
                default:
                    WriteLine("其他");
                    break;
            }
        }
    }
}
