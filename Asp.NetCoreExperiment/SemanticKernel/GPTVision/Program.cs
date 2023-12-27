using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;


var key = File.ReadAllText(@"C:\GPT\key.txt");
const string ImageUri = "https://upload.wikimedia.org/wikipedia/commons/d/d5/Half-timbered_mansion%2C_Zirkel%2C_East_view.jpg";

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


var list = chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory);

await foreach (var item in list)
{
    Console.Write($"{item.Content}");
}

//var reply = await chatCompletionService.GetChatMessageContentAsync(chatHistory);
//Console.WriteLine(reply.Content);

Console.ReadKey();