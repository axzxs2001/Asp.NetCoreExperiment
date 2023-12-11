
using System.Runtime.CompilerServices;

var myclass = new MyClass();
myclass.Print("测试");
myclass.Print("测试");


public class MyClass
{
    public void Print(string s)
    {
        Console.WriteLine($"MyClass.Print({s})");
    }
}
namespace Interceptors
{
    public static class MyClassIntercepts
    {
        [InterceptsLocation("C:\\MyFile\\Source\\Repos\\Asp.NetCoreExperiment\\Asp.NetCoreExperiment\\Interceptors\\InterceptorsDemo\\Program.cs", 5, 9)]
        public static void InterceptorPrint(this MyClass myclass, string s)
        {
            Console.WriteLine($"ABC.AOT下 MyClass.InterceptorPrint 拦截 MyClass.Print方法，参数是：{s}");
        }
    }
}

namespace System.Runtime.CompilerServices
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class InterceptsLocationAttribute(string filePath, int line, int column) : Attribute
    {
    }
}