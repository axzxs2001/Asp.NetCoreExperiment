using Azure;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Orchestration;
using Microsoft.SemanticKernel.Agents.Orchestration.GroupChat;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Text.Json;


#pragma warning disable
var modelID = "gpt-4.1";
var openAIKey = File.ReadAllText("c://gpt/key.txt");

var kernel = Kernel.CreateBuilder()
           .AddOpenAIChatCompletion(modelID, openAIKey).Build();


// Define the agents
ChatCompletionAgent farmer =
    CreateAgent(
        kernel: kernel,
        name: "Farmer",
        description: "一位来自东南亚的农村农民。",
        instructions:
        """
                你是来自东南亚的农民。
                你的生活与土地和家庭紧密相连。
                你重视传统与可持续发展。
                你正参与一场辩论，可以在保持尊重的前提下挑战其他参与者。
                """);
ChatCompletionAgent developer =
    CreateAgent(
        kernel: kernel,
        name: "Developer",
        description: "一位来自美国的城市软件开发人员。",
        instructions:
        """
                你是一位来自美国的软件开发人员。
                你的生活节奏快、以科技为驱动。
                你重视创新、自由与工作生活的平衡。
                你正在参与一场辩论，可以在保持尊重的前提下挑战其他参与者。
                """);
ChatCompletionAgent teacher =
    CreateAgent(
        kernel: kernel,
        name: "Teacher",
        description: "一位来自东欧的退休历史教师。",
        instructions:
        """
                你是一位来自东欧的退休历史教师。
                你在讨论中带来历史与哲学的视角。
                你重视传承、学习与文化的延续。
                你正在参与一场辩论，可以在保持尊重的前提下挑战其他参与者。
                """);
ChatCompletionAgent activist =
    CreateAgent(
        kernel: kernel,
        name: "Activist",
        description: "一位来自南美的年轻活动家。",
        instructions:
        """
                你是一位来自南美的年轻活动家。
                你关注社会正义、环境权利和代际变革。
                你正在参与一场辩论，可以在保持尊重的前提下挑战其他参与者。
                """);
ChatCompletionAgent spiritual =
    CreateAgent(
        kernel: kernel,
        name: "SpiritualLeader",
        description: "一位来自中东的精神领袖。",
        instructions:
        """
                你是一位来自中东的精神领袖。
                你提供基于宗教、道德和社区服务的见解。
                你正在参与一场辩论，可以在保持尊重的前提下挑战其他参与者。
                """);
ChatCompletionAgent artist =
    CreateAgent(
        kernel: kernel,
        name: "Artist",
        description: "一位来自非洲的艺术家。",
        instructions:
        """
                你是一位来自非洲的艺术家。
                你通过创造性表达、讲故事和集体记忆来看待生活。
                你正在参与一场辩论，可以在保持尊重的前提下挑战其他参与者。
                """);
ChatCompletionAgent immigrant =
    CreateAgent(
        kernel: kernel,
        name: "Immigrant",
        description: "一位来自亚洲、现居加拿大的移民企业家。",
        instructions:
        """
                你是一位来自亚洲、现居加拿大的移民企业家。
                你在传统与适应之间寻求平衡。
                你关注家庭的成功、风险和机遇。
                你正在参与一场辩论，可以在保持尊重的前提下挑战其他参与者。
                """);
ChatCompletionAgent doctor =
    CreateAgent(
        kernel: kernel,
        name: "Doctor",
        description: "一位来自斯堪的纳维亚的医生。",
        instructions:
        """
                你是一位来自斯堪的纳维亚的医生。
                你的观点受公共健康、公平和有序社会支持的影响。
                你正在参与一场辩论，可以在保持尊重的前提下挑战其他参与者。
                """);

// Define the orchestration
const string topic = "对你个人而言，什么是美好生活？";
var monitor = new OrchestrationMonitor();
var orchestration =
    new GroupChatOrchestration(
        new AIGroupChatManager(
            topic,
            kernel.GetRequiredService<IChatCompletionService>())
        {
            MaximumInvocationCount = 5,
             

        },
        farmer,
        developer,
        teacher,
        activist,
        spiritual,
        artist,
        immigrant,
        doctor)
    {
        ResponseCallback = monitor.ResponseCallback,
        LoggerFactory = NullLoggerFactory.Instance,
    };

// Start the runtime
InProcessRuntime runtime = new();
await runtime.StartAsync();

// Run the orchestration
Console.WriteLine($"\n# INPUT: {topic}\n");
OrchestrationResult<string> result = await orchestration.InvokeAsync(topic, runtime);
string text = await result.GetValueAsync(TimeSpan.FromSeconds(300 * 3));
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

class OrchestrationMonitor
{
    public ChatHistory History { get; } = [];

    public ValueTask ResponseCallback(Microsoft.SemanticKernel.ChatMessageContent response)
    {
        Console.WriteLine($"{response.AuthorName}({response.Role})：");
        Console.WriteLine(response?.Content);
        this.History.Add(response);
        return ValueTask.CompletedTask;
    }
}
class AIGroupChatManager(string topic, IChatCompletionService chatCompletion) : GroupChatManager
{
    private static class Prompts
    {
        public static string Termination(string topic) =>
            $"""
                你是一位引导主题为“{topic}”讨论的调解者。
                你需要判断讨论是否已经得出结论。
                如果你想结束讨论，请回复 True。否则请回复 False。
                """;

        public static string Selection(string topic, string participants) =>
            $"""
                你是一位引导主题为“{topic}”讨论的调解者。
                你需要选择下一个发言的参与者。
                以下是参与者的姓名和描述：
                {participants}\n
                请只回复你想选择的参与者的姓名。
                """;

        public static string Filter(string topic) =>
            $"""
                你是一位引导主题为“{topic}”讨论的调解者。
                你刚刚结束了讨论。
                请总结讨论内容并给出结语。
                """;
    }

    /// <inheritdoc/>
    public override ValueTask<GroupChatManagerResult<string>> FilterResults(ChatHistory history, CancellationToken cancellationToken = default) =>
        this.GetResponseAsync<string>(history, Prompts.Filter(topic), cancellationToken);

    /// <inheritdoc/>
    public override ValueTask<GroupChatManagerResult<string>> SelectNextAgent(ChatHistory history, GroupChatTeam team, CancellationToken cancellationToken = default)
    {
        var response= this.GetResponseAsync<string>(history, Prompts.Selection(topic, team.FormatList()), cancellationToken);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"选择下一个发言者:{response.Result.Value}");
        Console.WriteLine($"选择原因:{response.Result.Reason}");
        Console.ResetColor();
        return response;
    }

    /// <inheritdoc/>
    public override ValueTask<GroupChatManagerResult<bool>> ShouldRequestUserInput(ChatHistory history, CancellationToken cancellationToken = default) =>
        ValueTask.FromResult(new GroupChatManagerResult<bool>(false) { Reason = "AI 群聊管理器不会请求用户输入。" });

    /// <inheritdoc/>
    public override async ValueTask<GroupChatManagerResult<bool>> ShouldTerminate(ChatHistory history, CancellationToken cancellationToken = default)
    {
        GroupChatManagerResult<bool> result = await base.ShouldTerminate(history, cancellationToken);
        if (!result.Value)
        {
            result = await this.GetResponseAsync<bool>(history, Prompts.Termination(topic), cancellationToken);
        }
        return result;
    }

    private async ValueTask<GroupChatManagerResult<TValue>> GetResponseAsync<TValue>(ChatHistory history, string prompt, CancellationToken cancellationToken = default)
    {
        OpenAIPromptExecutionSettings executionSettings = new() { ResponseFormat = typeof(GroupChatManagerResult<TValue>) };
        ChatHistory request = [.. history, new ChatMessageContent(AuthorRole.System, prompt)];
     
       
        ChatMessageContent response = await chatCompletion.GetChatMessageContentAsync(request, executionSettings, kernel: null, cancellationToken);
        string responseText = response.ToString();
        return
            JsonSerializer.Deserialize<GroupChatManagerResult<TValue>>(responseText) ??
            throw new InvalidOperationException($"无法解析响应: {responseText}");
    }
}