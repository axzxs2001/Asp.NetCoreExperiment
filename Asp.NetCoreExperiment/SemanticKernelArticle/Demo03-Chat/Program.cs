using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.Runtime.CompilerServices;
using System.Text;

var chatModelId = "gpt-4o";
var geminiModelId = "gemini-1.5-flash-latest";
var geminiKey = File.ReadAllText(@"C:\GPT\gemini.txt");
var key = File.ReadAllText(@"C:\GPT\key.txt");
#pragma warning disable SKEXP0070
var kernel = Kernel.CreateBuilder()
   .AddOpenAIChatCompletion(chatModelId, key)
   .AddGoogleAIGeminiChatCompletion(geminiModelId, geminiKey)
   .Build();

var chatHistory = new ChatHistory(systemMessage: "你是一位.net高级讲师，回答问题言简意赅。");
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
    chatHistory.AddUserMessage(Console.ReadLine());
    Console.WriteLine();

    //var reply = await chat.GetChatMessageContentAsync(chatHistory, settings);
    //Console.ForegroundColor = ConsoleColor.Green;
    //Console.WriteLine("==========讲师回答：==========");
    //Console.WriteLine(reply.Content);
    //chatHistory.AddMessage(reply.Role, reply.Content);
    //Console.WriteLine();


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