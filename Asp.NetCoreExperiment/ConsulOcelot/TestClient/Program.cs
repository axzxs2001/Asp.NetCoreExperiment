using System;
using System.Net.Http;


namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var adminClient = new HttpClient();
            adminClient.DefaultRequestHeaders.Add("client_id", "admin");
            var userClient = new HttpClient();
            userClient.DefaultRequestHeaders.Add("client_id", "user");
            while (true)
            {
                Console.WriteLine("请输入a为admin clinet,u为user client,e为退出");
                var clientKey = Console.ReadLine();
                HttpClient client = null;
                switch (clientKey)
                {
                    case "a":
                        client = adminClient;
                        break;
                    case "b":
                        client = userClient;
                        break;
                    case "e":
                        return;
                }
                GetString(client);
            }
        }
        static void GetString(HttpClient client)
        {
            while (true)
            {
                Console.WriteLine("1为api001访问，2为访问api002，e为退出");
                var key = Console.ReadLine();
                try
                {
                    var result = "";
                    switch (key)
                    {
                        case "1":                         
                            result = client.GetStringAsync("http://192.168.1.99:5000/api001/values").GetAwaiter().GetResult();
                            break;
                        case "2":
                            result = client.GetStringAsync("http://192.168.1.99:5000/api002/values").GetAwaiter().GetResult();
                            break;
                        case "e":
                            return;
                    }
                    Console.WriteLine(result);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }
    }
}
