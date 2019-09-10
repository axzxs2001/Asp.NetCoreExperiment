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
            CookieContainer cookies = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = cookies;
            HttpClient client = new HttpClient(handler);
            var cookievalue = "";

            var uri = new Uri("http://localhost:5000/login?username=gsw&password=111111");
            while (true)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        var request = new HttpRequestMessage(HttpMethod.Post, uri);
                        var response = client.SendAsync(request).Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var responseCookies = cookies.GetCookies(uri).Cast<Cookie>();
                            cookievalue = responseCookies.FirstOrDefault().Value;
                            Console.WriteLine($"cookies{responseCookies.ToList().Count}");
                            Console.WriteLine(responseCookies.FirstOrDefault().Value);
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

                        HttpClientHandler handler1 = new HttpClientHandler();
                        CookieContainer cookies1 = new CookieContainer();
                        cookies1.SetCookies(uri, cookievalue);
                        handler1.CookieContainer = cookies1;
                        HttpClient client1 = new HttpClient(handler);
                        var uri3 = new Uri("http://localhost:5000/home/AdminPage");
                        var request3 = new HttpRequestMessage(HttpMethod.Get, uri3);
                        var response3 = client1.SendAsync(request3).Result;
                        if (response3.IsSuccessStatusCode)
                        {
                            Console.WriteLine(response3.IsSuccessStatusCode);
                        }
                        Console.WriteLine(response3.Content.ReadAsStringAsync().Result);
                        var responseCookies1 = cookies1.GetCookies(uri).Cast<Cookie>();
                        Console.WriteLine("--"+responseCookies1.FirstOrDefault().Value);
                        Console.WriteLine("====" + cookievalue);
                        break;
                }
            }
        }
    }
}
