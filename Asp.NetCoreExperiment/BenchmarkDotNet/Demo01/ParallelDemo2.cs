using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Demo01
{
    public class ParallelDemo2 : IDemo
    {
        public void Run()
        {
            BenchmarkRunner.Run<TestParallelDemo2>();
        }
    }

    public class TestParallelDemo2
    {

        [Benchmark]
        public void DemoAsync()
        {
            var list = new List<string>();
            for (var i = 0; i < 80000; i++)
            {
                list.Add(ToMD5Hash($"{i}{DateTime.Now.ToString("yyyyMMddHHmmssfffffff")}"));
            }
            foreach (var item in list)
            {
               // Console.WriteLine($"-----{item}-----");
            }
        }

        [Benchmark]
        public async Task PartitionerDemoAsync()
        {
            var source = new List<string>();
            for (var i = 0; i < 80000; i++)
            {
                source.Add($"{i}{DateTime.Now.ToString("yyyyMMddHHmmssfffffff")}");
            }
            var list = Partitioner
                      .Create(source)
                      .GetPartitions(12)
                      .AsParallel()
                      .Select(PartitionA);

            foreach (var item in list)
            {
                foreach (var t in await item)
                {
                   // Console.WriteLine($"-----{t}-----");
                }
            }

        }
        string ToMD5Hash(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return null;
            }
            var bytes = Encoding.ASCII.GetBytes(str);
            if (bytes == null || bytes.Length == 0)
            {
                return null;
            }
            using (var md5 = MD5.Create())
            {
                return string.Join("", md5.ComputeHash(bytes).Select(x => x.ToString("X2")));
            }
        }
        async Task<List<string>> PartitionA(IEnumerator<string> partition)
        {
            using (partition)
            {
                var list = new List<string>();
                while (partition.MoveNext())
                {
                    list.Add(ToMD5Hash(partition.Current));
                }
                //Console.WriteLine($"======={list.Count}========");
                return await Task.FromResult(list);
            }
        }
    }
}
