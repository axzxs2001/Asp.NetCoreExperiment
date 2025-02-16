using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Ollama;
using OpenAI.Chat;
using System.ComponentModel;

#pragma warning disable
var modelId = "deepseek-r1:1.5b";
var endpoint = new Uri("http://localhost:11434");
var builder = Kernel.CreateBuilder();
builder.Services.AddOllamaChatCompletion(modelId, endpoint);
var kernel = builder.Build();
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
var streamContent = chatCompletionService.GetStreamingChatMessageContentsAsync("出几道勾股定理的题");
await foreach (var content in streamContent)
{
    Console.Write(content.Content);
}




//builder.Plugins.AddFromType<TimePlugin>();
//var settings = new OllamaPromptExecutionSettings { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() };
//Console.Write("> ");

//string? input = null;
//while ((input = Console.ReadLine()) is not null)
//{
//    Console.WriteLine();

//    try
//    {
//        ChatMessageContent chatResult = await chatCompletionService.GetChatMessageContentAsync(input, settings, kernel);
//        Console.Write($"\n>>> Result: {chatResult}\n\n> ");
//    }
//    catch (Exception ex)
//    {
//        Console.WriteLine($"Error: {ex.Message}\n\n> ");
//    }
//}

//public class TimePlugin
//{
//    [KernelFunction("getCurrentTime")]
//    [Description("获取当前时间")]
//    [return: Description("An array of lights")]
//    public async Task<string> GetCurrentTime()
//    {
//        return DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒");
//    }
//}