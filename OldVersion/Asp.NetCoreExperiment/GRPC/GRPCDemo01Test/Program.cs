using Grpc.Core;
using Grpc.Net.Client;
using GRPCDemo01Entity;
using System;
using System.Threading.Tasks;

namespace GRPCDemo01Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("用户名：");
                var username = Console.ReadLine();
                Console.WriteLine("密码：");
                var password = Console.ReadLine();
                var tokenResponse = await Login(username, password);
                if (tokenResponse.Result)
                {
                    await Query(tokenResponse.Token);
                }
                else
                {
                    Console.WriteLine("登录失败");
                }
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        static async Task Query(string token)
        {
            token = $"Bearer {token }";
            var headers = new Metadata { { "Authorization", token } };
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Goodser.GoodserClient(channel);
            var query = await client.GetGoodsAsync(
                              new QueryRequest { Name = "桂素伟" }, headers);
            Console.WriteLine($"返回值  Name:{ query.Name},Quantity:{ query.Quantity}");
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName">userName</param>
        /// <param name="password">password</param>
        /// <returns></returns>
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
