using Grpc.Core;
using Grpc.Net.Client;
using GRPCDemo01Entity;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GRPCDemo01Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("密码：");
                var password = Console.ReadLine();
                var tokenResponse = await Login("gsw", password);
                if (tokenResponse.Result)
                {
                    var token = $"Bearer {tokenResponse.Token }";
                    var headers = new Metadata { { "Authorization", token } };
                    var channel = GrpcChannel.ForAddress("https://localhost:5001");
                    var client = new Goodser.GoodserClient(channel);
                    var query = await client.GetGoodsAsync(
                                      new QueryRequest { Name = "桂素伟" }, headers);
                    Console.WriteLine($"返回值  Name:{ query.Name},Quantity:{ query.Quantity}");
                }
                else
                {
                    Console.WriteLine("登录失败");
                }
            }
        }

        static async Task<LoginResponse> Login(string userName, string password)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Goodser.GoodserClient(channel);
            var response = await client.LoginAsync(
                              new LoginRequest() { Username = userName, Password = password });
            return response;
        }
    }
}
