
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Orchestration;
using Microsoft.SemanticKernel.Agents.Orchestration.Sequential;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
using Microsoft.SemanticKernel.ChatCompletion;

#pragma warning disable
var modelID = "gpt-4.1";
var openAIKey = File.ReadAllText("c://gpt/key.txt");

var kernel = Kernel.CreateBuilder()
           .AddOpenAIChatCompletion(modelID, openAIKey).Build();


ChatCompletionAgent analystAgent =
         CreateAgent(
             name: "Analyst",
             instructions:
             """
                你是一名市场分析师。根据产品描述，识别以下内容：
                -主要特征
                -目标受众
                -独特卖点
                """,
             description: "一个能从产品描述中提取关键概念的智能代理（Agent）。",
             kernel: kernel);
ChatCompletionAgent writerAgent =
    CreateAgent(
        name: "copywriter",
        instructions:
        """
                你是一名市场文案撰写人。根据一段描述产品特征、目标受众和独特卖点的文字，撰写一段引人注目的营销文案（例如新闻简报中的一个版块）。
                输出应简短（约150字），只输出一段完整的文案文本。
                """,
        description: "一个根据提取出的关键概念撰写营销文案的智能代理。",
        kernel: kernel);
ChatCompletionAgent editorAgent =
    CreateAgent(
        name: "editor",
        instructions:
        """
                你是一名编辑。根据提供的文案草稿，完成以下工作：
                -纠正语法错误
                -提高表达的清晰度
                -确保语气一致
                -调整格式
                -打磨语言，使其更加精炼、专业
                输出为一段完整的、优化后的文案文本。
                """,
        description: "一个对营销文案进行格式调整和校对的智能代理。",
        kernel: kernel);


var monitor = new OrchestrationMonitor();
// Define the orchestration
var orchestration =
   new SequentialOrchestration(analystAgent, writerAgent, editorAgent)
   {
       ResponseCallback = monitor.ResponseCallback,
       //LoggerFactory = NullLoggerFactory.Instance,
   };

// Start the runtime
InProcessRuntime runtime = new();
await runtime.StartAsync();

// Run the orchestration
string input = "一款环保型不锈钢水瓶，可使饮品保持冷却状态长达24小时。";
Console.WriteLine($"\n# INPUT: {input}\n");
OrchestrationResult<string> result = await orchestration.InvokeAsync(input, runtime);
string text = await result.GetValueAsync(TimeSpan.FromSeconds(30));
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
        this.History.Add(response);
        return ValueTask.CompletedTask;
    }
}