using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.ComponentModel;
using System.Text.Json.Serialization;

Console.WriteLine("回车开始");
Console.ReadLine();

var builder = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(
        modelId: "deepseek-chat", // 使用的DeepSeek模型ID
        endpoint: new Uri("https://api.deepseek.com/v1"), // DeepSeek API的Endpoint
        apiKey: File.ReadAllText("c://gpt/deepseek.txt") // 替换为你的实际API Key
    );

builder.Plugins.AddFromObject(new CurretnTime()); // 注册自定义函数
var kernel = builder.Build();
var chatService = kernel.GetRequiredService<IChatCompletionService>();
PromptExecutionSettings settings = new() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() };

var chatHistory = new ChatHistory();
chatHistory.AddUserMessage("下周三是几号");

var reply = chatService.GetStreamingChatMessageContentsAsync(chatHistory, settings,kernel);
await foreach (var message in reply)
{
    Console.Write(message.Content);
}

Console.ReadLine();


public class CurretnTime
{
    [KernelFunction("GetCurrentTime")]
    [Description("获取当前时间")]
    public string GetCurretnTime()
    {
        return DateTime.Now.ToString("yyyy年MM月dd日");
    }
}