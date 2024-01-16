using AgentsDemo;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Experimental.Agents;
using System.Reflection.Emit;
using YamlDotNet.Serialization;

var key = File.ReadAllText(@"C:\GPT\key.txt");
var chatModelId = "gpt-4";
var OpenAIFunctionEnabledModel = "gpt-3.5-turbo-1106";



// "Hello agent"
//await RunSimpleChatAsync();


// Run agent with "method" tool/function
//await RunWithMethodFunctionsAsync();

//// Run agent with "prompt" tool/function
//await RunWithPromptFunctionsAsync();

//// Run agent as function
await RunAsFunctionAsync();
/// <summary>
/// Common chat loop used for: RunSimpleChatAsync, RunWithMethodFunctionsAsync, and RunWithPromptFunctionsAsync.
/// 1. Reads agent definition from"resourcePath" parameter.
/// 2. Initializes agent with definition and the specified "plugin".
/// 3. Display the agent identifier
/// 4. Create a chat-thread
/// 5. Process the provided "messages" on the chat-thread
/// </summary>
static async Task ChatAsync(
   string resourcePath,
   KernelPlugin? plugin = null,
   KernelArguments? arguments = null,
   params string[] messages)
{

    var ApiKey = File.ReadAllText(@"C:\GPT\key.txt");
    var chatModelId = "gpt-4";

    // Read agent resource
    var definition = File.ReadAllText(resourcePath);
#pragma warning disable SKEXP0101

    var deserializer = new DeserializerBuilder().Build();

    var agentKernelModel = deserializer.Deserialize<MyYamlConfig>(definition);
    // Create agent
    var agent =
        await new AgentBuilder()
            .WithOpenAIChatCompletion(chatModelId, ApiKey)
            //.FromTemplate(definition)
            .WithPlugin(plugin)
            .WithInstructions(agentKernelModel.Instructions.Trim())
            .WithName(agentKernelModel.Name.Trim())
            .WithDescription(agentKernelModel.Description.Trim())

            .BuildAsync();

    // Create chat thread.  Note: Thread is not bound to a single agent.

    var thread = (await agent.NewThreadAsync());
    try
    {
        // Display agent identifier.
        Console.WriteLine($"[{agent.Id}]");

        // Process each user message and agent response.
        foreach (var response in messages.Select(m => thread.InvokeAsync(agent, m)))
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
        // Clean-up (storage costs $)
        await Task.WhenAll(
            thread?.DeleteAsync() ?? Task.CompletedTask,
            agent.DeleteAsync());
    }
}

/// <summary>
/// Chat using the "Parrot" agent.
/// Tools/functions: None
/// </summary>
static async Task RunSimpleChatAsync()
{
    Console.WriteLine("======== Run:SimpleChat ========");

    // Call the common chat-loop
    await ChatAsync(
        "ParrotAgent.yaml", // Defined under ./Resources/Agents
        plugin: null, // No plugin
        arguments: new KernelArguments { { "count", 3 } },
        "命运眷顾勇敢的人",
        "我来了，我看见了，我征服了。",
        "熟能生巧。");
}

/// <summary>
/// Chat using the "Tool" agent and a method function.
/// Tools/functions: MenuPlugin
/// </summary>
static async Task RunWithMethodFunctionsAsync()
{
    Console.WriteLine("======== Run:WithMethodFunctions ========");

    KernelPlugin plugin = KernelPluginFactory.CreateFromType<MenuPlugin>();

    // Call the common chat-loop
    await ChatAsync(
        "ToolAgent.yaml", // Defined under ./Resources/Agents
        plugin,
        arguments: null,
        "你好",
"有什么特色汤品吗？",
"有什么特色饮料吗？",
"谢谢！");
}



/// <summary>
/// Chat using the "Tool" agent and a prompt function.
/// Tools/functions: spellChecker prompt function
/// </summary>
static async Task RunWithPromptFunctionsAsync()
{
    Console.WriteLine("======== WithPromptFunctions ========");

    // Create a prompt function.
    var function = KernelFunctionFactory.CreateFromPrompt(
         "更正输入中提供的任何拼写或语法错误: {{$input}}",
          functionName: "spellChecker",
          description: "更正用户输入的拼写。");

    var plugin = KernelPluginFactory.CreateFromFunctions("spelling", "Spelling functions", new[] { function });

    // Call the common chat-loop
    await ChatAsync(
        "ToolAgent.yaml", // Defined under ./Resources/Agents
        plugin,
        arguments: null,
        "你好",
        "这个拼写正确吗： exercize",
        "特色汤是什么？",
        "谢谢");
}

/// <summary>
/// Invoke agent just like any other <see cref="KernelFunction"/>.
/// </summary>
async Task RunAsFunctionAsync()
{
    Console.WriteLine("======== Run:AsFunction ========");
 
    // Create parrot agent, same as the other cases.
    var agent =
        await new AgentBuilder()
            .WithOpenAIChatCompletion(chatModelId, key)
            .FromTemplate(File.ReadAllText("ParrotAgent.yaml"))
            .BuildAsync();

    try
    {
        // Invoke agent plugin.
        //var response = await agent.AsPlugin().InvokeAsync("熟能生巧。", new KernelArguments { { "count", 2 } });
        var response = await agent.AsPlugin().InvokeAsync("熟能生巧。");

        // Display result.
        Console.WriteLine(response ?? $"No response from agent: {agent.Id}");
    }
    finally
    {
        // Clean-up (storage costs $)
        await agent.DeleteAsync();
    }
}

