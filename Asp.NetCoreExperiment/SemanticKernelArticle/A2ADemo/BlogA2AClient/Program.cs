using A2A;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.A2A;

#pragma warning disable

using var loggerFactory = LoggerFactory.Create(builder =>
{
    builder.SetMinimumLevel(LogLevel.Information);
});
var logger = loggerFactory.CreateLogger("A2AClient");


var arr = File.ReadAllLines("C:/gpt/azure_key.txt");
var apiKey = arr[2];
var modelId = arr[0];
var endpoint = arr[1];
var agentUrl = "http://localhost:5000/";


var agent = await CreateNewAgentAsync(modelId, endpoint, apiKey, agentUrl);
try
{
    while (true)
    {
        Console.WriteLine("，请输入信息，按q退了");
        string? message = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(message))
        {
            Console.WriteLine("输入不能为空");
            continue;
        }
        if (message.ToLower() == "q")
        {
            break;
        }
        await foreach (AgentResponseItem<ChatMessageContent> response in agent!.InvokeAsync(message, thread))
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Agent: {response.Message.Content}");
            Console.ResetColor();        
        }
    }   
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

async Task<ChatCompletionAgent> CreateNewAgentAsync(string modelId, string endpoint, string apiKey, string agentUrl)
{
    try
    {
        var agent = await CreateAgentAsync(agentUrl);
        var agentFunction = AgentKernelFunctionFactory.CreateFromAgent(agent);
        var agentPlugin = KernelPluginFactory.CreateFromFunctions("AgentPlugin", [agentFunction]);
        var builder = Kernel.CreateBuilder();
        builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);
        builder.Plugins.Add(agentPlugin);
        var kernel = builder.Build();
        kernel.FunctionInvocationFilters.Add(new ConsoleOutputFunctionInvocationFilter());

        var chatAgent = new ChatCompletionAgent()
        {
            Kernel = kernel,
            Name = "HostClient",
            Instructions ="""
                 你专门负责处理用户的查询，并使用你的工具来提供答案。如果用错，请报详细的错误信息。
                 """,
            Arguments = new KernelArguments(new PromptExecutionSettings() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() }),
        };
        return chatAgent;
    }
    catch (Exception ex)
    {
        throw;
    }
}

async Task<A2AAgent> CreateAgentAsync(string agentUri)
{
    var url = new Uri(agentUri);
    var httpClient = new HttpClient
    {
        Timeout = TimeSpan.FromSeconds(60)
    };
    var client = new A2AClient(url, httpClient);
    var cardResolver = new A2ACardResolver(url, httpClient);
    var agentCard = await cardResolver.GetAgentCardAsync();
    return new A2AAgent(client, agentCard!);
}


class ConsoleOutputFunctionInvocationFilter() : IFunctionInvocationFilter
{
    private static string IndentMultilineString(string multilineText, int indentLevel = 1, int spacesPerIndent = 4)
    {
        var indentation = new string(' ', indentLevel * spacesPerIndent);
        char[] NewLineChars = { '\r', '\n' };
        string[] lines = multilineText.Split(NewLineChars, StringSplitOptions.None);

        return string.Join(Environment.NewLine, lines.Select(line => indentation + line));
    }
    public async Task OnFunctionInvocationAsync(FunctionInvocationContext context, Func<FunctionInvocationContext, Task> next)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;

        Console.WriteLine($"\nCalling Agent {context.Function.Name} with arguments:");
        Console.ForegroundColor = ConsoleColor.Gray;

        foreach (var kvp in context.Arguments)
        {
            Console.WriteLine(IndentMultilineString($"  {kvp.Key}: {kvp.Value}"));
        }

        await next(context);

        if (context.Result.GetValue<object>() is ChatMessageContent[] chatMessages)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;

            Console.WriteLine($"Response from Agent {context.Function.Name}:");
            foreach (var message in chatMessages)
            {
                Console.ForegroundColor = ConsoleColor.Gray;

                Console.WriteLine(IndentMultilineString($"{message}"));
            }
        }
        Console.ResetColor();
    }
}
