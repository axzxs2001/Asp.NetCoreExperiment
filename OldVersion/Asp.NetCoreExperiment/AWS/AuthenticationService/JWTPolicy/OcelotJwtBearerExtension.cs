using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace JWTAuthorizePolicy
{
    /// <summary>
    /// Ocelot下JwtBearer扩展
    /// </summary>
    public static class OcelotJwtBearerExtension
    {
        /// <summary>
        /// 注入Ocelot下JwtBearer，在ocelot网关的Startup的ConfigureServices中调用
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="issuer">发行人</param>
        /// <param name="audience">订阅人</param>
        /// <param name="secret">密钥</param>
        /// <param name="defaultScheme">默认架构</param>
        /// <param name="isHttps">是否https</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddOcelotJwtBearer(this IServiceCollection services, string issuer, string audience, string secret, string defaultScheme, bool isHttps = false)
        {
            var keyByteArray = Encoding.ASCII.GetBytes(secret);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = issuer,//发行人
                ValidateAudience = true,
                ValidAudience = audience,//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };
            return services.AddAuthentication(options =>
            {
                options.DefaultScheme = defaultScheme;
            })
             .AddJwtBearer(defaultScheme, opt =>
             {
                 //不使用https
                 opt.RequireHttpsMetadata = isHttps;
                 opt.TokenValidationParameters = tokenValidationParameters;
             });
        }

        /// <summary>
        /// 注入Ocelot jwt策略，在业务API服务应用中的Startup的ConfigureServices调用
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="issuer">发行人</param>
        /// <param name="audience">订阅人</param>
        /// <param name="secret">密钥</param>
        /// <param name="defaultScheme">默认架构</param>
        /// <param name="policyName">自定义策略名称</param>
        /// <param name="deniedUrl">拒绝路由</param>
        /// <param name="isHttps">是否https</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddOcelotPolicyJwtBearer(this IServiceCollection services, string issuer, string audience, string secret, string defaultScheme, string policyName, string deniedUrl, bool isHttps = false)
        {

            var keyByteArray = Encoding.ASCII.GetBytes(secret);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = issuer,//发行人
                ValidateAudience = true,
                ValidAudience = audience,//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,

            };
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            //如果第三个参数，是ClaimTypes.Role，上面集合的每个元素的Name为角色名称，如果ClaimTypes.Name，即上面集合的每个元素的Name为用户名
            var permissionRequirement = new PermissionRequirement(
               deniedUrl,
                ClaimTypes.Role,
                issuer,
                audience,
                signingCredentials,
                expiration: TimeSpan.FromHours(10)
                );
            //注入授权Handler
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton(permissionRequirement);
            return services.AddAuthorization(options =>
            {
                options.AddPolicy(policyName,
                          policy => policy.Requirements.Add(permissionRequirement));

            })
         .AddAuthentication(options =>
         {
             options.DefaultScheme = defaultScheme;
         })
         .AddJwtBearer(defaultScheme, o =>
         {
             //不使用https
             o.RequireHttpsMetadata = isHttps;
             o.TokenValidationParameters = tokenValidationParameters;
         });
        }
        /// <summary>
        /// 注入验证项目的Startup ConfigureServices中使用
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        /// <param name="issuer">发行人</param>
        /// <param name="audience">订阅人</param>
        /// <param name="secret">密钥</param>
        /// <param name="deniedUrl">拒绝路由</param>
        /// <returns></returns>
        public static IServiceCollection AddJTokenBuild(this IServiceCollection services, string issuer, string audience, string secret,  string deniedUrl)
        {
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)), SecurityAlgorithms.HmacSha256);
            //如果第三个参数，是ClaimTypes.Role，上面集合的每个元素的Name为角色名称，如果ClaimTypes.Name，即上面集合的每个元素的Name为用户名
            var permissionRequirement = new PermissionRequirement(
               deniedUrl,
                ClaimTypes.Role,
                issuer,
                audience,
                signingCredentials,
                expiration: TimeSpan.FromHours(10)
                );
            return services.AddSingleton(permissionRequirement);

        }

    }
}
