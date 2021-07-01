using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Demo01;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Concurrent;

public class Program
{

    public static async Task Main(string[] args)
    {
        //while (true)
        //{
        //    Console.ReadLine();
        //    var source = new List<int>();
        //    for (var i = 0; i < 80; i++)
        //    {
        //        source.Add(i);
        //    }

        //    var list = Partitioner
        //              .Create(source)
        //              .GetPartitions(8)
        //              .AsParallel()
        //              .Select(PartitionA);

        //    static async Task<List<string>> PartitionA(IEnumerator<int> partition)
        //    {
        //        using (partition)
        //        {
        //            var list = new List<string>();
        //            while (partition.MoveNext())
        //            {

        //                list.Add(partition.Current + "     " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
        //            }
        //            Console.WriteLine($"======={list.Count}========");
        //            return await Task.FromResult(list);
        //        }
        //    }

        //    var count = 0;
        //    foreach (var item in list)
        //    {
        //        count++;
        //        foreach (var t in await item)
        //        {
        //            // Console.WriteLine($"---{count}---{t}-----");
        //        }
        //    }
        //}

        //IDemo demo1 = new PropertyDemo();
        //demo1.Run();

        //IDemo demo2 = new MethodDemo();
        //demo2.Run();

        // IDemo demo3 = new DapperQueryDemo();
        //demo3?.Run();

        // IDemo demo4 = new ParallelDemo();
        // demo4?.Run();
    }
}

