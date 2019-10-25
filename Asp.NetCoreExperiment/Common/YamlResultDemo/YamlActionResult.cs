using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace YamlResultDemo
{
    /// <summary>
    /// YamlResul
    /// </summary>
    public class YamlResult : ActionResult
    {
        /// <summary>
        /// Yaml值
        /// </summary>
        public object Value { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="value"></param>
        public YamlResult(object value)
        {
            Value = value;
        }
        /// <summary>
        /// Result执行者
        /// </summary>
        /// <param name="context">上下文</param>
        /// <returns></returns>
        public override async Task ExecuteResultAsync(ActionContext context)
        {
            var services = context.HttpContext.RequestServices;
            var executor = services.GetRequiredService<IActionResultExecutor<YamlResult>>();
            await executor.ExecuteAsync(context, new YamlResult(this));
        }
    }

}
