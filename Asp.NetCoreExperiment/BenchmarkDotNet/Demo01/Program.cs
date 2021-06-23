using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Demo01;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;

public class Program
{
    public static async Task Main(string[] args)
    {
        //IDemo demo1 = new PropertyDemo();
        //demo1.Run();

        //IDemo demo2 = new MethodDemo();
        //demo2.Run();

        IDemo demo3 = new DapperQueryDemo();
        demo3?.Run();
    }
}
class A
{
    public List<string> Items { get; set; }
}
