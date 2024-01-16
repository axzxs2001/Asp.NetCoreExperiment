#pragma warning disable SKEXP0101
using Microsoft.SemanticKernel.Experimental.Agents;

var key = File.ReadAllText(@"C:\GPT\key.txt");
var chatModelId = "gpt-4";
var s_agents = new List<IAgent>();

// Run agent with 'code_interpreter' tool
await RunCodeInterpreterToolAsync();

// Run agent with 'retrieval' tool
await RunRetrievalToolAsync();


async Task RunCodeInterpreterToolAsync()
{
    Console.WriteLine("======== Run:CodeInterpreterTool ========");

    var builder =
        new AgentBuilder()
            .WithOpenAIChatCompletion(chatModelId, key)
            .WithInstructions("Write only code to solve the given problem without comment.");

    try
    {      
        var defaultAgent =
            Track(
                await builder.BuildAsync());

        var codeInterpreterAgent =
            Track(
                await builder.WithCodeInterpreter().BuildAsync());

        await ChatAsync(
            defaultAgent,
            codeInterpreterAgent,
            "What is the solution to `3x + 2 = 14`?",
            "What is the fibinacci sequence until 101?");
    }
    finally
    {
        await Task.WhenAll(s_agents.Select(a => a.DeleteAsync()));
    }
}

async Task RunRetrievalToolAsync()
{
    Console.WriteLine("======== Run:RunRetrievalTool ========");

    // REQUIRED:
    //
    // Use `curl` to upload document prior to running example and assign the
    // identifier to `fileId`.
    //
    // Powershell:
    // curl https://api.openai.com/v1/files `
    // -H "Authorization: Bearer $Env:OPENAI_APIKEY" `
    // -F purpose="assistants" `
    // -F file="@Resources/travelinfo.txt"

    var fileId = "<see comment>";

    var defaultAgent =
        await new AgentBuilder()
            .WithOpenAIChatCompletion(chatModelId, key)
            .BuildAsync();

    var retrievalAgent =
        await new AgentBuilder()
            .WithOpenAIChatCompletion(chatModelId, key)
            .WithRetrieval(fileId)
            .BuildAsync();

    try
    {
        await ChatAsync(
            defaultAgent,
            retrievalAgent,
            "Where did sam go?",
            "When does the flight leave Seattle?",
            "What is the hotel contact info at the destination?");
    }
    finally
    {
        await Task.WhenAll(s_agents.Select(a => a.DeleteAsync()));
    }
}

/// <summary>
/// Common chat loop used for: RunCodeInterpreterToolAsync and RunRetrievalToolAsync.
/// Processes each question for both "default" and "enabled" agents.
/// </summary>
async Task ChatAsync(
    IAgent defaultAgent,
    IAgent enabledAgent,
    params string[] questions)
{
    foreach (var question in questions)
    {
        Console.WriteLine("\nDEFAULT AGENT:");
        await InvokeAgentAsync(defaultAgent, question);

        Console.WriteLine("\nTOOL ENABLED AGENT:");
        await InvokeAgentAsync(enabledAgent, question);
    }

    async Task InvokeAgentAsync(IAgent agent, string question)
    {
        await foreach (var message in agent.InvokeAsync(question))
        {
            string content = message.Content;
            foreach (var annotation in message.Annotations)
            {
                content = content.Replace(annotation.Label, string.Empty, StringComparison.Ordinal);
            }

            Console.WriteLine($"# {message.Role}: {content}");

            if (message.Annotations.Count > 0)
            {
                Console.WriteLine("\n# files:");
                foreach (var annotation in message.Annotations)
                {
                    Console.WriteLine($"* {annotation.FileId}");
                }
            }
        }

        Console.WriteLine();
    }
}

IAgent Track(IAgent agent)
{
    s_agents.Add(agent);

    return agent;
}