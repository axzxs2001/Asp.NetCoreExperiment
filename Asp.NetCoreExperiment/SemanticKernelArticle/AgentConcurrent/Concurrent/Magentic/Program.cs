using Azure;
using Azure.AI.Agents.Persistent;
using Azure.Identity;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.AzureAI;
using Microsoft.SemanticKernel.Agents.Magentic;
using Microsoft.SemanticKernel.Agents.Orchestration;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using OpenAI.Assistants;
using CodeInterpreterToolDefinition = Azure.AI.Agents.Persistent.CodeInterpreterToolDefinition;


#pragma warning disable
string ManagerModel = "o3-mini";
string ResearcherModel = "gpt-4o-search-preview";

bool ForceOpenAI = true;
// 定义代理
Kernel researchKernel = CreateKernelWithOpenAIChatCompletion(ResearcherModel);
ChatCompletionAgent researchAgent =
    CreateAgent(
        name: "ResearchAgent",
        description: "一个可以访问网络搜索的有用助手。请要求它执行网络搜索。",
        instructions: "你是一个研究员。你查找信息，不进行额外的计算或定量分析。",
        kernel: researchKernel);




// 定义代理
Kernel researchKernel1 = CreateKernelWithAzureOpenAIChatCompletion(ResearcherModel);
ChatCompletionAgent coderAgent =
    CreateAgent(
        name: "CoderAgent",
        description: "编写并执行代码以处理和分析数据。",
        instructions: "你使用代码解决问题。请提供详细的分析和计算过程。",
        kernel: researchKernel);


// 创建一个监视器来捕获代理响应（通过ResponseCallback）
// 以在此示例结束时显示。（可选）
// 注意：创建您自己的回调以在您的应用程序或服务中捕获响应。
OrchestrationMonitor monitor = new();
// 定义编排
Kernel managerKernel = CreateKernelWithOpenAIChatCompletion(ManagerModel);
StandardMagenticManager manager =
    new(managerKernel.GetRequiredService<IChatCompletionService>(), new OpenAIPromptExecutionSettings())
    {
        MaximumInvocationCount = 5,
    };
MagenticOrchestration orchestration =
    new(manager, researchAgent, coderAgent)
    {
        ResponseCallback = monitor.ResponseCallback,
        LoggerFactory = NullLoggerFactory.Instance,
    };

// 启动运行时
InProcessRuntime runtime = new();
await runtime.StartAsync();

string input =
    """
            我正在准备一份关于不同机器学习模型架构能效的报告。
            请比较 ResNet-50、BERT-base 和 GPT-2 在标准数据集上的训练和推理能耗（例如，ResNet 用 ImageNet，BERT 用 GLUE，GPT-2 用 WebText）。
            然后，基于在 Azure Standard_NC6s_v3 虚拟机上训练 24 小时的情况，估算与每个模型相关的二氧化碳排放量。
            请使用表格呈现结果，以便更清晰。
            最后，请针对每种任务类型（图像分类、文本分类、文本生成）推荐最节能高效的模型。
            """;
Console.WriteLine($"\n# 输入:\n{input}\n");
OrchestrationResult<string> result = await orchestration.InvokeAsync(input, runtime);
string text = await result.GetValueAsync(TimeSpan.FromSeconds(30 * 10));
Console.WriteLine($"\n# 结果: {text}");

await runtime.RunUntilIdleAsync();

Console.WriteLine("\n\n编排历史");
foreach (ChatMessageContent message in monitor.History)
{
    Console.WriteLine($"{message.Role}:{message.Content}");
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
Kernel CreateKernelWithOpenAIChatCompletion(string model)
{
    IKernelBuilder builder = Kernel.CreateBuilder();

    builder.AddOpenAIChatCompletion(
        model,
        File.ReadAllText("c://gpt/key.txt"));

    return builder.Build();
}

Kernel CreateKernelWithAzureOpenAIChatCompletion(string model)
{
    IKernelBuilder builder = Kernel.CreateBuilder();

    builder.AddAzureOpenAIChatCompletion(
        "gpt-4.1",
      "https://smartfill-20241007.openai.azure.com/",

        File.ReadAllText("c://gpt/azure_key.txt"));

    return builder.Build();
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