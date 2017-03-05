using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Middleware
{
    /// <summary>
    /// 权限中间件
    /// </summary>
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 权限中间件构造
        /// </summary>
        /// <param name="next"></param>
        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        /// <summary>
        /// 调用管道
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            var cultureQuery = context.Request.Query["culture"];
            
            // 调用下件中间件
            return this._next(context);
        }
    }
}
