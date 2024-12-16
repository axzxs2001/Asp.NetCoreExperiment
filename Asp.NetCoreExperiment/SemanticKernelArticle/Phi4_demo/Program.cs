using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using OllamaSharp;
using System;
using System.ComponentModel;

#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0070

var ollamaApiClient = new OllamaApiClient(new Uri("http://localhost:11434"), "vanilj/Phi-4:latest");
var builder = Kernel.CreateBuilder();
builder.Services.AddScoped<IChatCompletionService>(_ => ollamaApiClient.AsChatCompletionService());
var kernel = builder.Build();
var chatService = kernel.GetRequiredService<IChatCompletionService>();

//kernel.Plugins.AddFromType<TimePlugin>();
kernel.Plugins.AddFromFunctions("time_plugin",
[
    KernelFunctionFactory.CreateFromMethod(
        method: () => DateTime.Now,
        functionName: "get_time",
        description: "得到当前时间"
    ),
]);

var history = new ChatHistory();
history.AddSystemMessage("你是一个知识渊博的助手");
while (true)
{
    Console.Write("用户：");
    var input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
    {
        break;
    }
    history.AddUserMessage(input);
    var response = chatService.GetStreamingChatMessageContentsAsync(history);
    var content = "";
    var role = AuthorRole.Assistant;
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("助手：");
    await foreach (var message in response)
    {
        Console.Write($"{message.Content}");
        content += message.Content;
        role = message.Role.Value;
    }
    Console.WriteLine();
    Console.ResetColor();
    history.AddMessage(role, content);
}

public class TimePlugin
{
    [KernelFunction("getCurrentTime")]
    [Description("获取当前时间")]
    [return: Description("An array of lights")]
    public async Task<string> GetCurrentTime()
    {
        return DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒");
    }
}

//var uri = new Uri("http://localhost:11434/");
//var ollama = new OllamaApiClient(uri);

//// select a model which should be used for further operations
//ollama.SelectedModel = "vanilj/Phi-4:latest";

//var chat = new Chat(ollama);
//while (true)
//{
//    var message = Console.ReadLine();
//    await foreach (var answerToken in chat.SendAsync(message))
//        Console.Write(answerToken);
//}