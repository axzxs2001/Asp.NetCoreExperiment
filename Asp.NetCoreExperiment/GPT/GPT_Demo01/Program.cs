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

var kernel = Kernel.Builder
    //.WithMemoryStorage(new VolatileMemoryStore())
    .Configure(c =>
    {
        //c.AddOpenAITextCompletionService("davinci-openai", "text-davinci-003", key);

        // c.AddOpenAITextEmbeddingGenerationService("davinci-openai", "text-embedding-ada-002", key);

        c.AddOpenAIChatCompletionService("davinci-openai", "gpt-4", key);

    })
    .Build();
//kernel.ImportSkill(new TextMemorySkill());


IChatCompletion chatGPT = kernel.GetService<IChatCompletion>();
var chatHistory = (OpenAIChatHistory)chatGPT.CreateNewChat("你是一个.net专家");
while (true)
{
    Console.WriteLine("问题：");
    // First user message
    chatHistory.AddUserMessage(Console.ReadLine());
    var cfg = new ChatRequestSettings();
    // First bot message
    string reply = await chatGPT.GenerateMessageAsync(chatHistory, cfg);
    chatHistory.AddAssistantMessage(reply);

    //// Second user message
    //chatHistory.AddUserMessage("I love history and philosophy, I'd like to learn something new about Greece, any suggestion?");

    //// Second bot message
    //reply = await chatGPT.GenerateMessageAsync(chatHistory, cfg);
    //chatHistory.AddAssistantMessage(reply);

    Console.WriteLine("聊天内容:");
    Console.WriteLine("------------------------");
    foreach (var message in chatHistory.Messages)
    {
        Console.WriteLine($"{message.AuthorRole}: {message.Content}");
        Console.WriteLine("------------------------");
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
