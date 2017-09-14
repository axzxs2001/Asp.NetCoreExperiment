using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Token_WebAPI01
{
    /// <summary>
    /// 权限授权Handler
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// 用户权限
        /// </summary>
        public List<Permission> Permissions { get; set; }

        public IAuthenticationSchemeProvider Schemes { get; set; }


        public PermissionRequirement Requirement
        { get; set; }

        public PermissionHandler(
        IAuthenticationSchemeProvider schemes)
        {
            Schemes = schemes;
        }

        string _path = "/Api/Token";

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            ////赋值用户权限
            Permissions = requirement.Permissions;
            Requirement = requirement;

            ////从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
            //var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext).HttpContext;
            ////请求Url
            //var questUrl = httpContext.Request.Path.Value.ToLower();
            ////是否经过验证
            //var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
            //if (isAuthenticated)
            //{
            //    //权限中是否存在请求的url
            //    if (Permissions.GroupBy(g => g.Url).Where(w => w.Key.ToLower() == questUrl).Count() > 0)
            //    {
            //        var name = httpContext.User.Claims.SingleOrDefault(s => s.Type == requirement.ClaimType).Value;                   
            //        //验证权限
            //        if (Permissions.Where(w => w.Name == name && w.Url.ToLower() == questUrl).Count() > 0)
            //        {
            //            context.Succeed(requirement);
            //        }
            //        else
            //        {
            //            //无权限跳转到拒绝页面
            //            httpContext.Response.Redirect(requirement.DeniedAction);
            //        }
            //    }
            //    else
            //    {
            //        context.Succeed(requirement);
            //    }
            //}
            //return Task.CompletedTask;
            var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext).HttpContext;

            httpContext.Features.Set<IAuthenticationFeature>(new AuthenticationFeature
            {
                OriginalPath = httpContext.Request.Path,
                OriginalPathBase = httpContext.Request.PathBase
            });
            //获取默认Scheme（或者AuthorizeAttribute指定的Scheme）的AuthenticationHandler
            var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
            //foreach (var scheme in await Schemes.GetRequestHandlerSchemesAsync())
            foreach (var scheme in Schemes.GetRequestHandlerSchemesAsync().Result)
            {
                //var handler = await handlers.GetHandlerAsync(httpContext, scheme.Name) as IAuthenticationRequestHandler;
                var handler = handlers.GetHandlerAsync(httpContext, scheme.Name).Result as IAuthenticationRequestHandler;
                //if (handler != null && await handler.HandleRequestAsync())
                if (handler != null && handler.HandleRequestAsync().Result)
                {
                    context.Fail();
                    return Task.CompletedTask;
                }
            }
            //var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
            var defaultAuthenticate = Schemes.GetDefaultAuthenticateSchemeAsync().Result;
            if (defaultAuthenticate != null)
            {
               // var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                var result =  httpContext.AuthenticateAsync(defaultAuthenticate.Name).Result;
                if (result?.Principal != null)
                {
                    httpContext.User = result.Principal;
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
            //


            if (!httpContext.Request.Path.Equals(_path.ToLower(), StringComparison.Ordinal))
            {
                context.Fail();
                //context.Succeed(requirement);
                return Task.CompletedTask;
            }
            // Request must be POST with Content-Type: application/x-www-form-urlencoded
            if (!httpContext.Request.Method.Equals("POST")
               || !httpContext.Request.HasFormContentType)
            {
                //await ReturnBadRequest(httpContext);
                // ReturnBadRequest(httpContext) ;
                context.Fail();
                return Task.CompletedTask;
            }

            //await GenerateAuthorizedResult(httpContext, requirement);
            // GenerateAuthorizedResult(httpContext, requirement);
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        /// <summary>
        /// 验证结果并得到token
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private  Task GenerateAuthorizedResult(HttpContext context, PermissionRequirement requirement)
        {
            var username = context.Request.Form["username"];
            var password = context.Request.Form["password"];

            var identity =  GetIdentity(username, password);
            if (identity == null)
            {
              return   ReturnBadRequest(context);            
            }

            // Serialize and return the response
            context.Response.ContentType = "application/json";
            var token = GetJwt(username, requirement);
            return  context.Response.WriteAsync(token);
        }

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            var isValidated = username == "gsw" && password == "111111";

            if (isValidated)
            {
                return Task.FromResult(new ClaimsIdentity(new System.Security.Principal.GenericIdentity(username, "Token"), new Claim[] { }));

            }
            return Task.FromResult<ClaimsIdentity>(null);
        }

        /// <summary>
        /// return the bad request (200)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task ReturnBadRequest(HttpContext context)
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Status = false,
                Message = "认证失败"
            }));
        }

        /// <summary>
        /// get the jwt
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private string GetJwt(string username, PermissionRequirement requirement)
        {
            var now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(),
                          ClaimValueTypes.Integer64),
                //用户名
                new Claim(ClaimTypes.Name,username),
                //角色
                new Claim(ClaimTypes.Role,"admin")
            };

            var jwt = new JwtSecurityToken(
                issuer: requirement.Issuer,
                audience: requirement.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(5000)),
                signingCredentials: requirement.SigningCredentials
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                Status = true,
                access_token = encodedJwt,
                expires_in = 5000 * 60,
                token_type = "Bearer"
            };
            return JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented });
        }
    }
}
