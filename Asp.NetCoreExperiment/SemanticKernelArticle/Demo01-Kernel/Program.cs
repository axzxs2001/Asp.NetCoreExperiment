using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Services;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;

var chatModelId = "gpt-4o";
var key = File.ReadAllText(@"C:\GPT\key.txt");
var endpoint = "";
#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0070
var builder = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(chatModelId, key);
//.AddAzureOpenAIChatCompletion(chatModelId, endpoint, key) 
//.AddGoogleAIGeminiChatCompletion(chatModelId, key) 
//.AddHuggingFaceChatCompletion(chatModelId, apiKey: key) 
//.AddMistralChatCompletion(chatModelId, key) 
//builder.Services.AddSingleton<IAIServiceSelector>(new GptAIServiceSelector());
builder.Services.AddScoped<IAIServiceSelector, GptAIServiceSelector>();
builder.Services.AddScoped<IMyService, MyService>();
builder.Services.AddLogging(c => c
.AddConsole()
//.AddJsonConsole()
.SetMinimumLevel(LogLevel.Information));
Kernel kernel = builder.Build();

var logger = kernel.LoggerFactory.CreateLogger("logger");
var prompt = "你好，你能帮我做什么";
var result = await kernel.InvokePromptAsync(prompt);
var usage = result.Metadata["Usage"] as Azure.AI.OpenAI.CompletionsUsage;
if (usage != null)
{
    var tokenStr = @$"====================Tokens==================
提示词Tokens数：{usage.PromptTokens}
返回内容Tokens数：{usage.CompletionTokens}
总Tokens数：{usage.TotalTokens}
===========================================";
    logger.LogInformation(tokenStr);
}
var message = @$"返回信息：
{result.GetValue<string>()}";
logger.LogInformation(message);


class GptAIServiceSelector : IAIServiceSelector
{
    private readonly IMyService _myService;
    private readonly ILogger<GptAIServiceSelector> _logger;
    public GptAIServiceSelector(IMyService myService, ILogger<GptAIServiceSelector> logger)
    {
        _myService = myService;
        _logger = logger;
    }
    public bool TrySelectAIService<T>(
        Kernel kernel, KernelFunction function, KernelArguments arguments,
        [NotNullWhen(true)] out T? service, out PromptExecutionSettings? serviceSettings) where T : class, IAIService
    {
        _myService.Print();
        foreach (var serviceToCheck in kernel.GetAllServices<T>())
        {
            var serviceModelId = serviceToCheck.GetModelId();
            var endpoint = serviceToCheck.GetEndpoint();
            if (!string.IsNullOrEmpty(serviceModelId))
            {
                _logger.LogInformation($"使用的模型: {serviceModelId} {endpoint}");
                _logger.LogInformation($"服务类型: {serviceToCheck.GetType().Name}");
                service = serviceToCheck;
                serviceSettings = new OpenAIPromptExecutionSettings();
                return true;
            }
        }
        service = null;
        serviceSettings = null;
        return false;
    }
}

interface IMyService
{
    void Print();
}
class MyService : IMyService
{
    readonly ILogger<MyService> _logger;
    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
        logger.LogInformation("MyService实例化");
    }
    public void Print()
    {
        _logger.LogWarning("开始报警");
    }
}