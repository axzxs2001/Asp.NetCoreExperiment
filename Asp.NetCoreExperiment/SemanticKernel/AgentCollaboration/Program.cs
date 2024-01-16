
using Microsoft.SemanticKernel.Experimental.Agents;

#pragma warning disable SKEXP0101
var key = File.ReadAllText(@"C:\GPT\key.txt");
var chatModelId = "gpt-4";
var s_agents = new List<IAgent>();


// NOTE: Either of these examples produce a conversation
// whose duration may vary depending on the collaboration dynamics.
// It is sometimes possible that agreement is never achieved.

// Explicit collaboration
//await RunCollaborationAsync();

// Coordinate collaboration as plugin agents (equivalent to previous case - shared thread)
await RunAsPluginsAsync();


/// <summary>
/// Show how two agents are able to collaborate as agents on a single thread.
/// </summary>
async Task RunCollaborationAsync()
{
    Console.WriteLine("======== Run:Collaboration ========");
    IAgentThread? thread = null;
    try
    {
        // Create copy-writer agent to generate ideas
        var copyWriter = await CreateCopyWriterAsync();
        // Create art-director agent to review ideas, provide feedback and final approval
        var artDirector = await CreateArtDirectorAsync();

        // Create collaboration thread to which both agents add messages.
        thread = await copyWriter.NewThreadAsync();

        // Add the user message
        var messageUser = await thread.AddUserMessageAsync("概念：用鸡蛋盒制成的地图。");
        DisplayMessage(messageUser);

        bool isComplete = false;
        do
        {
            // Initiate copy-writer input
            var agentMessages = await thread.InvokeAsync(copyWriter).ToArrayAsync();
            DisplayMessages(agentMessages, copyWriter);

            // Initiate art-director input
            agentMessages = await thread.InvokeAsync(artDirector).ToArrayAsync();
            DisplayMessages(agentMessages, artDirector);

            // Evaluate if goal is met.
            if (agentMessages.First().Content.Contains("打印它", StringComparison.OrdinalIgnoreCase))
            {
                isComplete = true;
            }
        }
        while (!isComplete);
    }
    finally
    {
        // Clean-up (storage costs $)
        await Task.WhenAll(s_agents.Select(a => a.DeleteAsync()));
    }
}

/// <summary>
/// Show how agents can collaborate as agents using the plug-in model.
/// </summary>
/// <remarks>
/// While this may achieve an equivalent result to <see cref="RunCollaborationAsync"/>,
/// it is not using shared thread state for agent interaction.
/// </remarks>
async Task RunAsPluginsAsync()
{
    Console.WriteLine("======== Run:AsPlugins ========");
    try
    {
        // Create copy-writer agent to generate ideas
        var copyWriter = await CreateCopyWriterAsync();
        // Create art-director agent to review ideas, provide feedback and final approval
        var artDirector = await CreateArtDirectorAsync();

        // Create coordinator agent to oversee collaboration
        var coordinator =
            Track(
                await new AgentBuilder()
                    .WithOpenAIChatCompletion(chatModelId, key)
                    .WithInstructions("回复提供的概念并让文案作者生成营销创意（副本）。 然后让艺术总监回复文案作者，对文案进行审查。 始终在任何消息中包含源副本。 与文案作者互动时，始终包含艺术总监的评论。 协调文案撰写者和艺术总监之间的重复回复，直到艺术总监批准文案。")
                    .WithPlugin(copyWriter.AsPlugin())
                    .WithPlugin(artDirector.AsPlugin())
                    .BuildAsync());

        // Invoke as a plugin function
        var response = await coordinator.AsPlugin().InvokeAsync("概念：用鸡蛋盒制成的地图。");

        // Display final result
        Console.WriteLine(response);
    }
    finally
    {
        // Clean-up (storage costs $)
        await Task.WhenAll(s_agents.Select(a => a.DeleteAsync()));
    }
}

async Task<IAgent> CreateCopyWriterAsync(IAgent? agent = null)
{
    return
        Track(
            await new AgentBuilder()
                .WithOpenAIChatCompletion(chatModelId, key)
                .WithInstructions("您是一位拥有十年经验的文案撰稿人，以简洁和冷幽默而闻名。 你全神贯注于手头的目标。 不要把时间浪费在闲聊上。 我们的目标是作为该领域的专家完善并决定最佳的副本。 完善想法时考虑建议。")
                .WithName("Copywriter")
                .WithDescription("文案撰稿人")
                .WithPlugin(agent?.AsPlugin())               
                .BuildAsync());
}

async Task<IAgent> CreateArtDirectorAsync()
{
    return
        Track(
            await new AgentBuilder()
                .WithOpenAIChatCompletion(chatModelId, key)
                .WithInstructions("你是一位艺术总监，对文案写作有自己的见解，源于对大卫·奥格威的热爱。 目标是确定给定的副本是否可以打印，即使它并不完美。 如果没有，请提供有关如何在没有示例的情况下完善建议副本的见解。 始终通过评估和提供批评来回应最新消息，而无需举例。 始终在开头重复副本。 如果副本可以接受并且符合您的标准，请说：打印它。")
                .WithName("Art Director")
                .WithDescription("艺术总监")
                .BuildAsync());
}

void DisplayMessages(IEnumerable<IChatMessage> messages, IAgent? agent = null)
{
    foreach (var message in messages)
    {
        DisplayMessage(message, agent);
    }
}

void DisplayMessage(IChatMessage message, IAgent? agent = null)
{
    Console.WriteLine($"[{message.Id}]");
    if (agent != null)
    {
        Console.WriteLine($"# {message.Role}: ({agent.Name}) {message.Content}");
    }
    else
    {
        Console.WriteLine($"# {message.Role}: {message.Content}");
    }
}

IAgent Track(IAgent agent)
{
    s_agents.Add(agent);
    return agent;
}