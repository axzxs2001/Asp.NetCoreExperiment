using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;


namespace WebApiError
{
    //--1、UseExceptionHandler方式
    /// <summary>
    /// 
    /// </summary>
    public class DaMeiProblemDetailsFactory : ProblemDetailsFactory
    {
        /// <summary>
        /// 处理普通错误
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="statusCode"></param>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <param name="detail"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override ProblemDetails CreateProblemDetails(HttpContext httpContext, int? statusCode = null, string title = null, string type = null, string detail = null, string instance = null)
        {
            var problem = new ProblemDetails()
            {
                Title = string.IsNullOrEmpty(type) ? title : $"业务异常错误：{title}",
                Detail = detail,
                Status = statusCode,
                Instance = instance,
                Type = type

            };
            return problem;
        }
        /// <summary>
        /// 处理model验证错误
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="modelStateDictionary"></param>
        /// <param name="statusCode"></param>
        /// <param name="title"></param>
        /// <param name="type"></param>
        /// <param name="detail"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public override ValidationProblemDetails CreateValidationProblemDetails(HttpContext httpContext, ModelStateDictionary modelStateDictionary, int? statusCode = null, string title = null, string type = null, string detail = null, string instance = null)
        {
            var problem = new ValidationProblemDetails()
            {
                Title = "Model验证错误",
                Detail = detail,
                Status = statusCode,
                Instance = instance,
                Type = type
            };
            foreach (var a in modelStateDictionary)
            {
                var errorList = new List<string>();
                foreach (var error in a.Value.Errors)
                {
                    errorList.Add(error.ErrorMessage);
                }
                problem.Errors.Add(new KeyValuePair<string, string[]>(a.Key, errorList.ToArray()));
            }
            return problem;
        }
    }
}