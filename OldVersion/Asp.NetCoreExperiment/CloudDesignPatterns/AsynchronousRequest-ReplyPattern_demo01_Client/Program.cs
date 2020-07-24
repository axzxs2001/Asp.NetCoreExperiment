using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;

namespace AsynchronousRequest_ReplyPattern_demo01_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("提交订单");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000");
                var request = new HttpRequestMessage(HttpMethod.Post, "pay");
                request.Content = new StringContent(JsonConvert.SerializeObject(new { orderno = "ORDER00001", quantity = 100 }), Encoding.UTF8, "application/json");
                var response = client.SendAsync(request).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    var id = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"{response.StatusCode}，订单被接收，返回ID={id}");
                    var uri = response.Headers.Location;
                    var millisecond = int.Parse(response.Headers.GetValues("Retry-Afte").FirstOrDefault());
                    Thread.Sleep(millisecond);
                    GetResult(uri, id);
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine($"{response.StatusCode}，返回值：" + content);
                }

                Console.ReadLine();
            }
        }
        static void GetResult(Uri uri, string id)
        {
            Console.WriteLine("查询订单状态");
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000");
                var request = new HttpRequestMessage(HttpMethod.Get, $"{uri.ToString()}?id={id}");
                var response = client.SendAsync(request).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    Console.WriteLine($"{response.StatusCode}，查询订单状态，返回ID={id}");
                    GetResult(uri,id);
                }
                else
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine($"{response.StatusCode}，返回值：" + content);
                    }
                    else
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine($"{response.StatusCode }  异常，返回值：" + content);
                    }
                }
            }
        }
    }
}
