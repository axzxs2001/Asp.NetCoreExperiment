using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.TextToImage;

#pragma warning disable

var key = File.ReadAllText("c:/gpt/key.txt");
//var kernel = Kernel.CreateBuilder()
//    .AddOpenAIChatCompletion("gpt-4o", key)
//    .Build();

//var chat = kernel.GetRequiredService<IChatCompletionService>();
//var chatHistory = new ChatHistory();
//chatHistory.AddUserMessage("请生成一张猫咪在樱花树下喝茶的图片。");

//var settings = new OpenAIPromptExecutionSettings
//{
//    MaxTokens = 1000,
//    Temperature = 0.7
//};

//var content = chat.GetStreamingChatMessageContentsAsync(chatHistory, settings);
//await foreach (var item in content)
//{
//    Console.Write(item.Content);
//}
var builder = Kernel.CreateBuilder()
       .AddOpenAITextToImage( 
           modelId: "dall-e-3",
           apiKey: key);

var kernel = builder.Build();
var service = kernel.GetRequiredService<ITextToImageService>();

var generatedImages = await service.GetImageContentsAsync(
    new TextContent("生成东京晴空塔动漫图，黑白线条图风格"),
    new OpenAITextToImageExecutionSettings { Size = (Width: 1792, Height: 1024) });

Console.WriteLine(generatedImages[0].Uri!.ToString());