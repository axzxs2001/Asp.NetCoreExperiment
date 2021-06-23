using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo01
{
    [MemoryDiagnoser]
    class MethodDemo : IDemo
    {
        public void Run()
        {
            BenchmarkRunner.Run<TestMethod>();
        }

        public class TestMethod
        {
            private readonly MyClass _myClass;
            private readonly Func<MyClass, string> _delegate;
            private readonly MethodInfo _methodinfo;

            public TestMethod()
            {
                _myClass = new MyClass();
                _methodinfo = _myClass.GetType().GetMethod("MyMethod");
                _delegate = (Func<MyClass, string>)Delegate.CreateDelegate(typeof(Func<MyClass, string>), _methodinfo);
            }

            [Benchmark]
            public string MethodA()
            {
                return _myClass.MyMethod();
            }
            [Benchmark]
            public string MethodAExt()
            {
                var myClass = new MyClass();
                return myClass.MyMethod();
            }

            [Benchmark]
            public string MethodB()
            {
                return _methodinfo.Invoke(_myClass, new object[0]).ToString();
            }
            [Benchmark]
            public string MethodBExt()
            {
                var myClass = new MyClass();
                var methodinfo = _myClass.GetType().GetMethod("MyMethod");
                return methodinfo.Invoke(myClass, new object[0]).ToString();
            }
            [Benchmark]
            public string MethodC()
            {
                return _delegate(_myClass);
            }
            [Benchmark]
            public string MethodCExt()
            {
                var myClass = new MyClass();
                var methodinfo = myClass.GetType().GetMethod("MyMethod");
                var dele = (Func<MyClass, string>)Delegate.CreateDelegate(typeof(Func<MyClass, string>), methodinfo);
                return dele(myClass);
            }

        }
    }
}
