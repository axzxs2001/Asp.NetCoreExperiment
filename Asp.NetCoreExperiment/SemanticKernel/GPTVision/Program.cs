using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;


var key = File.ReadAllText(@"C:\GPT\key.txt");
const string ImageUri = "https://github.com/axzxs2001/Asp.NetCoreExperiment/blob/master/Asp.NetCoreExperiment/SemanticKernel/GPTVision/a.png";

var kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion("gpt-4-vision-preview", key)
    .Build();

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

var chatHistory = new ChatHistory("You are a friendly assistant.");

chatHistory.AddUserMessage(new ChatMessageContentItemCollection
        {
            new TextContent("这个图片中有什么"),
            new ImageContent(new Uri(ImageUri))
        });

var reply = await chatCompletionService.GetChatMessageContentAsync(chatHistory);
var list = chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory);

await foreach (var item in list)
{
    Console.Write($"{item.Content}");
}

//
//Console.WriteLine(reply.Content);

Console.ReadKey();