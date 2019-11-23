using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GRPCDemo01Entity;
using Microsoft.Extensions.Logging;

namespace GRPCDemo01GRPCTest
{
    public class OrderService : Orderer.OrdererBase
    {
        private readonly ILogger<OrderService> _logger;
        private readonly Goodser.GoodserClient _client;
        public OrderService(ILogger<OrderService> logger, Goodser.GoodserClient client)
        {
            _client = client;
            _logger = logger;
        }
        async Task<LoginResponse> Login(string userName, string password)
        {

            var response = await _client.LoginAsync(
                              new LoginRequest() { Username = userName, Password = password });
            return response;
        }
        public override async Task<OrderResponse> GetGoods(OrderRequest request, ServerCallContext context)
        {
            var tokenResponse = await Login("gsw", "111111");
            if (tokenResponse.Result)
            {
                var token = $"Bearer {tokenResponse.Token }";
                var headers = new Metadata { { "Authorization", token } };
                var query = await _client.GetGoodsAsync(
                                  new QueryRequest { Name = "¹ðËØÎ°" }, headers);
                Console.WriteLine($"Greeting·µ»ØÖµ  Name:{ query.Name},Quantity:{ query.Quantity}");
                return new OrderResponse { Name = query.Name, Quantity = query.Quantity };
            }
            else
            {
                Console.WriteLine("µÇÂ¼Ê§°Ü");
                return null;
            }

        }
    }
}
