using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Text;
using System;
using Yarp.ReverseProxy.Configuration;
using System.Threading;
using Microsoft.Extensions.Primitives;
using System.IO;
using System.Linq;
using Yarp.ReverseProxy.Transforms;

namespace YARPDemo01
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            AddAuth(services);

            var routes = new[]{
                new RouteConfig()
                {
                    RouteId = "webapi01",
                    ClusterId = "webapi01_cluster",
                    //AuthorizationPolicy="Permission",
                  
                    Match = new RouteMatch
                    {
                        Path = "/webapi01/{**catch-all}"
                    }
                },

                new RouteConfig()
                {
                    RouteId = "authservice",
                    ClusterId = "auth_cluster",
                   // AuthorizationPolicy="Permission",
                    Match = new RouteMatch
                    {
                        Path = "/auth/{**catch-all}"
                    }
                },
                new RouteConfig()
                {
                    RouteId = "yarpservice",
                    ClusterId = "yarp_cluster",
                    AuthorizationPolicy="Permission",
                    Match = new RouteMatch
                    {
                        Path = "/yarp/{**catch-all}"
                    }
                }

            };
            var clusters = new[]{
                new ClusterConfig()
                {
                    ClusterId = "webapi01_cluster",
                    Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "webapi01_cluster/destination", new DestinationConfig() { Address ="https://localhost:9001/"} }
                    }
                },
                  new ClusterConfig()
                {
                    ClusterId = "auth_cluster",
                    Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "auth_cluster/destination", new DestinationConfig() { Address = "https://localhost:5001/" } }
                    }
                },
                new ClusterConfig()
                {
                    ClusterId = "yarp_cluster",
                    Destinations = new Dictionary<string, DestinationConfig>(StringComparer.OrdinalIgnoreCase)
                    {
                        { "yarp_cluster/destination", new DestinationConfig() { Address = "https://localhost:6001/" } }
                    }
                }
            };

            services.AddReverseProxy().LoadFromMemory(routes, clusters).AddTransforms(builderContext =>
            {
                //通过不同的body添加不同的header
                builderContext.AddRequestTransform(async transformContext =>
               {
                   var content = await transformContext.ProxyRequest.Content.ReadAsStringAsync();
                   transformContext.ProxyRequest.Headers.Add("X-NSS-UUID", "ABCD" + DateTime.Now.ToString()); 
               });
            });
            // services.AddReverseProxy().LoadFromConfig(Configuration.GetSection("ReverseProxy"));
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }         
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapReverseProxy();
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        void AddAuth(IServiceCollection services)
        {
            //读取配置文件
            var audienceConfig = Configuration.GetSection("Audience");
            var symmetricKeyAsBase64 = audienceConfig["Secret"];
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Issuer"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,

            };
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            //这个集合模拟用户权限表,可从数据库中查询出来
            var permission = new List<Permission> {
                              new Permission {  Url="/webapi01/test1", Name="admin"},
                              new Permission {  Url="/webapi01/test3", Name="admin"},
                              new Permission {  Url="/webapi02/test2", Name="admin"},
                              new Permission {  Url="/webapi02/test4", Name="admin"},
                              new Permission {  Url="/webapi02/test4", Name="admin"},
                          };

            services.AddSingleton(permission);
            //如果第三个参数，是ClaimTypes.Role，上面集合的每个元素的Name为角色名称，如果ClaimTypes.Name，即上面集合的每个元素的Name为用户名
            var permissionRequirement = new PermissionRequirement(
                "/api/denied",
                ClaimTypes.Role,
                audienceConfig["Issuer"],
                audienceConfig["Audience"],
                signingCredentials,
                expiration: TimeSpan.FromSeconds(1000000)//设置Token过期时间
                );

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Permission", policy => policy.AddRequirements(permissionRequirement));
            }).
            AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
            {
                //不使用https
                o.RequireHttpsMetadata = true;
                o.TokenValidationParameters = tokenValidationParameters;
            });
            //注入授权Handler
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton(permissionRequirement);
        }
    }
}
namespace Microsoft.Extensions.DependencyInjection
{
    public static class InMemoryConfigProviderExtensions
    {
        public static IReverseProxyBuilder LoadFromMemory(this IReverseProxyBuilder builder, IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters)
        {
            builder.Services.AddSingleton<IProxyConfigProvider>(new InMemoryConfigProvider(routes, clusters));
            return builder;
        }
    }
}

namespace Yarp.ReverseProxy.Configuration
{
    public class InMemoryConfigProvider : IProxyConfigProvider
    {
        private volatile InMemoryConfig _config;

        public InMemoryConfigProvider(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters)
        {
            _config = new InMemoryConfig(routes, clusters);
        }

        public IProxyConfig GetConfig()
        {
            return _config;
        }

        public void Update(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters)
        {
            var oldConfig = _config;
            _config = new InMemoryConfig(routes, clusters);
            oldConfig.SignalChange();
        }

        private class InMemoryConfig : IProxyConfig
        {
            private readonly CancellationTokenSource _cts = new CancellationTokenSource();

            public InMemoryConfig(IReadOnlyList<RouteConfig> routes, IReadOnlyList<ClusterConfig> clusters)
            {
                Routes = routes;
                Clusters = clusters;
                ChangeToken = new CancellationChangeToken(_cts.Token);
            }

            public IReadOnlyList<RouteConfig> Routes { get; }

            public IReadOnlyList<ClusterConfig> Clusters { get; }

            public IChangeToken ChangeToken { get; }

            internal void SignalChange()
            {
                _cts.Cancel();
            }
        }
    }
}
