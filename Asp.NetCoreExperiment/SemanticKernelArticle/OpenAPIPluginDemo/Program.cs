using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.OpenApi;
using System.IO;
using System.Text.Json;
#pragma warning disable 
var apikey = File.ReadAllText("c:/gpt/key.txt");
using HttpClient httpClient = new();

var kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddOpenAIChatCompletion("gpt-4o", apiKey: apikey);
var kernel = kernelBuilder.Build();

var pluginArr = new List<PluginSetting>
{
    new PluginSetting{PluginName="OrderService",UriString="http://localhost:5000/openapi/v1.json"}
};
foreach (var pluginItem in pluginArr)
{
    var plugin = await OpenApiKernelPluginFactory.CreateFromOpenApiAsync(pluginName: pluginItem.PluginName,
        uri: new Uri(pluginItem.UriString),
        executionParameters: new OpenApiFunctionExecutionParameters(httpClient)
        {
            IgnoreNonCompliantErrors = true,
            EnableDynamicPayload = true,
        });
    kernel.Plugins.Add(plugin);
}
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
var openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};
ChatHistory history = [];
while (true)
{
    Console.WriteLine("回车开始");
    Console.ReadLine();
    Console.WriteLine("用户 > 查询一下订单，然后总汇订单的总金额 ");
    history.AddUserMessage("总汇一下订单的总金额");
    var result = await chatCompletionService.GetChatMessageContentAsync(
        history,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);
    Console.WriteLine("助理 > " + result);
    history.AddMessage(result.Role, result.Content!);
}

public class PluginSetting
{
    public string PluginName { get; set; }
    public string UriString { get; set; }
}
