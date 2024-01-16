using AgentDelegation;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Experimental.Agents;
using System.Reflection.Emit;
using YamlDotNet.Serialization;

var key = File.ReadAllText(@"C:\GPT\key.txt");
var chatModelId = "gpt-4";

Console.WriteLine("======== Example71_AgentDelegation ========");

#pragma warning disable SKEXP0101
List<IAgent> s_agents = new();
IAgentThread? thread = null;

try
{

    var deserializer = new DeserializerBuilder().Build();
    var definition = File.ReadAllText("ToolAgent.yaml");
    var agentKernelModel = deserializer.Deserialize<MyYamlConfig>(definition);

    var plugin = KernelPluginFactory.CreateFromType<MenuPlugin>();
    var menuAgent =
        Track(
            await new AgentBuilder()
                .WithOpenAIChatCompletion(chatModelId, key)
                //.FromTemplate(File.ReadAllText("ToolAgent.yaml"))
                .WithInstructions(agentKernelModel.Instructions.Trim())
                .WithName(agentKernelModel.Name.Trim())
                .WithDescription(agentKernelModel.Description.Trim())

                .WithDescription("回答有关菜单如何使用该工具的问题。")
                .WithPlugin(plugin)
    .BuildAsync());


    var definition1 = File.ReadAllText("ParrotAgent.yaml");
    var agentKernelModel1 = deserializer.Deserialize<MyYamlConfig>(definition1);

    var parrotAgent =
        Track(
            await new AgentBuilder()
                .WithOpenAIChatCompletion(chatModelId, key)
                //.FromTemplate(File.ReadAllText("ToolAgent.yaml"))
                .WithInstructions(agentKernelModel1.Instructions.Trim())
                .WithName(agentKernelModel1.Name.Trim())
                .WithDescription(agentKernelModel1.Description.Trim())
                .BuildAsync());
    var toolAgent =
        Track(
            await new AgentBuilder()
                .WithOpenAIChatCompletion(chatModelId, key)
                //.FromTemplate(File.ReadAllText("ToolAgent.yaml"))
                .WithInstructions(agentKernelModel.Instructions.Trim())
                .WithName(agentKernelModel.Name.Trim())
                .WithDescription(agentKernelModel.Description.Trim())
                .WithPlugin(parrotAgent.AsPlugin())
                .WithPlugin(menuAgent.AsPlugin())
                .BuildAsync());

    var messages = new string[]
    {
        "菜单上有什么？",
        "你能像海盗一样说话吗？",
        "谢谢"
    };

    thread = await toolAgent.NewThreadAsync();
    foreach (var response in messages.Select(m => thread.InvokeAsync(toolAgent, m)))
    {
        await foreach (var message in response)
        {
            Console.WriteLine($"[{message.Id}]");
            Console.WriteLine($"# {message.Role}: {message.Content}");
        }
    }
}
finally
{
    await Task.WhenAll(
        thread?.DeleteAsync() ?? Task.CompletedTask,
        Task.WhenAll(s_agents.Select(a => a.DeleteAsync())));
}

IAgent Track(IAgent agent)
{
    s_agents.Add(agent);

    return agent;
}