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

    public class PropertyDemo : IDemo
    {
        public void Run()
        {
            BenchmarkRunner.Run<TestProperty>();
        }
    }

    [MemoryDiagnoser]
    public class TestProperty
    {

        private readonly MyClass _myClass;
        private readonly PropertyInfo _proinfo;
        private readonly Func<MyClass, string> _delegate;

        public TestProperty()
        {
            _myClass = new MyClass();
            _proinfo = _myClass.GetType().GetProperty("MyProperty1");
            _delegate = (Func<MyClass, string>)Delegate.CreateDelegate(typeof(Func<MyClass, string>), _proinfo.GetGetMethod(true)!);
        }

        [Benchmark]
        public string PropertyA()
        {
            return _myClass.MyProperty1;
        }
        [Benchmark]
        public string PropertyAExt()
        {
            var myClass = new MyClass();
            return myClass.MyProperty1;
        }
        //[Benchmark]
        //public string PropertyB()
        //{
        //    return _myClass.MyProperty2;
        //}    
        //[Benchmark]
        //public string PropertyBExt()
        //{
        //    var myClass = new MyClass();
        //    return myClass.MyProperty2;
        //}
        [Benchmark]
        public string PropertyB()
        {
            return _proinfo.GetValue(_myClass).ToString();

        }
        [Benchmark]
        public string PropertyBExt()
        {
            var myClass = new MyClass();
            var proinfo = myClass.GetType().GetProperty("MyProperty1");
            return proinfo.GetValue(myClass).ToString();
        }

        [Benchmark]
        public string PropertyC()
        {
            var value = _delegate(_myClass);
            return value;
        }
        [Benchmark]
        public string PropertyCExt()
        {
            var myClass = new MyClass();
            var proinfo = myClass.GetType().GetProperty("MyProperty1");
            var dele = (Func<MyClass, string>)Delegate.CreateDelegate(typeof(Func<MyClass, string>), proinfo.GetGetMethod(true)!);
            return dele(_myClass);
        }
    }

    public class MyClass
    {
        private string _myProperty1 = DateTime.Now.ToString();
        public string MyProperty1 { get { return _myProperty1; } }

        public string MyProperty2 { get { return DateTime.Now.ToString(); } }

        public string MyMethod()
        {
            return DateTime.Now.ToString();
        }
    }
}
