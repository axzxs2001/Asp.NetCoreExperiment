using System;
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
        public override async Task<OrderResponse> GetGoods(OrderRequest request, ServerCallContext context)
        {
            //µÇÂ¼
            var tokenResponse = await _client.LoginAsync(
                             new LoginRequest() {
                                 Username = "gsw",
                                 Password = "111111" 
                             });
            if (tokenResponse.Result)
            {
                var token = $"Bearer {tokenResponse.Token }";
                var headers = new Metadata { { "Authorization", token } };
                //²éÑ¯
                var query = await _client.GetGoodsAsync(
                                  new QueryRequest { Name = "¹ðËØÎ°" }, headers);
                Console.WriteLine($"·µ»ØÖµ  Name:{ query.Name},Quantity:{ query.Quantity}");
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
