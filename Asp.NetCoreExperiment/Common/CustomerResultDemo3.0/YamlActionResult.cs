using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace CustomerResultDemo3._0
{
    public class YamlResult : ActionResult
    {
        public object Value { get; private set; }
        public YamlResult(object value)
        {
            Value = value;
        }
        public override async Task ExecuteResultAsync(ActionContext context)
        {
            var services = context.HttpContext.RequestServices;
            var executor = services.GetRequiredService<IActionResultExecutor<YamlResult>>();
            await executor.ExecuteAsync(context, new YamlResult(this));
        }
    }
    public class YamlResultExecutor<T> : IActionResultExecutor<T> where T : YamlResult
    {
        public async Task ExecuteAsync(ActionContext context, T result)
        {
            var serialize = new YamlDotNet.Serialization.Serializer();
            var valueString = serialize.Serialize(result.Value);
            context.HttpContext.Response.ContentType = "Content-Type: text/html; charset=utf-8";
            await context.HttpContext.Response.WriteAsync(valueString);
        }
    }
}
