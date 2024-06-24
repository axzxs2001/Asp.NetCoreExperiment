using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

var chatModelId = "gpt-4o";
var key = File.ReadAllText(@"C:\GPT\key.txt");

var kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(chatModelId, key)
   .Build();

var chatHistory = new ChatHistory(systemMessage: "")
{

};
var chat = kernel.GetRequiredService<IChatCompletionService>();
chatHistory.AddUserMessage("Hi, I'm looking for new power tools, any suggestion?");


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

var reply = await chat.GetChatMessageContentAsync(chatHistory, settings);