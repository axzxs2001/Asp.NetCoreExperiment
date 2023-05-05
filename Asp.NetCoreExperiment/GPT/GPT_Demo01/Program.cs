using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.TextEmbedding;
using Microsoft.SemanticKernel.Memory;
using System;
using System.Runtime.InteropServices;

var key = File.ReadAllText(@"E:\\GPT\key.txt");

var kernel = Kernel.Builder

    .WithMemoryStorage(new VolatileMemoryStore())
    .Configure(c =>
    {
        c.AddOpenAITextCompletionService("davinci-openai", "text-davinci-003", key);

        c.AddOpenAITextEmbeddingGenerationService("davinci-openai", "text-embedding-ada-002", key);



    })
    .Build();


var prompt = @"{{$input}}";

var summarize = kernel.CreateSemanticFunction(prompt);

while (true)
{
    Console.WriteLine("问题：");
    string text1 = Console.ReadLine();
    var context = await summarize.InvokeAsync(text1);
   
    Console.WriteLine(context.Result);
}
