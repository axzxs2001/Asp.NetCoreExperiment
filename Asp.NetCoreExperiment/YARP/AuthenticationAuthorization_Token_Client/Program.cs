using RestSharp;
using System;
using System.Diagnostics;

namespace AuthenticationAuthorization_Token_Client
{
    class Program
    {
        /// <summary>
        /// 访问Url
        /// </summary>
        static string _url = "https://localhost:5001";
        static void Main(string[] args)
        {
            dynamic token = null;
            while (true)
            {
                Console.WriteLine("1、登录【admin】 2、登录【system】 3、登录【错误用户名密码】 4、查询数据 ");
                var mark = Console.ReadLine();
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                switch (mark)
                {
                    case "1":
                        token = AdminLogin();
                        break;
                    case "2":
                        token = SystemLogin();
                        break;
                    case "3":
                        token = NullLogin();
                        break;
                    case "4":
                        ApiInvock(token);
                        break;
                }
                stopwatch.Stop();
                TimeSpan timespan = stopwatch.Elapsed;
                Console.WriteLine($"间隔时间：{timespan.TotalSeconds}");
            }
        }
        static dynamic NullLogin()
        {
            var loginClient = new RestClient(_url);
            var loginRequest = new RestRequest("/api/login", Method.POST);
            loginRequest.AddParameter("username", "gswaa");
            loginRequest.AddParameter("password", "111111");
            //或用用户名密码查询对应角色
            loginRequest.AddParameter("role", "system");
            IRestResponse loginResponse = loginClient.Execute(loginRequest);
            var loginContent = loginResponse.Content;
            Console.WriteLine(loginContent);
            return Newtonsoft.Json.JsonConvert.DeserializeObject(loginContent);
        }
        static dynamic SystemLogin()
        {
            var loginClient = new RestClient(_url);
            var loginRequest = new RestRequest("/api/login", Method.POST);
            loginRequest.AddParameter("username", "gsw");
            loginRequest.AddParameter("password", "111111");
            //或用用户名密码查询对应角色
            loginRequest.AddParameter("role", "system");
            IRestResponse loginResponse = loginClient.Execute(loginRequest);
            var loginContent = loginResponse.Content;
            Console.WriteLine(loginContent);
            return Newtonsoft.Json.JsonConvert.DeserializeObject(loginContent);
        }
        static dynamic AdminLogin()
        {
            var loginClient = new RestClient(_url);
            var loginRequest = new RestRequest("/api/login", Method.POST);
            loginRequest.AddParameter("username", "gsw");
            loginRequest.AddParameter("password", "111111");
            //或用用户名密码查询对应角色
            loginRequest.AddParameter("role", "admin");
            IRestResponse loginResponse = loginClient.Execute(loginRequest);
            var loginContent = loginResponse.Content;
            Console.WriteLine(loginContent);
            return Newtonsoft.Json.JsonConvert.DeserializeObject(loginContent);
        }
        static void ApiInvock(dynamic token)
        {
            var client = new RestClient(_url);
            //这里要在获取的令牌字符串前加Bearer
            var tk = $"Bearer {Convert.ToString(token?.access_token)}";
            client.AddDefaultHeader("Authorization", tk);
            var request = new RestRequest("/adminapi", Method.GET);
            IRestResponse response = client.Execute(request);
            var content = response.Content;
            Console.WriteLine($"状态：{response.StatusCode}  {(int)response.StatusCode}返回结果：{content}");
        }
    }
}
