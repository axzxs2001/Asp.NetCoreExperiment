using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;
using System.Runtime.CompilerServices;
using System.Text;
var key = File.ReadAllText(@"C:\\GPT\key.txt");
var builder = WebApplication.CreateBuilder(args);
var kernel = Kernel.Builder  
    .WithOpenAIChatCompletionService("gpt-4", key, serviceId: "gsw_chat")
    .Build();
var chatGPT = kernel.GetService<IChatCompletion>();
builder.Services.AddSingleton(chatGPT);
builder.Services.AddSingleton((OpenAIChatHistory)chatGPT.CreateNewChat());
var app = builder.Build();
app.UseStaticFiles();
app.MapGet("/chat", AskAsync);
app.MapPost("/chat", AskPostAsync);
app.Run();
async IAsyncEnumerable<string> AskAsync(IChatCompletion chat, OpenAIChatHistory history, string ask)
{
    history.AddUserMessage(ask);
    var reply = chat.GenerateMessageStreamAsync(history, new ChatRequestSettings() { MaxTokens = 2048 });
    var answer = new StringBuilder();
    await foreach (var item in reply)
    { 
        if(item==null)
        {
            continue;
        }
        answer.Append(item);
        yield return item;
    }
    history.AddAssistantMessage(answer.ToString());
}
async IAsyncEnumerable<string> AskPostAsync(HttpContext context, IChatCompletion chat, OpenAIChatHistory history, [FromBody]Req req)
{
   var history1 = new OpenAIChatHistory();
    history1.AddUserMessage(req.Ask);
    var reply = chat.GenerateMessageStreamAsync(history1, new ChatRequestSettings() { MaxTokens = 2048 });
    await foreach (var item in reply)
    {
        if (item == null||string.IsNullOrWhiteSpace(item.Trim())|| item.Trim()=="\r" || item.Trim() == "\n")
        {
            continue;
        }     
        Console.WriteLine(item);
        yield return item;
    }
}

class Req
{
    public string Ask { get; set; }
}