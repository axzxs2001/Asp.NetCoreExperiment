using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PolicyPrivilegeManagement.Models
{
    /// <summary>
    /// 用户权限
    /// </summary>
    public class UserPermission
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        { get; set; }
        /// <summary>
        /// 请求Url
        /// </summary>
        public string Url
        { get; set; }
    }
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public static List<UserPermission> UserPermissions { get; set; }

        public string DeniedAction { get; private set; }

        public PermissionRequirement(string deniedAction)
        {
            DeniedAction = deniedAction;
        }
    }
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext).HttpContext;
            //请求Url
            var questUrl = httpContext.Request.Path.Value.ToLower();
            //是否经过验证
            var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                if (PermissionRequirement.UserPermissions.GroupBy(g => g.Url).Where(w => w.Key.ToLower() == questUrl).Count() > 0)
                {
                    //用户名
                    var userName = httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid).Value;
                    if (PermissionRequirement.UserPermissions.Where(w => w.UserName == userName && w.Url.ToLower() == questUrl).Count() > 0)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        //无权限跳转到拒绝页面
                        httpContext.Response.Redirect("/denied");
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
