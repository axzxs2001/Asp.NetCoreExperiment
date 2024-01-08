using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.ComponentModel;

var key = File.ReadAllText(@"C:\GPT\key.txt");

ILoggerFactory myLoggerFactory = NullLoggerFactory.Instance;
var builder = Kernel.CreateBuilder();
builder.Plugins.AddFromType<TimeInformation>();
builder.Services.AddSingleton(myLoggerFactory);

builder.AddOpenAIChatCompletion(
         "gpt-3.5-turbo",
         key);

var kernel = builder.Build();




// Add the handlers to the kernel
#pragma warning disable SKEXP0004 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
kernel.PromptRendering += MyRenderingHandler;
kernel.PromptRendered += MyRenderedHandler;

KernelArguments arguments = new() { { "card_number", "4444 3333 2222 1111" } };
Console.WriteLine(await kernel.InvokePromptAsync("告诉我有关该信用卡号的一些有用信息 {{$card_number}}?", arguments));

Console.ReadLine();
//案例
#pragma warning disable SKEXP0004 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。
void MyRenderingHandler(object? sender, PromptRenderingEventArgs e)
{
    if (e.Arguments.ContainsName("card_number"))
    {
        e.Arguments["card_number"] = "**** **** **** ****";
    }
}
// Handler which is called after a prompt is rendered
void MyRenderedHandler(object? sender, PromptRenderedEventArgs e)
{
    e.RenderedPrompt += " 没有性别歧视、种族主义或其他偏见/偏执";

    Console.WriteLine(e.RenderedPrompt);
}
#pragma warning disable SKEXP0004 // 类型仅用于评估，在将来的更新中可能会被更改或删除。取消此诊断以继续。


//案例

//string chatPrompt = @"
//            <message role=""user"">What is Seattle?</message>
//            <message role=""system"">Respond with JSON.</message>
//        ";
//Console.WriteLine(await kernel.InvokePromptAsync(chatPrompt));



//案例

// Example 1. Invoke the kernel with a prompt that asks the AI for inromation it cannot provide and may hallucinate
//Console.WriteLine(await kernel.InvokePromptAsync("中国春节还有多少天?"));

// Example 2. Invoke the kernel with a templated prompt that invokes a plugin and display the result
//Console.WriteLine(await kernel.InvokePromptAsync("现在是{{TimeInformation.GetCurrentUtcTime}}. 中国春节还有多少天?"));

// Example 3. Invoke the kernel with a prompt and allow the AI to automatically invoke functions
//OpenAIPromptExecutionSettings settings = new() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };
//Console.WriteLine(await kernel.InvokePromptAsync("现在是什么日期？中国春节还有多少天? 解释你的思考.", new(settings)));




/// <summary>
/// A plugin that returns the current time.
/// </summary>
public class TimeInformation
{
    [KernelFunction]
    [Description("返回一个UTC时间")]
    public string GetCurrentUtcTime() => DateTime.UtcNow.ToString("R");
}