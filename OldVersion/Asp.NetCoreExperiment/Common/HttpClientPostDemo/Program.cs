using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace HttpClientPostDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var request = "http://localhost:5000/login";
            var values = new List<KeyValuePair<string, string>>();
            values.Add(new KeyValuePair<string, string>("userName","gsw"));
            values.Add(new KeyValuePair<string, string>("password","gsw"));
            values.Add(new KeyValuePair<string, string>("returnUrl", "/"));
            var content = new FormUrlEncodedContent(values);           
            var _client = new HttpClient();
           
            var response = _client.PostAsync(request, content).Result;
            response.Headers.TryGetValues("Set-Cookie", out IEnumerable<string> list);

            Console.WriteLine(list);
        }
    }
}
