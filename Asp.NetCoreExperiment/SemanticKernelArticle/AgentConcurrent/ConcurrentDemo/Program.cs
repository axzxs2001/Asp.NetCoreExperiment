
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Orchestration;
using Microsoft.SemanticKernel.Agents.Orchestration.Concurrent;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
using Microsoft.SemanticKernel.ChatCompletion;
using OpenAI.Assistants;
using OpenAI.Chat;
#pragma warning disable

const int ResultTimeoutInSeconds = 30;
var modelID = "gpt-4.1";
var openAIKey = File.ReadAllText("c://gpt/key.txt");

var kernel = Kernel.CreateBuilder()
           .AddOpenAIChatCompletion(modelID, openAIKey).Build();

ChatCompletionAgent physicist =
    CreateAgent(
        instructions: "你是一位物理学专家。你从物理的角度回答问题。",
        description: "物理学专家",
        name: "physicist",
        kernel);
ChatCompletionAgent chemist =
    CreateAgent(
        instructions: "你是一位化学专家。你从化学的角度回答问题。",
        description: "化学专家",
        name: "chemist",
        kernel);

var monitor = new OrchestrationMonitor();

var orchestration =
   new ConcurrentOrchestration(physicist, chemist)
   {
         
       ResponseCallback = monitor.ResponseCallback,
       LoggerFactory = NullLoggerFactory.Instance
   };

var runtime = new InProcessRuntime();
await runtime.StartAsync();

string input = "什么是温度？简短说明";
Console.WriteLine($"\n# 输入: {input}\n");



OrchestrationResult<string[]> result = await orchestration.InvokeAsync(input, runtime);

string[] output = await result.GetValueAsync(TimeSpan.FromSeconds(ResultTimeoutInSeconds));
Console.WriteLine($"\n# 结果:\n{string.Join("\n\n", output.Select(text => $"{text}"))}");

await runtime.RunUntilIdleAsync();

Console.WriteLine("\n\n编排历史");
foreach (Microsoft.SemanticKernel.ChatMessageContent message in monitor.History)
{
    Console.WriteLine($"{message.AuthorName}:{message.Content}");
}

ChatCompletionAgent CreateAgent(string instructions, string? description = null, string? name = null, Kernel? kernel = null)
{
    return
        new ChatCompletionAgent
        {
            Name = name,
            Description = description,
            Instructions = instructions,
            Kernel = kernel,
        };
}






sealed class OrchestrationMonitor
{
    public ChatHistory History { get; } = [];

    public ValueTask ResponseCallback(Microsoft.SemanticKernel.ChatMessageContent response)
    {
        //Console.WriteLine($"ResponseCallback: {response.AuthorName}:{response.Content}");
        this.History.Add(response);
        return ValueTask.CompletedTask;
    }
}