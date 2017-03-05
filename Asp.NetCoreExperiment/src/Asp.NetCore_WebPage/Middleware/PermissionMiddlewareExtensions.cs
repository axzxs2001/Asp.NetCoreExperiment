using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Middleware
{
    /// <summary>
    /// 扩展权限中间件
    /// </summary>
    public static class PermissionMiddlewareExtensions
    {
        /// <summary>
        /// 引入权限中间件
        /// </summary>
        /// <param name="builder">扩展类型</param>
        /// <param name="login">登录页</param>
        /// <returns></returns>
        public static IApplicationBuilder UsePermission(
              this IApplicationBuilder builder,string login)
        {
            return builder.UseMiddleware<PermissionMiddleware>(login);
        }

    }
}
