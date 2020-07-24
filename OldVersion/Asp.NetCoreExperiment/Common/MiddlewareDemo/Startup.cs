using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using SimpleInjector.Integration.AspNetCore.Mvc;
using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace MiddlewareDemo
{
    public class Startup
    {
        SimpleInjector.Container _container=new Container ();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            //第二种
            //services.AddTransient<FactoryActivatedMiddleware>();



            //第三种
            IntegrateSimpleInjector(services);
            services.AddTransient<IMiddlewareFactory>(_ =>
            {
                return new SimpleInjectorMiddlewareFactory(_container);
            });        
            _container.Register<SimpleInjectorActivatedMiddleware>();
           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }
        private void IntegrateSimpleInjector(IServiceCollection services)
        {
            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IControllerActivator>(
                new SimpleInjectorControllerActivator(_container));
            services.AddSingleton<IViewComponentActivator>(
                new SimpleInjectorViewComponentActivator(_container));
            services.EnableSimpleInjectorCrossWiring(_container);
            services.UseSimpleInjectorAspNetRequestScoping(_container);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //第一种 使用中间件
            //app.UseRequestCulture();

            //第二种 使用工厂激活中间件
            //app.UseFactoryActivatedMiddleware();

            //第三种
            InitializeContainer(app);
            app.UseSimpleInjectorActivatedMiddleware();
            _container.Verify();

            app.UseMvc();
        }

        private void InitializeContainer(IApplicationBuilder app)
        {
            // Add application presentation components:
            _container.RegisterMvcControllers(app);
            _container.RegisterMvcViewComponents(app);

            // Add application services. For instance:
            //_container.Register<IUserService, UserService>(Lifestyle.Scoped);

            // Allow Simple Injector to resolve services from ASP.NET Core.
            _container.AutoCrossWireAspNetComponents(app);
        }
    }
    #region 第一种普通方式定义中间件
    //定义中间件
    public class RequestCultureMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestCultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cultureQuery = context.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                var culture = new CultureInfo(cultureQuery);
                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }
            // Call the next delegate/middleware in the pipeline
            await _next(context);
        }
    }
    //扩展中间件
    public static class RequestCultureMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCulture(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestCultureMiddleware>();
        }
    }
    #endregion

    #region 第二种工厂方式定义中间件
    public class FactoryActivatedMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var cultureQuery = context.Request.Query["culture"];
            if (!string.IsNullOrWhiteSpace(cultureQuery))
            {
                var culture = new CultureInfo(cultureQuery);
                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }
            Console.WriteLine("---------------FactoryActivatedMiddleware");
            // Call the next delegate/middleware in the pipeline
            await next(context);
        }
    }
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseFactoryActivatedMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FactoryActivatedMiddleware>();
        }
    }
    #endregion

    #region 第三种自定义容器中间件
    public class SimpleInjectorMiddlewareFactory : IMiddlewareFactory
    {
        private readonly Container _container;

        public SimpleInjectorMiddlewareFactory(Container container)
        {
            _container = container;
        }

        public IMiddleware Create(Type middlewareType)
        {
            return _container.GetInstance(middlewareType) as IMiddleware;
        }

        public void Release(IMiddleware middleware)
        {
            // The container is responsible for releasing resources.
        }
    }

    public class SimpleInjectorActivatedMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("SimpleInjectorActivatedMiddleware 中间件 InvokeAsync");

            await next(context);
        }
    }
    public static class SimpleInjectorMiddlewareExtensions
    {
        public static IApplicationBuilder UseSimpleInjectorActivatedMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SimpleInjectorActivatedMiddleware>();
        }
    }
    #endregion



}
