using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Orchestration;
using Microsoft.SemanticKernel.Agents.Orchestration.GroupChat;
using Microsoft.SemanticKernel.Agents.Orchestration.Handoff;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Text.Json;


#pragma warning disable
var modelID = "gpt-4.1";
var openAIKey = File.ReadAllText("c://gpt/key.txt");

var kernel = Kernel.CreateBuilder()
           .AddOpenAIChatCompletion(modelID, openAIKey).Build();
ChatCompletionAgent triageAgent =
         CreateAgent(
                kernel: kernel,
             instructions: "一个负责分流问题的客户支持代理。",
             name: "TriageAgent",
             description: "处理客户请求。");
ChatCompletionAgent statusAgent =
    CreateAgent(
        kernel: kernel,
        name: "OrderStatusAgent",
        instructions: "处理订单状态请求。",
        description: "一个负责查询订单状态的客户支持代理。");
statusAgent.Kernel.Plugins.Add(KernelPluginFactory.CreateFromObject(new OrderStatusPlugin()));
ChatCompletionAgent returnAgent =
    CreateAgent(
        kernel: kernel,
        name: "OrderReturnAgent",
        instructions: "处理订单退货请求。",
        description: "一个负责处理订单退货的客户支持代理。");
returnAgent.Kernel.Plugins.Add(KernelPluginFactory.CreateFromObject(new OrderReturnPlugin()));
ChatCompletionAgent refundAgent =
    CreateAgent(
        kernel: kernel,
        name: "OrderRefundAgent",
        instructions: "处理订单退款请求。",
        description: "一个负责处理订单退款的客户支持代理。");
refundAgent.Kernel.Plugins.Add(KernelPluginFactory.CreateFromObject(new OrderRefundPlugin()));

// Create a monitor to capturing agent responses (via ResponseCallback)
// to display at the end of this sample. (optional)
// NOTE: Create your own callback to capture responses in your application or service.
OrchestrationMonitor monitor = new();
// Define user responses for InteractiveCallback (since sample is not interactive)
Queue<string> responses = new();
string task = "我是一位需要订单帮助的客户";
responses.Enqueue("我想查询我的订单状态");
responses.Enqueue("我的订单号是123");
responses.Enqueue("我想退还另一份订单");
responses.Enqueue("订单号321");
responses.Enqueue("物品损坏");
responses.Enqueue("不需要了，再见");

// Define the orchestration
var orchestration =
    new HandoffOrchestration(OrchestrationHandoffs
            .StartWith(triageAgent)
            .Add(triageAgent, statusAgent, returnAgent, refundAgent)
            .Add(statusAgent, triageAgent, "如果问题与订单状态无关，则转接至此坐席")
            .Add(returnAgent, triageAgent, "如果问题与退货无关，则转接至此坐席")
            .Add(refundAgent, triageAgent, "如果问题与退款无关，则转接至此坐席"),
        triageAgent,
        statusAgent,
        returnAgent,
        refundAgent)
    {
        InteractiveCallback = () =>
        {
            string input = responses.Dequeue();
            Console.WriteLine($"\n# INPUT: {input}\n");
            return ValueTask.FromResult(new ChatMessageContent(AuthorRole.User, input));
        },
        ResponseCallback = monitor.ResponseCallback,
        LoggerFactory = NullLoggerFactory.Instance,
    };

// Start the runtime
InProcessRuntime runtime = new();
await runtime.StartAsync();

// Run the orchestration
Console.WriteLine($"\n# INPUT:\n{task}\n");
OrchestrationResult<string> result = await orchestration.InvokeAsync(task, runtime);

string text = await result.GetValueAsync(TimeSpan.FromSeconds(300));
Console.WriteLine($"\n# RESULT: {text}");

await runtime.RunUntilIdleAsync();

Console.WriteLine("\n\nORCHESTRATION HISTORY");
foreach (ChatMessageContent message in monitor.History)
{
    Console.WriteLine($"{message.AuthorName}:{message.Content}");
}


ChatCompletionAgent CreateAgent(string instructions, string? description = null, string? name = null, Kernel? kernel = null)
{
    return
        new ChatCompletionAgent
        {
            Name = name,
            Description = description,
            Instructions = instructions,
            Kernel = kernel,
             
        };
}

sealed class OrchestrationMonitor
{
    public ChatHistory History { get; } = [];

    public ValueTask ResponseCallback(Microsoft.SemanticKernel.ChatMessageContent response)
    {
        Console.WriteLine(response.Role);
        Console.WriteLine(response?.Content);
        this.History.Add(response);
        return ValueTask.CompletedTask;
    }
}

class OrderStatusPlugin
{
    [KernelFunction]
    public string CheckOrderStatus(string orderId) => $"订单 {orderId} 已发货，预计 2-3 天内送达。";
}

class OrderReturnPlugin
{
    [KernelFunction]
    public string ProcessReturn(string orderId, string reason) => $"订单 {orderId} 的退货已成功处理。";
}

class OrderRefundPlugin
{
    [KernelFunction]
    public string ProcessRefund(string orderId, string reason) => $"订单 {orderId} 的退款已成功处理。";
}
