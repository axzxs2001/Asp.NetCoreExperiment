using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("回车开始执行");
                Console.ReadLine();
                var httpClient = new HttpClient();
                var stopwatch = Stopwatch.StartNew();


                //var result1 = await GetAPI001(httpClient);
                //Console.WriteLine(result1);
                //var result2 = await GetAPI002(httpClient);
                //Console.WriteLine(result2);
                //var result3 = await GetAPI003(httpClient);
                //Console.WriteLine(result3);


                // var results = await Task.WhenAll(GetAPI001(httpClient), GetAPI002(httpClient), GetAPI003(httpClient));
               // Console.WriteLine(results);

                //var allTasks = Task.WhenAll(GetAPI001(httpClient), GetAPI002(httpClient), GetAPI003(httpClient));
                //try
                //{
                //    var results = await allTasks;
                //    foreach (var result in results)
                //    {
                //        Console.WriteLine(result);
                //    }
                //}
                //catch (Exception exc)
                //{
                //    Console.WriteLine($"捕捉到的异常：{exc.Message}");
                //}
                //if (allTasks.Exception != null)
                //{
                //    Console.WriteLine($"AllTasks异常：{ allTasks.Exception.Message}");
                //}


                Console.WriteLine($"用时{stopwatch.ElapsedMilliseconds}ms");
            }         
        }

        private static async Task<string> GetAPI001(HttpClient httpClient)
        {
            var content = await httpClient.GetStringAsync("http://localhost:5000/api001");
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


        private static async Task<string> GetAPI002(HttpClient httpClient)
        {
            var content = await httpClient.GetStringAsync("http://localhost:5000/api002");
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
        private static async Task<string> GetAPI003(HttpClient httpClient)
        {
            var content = await httpClient.GetStringAsync("http://localhost:5000/api003");
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
    }
    class ResponseResult<T>
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
