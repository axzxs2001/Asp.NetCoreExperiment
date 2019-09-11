using RestSharp;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace ClientCookieTest
{
    class Program
    {
        static void Main(string[] args)
        {
            F2();
        }
        static void F2()
        {
            var client = new RestClient("http://localhost:5000");

            //var request = new RestRequest("login", Method.POST);
            //request.AddParameter("username", "gsw"); 
            //request.AddParameter("password", "111111"); 
            var name = "";
            var value = "";
            while (true)
            {
                Console.WriteLine("1、登录    2、授权访问    3、登出   4、再登录     5、其他客户端访问");
                switch (Console.ReadLine())
                {
                    case "1":
                        var request = new RestRequest("login", Method.POST);
                        request.AddParameter("username", "gsw");
                        request.AddParameter("password", "111111");
                        IRestResponse response = client.Execute(request);
                        name = response.Cookies[0].Name;
                        value = response.Cookies[0].Value;
                        break;
                    case "2":                        
                        var request1 = new RestRequest("/home/AdminPage", Method.GET);
                        request1.AddCookie(name, value);
                        IRestResponse response1 = client.Execute(request1);
                        Console.WriteLine(response1.Content);
                        break;
                    case "3":
                        var request3 = new RestRequest("/home/Logout", Method.GET);
                        request3.AddCookie(name, value);
                        IRestResponse response3 = client.Execute(request3);
                        Console.WriteLine(response3.Content);
                        break;
                    case "4":
                        var request4 = new RestRequest("/home/AdminPage", Method.GET);
                        request4.AddCookie(name, value);
                        IRestResponse response4 = client.Execute(request4);
                        Console.WriteLine(response4.Content);
                        break;
                    case "5":
                        var client1 = new RestClient("http://localhost:5000");
                        var request5 = new RestRequest("/home/AdminPage", Method.GET);
                        request5.AddCookie(name, value);
                        IRestResponse response5 = client1.Execute(request5);
                        Console.WriteLine(response5.Content);
                        break;
                }

            }
        }
        static void F1()
        {
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            HttpClient client = new HttpClient(handler);
            var cookieheadvalue = "";
            var cookievalue = "";
            var uri = new Uri("http://localhost:5000/login?username=gsw&password=111111");
            while (true)
            {
                Console.WriteLine("1、登录    2、授权访问    3、登出   4、再登录     5、其他客户端访问");
                switch (Console.ReadLine())
                {
                    case "1":
                        var request = new HttpRequestMessage(HttpMethod.Post, uri);
                        var response = client.SendAsync(request).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
                            cookieheadvalue = cookies.GetCookieHeader(uri);
                            cookievalue = responseCookies.FirstOrDefault().Value;
                            Console.WriteLine($"responseCookies：{ responseCookies.FirstOrDefault().Value}");
                        }
                        break;
                    case "2":
                        var uri1 = new Uri("http://localhost:5000/home/AdminPage");
                        var request1 = new HttpRequestMessage(HttpMethod.Get, uri1);
                        var response1 = client.SendAsync(request1).Result;
                        if (response1.IsSuccessStatusCode)
                        {
                            Console.WriteLine(response1.IsSuccessStatusCode);
                        }
                        Console.WriteLine(response1.Content.ReadAsStringAsync().Result);
                        break;
                    case "3":
                        var uri2 = new Uri("http://localhost:5000/home/Logout");
                        var request2 = new HttpRequestMessage(HttpMethod.Get, uri2);
                        var response2 = client.SendAsync(request2).Result;
                        if (response2.IsSuccessStatusCode)
                        {
                            Console.WriteLine(response2.IsSuccessStatusCode);
                        }
                        break;
                    case "4":
                        handler.CookieContainer.SetCookies(uri, cookieheadvalue);
                        var uri3 = new Uri("http://localhost:5000/home/AdminPage");
                        var request3 = new HttpRequestMessage(HttpMethod.Get, uri3);
                        var response3 = client.SendAsync(request3).Result;
                        if (response3.IsSuccessStatusCode)
                        {
                            Console.WriteLine(response3.IsSuccessStatusCode);
                        }
                        Console.WriteLine(response3.Content.ReadAsStringAsync().Result);
                        break;
                    case "5":
                        OthoerHttpGet(cookieheadvalue, uri);
                        break;
                }
            }
        }

        private static void OthoerHttpGet(string cookievalue, Uri uri)
        {
            HttpClientHandler handler1 = new HttpClientHandler();
            CookieContainer cookies1 = new CookieContainer();
            cookies1.SetCookies(uri, cookievalue);
            handler1.CookieContainer = cookies1;
            var client1 = new HttpClient(handler1);
            var uri3 = new Uri("http://localhost:5000/home/AdminPage");
            var request3 = new HttpRequestMessage(HttpMethod.Get, uri3);
            var response3 = client1.SendAsync(request3).Result;
            if (response3.IsSuccessStatusCode)
            {
                Console.WriteLine(response3.IsSuccessStatusCode);
            }
            Console.WriteLine(response3.Content.ReadAsStringAsync().Result);
            var responseCookies1 = cookies1.GetCookies(uri).Cast<Cookie>();
            Console.WriteLine($"responseCookies1：{ responseCookies1.FirstOrDefault().Value}");
            Console.WriteLine($"cookievalue :{cookievalue}");
        }
    }
}
