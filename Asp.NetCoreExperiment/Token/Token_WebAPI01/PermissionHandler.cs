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

        //protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        //{
        //    ////赋值用户权限
        //    Permissions = requirement.Permissions;
        //    Requirement = requirement;

        //    //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
        //    var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext).HttpContext;
        //    //请求Url
        //    var questUrl = httpContext.Request.Path.Value.ToLower();
        //    //是否经过验证
        //    var isAuthenticated = httpContext.User.Identity.IsAuthenticated;
        //    if (isAuthenticated)
        //    {
        //        //权限中是否存在请求的url
        //        if (Permissions.GroupBy(g => g.Url).Where(w => w.Key.ToLower() == questUrl).Count() > 0)
        //        {
        //            var name = httpContext.User.Claims.SingleOrDefault(s => s.Type == requirement.ClaimType).Value;
        //            //验证权限
        //            if (Permissions.Where(w => w.Name == name && w.Url.ToLower() == questUrl).Count() > 0)
        //            {
        //                context.Succeed(requirement);


        //            }
        //            else
        //            {
        //                //无权限跳转到拒绝页面
        //                httpContext.Response.Redirect(requirement.DeniedAction);
        //            }
        //        }
        //        else
        //        {
        //            context.Succeed(requirement);
        //        }
        //    }



        //    httpContext.Features.Set<IAuthenticationFeature>(new AuthenticationFeature
        //    {
        //        OriginalPath = httpContext.Request.Path,
        //        OriginalPathBase = httpContext.Request.PathBase
        //    });
        //    //获取默认Scheme（或者AuthorizeAttribute指定的Scheme）的AuthenticationHandler
        //    var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
        //    //foreach (var scheme in await Schemes.GetRequestHandlerSchemesAsync())

        //    foreach (var scheme in Schemes.GetRequestHandlerSchemesAsync().Result)
        //    {
        //        //var handler = await handlers.GetHandlerAsync(httpContext, scheme.Name) as IAuthenticationRequestHandler;
        //        var handler = handlers.GetHandlerAsync(httpContext, scheme.Name).Result as IAuthenticationRequestHandler;
        //        //if (handler != null && await handler.HandleRequestAsync())
        //        if (handler != null && handler.HandleRequestAsync().Result)
        //        {
        //            context.Fail();
        //            return Task.CompletedTask;
        //        }
        //    }
        //    //var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
        //    var defaultAuthenticate = Schemes.GetDefaultAuthenticateSchemeAsync().Result;
        //    if (defaultAuthenticate != null)
        //    {
        //       // var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
        //        var result =  httpContext.AuthenticateAsync(defaultAuthenticate.Name).Result;
        //        if (result?.Principal != null)
        //        {
        //            httpContext.User = result.Principal;
        //            context.Succeed(requirement);
        //            return Task.CompletedTask;
        //        }
        //    }
        //    //


        //    if (!httpContext.Request.Path.Equals(_path.ToLower(), StringComparison.Ordinal))
        //    {
        //        context.Fail();
        //        //context.Succeed(requirement);
        //        return Task.CompletedTask;
        //    }
        //    // Request must be POST with Content-Type: application/x-www-form-urlencoded
        //    if (!httpContext.Request.Method.Equals("POST")
        //       || !httpContext.Request.HasFormContentType)
        //    {
        //        //await ReturnBadRequest(httpContext);
        //        // ReturnBadRequest(httpContext) ;
        //        context.Fail();
        //        return Task.CompletedTask;
        //    }

        //    //await GenerateAuthorizedResult(httpContext, requirement);
        //    // GenerateAuthorizedResult(httpContext, requirement);
        //    context.Succeed(requirement);
        //    return Task.CompletedTask;
        //}



        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {



            ////赋值用户权限
            Permissions = requirement.Permissions;
            Requirement = requirement;

            //从AuthorizationHandlerContext转成HttpContext，以便取出表求信息
            var httpContext = (context.Resource as Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext).HttpContext;

            //请求Url
            var questUrl = httpContext.Request.Path.Value.ToLower();




            #region 
            //获取默认Scheme（或者AuthorizeAttribute指定的Scheme）的AuthenticationHandler
            var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();

            foreach (var scheme in await Schemes.GetRequestHandlerSchemesAsync())
            {
                var handler = await handlers.GetHandlerAsync(httpContext, scheme.Name) as IAuthenticationRequestHandler;
                if (handler != null && await handler.HandleRequestAsync())
                {
                    context.Fail();
                    return;
                }
            }
            var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate != null)
            {
                var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                if (result?.Principal != null)
                {
                    httpContext.User = result.Principal;
                    //权限中是否存在请求的url
                    if (Permissions.GroupBy(g => g.Url).Where(w => w.Key.ToLower() == questUrl).Count() > 0)
                    {
                        var name = httpContext.User.Claims.SingleOrDefault(s => s.Type == requirement.ClaimType).Value;
                        //验证权限
                        if (Permissions.Where(w => w.Name == name && w.Url.ToLower() == questUrl).Count() <= 0)
                        {
                            //无权限跳转到拒绝页面
                            httpContext.Response.Redirect(requirement.DeniedAction);

                        }
                    }
                    context.Succeed(requirement);
                    return;
                }
            }
            if (!httpContext.Request.Path.Equals(_path.ToLower(), StringComparison.Ordinal))
            {
                context.Fail();
                return;
            }

            if (!httpContext.Request.Method.Equals("POST")
               || !httpContext.Request.HasFormContentType)
            {
                context.Fail();
                return;
            }


            context.Succeed(requirement);
            #endregion




        }
    }
}
