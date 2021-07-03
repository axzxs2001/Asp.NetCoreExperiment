using Demo01;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;

public class Program
{

    public static async Task Main(string[] args)
    {
        //IDemo demo1 = new PropertyDemo();
        //demo1.Run();

        //IDemo demo2 = new MethodDemo();
        //demo2.Run();

        // IDemo demo3 = new DapperQueryDemo();
        //demo3?.Run();

        //IDemo demo4 = new ParallelDemo();
        //demo4?.Run();


        IDemo demo5 = new ParallelDemo2();
        demo5?.Run();
    }



}

