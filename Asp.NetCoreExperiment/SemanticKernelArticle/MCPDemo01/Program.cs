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
using ModelContextProtocol.SemanticKernel.Options;
using ModelContextProtocol.Server;
using OpenAI;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

var key = File.ReadAllText("c:/gpt/key.txt");

while (true)
{
    Console.WriteLine("===========================================================");
    Console.WriteLine("1、获取工具列表  2、Client调用工具  3、SK调用工具  0、退出");
    Console.WriteLine("===========================================================");
    var no = Console.ReadLine();
    switch (no)
    {
        case "1":
            await MCPClientToolsListAsync();
            break;
        case "2":

            await MCPClientAsync();
            break;
        case "3":
            await SKClientAsync();
            break;
        case "0":
            return;
    }
}

async Task MCPClientToolsListAsync()
{
    var serverConfig = new McpServerConfig
    {
        Id = "QueryOrder",
        Name = "QueryOrder",
        TransportType = TransportTypes.Sse,
        Location = "http://localhost:3001/sse"
    };
    var clientOptions = new McpClientOptions
    {
        ClientInfo = new()
        {
            Name = "QueryOrderClient",
            Version = "0.0.1",
        }
    };
    var mcpClient = await McpClientFactory.CreateAsync(serverConfig, clientOptions);
    Console.WriteLine("获取Tools:");
    var tools = await mcpClient.GetAIFunctionsAsync();
    
    foreach (var tool in tools)
    {

        Console.WriteLine($"{tool.Name},{tool.Description}");
    }
    Console.WriteLine();
}

async Task MCPClientAsync()
{
    var serverConfig = new McpServerConfig
    {
        Id = "QueryOrder",
        Name = "MCPOrderTool",
        TransportType = TransportTypes.Sse,
        Location = "http://localhost:3001/sse"
    };
    var clientOptions = new McpClientOptions
    {
        ClientInfo = new()
        {
            Name = "QueryOrderClient",
            Version = "0.0.1",
        }
    };
    var mcpClient = await McpClientFactory.CreateAsync(serverConfig, clientOptions);
    var functions = await mcpClient.GetAIFunctionsAsync();
    IChatClient chatClient = new OpenAIClient(key).AsChatClient("gpt-4o-mini")
    .AsBuilder().UseFunctionInvocation().Build();
    var response = chatClient.GetStreamingResponseAsync(
     "查询本周的订单",
     new()
     {
         Tools = [.. functions],
     });
    await foreach (var item in response)
    {
        Console.Write(item.Text);
    }
    Console.WriteLine();
}

async Task SKClientAsync()
{
    var builder = Kernel.CreateBuilder();
    builder.Services.AddLogging(c => c.AddDebug().SetMinimumLevel(LogLevel.Trace));

    builder.Services.AddOpenAIChatCompletion(
        serviceId: "openai",
        modelId: "gpt-4o-mini",
        apiKey: key);

    var kernel = builder.Build();
    kernel.Plugins.AddFromType<TimeInformationPlugin>();
    await kernel.Plugins.AddMcpFunctionsFromSseServerAsync("MCPOrderTool", "http://localhost:3001/sse");
    var executionSettings = new OpenAIPromptExecutionSettings
    {
        Temperature = 0,
        FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
    };
    var prompt = "请查询本月的订单";
    var result = kernel.InvokePromptStreamingAsync(prompt, new(executionSettings));
    await foreach (var item in result)
    {
        Console.Write(item.ToString());
    }
    Console.WriteLine();
}


public class TimeInformationPlugin
{
    [KernelFunction, Description("获取当前的 UTC 时间。")]
    public string GetCurrentUtcTime()
        => DateTime.UtcNow.ToString("R");
}