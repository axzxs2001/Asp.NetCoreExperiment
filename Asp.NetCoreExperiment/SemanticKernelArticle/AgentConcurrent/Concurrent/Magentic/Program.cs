using Azure.AI.Agents.Persistent;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Orchestration;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using OpenAI.Assistants;

 string ManagerModel = "o3-mini";
 string ResearcherModel = "gpt-4o-search-preview";


 bool ForceOpenAI => true;



// Define the agents
Kernel researchKernel = CreateKernelWithOpenAIChatCompletion(ResearcherModel);
ChatCompletionAgent researchAgent =
    CreateAgent(
        name: "ResearchAgent",
        description: "A helpful assistant with access to web search. Ask it to perform web searches.",
        instructions: "You are a Researcher. You find information without additional computation or quantitative analysis.",
        kernel: researchKernel);

PersistentAgentsClient agentsClient = AzureAIAgent.CreateAgentsClient(TestConfiguration.AzureAI.Endpoint, new AzureCliCredential());
PersistentAgent definition =
    await agentsClient.Administration.CreateAgentAsync(
        TestConfiguration.AzureAI.ChatModelId,
        name: "CoderAgent",
        description: "Write and executes code to process and analyze data.",
        instructions: "You solve questions using code. Please provide detailed analysis and computation process.",
        tools: [new CodeInterpreterToolDefinition()]);
AzureAIAgent coderAgent = new(definition, agentsClient);

// Create a monitor to capturing agent responses (via ResponseCallback)
// to display at the end of this sample. (optional)
// NOTE: Create your own callback to capture responses in your application or service.
OrchestrationMonitor monitor = new();
// Define the orchestration
Kernel managerKernel = CreateKernelWithChatCompletion(ManagerModel);
StandardMagenticManager manager =
    new(managerKernel.GetRequiredService<IChatCompletionService>(), new OpenAIPromptExecutionSettings())
    {
        MaximumInvocationCount = 5,
    };
MagenticOrchestration orchestration =
    new(manager, researchAgent, coderAgent)
    {
        ResponseCallback = monitor.ResponseCallback,
        LoggerFactory = this.LoggerFactory,
    };

// Start the runtime
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
Console.WriteLine($"\n# INPUT:\n{input}\n");
OrchestrationResult<string> result = await orchestration.InvokeAsync(input, runtime);
string text = await result.GetValueAsync(TimeSpan.FromSeconds(30 * 10));
Console.WriteLine($"\n# RESULT: {text}");

await runtime.RunUntilIdleAsync();

Console.WriteLine("\n\nORCHESTRATION HISTORY");
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