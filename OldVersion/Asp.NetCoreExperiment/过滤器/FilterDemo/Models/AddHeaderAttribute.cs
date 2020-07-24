using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace FilterDemo.Models
{
    public class AddHeaderAttribute : ResultFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        public AddHeaderAttribute(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("AddHeaderAttribute的action进来了");
            context.HttpContext.Response.Headers.Add(
                _name, new string[] { _value });
            base.OnResultExecuting(context);
        }
    }

    /// <summary>
    /// 注入action特性 在ConfigService中注入
    /// </summary>
    //public class AllAddAttribute : ResultFilterAttribute
    //{

    //    private readonly ILogger<AllAddAttribute> _logger;
    //    public AllAddAttribute(ILogger<AllAddAttribute> logger)
    //    {
    //        _logger = logger;
    //    }

    //    public override void OnResultExecuting(ResultExecutingContext context)
    //    {

    //        _logger.LogInformation("AllAddAttribute的action进来了");
    //    }
    //}

    /// <summary>
    /// 注入action特性 在ConfigService中注入
    /// </summary>
    public class AllAddAttribute : ActionFilterAttribute
    {

        private readonly ILogger<AllAddAttribute> _logger;
        public AllAddAttribute(ILogger<AllAddAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {


            _logger.LogInformation("AllAddAttribute的action进来了  OnResultExecuting  url:" + context.HttpContext.Request.Path);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {          
            var parmeters = context.ActionArguments;
            _logger.LogInformation("AllAddAttribute的action进来了  OnActionExecuting  url:" + context.HttpContext.Request.Path);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {       
            
            switch (context.Result)
            {
                case JsonResult json:

                    break;
                case ViewResult view:
                    break;
            }
            _logger.LogInformation("AllAddAttribute的action进来了  OnActionExecuted  url:" + context.HttpContext.Request.Path);
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _logger.LogInformation("AllAddAttribute的action进来了  OnActionExecutionAsync  url:" + context.HttpContext.Request.Path);

            return next();
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogInformation("AllAddAttribute的action进来了  OnResultExecuted  url:" + context.HttpContext.Request.Path);
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            _logger.LogInformation("AllAddAttribute的action进来了  OnResultExecuted  url:" + context.HttpContext.Request.Path);
            return next();
        }
    }
}
