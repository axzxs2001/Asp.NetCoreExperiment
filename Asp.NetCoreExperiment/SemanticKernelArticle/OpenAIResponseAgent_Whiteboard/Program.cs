using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.OpenAI;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Memory;
using System.Runtime.CompilerServices;
using System.Linq;

#pragma warning disable

const string AgentName = "FriendlyAssistant";
const string AgentInstructions = "You are a friendly assistant";

var key = File.ReadAllText("C:/gpt/key.txt");
var Client = new OpenAI.Responses.OpenAIResponseClient("gpt-4.1", key);


IChatClient chatClient = new OpenAI.OpenAIClient(key)
           .GetChatClient("gpt-4.1")
           .AsIChatClient();

// Create the whiteboard.
WhiteboardProvider whiteboardProvider = new(chatClient);

var agent = new OpenAIResponseAgent(Client)
{
    Name = AgentName,
    Instructions = AgentInstructions,
    Arguments = new KernelArguments(new PromptExecutionSettings { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() }),
    StoreEnabled = false,
};

// Create the agent with our sample plugin.
agent.Kernel.Plugins.AddFromType<VMPlugin>();

// Create a chat history reducer that we can use to truncate the chat history
// when it goes over 3 items.
ChatHistoryTruncationReducer chatHistoryReducer = new(3, 3);

// Create a thread for the agent and add the whiteboard to it.
ChatHistoryAgentThread agentThread = new();
agentThread.AIContextProviders.Add(whiteboardProvider);

// Simulate a conversation with the agent.
// We will also truncate the conversation once it goes over a few items.
await InvokeWithConsoleWriteLine("你好");
await InvokeWithConsoleWriteLine("我想创建一台虚拟机");
await InvokeWithConsoleWriteLine("我希望它有 3 个核心。");
await InvokeWithConsoleWriteLine("我希望它有 48GB 的内存。");
await InvokeWithConsoleWriteLine("我希望它有一个 500GB 的硬盘。");
await InvokeWithConsoleWriteLine("我希望它在欧洲。");
await InvokeWithConsoleWriteLine("你能把它设置为 Linux，并命名为 'ContosoVM' 吗？");
await InvokeWithConsoleWriteLine("好的，那我们把它命名为 `ContosoFinanceVM_Europe` 吧。");
await InvokeWithConsoleWriteLine("谢谢，现在我还想再创建一台虚拟机。");
await InvokeWithConsoleWriteLine("所有选项和上一个相同，除了区域改为北美，名称改为 'ContosoFinanceVM_NorthAmerica'。");


async Task InvokeWithConsoleWriteLine(string message)
{
    // Print the user input.
    Console.WriteLine($"User: {message}");

    // Use the alias to specify the correct AsyncEnumerable type.
    var res = agent.InvokeAsync(message, agentThread);
    if (res != null)
    {
        var r = await res.AnyAsync();
        if (r)
        {

            var response = await res.FirstAsync();

            // Print the response.
            Console.WriteLine(response.Message.Content);
        }
    }
    // Make sure any async whiteboard processing is complete before we print out its contents.
    await whiteboardProvider.WhenProcessingCompleteAsync();

    // Print out the whiteboard contents.
    Console.WriteLine("Whiteboard contents:");
    foreach (var item in whiteboardProvider.CurrentWhiteboardContent)
    {
        Console.WriteLine($"- {item}");
    }
    Console.WriteLine();

    // Truncate the chat history if it gets too big.
    await agentThread.ChatHistory.ReduceInPlaceAsync(chatHistoryReducer, CancellationToken.None);
}


sealed class VMPlugin
{
    [KernelFunction]
    public Task<VMCreateResult> CreateVM(Region region, OperatingSystem os, string name, int numberOfCores, int memorySizeInGB, int hddSizeInGB)
    {
        if (name == "ContosoVM")
        {
            throw new Exception("VM name already exists");
        }

        return Task.FromResult(new VMCreateResult { VMId = Guid.NewGuid().ToString() });
    }
}

class VMCreateResult
{
    public string VMId { get; set; } = string.Empty;
}

enum Region
{
    NorthAmerica,
    SouthAmerica,
    Europe,
    Asia,
    Africa,
    Australia
}

enum OperatingSystem
{
    Windows,
    Linux,
    MacOS
}