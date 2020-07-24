using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HttpClientDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var url = "http://localhost:4444";

            //GetPerson1(url, 1);
            //GetPerson1(url, 2);
            //GetPerson3(url);
            PostPerson2(url);
        }

        static void PostPerson2(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var request = new HttpRequestMessage(HttpMethod.Post, "postperson2");
                request.Content = new StringContent(JsonConvert.SerializeObject(new Person { Name = "桂素伟", Age = 99 }), Encoding.UTF8, "application/json");
                var response = client.SendAsync(request).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    var p = JsonConvert.DeserializeObject<Person>(content);
                    Console.WriteLine("返回值：" + content);
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("返回值：" + content);
                }
            }
        }

        static void PostPerson1(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var request = new HttpRequestMessage(HttpMethod.Post, "postperson1");
                var values = new List<KeyValuePair<string, string>>();
                values.Add(new KeyValuePair<string, string>("Name", "桂素伟"));
                values.Add(new KeyValuePair<string, string>("Age", "99"));
                request.Content = new FormUrlEncodedContent(values);
                var response = client.SendAsync(request).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("返回值：" + content);

                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("返回值：" + content);
                }
            }
        }
        static void GetPerson3(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var request = new HttpRequestMessage(HttpMethod.Get, "getperson3?name=桂素伟&age=99");
                var response = client.SendAsync(request).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("返回值：" + content);

                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("返回值：" + content);
                }
            }
        }
        static void GetPerson1(string url, int no)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var request = new HttpRequestMessage(HttpMethod.Get, "getperson" + no);
                request.Content = new StringContent(JsonConvert.SerializeObject(new Person { Name = "桂素伟", Age = 99 }), Encoding.UTF8, "application/json");
                var response = client.SendAsync(request).Result;
                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("返回值：" + content);

                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("返回值：" + content);
                }
            }
        }
    }
    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
