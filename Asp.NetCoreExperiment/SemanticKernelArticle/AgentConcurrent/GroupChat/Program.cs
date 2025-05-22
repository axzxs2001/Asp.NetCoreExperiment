




using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Orchestration;
using Microsoft.SemanticKernel.Agents.Orchestration.GroupChat;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
using Microsoft.SemanticKernel.ChatCompletion;


#pragma warning disable
var modelID = "gpt-4.1";
var openAIKey = File.ReadAllText("c://gpt/key.txt");

var kernel = Kernel.CreateBuilder()
           .AddOpenAIChatCompletion(modelID, openAIKey).Build();



ChatCompletionAgent writer =
          CreateAgent(
              name: "CopyWriter",
              description: "A copy writer",
              instructions:
              """
                你是一位拥有十年经验的文案撰写人，以简洁和冷幽默著称。
                目标是以专家视角打磨并确定唯一最优的文案。
                每次回复只提供一个提案。
                专注于任务本身，不浪费时间闲聊。
                在打磨文案时，考虑合理的建议。
                """,
              kernel: kernel);
ChatCompletionAgent editor =
    CreateAgent(
        name: "Reviewer",
        description: "An editor.",
        instructions:
        """
                你是一位具有明确观点的艺术总监，对文案写作充满热情，深受大卫·奥格威的影响。
                你的目标是判断所提供的文案是否可以用于印刷。
                如果可以，直接说明“已批准”。
                如果不可以，提供改进方向，但不提供示例。
                """,
              kernel: kernel);

var monitor = new OrchestrationMonitor();
var orchestration =
    new GroupChatOrchestration(new RoundRobinGroupChatManager()
    {
        MaximumInvocationCount = 15
    },
    writer,
    editor)
    {
        ResponseCallback = monitor.ResponseCallback,
        LoggerFactory = NullLoggerFactory.Instance,
    };

// Start the runtime
var runtime = new InProcessRuntime();
await runtime.StartAsync();

string input = "为一款既实惠又好开的全新电动SUV创建一句广告标语。";
Console.WriteLine($"\n# INPUT: {input}\n");
OrchestrationResult<string> result = await orchestration.InvokeAsync(input, runtime);
string text = await result.GetValueAsync(TimeSpan.FromSeconds(30 * 3));


Console.WriteLine($"\n# RESULT: {text}");

await runtime.RunUntilIdleAsync();

Console.WriteLine("\n\nORCHESTRATION HISTORY");
foreach (ChatMessageContent message in monitor.History)
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
        this.History.Add(response);
        return ValueTask.CompletedTask;
    }
}