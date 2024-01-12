using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;


var key = File.ReadAllText(@"C:\GPT\key.txt");
const string ImageUri = "https://github.com/axzxs2001/Asp.NetCoreExperiment/blob/master/Asp.NetCoreExperiment/SemanticKernel/GPTVision/b.png?raw=true";

var kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion("gpt-4-vision-preview", key)
    .Build();

var chatGPT = kernel.GetRequiredService<IChatCompletionService>();

var chatHistory = new ChatHistory("You are a friendly assistant.");

chatHistory.AddUserMessage(new ChatMessageContentItemCollection
        {
            new TextContent("请用是或不是回签，这张图片是日本的在留卡吗。"),
            new ImageContent(new Uri(ImageUri))
        });

var reply = await chatGPT.GetChatMessageContentAsync(chatHistory, new OpenAIPromptExecutionSettings() { MaxTokens = 1000 });
Console.WriteLine(reply.Content);

while (true)
{
    Console.WriteLine("您输入问题：");
    chatHistory.AddUserMessage(Console.ReadLine());
    reply = await chatGPT.GetChatMessageContentAsync(chatHistory, new OpenAIPromptExecutionSettings() { MaxTokens = 1000 });
    chatHistory.Add(reply);
    await MessageStreamOutputAsync(chatGPT, chatHistory);
    Console.WriteLine();
}



Console.ReadKey();

static async Task MessageStreamOutputAsync(IChatCompletionService chatGPT, ChatHistory chatHistory)
{
    var first = true;
    AuthorRole? role = AuthorRole.Assistant;
    var fullMessage = string.Empty;
    var list = chatGPT.GetStreamingChatMessageContentsAsync(chatHistory, new OpenAIPromptExecutionSettings() { MaxTokens = 1000 });
    await foreach (var item in list)
    {
        if (item == null)
        {
            continue;
        }
        if (first)
        {
            role = item.Role;
            first = false;
            Console.Write($"{role}：");
        }
        fullMessage += item.Content;
        Console.Write($"{item.Content}");
    }
    chatHistory.AddMessage(role.Value, fullMessage);
}