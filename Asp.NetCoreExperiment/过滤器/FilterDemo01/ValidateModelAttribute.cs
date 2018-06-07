using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilterDemo01
{
    //第一步：创建action过滤器特性类

    /// <summary>
    /// 验证action的参数的过滤器 用       [ServiceFilter(typeof(ValidateModelAttribute))]方式在Action方法上
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {

        IAAA _aAA;
        public ValidateModelAttribute([FromServices]IAAA aAA = null)
        {
            _aAA = aAA;
        }

        /// <summary>
        /// action执行前验证
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (!context.ModelState.IsValid)
            {
                var messages = new List<string>();
                foreach (var ms in context.ModelState)
                {
                    var key = ms.Key;
                    foreach (var error in ms.Value.Errors)
                    {
                        messages.Add(error.ErrorMessage);
                    }
                }
                context.Result = new JsonResult(new { Result = false, Data = "", Messages = messages.ToArray() });
            }
        }
    }


    /// <summary>
    /// 用  [Validate1Model]方式在Action方法上
    /// </summary>
    public class Validate1ModelAttribute : TypeFilterAttribute
    {
        public Validate1ModelAttribute() : base(typeof(MyFilterAttributeImpl))
        {

        }

        private class MyFilterAttributeImpl : IActionFilter
        {
            private readonly IAAA _sv;

            public MyFilterAttributeImpl(IAAA sv)
            {
                _sv = sv;
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                if (!context.ModelState.IsValid)
                {
                    var messages = new List<string>();
                    foreach (var ms in context.ModelState)
                    {
                        var key = ms.Key;
                        foreach (var error in ms.Value.Errors)
                        {
                            messages.Add(error.ErrorMessage);
                        }
                    }
                    context.Result = new JsonResult(new { Result = false, Data = "", Messages = messages.ToArray() });
                }
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {

            }
        }



    }
}
