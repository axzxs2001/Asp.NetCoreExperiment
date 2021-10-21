
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
using Yarp.ReverseProxy.Configuration;

namespace YARPDemo01
{
    /// <summary>
    /// 权限授权Handler
    /// </summary>
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        readonly List<Permission> _userPermissions;
        readonly IProxyConfigProvider _proxyConfig;
        public PermissionHandler(List<Permission> userPermissions)
        {
            _userPermissions = userPermissions;
        }
        public PermissionHandler(List<Permission> userPermissions, IProxyConfigProvider proxyConfig)
        {
            _proxyConfig = proxyConfig;
            _userPermissions = userPermissions;
        }

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
            var memoryProvider = (_proxyConfig as InMemoryConfigProvider);
            var fileContext = (context.Resource as HttpContext);
            var questUrl = fileContext?.Request?.Path.Value?.ToLower();
            var method = fileContext?.Request?.Method;

            //赋值用户权限
            UserPermissions = _userPermissions;
            //是否经过验证
            var isAuthenticated = context?.User?.Identity?.IsAuthenticated;
            if (isAuthenticated.HasValue && isAuthenticated.Value)
            {
                #region 处理配置文件
                if (questUrl.Contains("add"))
                {
                    var name = questUrl.Split('/', StringSplitOptions.RemoveEmptyEntries)[2];
                    var port = questUrl.Split('/', StringSplitOptions.RemoveEmptyEntries)[3];
                    var routes = new List<RouteConfig>(memoryProvider.GetConfig().Routes);
                    routes.Add(new RouteConfig()
                    {
                        RouteId = name,
                        ClusterId = $"{name}_cluster",
                        AuthorizationPolicy = "Permission",
                        Match = new RouteMatch
                        {
                            Path = $"/{name}/{{**catch-all}}"
                        }
                    });


                    var clusters = new List<ClusterConfig>(memoryProvider.GetConfig().Clusters);
                    clusters.Add(new ClusterConfig()
                    {
                        ClusterId = $"{name}_cluster",
                        Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
                    {
                        { $"{name}_cluster/destination", new DestinationConfig() { Address = $"https://localhost:{port}/" } }
                    }
                    });

                    memoryProvider.Update(routes, clusters);
                }
                if (questUrl.Contains("delete"))
                {
                    var name = questUrl.Split('/', StringSplitOptions.RemoveEmptyEntries)[2];
                    var routes = new List<RouteConfig>();
                    foreach (var route in memoryProvider.GetConfig().Routes)
                    {
                        if (route.RouteId != name)
                        {
                            routes.Add(route);
                        }
                    }
                    var clusters = new List<ClusterConfig>();
                    foreach (var cluster in memoryProvider.GetConfig().Clusters)
                    {
                        if (cluster.ClusterId != name)
                        {
                            clusters.Add(cluster);
                        }
                    }
                    memoryProvider.Update(routes, clusters);
                }
                #endregion

                if (UserPermissions.Where(w => w.Url.ToLower() == questUrl).Count() > 0)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            return Task.CompletedTask;
        }
    }
}