using Microsoft.Extensions.AI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using ModelContextProtocol;
using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol.Transport;

#pragma warning disable
var apiKey = File.ReadAllText("C://gpt/key.txt");

await using var mcpClient = await McpClientFactory.CreateAsync(
    new McpServerConfig()
    {
        Id = "MCPServer",
        Name = "MCPServer",
        TransportType = TransportTypes.Sse,
        Location = "http://localhost:3001/sse"
    },
    new McpClientOptions()
    {
        ClientInfo = new() { Name = "MCPClient", Version = "1.0.0" }
    }
);

var tools = await mcpClient.ListToolsAsync();
Console.ReadLine();
IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
var currentFunction = KernelFunctionFactory.CreateFromMethod(
    (KernelArguments arguments) =>
    {
        var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        arguments["currentTime"] = time; // 存到 arguments 里
        return Task.FromResult(time);
    },
    functionName: "currentTime",
    description: "获取当前时间"
);
kernelBuilder.Plugins.AddFromFunctions("GetCurrentTime", new KernelFunction[] { currentFunction });
kernelBuilder.Plugins.AddFromFunctions("Order", tools.Select(aiFunction => aiFunction.AsKernelFunction()));
kernelBuilder.Services.AddOpenAIChatCompletion(serviceId: "openai", modelId: "gpt-4o-mini", apiKey: apiKey);

Kernel kernel = kernelBuilder.Build();


OpenAIPromptExecutionSettings executionSettings = new()
{
    Temperature = 0,
    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
};

// Execute a prompt using the MCP tools. The AI model will automatically call the appropriate MCP tools to answer the prompt.
var prompt = "查一下今天的订单";
var result = await kernel.InvokePromptAsync(prompt, new(executionSettings));
Console.WriteLine(result);

Console.ReadLine();