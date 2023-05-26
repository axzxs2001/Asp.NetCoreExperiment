using Microsoft.Extensions.Caching.Memory;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;
using System.Text;
var key = File.ReadAllText(@"C:\\GPT\key.txt");
var builder = WebApplication.CreateBuilder(args);
var kernel = Kernel.Builder
    .Configure(c =>
    {
        c.AddOpenAIChatCompletionService("gpt-4", key, serviceId: "gsw_chat");
    })
    .Build();
builder.Services.AddSingleton(kernel.GetService<IChatCompletion>());
builder.Services.AddMemoryCache();
var app = builder.Build();
app.UseStaticFiles();
app.MapGet("/chat", AskAsync);
app.Run();
async IAsyncEnumerable<string> AskAsync(IMemoryCache cache, IChatCompletion chat, HttpRequest request, string ask)
{
    var requestID = request.Headers["Request-ID"];
    if (!cache.TryGetValue(requestID,  out OpenAIChatHistory? history))
    {
        history = (OpenAIChatHistory)chat.CreateNewChat();
        cache.Set(requestID, history);
    }
    history?.AddUserMessage(ask);
    var reply = chat.GenerateMessageStreamAsync(history!, new ChatRequestSettings() { MaxTokens = 2048 });
    var answer = new StringBuilder();
    await foreach (var item in reply)
    {
        if (item == null)
        {
            continue;
        }
        answer.Append(item);
        yield return item;
    }
    history?.AddAssistantMessage(answer.ToString());
}