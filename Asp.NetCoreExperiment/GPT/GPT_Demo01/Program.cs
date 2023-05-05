using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.TextEmbedding;
using Microsoft.SemanticKernel.CoreSkills;
using Microsoft.SemanticKernel.Memory;
using System;
using System.Runtime.InteropServices;

var key = File.ReadAllText(@"E:\\GPT\key.txt");
await Chat(key);
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
