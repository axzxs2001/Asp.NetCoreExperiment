
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

namespace YARPDemo01
{
    /// <summary>
    /// 权限授权Handler
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// 用户权限
        /// </summary>
        public List<Permission> UserPermissions { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            //请求的url
            var questUrl = "";
            //请求谓词
            var method = "";
            if (context.Resource is RouteEndpoint)
            {
                var route = (context.Resource as Microsoft.AspNetCore.Routing.RouteEndpoint);
                if (route.RoutePattern.Parameters.Count > 0)
                {
                    questUrl = $"{route.RoutePattern.Defaults["controller"].ToString().ToLower() }/{route.RoutePattern.Defaults["action"].ToString().ToLower()}";
                }
                else
                {
                    questUrl = route.RoutePattern.RawText;
                }
            }
            else
            {
                var fileContext = (context.Resource as AuthorizationFilterContext);
                questUrl = fileContext?.HttpContext?.Request?.Path.Value?.ToLower();
                method = fileContext?.HttpContext?.Request?.Method;
            }
            //赋值用户权限
            UserPermissions = requirement.Permissions;
            //是否经过验证
            var isAuthenticated = context?.User?.Identity?.IsAuthenticated;
            if (isAuthenticated.HasValue && isAuthenticated.Value)
            {
                if (UserPermissions.GroupBy(g => g.Url).Where(w => w.Key.ToLower() == questUrl).Count() > 0)
                {
                    //用户名
                    var userName = context.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid).Value;
                    if (UserPermissions.Where(w => w.Name == userName && w.Url.ToLower() == questUrl).Count() > 0)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        //无权限跳转到拒绝页面
                        context.Fail();
                    }
                }
                else
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}