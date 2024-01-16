using AgentsDemo;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Experimental.Agents;
using System.Reflection.Emit;

var key = File.ReadAllText(@"C:\GPT\key.txt");
var chatModelId = "gpt-4";
var OpenAIFunctionEnabledModel = "gpt-3.5-turbo-1106";



// "Hello agent"
await RunSimpleChatAsync();


// Run agent with "method" tool/function
//await RunWithMethodFunctionsAsync();

//// Run agent with "prompt" tool/function
//await RunWithPromptFunctionsAsync();

//// Run agent as function
//await RunAsFunctionAsync();
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
   params string[] messages){

    var ApiKey = File.ReadAllText(@"C:\GPT\key.txt");
    var chatModelId = "gpt-4";

    // Read agent resource
    var definition = File.ReadAllText(resourcePath);
#pragma warning disable SKEXP0101
    // Create agent
    var agent =
        await new AgentBuilder()
            .WithOpenAIChatCompletion(chatModelId, ApiKey)
            .FromTemplate(definition)
            .WithPlugin(plugin)
           
            .BuildAsync();

    // Create chat thread.  Note: Thread is not bound to a single agent.

    var thread = await agent.NewThreadAsync();
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
        "Fortune favors the bold.",
        "I came, I saw, I conquered.",
        "Practice makes perfect.");
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
        "Hello",
        "What is the special soup?",
        "What is the special drink?",
        "Thank you!");
}



///// <summary>
///// Chat using the "Tool" agent and a prompt function.
///// Tools/functions: spellChecker prompt function
///// </summary>
//static async Task RunWithPromptFunctionsAsync()
//{
//    Console.WriteLine("======== WithPromptFunctions ========");

//    // Create a prompt function.
//    var function = KernelFunctionFactory.CreateFromPrompt(
//         "Correct any misspelling or gramatical errors provided in input: {{$input}}",
//          functionName: "spellChecker",
//          description: "Correct the spelling for the user input.");

//    var plugin = KernelPluginFactory.CreateFromFunctions("spelling", "Spelling functions", new[] { function });

//    // Call the common chat-loop
//    await ChatAsync(
//        "Agents.ToolAgent.yaml", // Defined under ./Resources/Agents
//        plugin,
//        arguments: null,
//        "Hello",
//        "Is this spelled correctly: exercize",
//        "What is the special soup?",
//        "Thank you!");
//}

///// <summary>
///// Invoke agent just like any other <see cref="KernelFunction"/>.
///// </summary>
//async Task RunAsFunctionAsync()
//{
//    Console.WriteLine("======== Run:AsFunction ========");

//    // Create parrot agent, same as the other cases.
//    var agent =
//        await new AgentBuilder()
//            .WithOpenAIChatCompletion(OpenAIFunctionEnabledModel, TestConfiguration.OpenAI.ApiKey)
//            .FromTemplate(EmbeddedResource.Read("Agents.ParrotAgent.yaml"))
//            .BuildAsync();

//    try
//    {
//        // Invoke agent plugin.
//        var response = await agent.AsPlugin().InvokeAsync("Practice makes perfect.", new KernelArguments { { "count", 2 } });

//        // Display result.
//        Console.WriteLine(response ?? $"No response from agent: {agent.Id}");
//    }
//    finally
//    {
//        // Clean-up (storage costs $)
//        await agent.DeleteAsync();
//    }
//}

