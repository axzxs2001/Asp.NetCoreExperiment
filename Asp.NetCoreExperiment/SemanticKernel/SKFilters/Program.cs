using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
#pragma warning disable SKEXP0001
var key = File.ReadAllText(@"C:\GPT\key.txt");
var chatModelId = "gpt-4";

var builder = Kernel.CreateBuilder();

builder.AddOpenAIChatCompletion(chatModelId, key);

builder.Services.AddSingleton<IFunctionInvocationFilter, FirstFunctionFilter>();
builder.Services.AddSingleton<IFunctionInvocationFilter, SecondFunctionFilter>();

var kernel = builder.Build();

var function = kernel.CreateFunctionFromPrompt("What is Seattle", functionName: "MyFunction");
kernel.Plugins.Add(KernelPluginFactory.CreateFromFunctions("MyPlugin", functions: [function]));
var result = await kernel.InvokeAsync(kernel.Plugins["MyPlugin"]["MyFunction"]);

Console.WriteLine(result);


class FirstFunctionFilter : IFunctionInvocationFilter
{

    public async Task OnFunctionInvocationAsync(FunctionInvocationContext context, Func<FunctionInvocationContext, Task> next)
    {
        Console.WriteLine($"{nameof(FirstFunctionFilter)}.FunctionInvoking - {context.Function.PluginName}.{context.Function.Name}");

      
        await next(context);

        Console.WriteLine($"{nameof(FirstFunctionFilter)}.FunctionInvoked - {context.Function.PluginName}.{context.Function.Name}");
    }
}

class SecondFunctionFilter : IFunctionInvocationFilter
{
    public async Task OnFunctionInvocationAsync(FunctionInvocationContext context, Func<FunctionInvocationContext, Task> next)
    {
        Console.WriteLine($"{nameof(SecondFunctionFilter)}.FunctionInvoking - {context.Function.PluginName}.{context.Function.Name}");

        await next(context);

        Console.WriteLine($"{nameof(SecondFunctionFilter)}.FunctionInvoked - {context.Function.PluginName}.{context.Function.Name}");
    }
}