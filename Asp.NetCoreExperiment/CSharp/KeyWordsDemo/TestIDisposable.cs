using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
namespace KeyWordsDemo
{
    class TestIDisposable : IDemo
    {
        public void Run()
        {


            Call01();

            WriteLine("call01 over");

            WriteLine();

            Call02();

            WriteLine("call02 over");

            //var isException = true;
            //var isCallMethod_3 = true;
            //WriteLine("RunMethod1");
            //RunMethod1(isException, isCallMethod_3);
            //WriteLine();

            //WriteLine("RunMethod2");
            //RunMethod2(isException, isCallMethod_3);
            //WriteLine();

            //WriteLine("RunMethod3");
            //RunMethod3(isException, isCallMethod_3);
            //WriteLine();

            //WriteLine("RunMethod4");
            //RunMethod4(isException, isCallMethod_3);
        }
        
        void Call01()
        {
            WriteLine("begin");
            using (var test = new Test("1"))
            {
                test.CallMethod_1();
                test.CallMethod_3();
            }
            WriteLine("end");
        }
        void Call02()
        {
            WriteLine("begin");
            using var test = new Test("2");
            test.CallMethod_1();
            test.CallMethod_3();        
            WriteLine("end");
            WriteLine("end");
            WriteLine("end");
        }
        void RunMethod1(bool isException, bool isCallMethod_3)
        {
            try
            {
                using (var test = new Test("1"))
                {
                    test.CallMethod_1();
                    if (isException)
                    {
                        test.CallMethod_2();
                    }
                    if (isCallMethod_3)
                    {
                        test.CallMethod_3();
                    }
                }
            }
            catch (Exception exc)
            {
                WriteLine($"Run Exception:{exc.Message}");
            }
        }
        void RunMethod2(bool isException, bool isCallMethod_3)
        {
            try
            {
                using (var test = new Test("2"))
                {
                    test.CallMethod_1();
                    if (isException)
                    {
                        throw new Exception("Exception of Run");
                    }
                    if (isCallMethod_3)
                    {
                        test.CallMethod_3();
                    }
                }
            }
            catch (Exception exc)
            {
                WriteLine($"RunException Print:{exc.Message}");
            }
        }
        void RunMethod3(bool isException, bool isCallMethod_3)
        {

            try
            {
                using var test = new Test("3");
                test.CallMethod_1();
                if (isException)
                {
                    test.CallMethod_2();
                }
                if (isCallMethod_3)
                {
                    test.CallMethod_3();
                }
            }
            catch (Exception exc)
            {
                WriteLine($"Run Exception:{exc.Message}");
            }
        }
        void RunMethod4(bool isException, bool isCallMethod_3)
        {
            try
            {
                using var test = new Test("4");
                test.CallMethod_1();
                if (isException)
                {
                    throw new Exception("Exception of Run");
                }
                if (isCallMethod_3)
                {
                    test.CallMethod_3();
                }
            }
            catch (Exception exc)
            {
                WriteLine($"RunException Print:{exc.Message}");
            }
        }

    }

    class Test : IDisposable
    {
        private readonly string _name;
        public Test(string name)
        {
            _name = name;
        }
        public void Dispose()
        {
            WriteLine($"{_name} Dispose");
        }
        public void CallMethod_1()
        {
            WriteLine("CallMethod_1");
        }
        public void CallMethod_2()
        {
            throw new Exception("Exception of CallMethod_2");
        }
        public void CallMethod_3()
        {
            WriteLine("CallMethod_3");
        }
    }
}
