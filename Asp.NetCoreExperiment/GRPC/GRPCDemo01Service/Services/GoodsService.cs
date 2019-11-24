using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Grpc.Core;
using GRPCDemo01ServiceEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace GRPCDemo01Service
{
    [Authorize("Permission")]
    public class GoodsService : Goodser.GoodserBase
    {
        private readonly ILogger<GoodsService> _logger;
        readonly PermissionRequirement _requirement;
        public GoodsService(ILogger<GoodsService> logger, PermissionRequirement requirement)
        {
            _requirement = requirement;
            _logger = logger;
        }
        public override Task<QueryResponse> GetGoods(QueryRequest request, ServerCallContext context)
        {
            return Task.FromResult(new QueryResponse
            {
                Name = "Hello " + request.Name,
                Quantity = 10
            });
        }
        [AllowAnonymous]
        public override Task<LoginResponse> Login(LoginRequest user, ServerCallContext context)
        {

            var isValidated = user.Username == "gsw" && user.Password == "111111";
            if (!isValidated)
            {
                return Task.FromResult(new LoginResponse()
                {
                    Message = "认证失败"
                });
            }
            else
            {
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new Claim[] {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString())
                };

                var token = JwtToken.BuildJwtToken(claims, _requirement);
                return Task.FromResult(new LoginResponse()
                {
                    Result = true,
                    Token = token.access_token
                });

            }
        }
    }
}
