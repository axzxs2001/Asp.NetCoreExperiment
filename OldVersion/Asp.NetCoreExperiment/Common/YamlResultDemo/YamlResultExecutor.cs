using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YamlResultDemo
{
    /// <summary>
    /// YamlResult执行者
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class YamlResultExecutor<T> : IActionResultExecutor<T> where T : YamlResult
    {
        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context">上下文</param>
        /// <param name="result">值</param>
        /// <returns></returns>
        public async Task ExecuteAsync(ActionContext context, T result)
        {
            var serialize = new YamlDotNet.Serialization.Serializer();
            var valueString = serialize.Serialize(result.Value);
            context.HttpContext.Response.ContentType = "Content-Type: text/html; charset=utf-8";
            await context.HttpContext.Response.WriteAsync(valueString);
        }
    }
}
