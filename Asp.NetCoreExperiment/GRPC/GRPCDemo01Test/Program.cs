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
                    var client = new Greeter.GreeterClient(channel);
                    var reply = await client.SayHelloAsync(
                                      new HelloRequest { Name = "桂素伟" }, headers);
                    Console.WriteLine("Greeting返回值: " + reply.Message);
                }
                else
                {
                    Console.WriteLine("登录失败");
                }
            }
        }

        static async Task<UserTokenResponse> Login(string userName, string password)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Greeter.GreeterClient(channel);
            var response = await client.LoginAsync(
                              new UserRequest { Username = userName, Password = password });
            return response;
        }
    }
}
