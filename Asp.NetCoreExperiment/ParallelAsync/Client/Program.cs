using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Console;
namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                WriteLine("回车开始执行");
                ReadLine();

                var stopwatch = Stopwatch.StartNew();
                //await SyncCall();
                //WriteLine($"用时{stopwatch.ElapsedMilliseconds}ms");

                ////WriteLine("===========================================");
                //stopwatch.Restart();

                await AsyncCall();
                WriteLine($"用时{stopwatch.ElapsedMilliseconds}ms");
            }
        }


        static async Task SyncCall()
        {
            using var httpClient = new HttpClient();
            try
            {
                var result1 = await GetAPI001(httpClient);
                WriteLine(result1);
            }
            catch (Exception exc)
            {
                WriteLine(exc.Message);
            }
            try
            {
                var result2 = await GetAPI002(httpClient);
                Console.WriteLine(result2);

            }
            catch (Exception exc)
            {
                WriteLine(exc.Message);
            }
            try
            {
                var result3 = await GetAPI003(httpClient);
                Console.WriteLine(result3);
            }
            catch (Exception exc)
            {
                WriteLine(exc.Message);
            }
        }

        static async Task AsyncCall()
        {
            using var httpClient = new HttpClient();
            var allTasks = Task.WhenAll(GetAPI001(httpClient), GetAPI002(httpClient), GetAPI003(httpClient));
            try
            {
                var results = await allTasks;
                foreach (var result in results)
                {
                    Console.WriteLine(result);
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine($"捕捉到的异常：{exc.Message}");
            }
            if (allTasks.Exception != null)
            {
                Console.WriteLine($"AllTasks异常：{ allTasks.Exception.Message}");
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
