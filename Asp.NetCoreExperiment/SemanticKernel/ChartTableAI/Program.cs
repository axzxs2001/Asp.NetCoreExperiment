
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;


var ApiKey = File.ReadAllText(@"C:\GPT\key.txt");
var ChatModelId = "gpt-4o";
var chatGPT = new OpenAIChatCompletionService(ChatModelId, ApiKey);


var chatHistory = new ChatHistory("你是一个高级图表助理，能根据用户提示生成实用图表。");
chatHistory.AddUserMessage(@"把下面的Json以 chartjs 图表的形式表示出来，最好能体现占比。Json数据如下：
[
    {
        ""name"": ""A"", 
        ""quantity"": 20
    }, 
    {
        ""name"": ""B"", 
        ""quantity"": 36
    }, 
    {
        ""name"": ""C"", 
        ""quantity"": 14
    }, 
    {
        ""name"": ""D"", 
        ""quantity"": 33
    }
]");
var reply = await chatGPT.GetChatMessageContentAsync(chatHistory);
Console.WriteLine(reply.Content);