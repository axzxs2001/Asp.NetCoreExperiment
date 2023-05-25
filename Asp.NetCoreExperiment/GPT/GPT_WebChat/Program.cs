using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;
using System.Collections.Generic;

var key = File.ReadAllText(@"C:\\GPT\key.txt");
var builder = WebApplication.CreateBuilder(args);
var kernel = Kernel.Builder
    .Configure(c =>
    {
        c.AddOpenAIChatCompletionService("gpt-4", key, serviceId: "davinci-openai");
    })
    .Build();
builder.Services.AddSingleton<IKernel>(kernel);

var app = builder.Build();



//app.MapGet("/chat", AskAsync);


app.Run();

//async IAsyncEnumerable<string> AskAsync(string ask)
//{
//    var chatGPT = kernel.GetService<IChatCompletion>();
//    var chatHistory = (OpenAIChatHistory)chatGPT.CreateNewChat();
//    chatHistory.AddUserMessage(ask);
//    var cfg = new ChatRequestSettings();
//    var reply = chatGPT.GenerateMessageStreamAsync(chatHistory, cfg);

//    await reply.ForEachAsync(s =>
//    {
//       // yield return s;
       

//    });


//}

