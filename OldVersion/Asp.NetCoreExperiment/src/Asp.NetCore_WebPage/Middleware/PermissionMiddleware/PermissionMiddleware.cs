using Asp.NetCore_WebPage.Model.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
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
        /// <summary>
        /// 管道代理对象
        /// </summary>
        private readonly RequestDelegate _next;
        /// <summary>
        /// 权限中间件的配置选项
        /// </summary>
        private readonly PermissionMiddlewareOption _option;

        /// <summary>
        /// 权限仓储对象
        /// </summary>
        IPermissionResitory _permissionResitory;

        /// <summary>
        /// 权限中间件构造
        /// </summary>
        /// <param name="next">管道代理对象</param>
        /// <param name="permissionResitory">权限仓储对象</param>
        /// <param name="option">权限中间件配置选项</param>
        public PermissionMiddleware(RequestDelegate next, IPermissionResitory permissionResitory, PermissionMiddlewareOption option)
        {
            _option = option;
            _permissionResitory = permissionResitory;
            _next = next;
        }
        /// <summary>
        /// 调用管道
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task Invoke(HttpContext context)
        {             
            //过滤客户端文件和无权限页面
            if (!Path.HasExtension(context.Request.Path.Value)&& context.Request.Path.Value != _option.NoPermissionAction&&context.Request.Path.Value!=@"/ws")
            {
                var cookie = context.Request.Cookies["login"];
                if (cookie == null)
                {
                    if (context.Request.Path.Value == _option.LoginAction)
                    {
                        if (context.Request.Method == "POST")
                        {
                            var userName = context.Request.Form["username"];
                            var password = context.Request.Form["password"];
                            //验证用户名密码
                            var user = _permissionResitory.ValidateUser(userName, password);
                            if (user != null)
                            {
                                if (user != null)
                                {
                                    //添加cookie
                                    context.Response.Cookies.Append("login", user.ID.ToString(), new CookieOptions() { Path="/" });
                                }
                                else
                                {
                                    context.Response.Redirect(_option.LoginAction);
                                }
                            }
                        }
                    }
                    else
                    {
                        context.Response.Redirect(_option.LoginAction);
                    }
                }
                else
                {
                    //验证权限
                    var permissions = _permissionResitory.GetPermissionByUserID(Convert.ToInt32(cookie));
                    var actions = permissions.Select(s => s.ActionName).ToList<string>();
                    if (!actions.Contains(context.Request.Path.Value))
                    {
                        context.Response.Redirect(_option.NoPermissionAction);
                    }
                }
            }
            return this._next(context);
        }
    }
}
