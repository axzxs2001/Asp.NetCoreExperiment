#pragma warning disable SKEXP0101
using Microsoft.SemanticKernel.Experimental.Agents;

var key = File.ReadAllText(@"C:\GPT\key.txt");
var chatModelId = "gpt-4";
var s_agents = new List<IAgent>();



// Run demo by invoking agent directly
//await RunAgentAsync();

// Run demo by invoking agent as a plugin
await RunAsPluginAsync();


async Task RunAgentAsync()
{
    try
    {
        // Initialize the agent with tools
        IAgent articleGenerator = await CreateArticleGeneratorAsync();

        // "Stream" messages as they become available
        await foreach (IChatMessage message in articleGenerator.InvokeAsync("中国菜是世界上最好吃的"))
        {
            Console.WriteLine($"[{message.Id}]");
            Console.WriteLine($"# {message.Role}: {message.Content}");
        }
    }
    finally
    {
        await Task.WhenAll(s_agents.Select(a => a.DeleteAsync()));
    }
}

async Task RunAsPluginAsync()
{
    try
    {
        // Initialize the agent with tools
        IAgent articleGenerator = await CreateArticleGeneratorAsync();

        // Invoke as a plugin function
        string response = await articleGenerator.AsPlugin().InvokeAsync("中国菜是世界上最好吃的");

        // Display final result
        Console.WriteLine(response);
    }
    finally
    {
        await Task.WhenAll(s_agents.Select(a => a.DeleteAsync()));
    }
}

async Task<IAgent> CreateArticleGeneratorAsync()
{
    // Initialize the outline agent
    var outlineGenerator = await CreateOutlineGeneratorAsync();
    // Initialize the research agent
    var sectionGenerator = await CreateResearchGeneratorAsync();

    // Initialize agent so that it may be automatically deleted.
    return
        Track(
            await new AgentBuilder()
                .WithOpenAIChatCompletion(chatModelId, key)
                .WithInstructions("您撰写简洁、观点鲜明的文章并在网上发表。 使用大纲生成一篇文章，其中每个顶级大纲元素都有一段散文。 每个部分均基于研究，最多 120 个字。")
                .WithName("Article Author")
                .WithDescription("就给定主题撰写一篇文章。")
                .WithPlugin(outlineGenerator.AsPlugin())
                .WithPlugin(sectionGenerator.AsPlugin())
                .BuildAsync());
}

async Task<IAgent> CreateOutlineGeneratorAsync()
{
    // Initialize agent so that it may be automatically deleted.
    return
        Track(
            await new AgentBuilder()
                .WithOpenAIChatCompletion(chatModelId, key)
                .WithInstructions("根据给定主题生成最多 3 个部分的单级大纲（无子元素）。")
                .WithName("Outline Generator")
                .WithDescription("生成大纲。")
                .BuildAsync());
}

async Task<IAgent> CreateResearchGeneratorAsync()
{
    // Initialize agent so that it may be automatically deleted.
    return
        Track(
            await new AgentBuilder()
                .WithOpenAIChatCompletion(chatModelId, key)
                .WithInstructions("根据您对大纲主题的了解，提供支持给定主题的富有洞察力的研究。")
                .WithName("Researcher")
                .WithDescription("作者研究总结。")
                .BuildAsync());
}

IAgent Track(IAgent agent)
{
    s_agents.Add(agent);

    return agent;
}