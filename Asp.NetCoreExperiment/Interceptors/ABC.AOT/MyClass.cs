using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.Globalization;
using System.Runtime;
using System.Runtime.CompilerServices;

namespace ABC.AOT
{
    public class MyClass : ABC.MyClass
    {
        public  void Print(string s)
        {
            base.Print(s);
        }
    }
}

namespace ABC
{
    public static class MyClassIntercepts
    {
        [InterceptsLocation("C:\\MyFile\\Source\\Repos\\Asp.NetCoreExperiment\\Asp.NetCoreExperiment\\Interceptors\\ABC.AOT\\MyClass.cs", 13, 18)]
        public static void InterceptorPrint(this ABC.MyClass myclass, string s)
        {
            Console.WriteLine($"ABC.AOT下 MyClass.InterceptorPrint 拦截 ABC下MyClass.Print方法，参数是：{s}");

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