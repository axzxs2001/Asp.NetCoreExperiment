using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class DynamicControllerFeatureProvider : ControllerFeatureProvider
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            var type = typeInfo.AsType();

            if ((!typeof(IDynamicController).IsAssignableFrom(type) && type.BaseType != typeof(Microsoft.AspNetCore.Mvc.Controller))
                || !typeInfo.IsPublic || typeInfo.IsAbstract || typeInfo.IsGenericType)
            {
                return false;
            }

            return true;
        }
    }

    public static class DynamicControllerServiceExtension
    {
        public static IServiceCollection AddDyanmicController(this IServiceCollection services, DynamicControllerOptions options = null)
        {
            var partManager = services.GetService<ApplicationPartManager>();
            if (partManager == null)
            {
                throw new InvalidOperationException("请在AddMvc()方法后调用AddDynamicController()");
            }

            partManager.FeatureProviders.Add(new DynamicControllerFeatureProvider());
            if (options == null) options = DynamicControllerOptions.Default;
            services.AddSingleton(typeof(DynamicControllerOptions), options);
            services.Configure<MvcOptions>(o =>
            {
                o.Conventions.Add(new DynamicControllerConvention(services));
            });

            return services;
        }

        public static TService GetService<TService>(this IServiceCollection services) where TService : class
        {
            var service = services.FirstOrDefault(s => s.ServiceType == typeof(TService));
            if (service != null)
                return service.ImplementationInstance as TService;

            return null;
        }
    }
    public interface IDynamicController
    {
    }
    public class DynamicControllerConvention : IApplicationModelConvention
    {
        /// <summary>
        /// ServiceCollection
        /// </summary>
        private readonly IServiceCollection _serviceCollection;

        /// <summary>
        /// DynamicControllerOptions
        /// </summary>
        private readonly DynamicControllerOptions _dynamicControllerOptions;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="serviceCollection">ServiceCollection</param>
        public DynamicControllerConvention(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
            _dynamicControllerOptions = serviceCollection.GetService<DynamicControllerOptions>();
        }

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                var controllerType = controller.ControllerType.AsType();
                var controllerAttributes = controllerType.GetCustomAttributes(typeof(DynamicControllerAttribute), false);
                DynamicControllerAttribute controllerAttribute = null;
                if (controllerAttributes != null && controllerAttributes.Any())
                    controllerAttribute = controllerAttributes[0] as DynamicControllerAttribute;
                if (typeof(IDynamicController).IsAssignableFrom(controllerType))
                {
                    ConfigureApiExplorer(controller);
                    ConfigureSelector(controller, controllerAttribute);
                    ConfigureParameters(controller);
                }
            }
        }

        private void ConfigureApiExplorer(ControllerModel controller)
        {
            if (string.IsNullOrEmpty(controller.ApiExplorer.GroupName))
                controller.ApiExplorer.GroupName = controller.ControllerName;

            if (controller.ApiExplorer.IsVisible == null)
                controller.ApiExplorer.IsVisible = true;

            controller.Actions.ToList().ForEach(action => ConfigureApiExplorer(action));
        }

        private void ConfigureApiExplorer(ActionModel action)
        {
            if (action.ApiExplorer.IsVisible == null)
                action.ApiExplorer.IsVisible = true;
        }

        private void ConfigureSelector(ControllerModel controller, DynamicControllerAttribute controllerAttribute)
        {
            if (_dynamicControllerOptions.UseFriendlyControllerName)
            {
                var suffixsToRemove = _dynamicControllerOptions.RemoveControllerSuffix;
                if (suffixsToRemove != null && suffixsToRemove.Any())
                    suffixsToRemove.ToList().ForEach(suffix => controller.ControllerName = controller.ControllerName.Replace(suffix, ""));
            }

            controller.Selectors.ToList().RemoveAll(selector =>
                selector.AttributeRouteModel == null && (selector.ActionConstraints == null || !selector.ActionConstraints.Any())
            );

            if (controller.Selectors.Any(selector => selector.AttributeRouteModel != null))
                return;

            var areaName = string.Empty;
            if (controllerAttribute != null)
                areaName = controllerAttribute.AreaName;

            controller.Actions.ToList().ForEach(action => ConfigureSelector(areaName, controller.ControllerName, action));
        }

        private void ConfigureSelector(string areaName, string controllerName, ActionModel action)
        {
            action.Selectors.ToList().RemoveAll(selector =>
                selector.AttributeRouteModel == null && (selector.ActionConstraints == null || !selector.ActionConstraints.Any())
            );

            if (!action.Selectors.Any())
            {
                action.Selectors.Add(CreateActionSelector(areaName, controllerName, action));
            }
            else
            {
                action.Selectors.ToList().ForEach(selector =>
                {
                    var routePath = $"{_dynamicControllerOptions.DefaultApiRoutePrefix}/{areaName}/{controllerName}/{action.ActionName}".Replace("//", "/");
                    var routeModel = new AttributeRouteModel(new Microsoft.AspNetCore.Mvc.RouteAttribute(routePath));
                    if (selector.AttributeRouteModel == null || !_dynamicControllerOptions.UseCustomRouteFirst)
                        selector.AttributeRouteModel = routeModel;
                });

            }
        }

        private void ConfigureParameters(ControllerModel controller)
        {
            controller.Actions.ToList().ForEach(action => ConfigureActionParameters(action));
        }

        private void ConfigureActionParameters(ActionModel action)
        {
            foreach (var parameter in action.Parameters)
            {
                if (parameter.BindingInfo != null)
                    continue;

                var type = parameter.ParameterInfo.ParameterType;
                if (type.IsPrimitive || type.IsEnum ||
                    (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)))
                {
                    if (IsFromBodyEnable(action, parameter))
                    {
                        parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                    }
                }
            }
        }

        private SelectorModel CreateActionSelector(string areaName, string controllerName, ActionModel action)
        {
            var selectorModel = new SelectorModel();
            var actionName = action.ActionName;
            var routeAttributes = action.ActionMethod.GetCustomAttributes(typeof(HttpMethodAttribute), false);
            if (routeAttributes != null && routeAttributes.Any())
            {
                var httpVerbs = routeAttributes.SelectMany(s => (s as HttpMethodAttribute).HttpMethods).ToList().Distinct();
                var routePath = $"{_dynamicControllerOptions.DefaultApiRoutePrefix}/{areaName}/{controllerName}/{action.ActionName}".Replace("//", "/");
                selectorModel.AttributeRouteModel = new AttributeRouteModel(new Microsoft.AspNetCore.Mvc.RouteAttribute(routePath));
                selectorModel.ActionConstraints.Add(new HttpMethodActionConstraint(httpVerbs));
                return selectorModel;
            }
            else
            {
                var httpVerb = string.Empty;
                var methodName = action.ActionMethod.Name.ToUpper();
                if (methodName.StartsWith("GET") || methodName.StartsWith("QUERY"))
                {
                    httpVerb = "GET";
                }
                else if (methodName.StartsWith("SAVE"))
                {
                    httpVerb = "POST";
                }
                else if (methodName.StartsWith("UPDATE"))
                {
                    httpVerb = "PUT";
                }
                else if (methodName.StartsWith("DELETE"))
                {
                    httpVerb = "DELETE";
                }
                var routePath = $"api/{areaName}/{controllerName}/{action.ActionName}".Replace("//", "/");
                selectorModel.AttributeRouteModel = new AttributeRouteModel(new Microsoft.AspNetCore.Mvc.RouteAttribute(routePath));
                selectorModel.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { httpVerb }));
                return selectorModel;
            }
        }

        private bool IsFromBodyEnable(ActionModel action, ParameterModel parameter)
        {
            foreach (var selector in action.Selectors)
            {
                if (selector.ActionConstraints == null)
                    continue;

                var httpMethods = new string[] { "GET", "DELETE", "TRACE", "HEAD" };
                var actionConstraints = selector.ActionConstraints
                    .Select(ac => ac as HttpMethodActionConstraint)
                    .Where(ac => ac != null)
                    .SelectMany(ac => ac.HttpMethods).Distinct().ToList();
                if (actionConstraints.Any(ac => httpMethods.Contains(ac)))
                    return false;
            }

            return true;
        }
    }
    public class DynamicControllerOptions
    {
        /// <summary>
        /// 是否优先使用自定义路由
        /// </summary>
        public bool UseCustomRouteFirst { get; set; } = true;

        /// <summary>
        /// 是否使用RESTful风格的Action
        /// </summary>
        public bool UseRestfulActionName { get; set; } = true;

        /// <summary>
        /// 是否使用友好的控制器名称
        /// </summary>
        public bool UseFriendlyControllerName { get; set; } = true;

        /// <summary>
        /// 默认Api路由名称前缀
        /// </summary>
        public string DefaultApiRoutePrefix { get; set; } = "api";

        /// <summary>
        /// 默认删除的控制器名称后缀
        /// </summary>
        public string[] RemoveControllerSuffix { get; set; } = new string[] { "Controller", "Service", "AppService" };

        /// <summary>
        /// 默认配置
        /// </summary>
        public static DynamicControllerOptions Default => new DynamicControllerOptions();
    }


    [Serializable]
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class DynamicControllerAttribute : Attribute
    {
        public string AreaName { get; set; }
    }
}
