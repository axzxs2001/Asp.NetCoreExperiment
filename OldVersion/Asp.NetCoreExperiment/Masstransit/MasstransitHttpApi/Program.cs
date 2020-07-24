using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MasstransitHttpApi
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "http://guest:guest@localhost:15672/api/queues");
                var byteArray = Encoding.ASCII.GetBytes("guest:guest");
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
                var result = client.SendAsync(request).Result;
                var list = System.Text.Json.JsonSerializer.Deserialize<dynamic>(result.Content.ReadAsStringAsync().Result);

                Console.WriteLine("------------");
                for (var i = 0; i < list.GetArrayLength(); i++)
                {
                    System.Text.Json.JsonElement item = list[i];
                    Console.WriteLine(item.GetProperty("backing_queue_status").GetProperty("len").GetInt32());
                }
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
