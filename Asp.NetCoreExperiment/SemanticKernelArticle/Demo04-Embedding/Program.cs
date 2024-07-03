using Azure.AI.OpenAI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Connectors.Redis;
using Microsoft.SemanticKernel.Memory;
using StackExchange.Redis;
using System.Runtime.CompilerServices;
using System.Text;

var chatModelId = "gpt-4o";
var embeddingId = "text-embedding-ada-002";
var key = File.ReadAllText(@"C:\GPT\key.txt");
#pragma warning disable SKEXP0020
#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
var kernel = Kernel.CreateBuilder()
   .AddOpenAIChatCompletion(chatModelId, key)
   .Build();


var connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(new ConfigurationOptions
{
    EndPoints = { "localhost:6379" },
    ConnectTimeout = 10000,
    ConnectRetry = 3

});
var database = connectionMultiplexer.GetDatabase();
var store = new RedisMemoryStore(database, vectorSize: 1536);
var embeddingGenerator = new OpenAITextEmbeddingGenerationService(embeddingId, key);
var memory = new SemanticTextMemory(store, embeddingGenerator);
var dic = new Dictionary<string, string>
{
    {"name","我的名字是桂素伟" },
    {"age","我今年30岁" },
    {"job","我是一位.net高级讲师" },
    {"experience","我有10年的.net开发经验" },
    {"skill","我精通.net core、asp.net core、微服务、docker、k8s等技术" },
    {"hobby","我喜欢阅读、写作、旅行" },
    {"motto","我的座右铭是：行成于思，毁于随" }
};
foreach (var item in dic)
{
    await memory.SaveInformationAsync("ask", id: item.Key, text: item.Value);
}

var chatHistory = new ChatHistory();
var chat = kernel.GetRequiredService<IChatCompletionService>();

var settings = new PromptExecutionSettings
{
    ExtensionData = new Dictionary<string, object>
    {
        ["max_tokens"] = 1000,
        ["temperature"] = 0.2,
        ["top_p"] = 0.8,
        ["presence_penalty"] = 0.0,
        ["frequency_penalty"] = 0.0

    }
};
while (true)
{
    Console.ResetColor();
    Console.WriteLine("----------学生提问：----------");
    var ask = Console.ReadLine();
    chatHistory.Clear();
    chatHistory.AddSystemMessage("基于下面的信息回复问题：");
    await foreach (var answer in memory.SearchAsync(
        collection: "ask",
        query: ask,
        limit: 3,
        minRelevanceScore: 0.65d,
        withEmbeddings: true))
    {
        chatHistory.AddSystemMessage(answer.Metadata.Text);
    };

    chatHistory.AddUserMessage(ask);
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("==========讲师回答：==========");
    AuthorRole? role = AuthorRole.Assistant;
    var contentBuilder = new StringBuilder();
    await foreach (var reply in chat.GetStreamingChatMessageContentsAsync(chatHistory, settings))
    {
        if (reply.Role.HasValue && role != reply.Role)
        {
            role = reply.Role;
        }
        Console.Write(reply.Content);
        contentBuilder.Append(reply.Content);
    }
    chatHistory.AddMessage(role.Value, contentBuilder.ToString());
    Console.WriteLine();

}