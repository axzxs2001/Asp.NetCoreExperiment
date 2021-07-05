using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiChildDI.Services
{
    public interface IDemoService
    {
        void F1();
    }
    public class DemoService01 : IDemoService
    {
        public DemoService01()
        {
            Console.WriteLine("---------------DemoService01");
        }
        public void F1()
        {
            Console.WriteLine("---------------DemoService01.F1()");
        }
    }
    public class DemoService02 : IDemoService
    {
        public DemoService02()
        {
            Console.WriteLine("---------------DemoService02");
        }
        public void F1()
        {
            Console.WriteLine("---------------DemoService02.F1()");
        }
    }
    public class DemoService03 : IDemoService
    {
        public DemoService03()
        {
            Console.WriteLine("---------------DemoService03");
        }
        public void F1()
        {
            Console.WriteLine("---------------DemoService03.F1()");
        }
    }
}
