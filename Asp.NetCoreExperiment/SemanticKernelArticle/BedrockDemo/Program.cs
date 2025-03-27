

using Amazon;
using Amazon.BedrockRuntime;
using Amazon.Runtime;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

#pragma warning disable
var text=File.ReadAllText("c://gpt/bedrock.txt");
var accesskeyID = text.Split(',')[0];
var secretAccessKey = text.Split(',')[1];
var modelID = text.Split(',')[2];

var runtime =new AmazonBedrockRuntimeClient(accesskeyID,secretAccessKey,RegionEndpoint.USWest2);
var kernel = Kernel.CreateBuilder()
           .AddBedrockChatCompletionService(modelID, runtime).Build();
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
var chatHistory = new ChatHistory();
chatHistory.AddUserMessage("你是谁？你有什么功能，你是那个模型？用200字回答。");
var result = chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory);
await foreach (var message in result)
{
    Console.Write($"{message.Content}");
}

Console.ReadLine();