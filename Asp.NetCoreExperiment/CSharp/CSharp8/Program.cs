using System;

namespace CSharp8
{
    class Program
    {
        static void Main(string[] args)
        {

            Test1();

            Console.WriteLine("Hello World!");
        }

        static void Test1()
        {
            ITest1 a = new Test1();
            ITest1.SetP3(DateTime.Now);
            a.P1 = 1234;
            a.P2 = "gsw";
            a.Print();

            var a1 = new Test1();
           
            a1.P1 = 5678;
            a1.P2 = "ggg";
            a1.Print1();
            //如果a定义成Test1类，就不会有Print
        }
    }

    #region Test1
    interface ITest1
    {
        private static DateTime P3;
        public static void SetP3(DateTime P3)
        {
            ITest1.P3 = P3;
        }

        int P1 { get; set; }
        string P2 { get; set; }
        public void Print()
        {
            Get(this);
        }
        protected static void Get(ITest1 test1)
        {
            Console.WriteLine($"{test1.P1}  {test1.P2}  {P3}");
        }
    }

    public class Test1 : ITest1
    {
        public int P1 { get; set; }
        public string P2 { get; set; }

        public void Print1()
        {
            ITest1.Get(this);
        }
    }
    #endregion
}
