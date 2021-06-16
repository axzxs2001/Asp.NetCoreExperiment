using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Reflection;
using System.Threading.Tasks;

public class Program
{
    public static void Main(string[] args)
    {
        //var testMet = new TestMethod();
        //Console.WriteLine(testMet.MethodA());
        //Console.WriteLine(testMet.MethodB());
        //Console.WriteLine(testMet.MethodC());

        //var testPro = new TestProperty();
        //Console.WriteLine(testPro.PropertyA());
        //Console.WriteLine(testPro.PropertyB());
        //Console.WriteLine(testPro.PropertyC());


        BenchmarkRunner.Run<TestProperty>();
        //BenchmarkRunner.Run<TestMethod>();

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

[MemoryDiagnoser]
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

