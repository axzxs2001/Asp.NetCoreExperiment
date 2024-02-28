
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Planning;
using Microsoft.SemanticKernel.Planning.Handlebars;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using System.ComponentModel;
#pragma warning disable SKEXP0061
#pragma warning disable SKEXP0060

//https://github.com/MicrosoftDocs/semantic-kernel-docs/blob/main/samples/dotnet/11-Planner/Program.cs


var chatModelId = "gpt-4";
var key = File.ReadAllText(@"C:\GPT\key.txt");

var builder = Kernel.CreateBuilder();


var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddOpenTelemetry(options =>
    {        
        options.IncludeFormattedMessage = true;
    });
    builder.AddConsole();
    builder.SetMinimumLevel(LogLevel.Information);
});
builder.Services.AddSingleton(loggerFactory);


var meterProvider = Sdk.CreateMeterProviderBuilder()
   .AddMeter("Microsoft.SemanticKernel*")
   .AddPrometheusExporter()  
   .AddPrometheusHttpListener(options => options.UriPrefixes = new string[] { "http://localhost:9465/" })
   .Build();

var traceProvider = Sdk.CreateTracerProviderBuilder()
           .AddSource("Microsoft.SemanticKernel*")
           .AddLegacySource("Microsoft.SemanticKernel*")        
           .Build();


builder.Services.AddOpenAIChatCompletion(chatModelId, key);
builder.Plugins.AddFromType<Dispatch>();
var kernel = builder.Build();

ChatHistory history = [];
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

while (true)
{
    Console.Write("User > ");
    history.AddUserMessage(Console.ReadLine()!);

    OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
    {
        ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
    };

    var result = chatCompletionService.GetStreamingChatMessageContentsAsync(
        history,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);

    string fullMessage = "";
    var first = true;
    await foreach (var content in result)
    {
        if (content.Role.HasValue && first)
        {
            Console.Write("Assistant > ");
            first = false;
        }
        Console.Write(content.Content);
        fullMessage += content.Content;
    }
    Console.WriteLine();

    history.AddAssistantMessage(fullMessage);
}

class Dispatch
{
    [KernelFunction]
    [Description("按照提供的内容，完成具体的工作")]
    public async Task SolveAsync(
      Kernel kernel,
      [Description("按要求一步一步完成下面工作")] string content)
    {
        var kernelWithMath = kernel.Clone();
        kernelWithMath.Plugins.Remove(kernelWithMath.Plugins["Dispatch"]);

        kernelWithMath.Plugins.AddFromType<Work>();

        var planner = new HandlebarsPlanner(new HandlebarsPlannerOptions() { AllowLoops = true });

        var plan = await planner.CreatePlanAsync(kernelWithMath, content);
        Console.WriteLine($"Plan: {plan}");

        var result = (await plan.InvokeAsync(kernelWithMath, [])).Trim();
        Console.WriteLine($"Results: {result}");

    }
}
class Work
{
    [KernelFunction]
    [Description("转成大写")]
    public async Task<string> ToUpper([Description("转成大写的内容")] string content)
    {
        Console.WriteLine("转成大写");
        return await Task.FromResult(content.ToUpper());
    }

    [KernelFunction]
    [Description("转成小写")]
    public async Task<string> ToLower([Description("转成小写的内容")] string content)
    {
        Console.WriteLine("转成小写");
        return await Task.FromResult(content.ToLower());
    }
}
