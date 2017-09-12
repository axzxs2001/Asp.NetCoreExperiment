using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToenTest.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic token = null;
            while (true)
            {
                Console.WriteLine("1、登录 2、查询数据 ");
                var mark = Console.ReadLine();
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                switch (mark)
                {
                    case "1":
                        var loginClient = new RestClient("http://localhost:21855");
                        var loginRequest = new RestRequest("/api/Token/login", Method.POST);
                        loginRequest.AddParameter("username", "gsw");
                        loginRequest.AddParameter("password", "111111");
                        loginRequest.AddParameter("audience", "TestAudience");
                        //或用用户名密码查询对应角色
                        loginRequest.AddParameter("role", "admin");
                        IRestResponse loginResponse = loginClient.Execute(loginRequest);
                        var loginContent = loginResponse.Content;
                        Console.WriteLine(loginContent);
                        token = Newtonsoft.Json.JsonConvert.DeserializeObject(loginContent);
                        break;
                    case "2":
                        var client = new RestClient("http://localhost:21855");
                        //这里要在获取的令牌字符串前加Bearer
                        string tk = "Bearer " + Convert.ToString(token?.access_token);
                        client.AddDefaultHeader("Authorization", tk);
                        var request = new RestRequest("/api/Token/abc", Method.POST);
                        IRestResponse response = client.Execute(request);
                        var content = response.Content;
                        Console.WriteLine($"状态：{response.StatusCode}  返回结果：{content}");
                        break;
                }
                stopwatch.Stop();
                TimeSpan timespan = stopwatch.Elapsed;
                Console.WriteLine($"间隔时间：{timespan.TotalSeconds}");
            }
        }
    }
}
