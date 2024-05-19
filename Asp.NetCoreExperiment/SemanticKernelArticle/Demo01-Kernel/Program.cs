using Microsoft.SemanticKernel;



var chatModelId = "gpt-4";
var key = File.ReadAllText(@"C:\GPT\key.txt");
#pragma warning disable SKEXP0010
Kernel kernel = Kernel.CreateBuilder()
    .AddOpenAITextToImage(key) // Add your text to image service
    .AddOpenAIChatCompletion(chatModelId, key) // Add your chat completion service
    .Build();