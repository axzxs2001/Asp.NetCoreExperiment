using Azure.Core;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using ModelContextProtocol.Client;
using ModelContextProtocol.Configuration;
using ModelContextProtocol.Protocol.Transport;
using ModelContextProtocol.Protocol.Types;
using ModelContextProtocol.SemanticKernel.Extensions;
using ModelContextProtocol.Server;
using OpenAI;
using System.Text.Json;
using System.Text.Json.Serialization;

var key = File.ReadAllText("c:/gpt/key.txt");

Console.WriteLine("回车开始运行：");
Console.ReadLine();
//await MCPClientDemoAsync();
await SKMCPDemoAsync();
Console.ReadLine();

async Task MCPClientDemoAsync()
{ 
    Console.WriteLine("客户端连接服务端中……");
      
    var serverConfig = new McpServerConfig
    {
        Id = "test",
        Name = "test",
        TransportType = TransportTypes.Sse,
        Location = "http://localhost:3001/sse"
    };
    var opt = new McpClientOptions
    {
        ClientInfo = new()
        {
            Name = "echo-client",
            Version = "1.0.0",
        }
    }; 
    var mcpClient = await McpClientFactory.CreateAsync(serverConfig, opt);

    //Console.WriteLine("当前工具:");
    //var tools = mcpClient.ListToolsAsync();
    //await foreach (var tool in tools)
    //{     
    //    Console.WriteLine($"  {tool.Name}");
    //}
    //Console.WriteLine();

    IList<AIFunction> functions = await mcpClient.GetAIFunctionsAsync();
    IChatClient chatClient = new OpenAIClient(key).AsChatClient("gpt-4o-mini")
    .AsBuilder().UseFunctionInvocation().Build();
    var response = chatClient.GetStreamingResponseAsync(
     "调用 echo tool 参数用 'Hello Gui SuWei!'，然后把response返回",
     new()
     {
         Tools = [.. functions],
     });
    await foreach (var item in response)
    {
        Console.Write(item.Text);
    }
}

async Task SKMCPDemoAsync()
{
    var builder = Kernel.CreateBuilder();
    builder.Services.AddLogging(c => c.AddDebug().SetMinimumLevel(LogLevel.Trace));

    builder.Services.AddOpenAIChatCompletion(
        serviceId: "openai",
        modelId: "gpt-4o-mini",
        apiKey: key);

    var kernel = builder.Build();
    //var transportOptions = new Dictionary<string, string>
    //{
    //    ["command"] = "npx",
    //    ["arguments"] = "-y --verbose @modelcontextprotocol/server-everything"
    //};
    //// 💡 Add this line to enable MCP functions from a Stdio server named "Everything"
    //await kernel.Plugins.AddMcpFunctionsFromStdioServerAsync("Everything", transportOptions);
    try
    {
        await kernel.Plugins.AddMcpFunctionsFromSseServerAsync("echo", "http://localhost:3001/sse");
    }
    catch
    {

    }

    var executionSettings = new OpenAIPromptExecutionSettings
    {
        Temperature = 0,
        FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
    };

    var prompt = "调用 echo tool 参数用 'Hello Gui SuWei!'，然后把response返回";
    var result = await kernel.InvokePromptAsync(prompt, new(executionSettings)).ConfigureAwait(false);
    Console.WriteLine($"\n\n{prompt}\n{result}");
}