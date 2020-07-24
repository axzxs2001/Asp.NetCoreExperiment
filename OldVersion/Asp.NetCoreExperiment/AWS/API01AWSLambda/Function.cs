using System;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace API01AWSLambda
{
    public class Function
    {

        /// <summary>
        ///验证Token的Lambda函数
        /// </summary>
        /// <param name="apigAuthRequest">请求</param>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public APIGatewayCustomAuthorizerResponse FunctionHandler(APIGatewayCustomAuthorizerRequest apigAuthRequest, ILambdaContext context)
        {
            LambdaLogger.Log($"AWS Lambda函数验证Token开始");
            var TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = SecurityConstants.Issuer,
                ValidateAudience = true,
                ValidAudience = SecurityConstants.Audience,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecurityConstants.SecurityKey)),
                ClockSkew = TimeSpan.Zero,
            };
            var authorized = false;
            //删除Bearer再来验证
            var token = apigAuthRequest.AuthorizationToken?.Replace("Bearer ", "");
            if (!string.IsNullOrWhiteSpace(token))
            {
                try
                {
                    SecurityToken validatedToken;
                    var handler = new JwtSecurityTokenHandler();
                    var user = handler.ValidateToken(token, TokenValidationParameters, out validatedToken);
                    var claim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                    if (claim != null)
                    {
                        authorized = claim.Value == SecurityConstants.ClaimName;
                    }
                }
                catch (Exception ex)
                {
                    LambdaLogger.Log($"Error occurred validating token: {ex.Message}");
                }
            }
            var policy = new APIGatewayCustomAuthorizerPolicy
            {
                Version = "2012-10-17",
                Statement = new List<APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement>(),

            };
            policy.Statement.Add(new APIGatewayCustomAuthorizerPolicy.IAMPolicyStatement
            {
                Action = new HashSet<string>(new string[] { "execute-api:Invoke" }),
                Effect = authorized ? "Allow" : "Deny",
                Resource = new HashSet<string>(new string[] { apigAuthRequest.MethodArn })

            });
            var contextOutput = new APIGatewayCustomAuthorizerContextOutput();
            contextOutput["User"] = authorized ? SecurityConstants.ClaimName : "User";
            contextOutput["Path"] = apigAuthRequest.MethodArn;
            LambdaLogger.Log($"AWS Lambda函数验证Token结束");
            return new APIGatewayCustomAuthorizerResponse
            {
                PrincipalID = authorized ? SecurityConstants.ClaimName : "User",
                Context = contextOutput,
                PolicyDocument = policy,
            };
        }
    }
    /// <summary>
    /// 测试用，正式环境可以放在云配置中
    /// </summary>
    public class SecurityConstants
    {
        public const string Issuer = "gsw";
        public const string SecurityKey = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        public const string Audience = "everone";
        public const string Password = "111111";
        public const string ClaimName = "gsw";
    }
}
