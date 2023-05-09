using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.TextEmbedding;
using Microsoft.SemanticKernel.CoreSkills;
using Microsoft.SemanticKernel.Memory;
using System;
using System.Runtime.InteropServices;

var key = File.ReadAllText(@"C:\\GPT\key.txt");

await BootAsync(key);

static async Task BootAsync(string key)
{
    var kernel = Kernel.Builder
        .Configure(c =>
        {            
            c.AddOpenAITextCompletionService("openai", "text-davinci-003", key);
            c.AddOpenAITextEmbeddingGenerationService("openai", "text-embedding-ada-002", key);
        })
        .WithMemoryStorage(new VolatileMemoryStore())
        .Build();
    const string MemoryCollectionName = "aboutMe";

    await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info1", text: "桂素伟，性别男，身高171cm，体重75千克");
    await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info2", text: "桂素伟的职业是农民，他擅长种茄子");
    await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info3", text: "桂素伟有20年的种地经验");
    await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info4", text: "桂素伟现在信在五十亩村");
    await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info5", text: "我是桂素伟");

    var prompt =
    """
给出答案或者不知道答案时说“非常抱歉，我没有找到你要的问题！”
 
对话中的关于桂素伟的信息:
{{ $fact }}
 
User: {{ $ask }}
ChatBot:
""";
    
    var skill = kernel.CreateSemanticFunction(prompt,temperature:0,topP:0);
    while (true)
    {
        Console.WriteLine("请输入问题：");
        var ask = Console.ReadLine();
        var fact = await kernel.Memory.SearchAsync(MemoryCollectionName, ask).FirstOrDefaultAsync();
        var context = kernel.CreateNewContext();
        context["fact"] = fact?.Metadata?.Text;
        context["ask"] = ask;
        var resultContext = await skill.InvokeAsync(context);
        Console.WriteLine($"Bot:{resultContext.Result}");
    }
}





//await Chat(key);
//聊天
static async Task Chat(string key)
{
    var kernel = Kernel.Builder
        .Configure(c =>
        {
            c.AddOpenAIChatCompletionService("davinci-openai", "gpt-4", key);
        })
        .Build();

    var chatGPT = kernel.GetService<IChatCompletion>();
    var chatHistory = (OpenAIChatHistory)chatGPT.CreateNewChat("你是一个.net专家");
    while (true)
    {
        Console.WriteLine("输入问题：");
        chatHistory.AddUserMessage(Console.ReadLine());
        var cfg = new ChatRequestSettings();
        var reply = await chatGPT.GenerateMessageAsync(chatHistory, cfg);
        chatHistory.AddAssistantMessage(reply);

        Console.WriteLine("聊天内容:");
        Console.WriteLine("------------------------");
        foreach (var message in chatHistory.Messages)
        {
            Console.WriteLine($"{message.AuthorRole}: {message.Content}");
            Console.WriteLine("------------------------");
        }
    }

}





//var prompt = @"
//请回答下面的问题：
//{{$input}}
//";



//var i = 0;
//while (true)
//{
//    i++;

//    var summarize = kernel.CreateSemanticFunction(prompt, "f"+i, "sk1");
//    Console.WriteLine("问题：");
//    string input = Console.ReadLine();
//    var context = await summarize.InvokeAsync(input);

//    Console.WriteLine(context.Result);
//}
