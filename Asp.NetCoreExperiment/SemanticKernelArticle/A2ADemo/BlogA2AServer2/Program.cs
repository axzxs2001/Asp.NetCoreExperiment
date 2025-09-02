using A2A;
using A2A.AspNetCore;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.A2A;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
var arr = File.ReadAllLines("C:/gpt/azure_key.txt");
string? apiKey = arr[2];
string? endpoint = arr[1];
string modelId = arr[0];

A2AHostAgent? translateHostAgent = CreateChatCompletionHostAgent(
            modelId, endpoint, apiKey, "TranslateAgent",
            """
            你能把用户输入的准确的翻译成日语和英语。
            """, new KernelPlugin[0]);

app.MapA2A(translateHostAgent!.TaskManager!, "/");

app.Run();


A2AHostAgent CreateChatCompletionHostAgent(string modelId, string endpoint, string apiKey, string name, string instructions, IEnumerable<KernelPlugin>? plugins = null)
{
    var builder = Kernel.CreateBuilder();
    builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);
    if (plugins is not null)
    {
        foreach (var plugin in plugins)
        {
            builder.Plugins.Add(plugin);
        }
    }
    var kernel = builder.Build();

    var agent = new ChatCompletionAgent()
    {
        Kernel = kernel,
        Name = name,
        Instructions = instructions,
        Arguments = new KernelArguments(new PromptExecutionSettings() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() }),
    };

    var agentCard = GetTranslateAgentCard();
    return new A2AHostAgent(agent, agentCard);
}
AgentCard GetTranslateAgentCard()
{
    var capabilities = new AgentCapabilities()
    {
        Streaming = false,
        PushNotifications = false,
    };

    var invoiceQuery = new AgentSkill()
    {
        Id = "id_translate_agent",
        Name = "TranslateAgent",
        Description = "你能把用户输入的内容准确地翻译成日语和英语。",
        Tags = ["翻译", "semantic-kernel"],
        Examples =
        [
        ],
    };

    return new()
    {
        Name = "TranslateAgent",
        Description = "你能把用户输入的准确的翻译成日语和英语。",
        Version = "1.0.0",
        DefaultInputModes = ["text"],
        DefaultOutputModes = ["text"],
        Capabilities = capabilities,
        Skills = [invoiceQuery],
    };
}

