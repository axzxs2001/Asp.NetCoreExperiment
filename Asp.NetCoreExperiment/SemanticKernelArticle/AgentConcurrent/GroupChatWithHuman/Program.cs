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

var azureParams = File.ReadAllLines("C://gpt/azure_key.txt");

var kernel = Kernel.CreateBuilder()
           //.AddOpenAIChatCompletion(modelID, openAIKey).Build();
           .AddAzureOpenAIChatCompletion(azureParams[0], azureParams[1], azureParams[2]).Build();

ChatCompletionAgent writer =
    CreateAgent(
        name: "CopyWriter",
        description: "A copy writer",
        instructions:
"""
                你是一位拥有十年经验的文案撰写人，以简洁和冷幽默著称。
                目标是以专家视角打磨并确定唯一最优的文案。
                每次回复只提供一个提案。
                专注于任务，不浪费时间闲聊。
                在打磨创意时，会考虑提出的建议。
                """,
        kernel: kernel);
ChatCompletionAgent editor =
    CreateAgent(
        name: "Reviewer",
        description: "An editor.",
        instructions:
        """
                你是一位对文案有独到见解的艺术总监，这种见解源自你对大卫·奥格威的热爱。
                你的目标是判断所提供的文案是否可以投入印刷。
                如果可以，请直接说明“已批准”。
                如果不可以，请提供改进方向，但不提供示例。
                """,
        kernel: kernel);

// Define the orchestration
var orchestration =
    new GroupChatOrchestration(
        new CustomRoundRobinGroupChatManager()
        {
            MaximumInvocationCount = 5,
            InteractiveCallback = () =>
            {
                Console.WriteLine("请用户输入内容：");
                var userContent = Console.ReadLine();
                ChatMessageContent input = new(AuthorRole.User, userContent);
                Console.WriteLine($"\n# INPUT: {input.Content}\n");
                return ValueTask.FromResult(input);
            }
        },
        writer,
        editor)
    {
        LoggerFactory = NullLoggerFactory.Instance,
    };

// Start the runtime
InProcessRuntime runtime = new();
await runtime.StartAsync();

// Run the orchestration
string input = "为一款既实惠又好开的全新电动SUV创建一句广告标语。";
Console.WriteLine($"\n# INPUT: {input}\n");
OrchestrationResult<string> result = await orchestration.InvokeAsync(input, runtime);
string text = await result.GetValueAsync(TimeSpan.FromSeconds(30 * 3));
Console.WriteLine($"\n# RESULT: {text}");

await runtime.RunUntilIdleAsync();



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
class CustomRoundRobinGroupChatManager : RoundRobinGroupChatManager
{
    public override ValueTask<GroupChatManagerResult<bool>> ShouldRequestUserInput(ChatHistory history, CancellationToken cancellationToken = default)
    {
        string? lastAgent = history.LastOrDefault()?.AuthorName;

        if (lastAgent is null)
        {
            return ValueTask.FromResult(new GroupChatManagerResult<bool>(false) { Reason = "目前尚无代理发言。" });
        }

        if (lastAgent == "Reviewer")
        {
            return ValueTask.FromResult(new GroupChatManagerResult<bool>(true) { Reason = "在审阅者留言后，需要用户输入。" });
        }

        return ValueTask.FromResult(new GroupChatManagerResult<bool>(false) { Reason = "在审阅者留言之前，不需要用户输入。" });
    }
}