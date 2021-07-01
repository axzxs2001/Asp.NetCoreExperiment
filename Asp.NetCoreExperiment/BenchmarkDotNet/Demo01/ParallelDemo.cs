using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Demo01
{
    public class ParallelDemo : IDemo
    {
        public void Run()
        {
            BenchmarkRunner.Run<ForHttpClientDemo>();
        }
    }

    public class ForDemo
    {
        [Benchmark]
        public int[] ParallelForEach()
        {
            var array = new int[1_000_000];
            Parallel.For(0, array.Length, i =>
            {
                array[i] = i;

            });
            return array;
        }
        [Benchmark]
        public int[] NormalForEach()
        {
            var array = new int[1_000_000];
            for (var i = 0; i < array.Length; i++)
            {
                array[i] = i;

            }
            return array;
        }

    }
    public class ForHttpClientDemo
    {
        static readonly HttpClient httpClient = new();
        const int taskCount = 100;

        [Benchmark]
        public async Task<List<string>> ForEachVersion()
        {
            var list = new List<string>();
            var tasks = Enumerable.Range(0, taskCount).Select(_ => new Func<Task<string>>(() => GetAPI004(httpClient))).ToList();
            foreach (var task in tasks)
            {
                list.Add(await task());
            }

            return list;
        }
        [Benchmark]
        public List<string> UnlimitedParallelVersion() => ParallelVersion(-1);

        [Benchmark]
        public List<string> LimitedParallelVersion() => ParallelVersion(4);

        public List<string> ParallelVersion(int maxDegreeOfParallelism)
        {
            var list = new List<string>();
            var tasks = Enumerable.Range(0, taskCount).Select(_ => new Func<string>(() => GetAPI004(httpClient).GetAwaiter().GetResult())).ToList();
            Parallel.For(0, tasks.Count, new ParallelOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            }, i => list.Add(tasks[i]()));
            return list;
        }


        [Benchmark]
        public async Task<List<string>> WhenAllVersion()
        {
            var tasks = Enumerable.Range(0, taskCount).Select(_ => GetAPI004(httpClient));
            var results = await Task.WhenAll(tasks);
            return results.ToList();
        }

        private async Task<string> GetAPI004(HttpClient httpClient)
        {
            var content = await httpClient.GetStringAsync("http://localhost:5000/api004");
            var result = JsonSerializer.Deserialize<ResponseResult<string>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            if (result.Result)
            {
                return result.Data;
            }
            else
            {
                return result.Message;
            }
        }



        [Benchmark]
        public async Task<List<string>> AsyncParallelVersion() => await AsyncParallelVersion(100);

        public async Task<List<string>> AsyncParallelVersion(int batches)
        {
            var list = new List<string>();
            var tasks = Enumerable.Range(0, taskCount)
                .Select(_ => new Func<Task<string>>(() => GetAPI004(httpClient))).ToList();

            await ParallelForEachAsync(tasks, batches, async func =>
            {
                list.Add(await func());

            });
            return list;
        }


        public Task ParallelForEachAsync<T>(IEnumerable<T> source, int degreeOfParallelization, Func<T, Task> body)
        {
            async Task AwaitPartition(IEnumerator<T> partition)
            {
                using (partition)
                {
                    while (partition.MoveNext())
                    {
                        await body(partition.Current);
                    }
                }
            }
            return Task.WhenAll(
                Partitioner
                .Create(source)
                .GetPartitions(degreeOfParallelization)
                .AsParallel()
                .Select(AwaitPartition));
        }


    }
    class ResponseResult<T>
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
