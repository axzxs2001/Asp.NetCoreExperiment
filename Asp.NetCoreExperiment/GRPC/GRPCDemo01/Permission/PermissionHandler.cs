
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Collections.Generic;

using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace GRPCDemo01
{
    /// <summary>
    /// 权限授权Handler
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {

        /// <summary>
        /// 验证方案提供对象
        /// </summary>
        public IAuthenticationSchemeProvider Schemes { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="schemes"></param>
        public PermissionHandler(IAuthenticationSchemeProvider schemes)
        {
            Schemes = schemes;
        }
        /// <summary>
        /// 验证每次请求
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            if (context.Resource is RouteEndpoint route && route != null)
            {
                var questUrl = route.RoutePattern.RawText?.ToLower();
                if (requirement.Permissions.GroupBy(g => g.Url).Where(w => w.Key.ToLower() == questUrl).Count() > 0)
                {
                    var name = context.User.Claims.SingleOrDefault(s => s.Type == requirement.ClaimType)?.Value;
                    //验证权限
                    if (requirement.Permissions.Where(w => w.Name == name && w.Url.ToLower() == questUrl).Count() > 0)
                    {                        //判断过期时间
                        if (DateTime.Parse(context.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration).Value) >= DateTime.Now)
                        {
                            context.Succeed(requirement);
                            return Task.CompletedTask;
                        }
                    }
                }
            }
            context.Fail();
            return Task.CompletedTask;
        }
    }
}