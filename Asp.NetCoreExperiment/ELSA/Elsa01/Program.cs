using Elsa.Activities.Console.Activities;
using Elsa.Activities.Http.Activities;
using Elsa.Activities.Http.Parsers;
using Elsa.Activities.Http.Services;
using Elsa.Expressions;
using Elsa.Scripting.JavaScript;
using Elsa.Services;
using Elsa.Services.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Elsa01
{
    class Program
    {
        async static Task Main(string[] args)
        {
            Console.WriteLine("回车开始");
            Console.ReadLine();
            var services = new ServiceCollection()
              .AddElsa()
              //注入Response返回格式
              .AddScoped<IHttpResponseBodyParser, JsonHttpResponseBodyParser>()
              //注入httpclient工厂
              .AddHttpClient()
              .AddActivity<SendHttpRequest>()
              .AddActivity<WriteLine>()
              .BuildServiceProvider();
            var invoker = services.GetService<IWorkflowInvoker>();
            await invoker.StartAsync<PersonHandleWorkflow>();
            Console.WriteLine("回车结束");
            Console.ReadLine();
        }
    }
    /// <summary>
    /// 定义工作流两个activity,一个是请求webapi，一个是显示请求结果
    /// </summary>
    public class PersonHandleWorkflow : IWorkflow
    {
        public void Build(IWorkflowBuilder builder)
        {
            builder
                .StartWith<SendHttpRequest>(RequestAPI, "http5001")
                .Then<WriteLine>(ShowMessage);
        }

        void ShowMessage(WriteLine write)
        {
            //用javascript脚本来处理返回信息
            write.TextExpression = new JavaScriptExpression<string>("'ID:'+lastResult('http5001').Content[\"id\"]+'  Name:'+lastResult('http5001').Content[\"name\"]+'  Age:'+lastResult('http5001').Content[\"age\"]");
        }
        void RequestAPI(SendHttpRequest http)
        {
            //定义http发送参数
            http.ContentType = "application/json";
            http.Method = "get";
            http.Url = new WorkflowExpression<Uri>(LiteralEvaluator.SyntaxName, "https://localhost:5001/home");
        }
    }
}