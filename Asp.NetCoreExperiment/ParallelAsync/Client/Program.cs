using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static async Task Main(string[] args)
        {

            var httpClient = new HttpClient();

            Console.WriteLine("Hello World!");



        }

        private static async Task<string> GetAPI001(HttpClient httpClient)
        {
            var content = await httpClient.GetStringAsync("http://localhost:5000/api001");
            var result = JsonSerializer.Deserialize<ResponseResult<string>>(content);
            if (result.Restlt)
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
            var result = JsonSerializer.Deserialize<ResponseResult<string>>(content);
            if (result.Restlt)
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
            var result = JsonSerializer.Deserialize<ResponseResult<string>>(content);
            if (result.Restlt)
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
        public bool Restlt { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
