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
        //var test = new Test();
        //Console.WriteLine(test.A());
        //Console.WriteLine(test.B());
        //Console.WriteLine(test.C());
        BenchmarkRunner.Run<Test>();
    }
}
[MemoryDiagnoser]
public class Test
{
    private readonly PropertyInfo _proinfo;
    private readonly MyClass _test;
    private readonly Func<MyClass, string> _del;
    private readonly MethodInfo _methodinfo;
    public Test()
    {
        _test = new MyClass() { MyProperty = "abc" };
        _proinfo = _test.GetType().GetProperty("MyProperty");
        _methodinfo = _proinfo.GetGetMethod(true);
        _del = (Func<MyClass, string>)Delegate.CreateDelegate(typeof(Func<MyClass, string>), _proinfo.GetGetMethod(true)!);
    }

    [Benchmark]
    public string A()
    {
        var value = _proinfo.GetValue(_test);
        return value.ToString();
    }
    [Benchmark]
    public string B()
    {
        var value = _methodinfo.Invoke(_test, new object[0]);
        return value.ToString();
    }
    [Benchmark]
    public string C()
    {
        var value = _del(_test);
        return value;
    }
}

public class MyClass
{
    public string MyProperty { get; set; }
}

