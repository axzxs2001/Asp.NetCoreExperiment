using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace RateLimitDemo01_Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("");
            Console.WriteLine("回车开始：");
            Console.ReadLine();
            var url = "https://localhost:5001";
            Console.WriteLine("---------None-------------");
            await None(url);
            Console.WriteLine("---------WhiteIP1-------------");
            await WhiteIP1(url);
            System.Threading.Thread.Sleep(2000);
            Console.WriteLine("---------WhiteIP2-------------");
            await WhiteIP2(url);
            Console.WriteLine("---------ClientID001-------------");
            await ClientID001(url);
            Console.WriteLine("---------ClientID002-------------");
            await ClientID002(url);
            Console.ReadLine();
        }
        static async Task None(string url)
        {
            for (var i = 0; i < 5; i++)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    //appsettings中配置，所以自由访问 "EndpointWhitelist": [ "get:/none", "*:/home/add" ],
                    var request = new HttpRequestMessage(HttpMethod.Get, "/none");
                    var response = await client.SendAsync(request);
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"状态码:{response.StatusCode},{(int)response.StatusCode},返回值：" + content);
                }
            }
        }

        static async Task WhiteIP1(string url)
        {
            for (var i = 0; i < 5; i++)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    /*"
                     GeneralRules": [
      {
        "Endpoint": "*",
        "Period": "1s",
        "Limit": 2
      }
                    ……
                    */
                    var request = new HttpRequestMessage(HttpMethod.Get, "/whiteip1");
                    var response = await client.SendAsync(request);
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"状态码:{response.StatusCode},{(int)response.StatusCode},返回值：" + content);
                }
            }
        }

        static async Task WhiteIP2(string url)
        {
            for (var i = 0; i < 5; i++)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    /*"GeneralRules": [
      ……
      {
        "Endpoint": "*",
        "Period": "1m",
        "Limit": 5
      }
    ]*/
                    System.Threading.Thread.Sleep(1000);
                    var request = new HttpRequestMessage(HttpMethod.Get, "/whiteip2");
                    var response = await client.SendAsync(request);
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"状态码:{response.StatusCode},{(int)response.StatusCode},返回值：" + content);
                }
            }
        }
        static async Task ClientID001(string url)
        {
            for (var i = 0; i < 5; i++)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var request = new HttpRequestMessage(HttpMethod.Get, "/clientid");
                    request.Headers.Add("X-ClientId", "client_level_001");
                    var response = await client.SendAsync(request);
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"状态码:{response.StatusCode},{(int)response.StatusCode},返回值：" + content);
                }
            }
        }
        static async Task ClientID002(string url)
        {
            for (var i = 0; i < 5; i++)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    var request = new HttpRequestMessage(HttpMethod.Get, "/clientid");
                    request.Headers.Add("X-ClientId", "client_level_002");
                    var response = await client.SendAsync(request);
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"状态码:{response.StatusCode},{(int)response.StatusCode},返回值：" + content);
                }
            }
        }

    }
}
