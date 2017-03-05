using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Asp.NetCore_WebPage.Middleware
{
    /// <summary>
    /// 权限中间件
    /// </summary>
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _login;
        /// <summary>
        /// 权限中间件构造
        /// </summary>
        /// <param name="next"></param>
        public PermissionMiddleware(RequestDelegate next,string login)
        {
            _login = login;
            _next = next;
        }
        /// <summary>
        /// 调用管道
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {
            var l = _login;
            var cookie = context.Request.Cookies["login"];
            if (cookie == null)
            {
                if (context.Request.Path.Value == _login&& context.Request.Method == "POST")
                {
                    if (context.Request.Form["username"] == "aaa" && context.Request.Form["password"] == "111")
                    {
                        var user = new { ID = 1, UserName = "zsf", Password = "111", Name = "张三丰", RoleTypeID = 1, RoleType = "admin", RoleTypeName = "管理员" };
                        if (user != null)
                        { 
                            context.Response.Cookies.Append("login", "aaa111", new CookieOptions() {  });
                        }
                        else
                        {
                            context.Response.Redirect(_login);
                        }
                    }
                }
                else
                {
                    context.Response.Redirect(_login);

                }
            }
            return this._next(context);
        }
    }
}
