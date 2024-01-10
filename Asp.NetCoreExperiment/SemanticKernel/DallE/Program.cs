// See https://aka.ms/new-console-template for more information
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.TextToImage;
using Microsoft.SemanticKernel;

await OpenAIDallEAsync();
Console.ReadLine();

static async Task OpenAIDallEAsync()
{
    var chatModelId = "gpt-4";
    var key = File.ReadAllText(@"C:\GPT\key.txt");
#pragma warning disable SKEXP0012
    Kernel kernel = Kernel.CreateBuilder()
        .AddOpenAITextToImage(key) // Add your text to image service
        .AddOpenAIChatCompletion(chatModelId, key) // Add your chat completion service
        .Build();
#pragma warning disable SKEXP0002
    ITextToImageService dallE = kernel.GetRequiredService<ITextToImageService>();
    var width = 512;

    var imageDescription = "画一个科技感十足的AI机器人，有ChatGPT标志。";
    var image = await dallE.GenerateImageAsync(imageDescription, width, width);

    Console.WriteLine(imageDescription);
    Console.WriteLine("Image URL: " + image);

    Console.ReadLine();
    Console.WriteLine("======== 用图片聊天 ========");

    var chatGPT = kernel.GetRequiredService<IChatCompletionService>();
    var chatHistory = new ChatHistory(
       "您正在与用户聊天。 不要直接回复用户，而是提供表达您想说内容的图像描述。 用户不会看到您的消息，他们只会看到图像。 系统会根据您的描述生成图像，因此请务必详细描述图像。");

    var msg = "你好，我来自东京，你来自哪里？";
    chatHistory.AddUserMessage(msg);
    Console.WriteLine("User: " + msg);

    var reply = await chatGPT.GetChatMessageContentAsync(chatHistory);
    chatHistory.Add(reply);
    image = await dallE.GenerateImageAsync(reply.Content!, width, width);
    Console.WriteLine("Bot: " + image);
    Console.WriteLine("Img description: " + reply);


    msg = "哦，不知道在哪里，您能在地图上帮我标注吗？";
    chatHistory.AddUserMessage(msg);
    Console.WriteLine("User: " + msg);
    reply = await chatGPT.GetChatMessageContentAsync(chatHistory);
    chatHistory.Add(reply);
    image = await dallE.GenerateImageAsync(reply.Content!, width, width);
    Console.WriteLine("Bot: " + image);
    Console.WriteLine("Img description: " + reply);

}