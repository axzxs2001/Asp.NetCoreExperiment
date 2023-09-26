using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Net.Mime.MediaTypeNames;



var test = new TestClass();
Console.WriteLine(test.GetType().GetMethod("GetTime", BindingFlags.NonPublic | BindingFlags.Instance)?.Invoke(test, new object[0]));
Console.WriteLine(Test.GetTime(test));

BenchmarkRunner.Run<Test>();

public class Test
{
    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "GetTime")]
    public static extern DateTime GetTime(TestClass test);


    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_no")]
    public static extern ref int GetNo(TestClass test);
    [Benchmark]
    public void UnsafeTest()
    {
        var test = new TestClass();
        var t = GetTime(test);
    }
    [Benchmark]
    public void RefTest()
    {
        var test = new TestClass();
        var type = test.GetType();
        var method=type.GetMethod("GetTime", BindingFlags.NonPublic | BindingFlags.Instance);
        var t = method?.Invoke(test, new object[0]);   
    }
}

public class TestClass
{

    int _no = 10;
    DateTime GetTime()
    {
        return DateTime.Now;
    }
}