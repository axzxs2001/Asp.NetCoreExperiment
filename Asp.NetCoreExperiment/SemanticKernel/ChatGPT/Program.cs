

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

await OpenAIChatSampleAsync();

static async Task OpenAIChatSampleAsync()
{
    Console.WriteLine("======== Open AI - ChatGPT ========");
    var key = File.ReadAllText(@"C:\GPT\key.txt");
    var chatModelId = "gpt-4";
    OpenAIChatCompletionService chatCompletionService = new(chatModelId, key);
    await StartChatAsync(chatCompletionService);
}

static async Task StartChatAsync(IChatCompletionService chatGPT)
{
    Console.WriteLine("开始聊天:");
    Console.WriteLine("------------------------");

    var chatHistory = new ChatHistory("你是一个聊天机器人");

    while (true)
    {
        Console.WriteLine("您输入问题：");
        chatHistory.AddUserMessage(Console.ReadLine());
        var reply = await chatGPT.GetChatMessageContentAsync(chatHistory);
        await MessageStreamOutputAsync(chatGPT, chatHistory);
        chatHistory.Add(reply);
        Console.WriteLine();
    }
}

static async Task MessageStreamOutputAsync(IChatCompletionService chatGPT, ChatHistory chatHistory)
{
    var list = chatGPT.GetStreamingChatMessageContentsAsync(chatHistory);
    var first = true;
    AuthorRole? role = AuthorRole.Assistant;
    var fullMessage = string.Empty;
    await foreach (var item in list)
    {
        if (first)
        {
            role = item.Role;
            first = false;
            Console.Write($"{role}：");
        }
        if (item == null)
        {
            continue;
        }
        fullMessage += item.Content;
        Console.Write($"{item.Content}");
    }
    chatHistory.AddMessage(role.Value, fullMessage);
}
