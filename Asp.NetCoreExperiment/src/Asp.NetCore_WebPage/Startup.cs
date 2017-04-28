using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Asp.NetCore_WebPage.Model;
using Microsoft.EntityFrameworkCore;
using Asp.NetCore_WebPage.Model.Repository;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Swashbuckle.AspNetCore.Swagger;
using Asp.NetCore_WebPage.Middleware;
using Asp.NetCore_WebPage.Model.生成最大编号;

namespace Asp.NetCore_WebPage
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //注入连接字符串类
            services.Configure<ConnectionSetting>(Configuration.GetSection("ConnectionStrings"));
            services.AddTransient<ICreateSN, CreateSN>();
            //添加数据操作
            var connection = Configuration.GetConnectionString("DefaultConnection");
            //services.AddDbContext<ExperimentPageContext>(options => options.UseSqlServer(connection));
            //添加权限模块
            //services.AddTransient<IPermissionResitory, PermissionResitory>();
            //添加验证服务
            services.AddTransient<VierificationCodeServices, VierificationCodeServices>();

            services.AddMvc();

            #region 添加session
            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromSeconds(120000);
                options.CookieHttpOnly = true;
            });
            #endregion


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Asp.NetCore_WebPage",
                    Version = "v1",
                    Description = "Asp.NetCore_WebPage RESTful API ",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "桂素伟",
                        Email = "axzxs2001@163.com"
                    },
                });
                //设置xml注释文档，注意名称一定要与项目名称相同
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "Asp.NetCore_WebPage.xml");
                c.IncludeXmlComments(filePath);
                //处理复杂名称
                c.CustomSchemaIds((type) => type.FullName);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            //添加session
            app.UseSession();

            //添加websocket中间件
            //app.UseWebSockets();
            //app.UseWebSocketNotify();

            //app.UsePermission(new PermissionMiddlewareOption()
            //{
            //    LoginAction = @"/login",
            //     NoPermissionAction=@"/nopermission"
            //});
            app.UseStaticFiles();
            app.UseSwagger(c =>
            {
                //设置json路径
                c.RouteTemplate = "docs/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                //访问swagger UI的路由，如http://localhost:端口/docs
                c.RoutePrefix = "docs";
                c.SwaggerEndpoint("/docs/v1/swagger.json", "Asp.NetCore_WebPage V1");
                //更改UI样式
                c.InjectStylesheet("/swagger-ui/custom.css");
                //引入UI变更js
                c.InjectOnCompleteJavaScript("/swagger-ui/custom.js");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
