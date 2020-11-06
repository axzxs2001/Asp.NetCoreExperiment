using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiError
{
    /// <summary>
    /// 自定义过滤器处理异常
    /// </summary>
    public class DaMeiExceptionFilter : IActionFilter, IOrderedFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public int Order { get; } = int.MaxValue - 10;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

            if (context?.Exception != null)
            {
                if (context.Exception is DaMeiException)
                {
                    context.Result = new ObjectResult(context.Exception.Message)
                    {
                        Value = $"业务异常：{ context.Exception.Message}",
                        StatusCode = 500
                    };
                }
                else
                {
                    context.Result = new ObjectResult(context.Exception.Message)
                    {
                        Value = context.Exception.Message,
                        StatusCode = 500
                    };
                }
                context.ExceptionHandled = true;
            }
        }
    }
}
