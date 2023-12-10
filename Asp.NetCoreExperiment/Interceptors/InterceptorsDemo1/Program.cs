using System;
using System.Runtime.CompilerServices;



var myClass = new ABC.AOT.MyClass();
myClass.Print("参数");

var myClass1 = new ABC.MyClass();
myClass1.Print("参数");


Console.ReadLine();














//var c = new C();
//c.InterceptableMethod(10);


//class C
//{
//    public void InterceptableMethod(int param)
//    {
//        Console.WriteLine($"原始 {param}");
//    }
//}

//namespace ABC.ABC
//{
//    static class D
//    {
//        [InterceptsLocation("C:\\MyFile\\Asp.NetCoreExperiment\\Asp.NetCoreExperiment\\Interceptors\\InterceptorsDemo1\\Program.cs", 5, 3)]
//        internal static void InterceptorMethod(this C c, int param)
//        {
//            Console.WriteLine($"拦截 {param}");
//        }
//    }
//}

//namespace System.Runtime.CompilerServices
//{
//    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
//    internal sealed class InterceptsLocationAttribute(string filePath, int line, int column) : Attribute
//    {
//    }
//}