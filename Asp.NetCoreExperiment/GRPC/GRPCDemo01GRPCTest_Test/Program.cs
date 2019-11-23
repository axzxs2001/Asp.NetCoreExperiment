using Grpc.Net.Client;
using GRPCDemo01GRPCTest;
using System;
using System.Threading.Tasks;

namespace GRPCDemo01GRPCTest_Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5002");
            var client = new Orderer.OrdererClient(channel);
            var query = await client.GetGoodsAsync(
                              new OrderRequest { Name = "张三" });
            Console.WriteLine($"Greeting返回值  Name:{ query.Name},Quantity:{ query.Quantity}");
            Console.ReadKey();
        }
    }
}
