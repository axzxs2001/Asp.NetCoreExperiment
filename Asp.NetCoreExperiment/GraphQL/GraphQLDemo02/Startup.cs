using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo02
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
            services.AddPooledDbContextFactory<AdventureWorks2016Context>(
              (services, options) => options
              .UseSqlServer(Configuration.GetConnectionString("ConnectionString"))
              .UseLoggerFactory(services.GetRequiredService<ILoggerFactory>()))
              .AddGraphQLServer()
              .AddQueryType<Query>()
              .AddFiltering()
              .AddSorting()
              .AddProjections();
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
                endpoints.MapGraphQL();
            });
        }
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
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = tokenValidationParameters;

            });
            //注入授权Handler
            services.AddSingleton<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton(permissionRequirement);
        }
    }
}
