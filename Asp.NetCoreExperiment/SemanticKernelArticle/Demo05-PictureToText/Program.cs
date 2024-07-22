using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var chatModelId = "gpt-4o";
var key = File.ReadAllText(@"C:\GPT\key.txt");
#pragma warning disable SKEXP0070
#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0110
var kernel = Kernel.CreateBuilder()
   .AddOpenAIChatCompletion(chatModelId, key)
   .Build();

var chat = kernel.GetRequiredService<IChatCompletionService>();
var chatHistory = new ChatHistory();
//chatHistory.AddUserMessage(new ChatMessageContentItemCollection
//{
//     new TextContent("请说明这是那里，什么样的天气，大家在干什么?一共有多少人"),
//     new ImageContent(File.ReadAllBytes("tam.png"),"image/jpeg")
//});
//var settings = new Dictionary<string, object>
//{
//    ["max_tokens"] = 1000,
//    ["temperature"] = 0.2,
//    ["top_p"] = 0.8,
//    ["presence_penalty"] = 0.0,
//    ["frequency_penalty"] = 0.0
//};

//var content = chat.GetStreamingChatMessageContentsAsync(chatHistory, new PromptExecutionSettings
//{
//    ExtensionData = settings
//});
//await foreach (var item in content)
//{
//    Console.Write(item.Content);
//}
//Console.ReadLine();


chatHistory.AddUserMessage(new ChatMessageContentItemCollection
{
     new TextContent("请识别图片上的文字，并输出"),
     new ImageContent(File.ReadAllBytes("japancard.png"),"image/jpeg")
});
var settings = new Dictionary<string, object>
{
    ["max_tokens"] = 1000,
    ["temperature"] = 0.2,
    ["top_p"] = 0.8,
    ["presence_penalty"] = 0.0,
    ["frequency_penalty"] = 0.0
};

var content = chat.GetStreamingChatMessageContentsAsync(chatHistory, new PromptExecutionSettings
{
    ExtensionData = settings
});
await foreach (var item in content)
{
    Console.Write(item.Content);
}
Console.ReadLine();