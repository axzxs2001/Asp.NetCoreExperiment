using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Token_WebAPI01
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {           
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
                ClockSkew = TimeSpan.Zero
            };
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            services.AddAuthorization(options =>
            {
                //这个集合模拟用户权限表,可从数据库中查询出来
                var permission = new List<Permission> {
                              new Permission {  Url="/", Name="admin"},
                              new Permission {  Url="/api/values", Name="admin"},
                              new Permission {  Url="/", Name="system"},
                              new Permission {  Url="/api/values1", Name="system"}
                          };
                //如果第三个参数，是ClaimTypes.Role，上面集合的每个元素的Name为角色名称，如果ClaimTypes.Name，即上面集合的每个元素的Name为用户名
                var permissionRequirement = new PermissionRequirement("/api/denied", permission, ClaimTypes.Role, audienceConfig["Issuer"], audienceConfig["Audience"], signingCredentials);
                options.AddPolicy("Permission",
                          policy => policy.Requirements.Add(permissionRequirement));
            }).AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                //不使用https
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = tokenValidationParameters;
            });
            //注入授权Handler
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }           
            app.UseMvc();
        }
    }
}
