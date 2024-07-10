using Azure.AI.OpenAI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

var chatModelId = "gpt-4o";
//var geminiModelId = "gemini-1.5-flash-latest";
//var geminiKey = File.ReadAllText(@"C:\GPT\gemini.txt");
var key = File.ReadAllText(@"C:\GPT\key.txt");
#pragma warning disable SKEXP0070
#pragma warning disable SKEXP0010
#pragma warning disable SKEXP0001
#pragma warning disable SKEXP0110
var kernel = Kernel.CreateBuilder()
   .AddOpenAIChatCompletion(chatModelId, key)
   //.AddGoogleAIGeminiChatCompletion(geminiModelId, geminiKey)
   .AddOpenAIFiles(apiKey: key)
   .Build();

var chat = kernel.GetRequiredService<IChatCompletionService>();
var chatHistory = new ChatHistory();// new ChatHistory(systemMessage: "你是一位.net高级讲师，回答问题言简意赅。");
var fileSev = kernel.GetRequiredService<OpenAIFileService>();
var refFile = await fileSev.UploadContentAsync(new BinaryContent(File.ReadAllBytes(@"C:\GPT\202103121100.docx"), "application/octet-stream"), new OpenAIFileUploadExecutionSettings("202103121100.docx", OpenAIFilePurpose.Assistants));


chatHistory.Add(new ChatMessageContent()
{
    Role = AuthorRole.User,
    Items = [
    new FileReferenceContent(refFile.Id) 
    ]
});




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
    chatHistory.AddUserMessage("参会的人都有谁");
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
    Console.ReadLine();
}