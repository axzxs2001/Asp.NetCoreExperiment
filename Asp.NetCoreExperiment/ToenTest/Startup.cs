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
using Microsoft.Extensions.PlatformAbstractions;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ToenTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = new JWTTokenOptions();
        }

        public IConfiguration Configuration { get; }

        JWTTokenOptions _tokenOptions;
   
        public void ConfigureServices(IServiceCollection services)
        {
            // 从文件读取密钥
            string keyDir = PlatformServices.Default.Application.ApplicationBasePath;



           // RSAUtils.GenerateAndSaveKey(keyDir);
            if (RSAUtils.TryGetKeyParameters(keyDir, true, out RSAParameters keyparams) == false)
            {
                _tokenOptions.Key = default(RsaSecurityKey);
            }
            else
            {
                _tokenOptions.Key = new RsaSecurityKey(keyparams);
            }
            _tokenOptions.Issuer = "TestIssuer"; // 设置签发者
            _tokenOptions.Audience = "TestAudience"; // 设置签收者，也就是这个应用服务器的名称
            _tokenOptions.Credentials = new SigningCredentials(_tokenOptions.Key, SecurityAlgorithms.RsaSha256Signature);

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build());
            });

            services.AddAuthentication().AddJwtBearer(jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = _tokenOptions.Key,
                    ValidAudience = _tokenOptions.Audience,
                    ValidIssuer = _tokenOptions.Issuer,
                    ValidateLifetime = true
                };
            });

            // 添加到 IoC 容器
            services.AddSingleton(_tokenOptions);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
