using A2A;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

while (true)
{
    Console.WriteLine("1、水果Agent  2、翻译Agent");
    switch (Console.ReadLine())
    {

        case "1":
            await ShopAgentAsync();
            break;
        case "2":
            await TranslateAgentAsync();
            break;
        default:
            Console.WriteLine("输入错误，默认水果Agent");
            break;
    }
}
async Task ShopAgentAsync()
{
    var cardResolver = new A2ACardResolver(new Uri("http://localhost:5000/"));
    var agentCard = await cardResolver.GetAgentCardAsync();
    var options = new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };
    Console.WriteLine("Card信息：");
    Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(agentCard, options));
    var client = new A2AClient(new Uri(agentCard.Url));

    Console.WriteLine("--------------------------------");
    Console.WriteLine("正在请求：“给出单价大于10块的水果”……");
    var a2aResponse = await client.SendMessageAsync(new MessageSendParams
    {
        Message = new AgentMessage
        {
            Role = MessageRole.User,
            Parts = [new TextPart { Text = "给出单价大于10块的水果!" }]
        }
    });
    if (a2aResponse is AgentTask)
    {
        var message = a2aResponse as AgentTask;
        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(message, options));
    }
}
async Task TranslateAgentAsync()
{
    var cardResolver = new A2ACardResolver(new Uri("http://localhost:6000/TranslateAgent"));
    var agentCard = await cardResolver.GetAgentCardAsync();
    var options = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
    };
    Console.WriteLine("Card信息：");
    Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(agentCard, options));

    Console.WriteLine("--------------------------------");
    Console.WriteLine("正在请求：“把我输入的内容翻译成日语：明天是个好日子”……");
    var client = new A2AClient(new Uri(agentCard.Url));
    var a2aResponse = await client.SendMessageAsync(new MessageSendParams
    {
        Message = new AgentMessage
        {
            Role = MessageRole.User,
            Parts = [new TextPart { Text = "把我输入的内容翻译成日语：明天是个好日子" }]
        }
    });
    if (a2aResponse is AgentTask)
    {
        var message = a2aResponse as AgentTask;
        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(message, options));
    }
}

//return;

//using var loggerFactory = LoggerFactory.Create(builder =>
//{
//    builder.SetMinimumLevel(LogLevel.Information);
//});
//var logger = loggerFactory.CreateLogger("A2AClient");


//var arr = File.ReadAllLines("C:/gpt/azure_key.txt");
//var apiKey = arr[2];
//var modelId = arr[0];
//var endpoint = arr[1];
//var agentUrls = new string[] { "http://localhost:5000/", "http://localhost:6000/" };


//var agent = await CreateNewAgentsAsync(modelId, endpoint, apiKey, agentUrls);
//try
//{
//    while (true)
//    {
//        Console.WriteLine("请输入你请求，按q退了");
//        string? message = Console.ReadLine();
//        if (string.IsNullOrWhiteSpace(message))
//        {
//            Console.WriteLine("输入不能为空");
//            continue;
//        }
//        if (message.ToLower() == "q")
//        {
//            break;
//        }
//        await foreach (AgentResponseItem<ChatMessageContent> response in agent!.InvokeAsync(message))
//        {
//            Console.WriteLine();
//            Console.ForegroundColor = ConsoleColor.Cyan;
//            Console.WriteLine($"Agent: {response.Message.Content}");
//            Console.ResetColor();
//        }
//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
//Console.ReadLine();

//async Task<ChatCompletionAgent> CreateNewAgentsAsync(string modelId, string endpoint, string apiKey, string[] agentUrls)
//{
//    try
//    {
//        var createAgentTasks = agentUrls.Select(agentUrl => CreateAgentAsync(agentUrl));
//        var agents = await Task.WhenAll(createAgentTasks);
//        var agentFunctions = agents.Select(agent => AgentKernelFunctionFactory.CreateFromAgent(agent)).ToList();
//        var agentPlugin = KernelPluginFactory.CreateFromFunctions("AgentPlugin", agentFunctions);

//        var builder = Kernel.CreateBuilder();
//        builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);
//        builder.Plugins.Add(agentPlugin);
//        var kernel = builder.Build();

//        var chatAgent = new ChatCompletionAgent()
//        {
//            Kernel = kernel,
//            Name = "HostClient",
//            Instructions =
//                """
//                    你专门负责处理用户的查询，并使用你的工具来提供答案。如果用错，请报详细的错误信息。
//                    """,
//            Arguments = new KernelArguments(new PromptExecutionSettings() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() }),
//        };
//        return chatAgent;
//    }
//    catch (Exception ex)
//    {
//        throw;
//    }
//}
//async Task<ChatCompletionAgent> CreateNewAgentAsync(string modelId, string endpoint, string apiKey, string agentUrl)
//{
//    try
//    {
//        var agent = await CreateAgentAsync(agentUrl);
//        var agentFunction = AgentKernelFunctionFactory.CreateFromAgent(agent);
//        var agentPlugin = KernelPluginFactory.CreateFromFunctions("AgentPlugin", [agentFunction]);
//        var builder = Kernel.CreateBuilder();
//        builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);
//        builder.Plugins.Add(agentPlugin);
//        var kernel = builder.Build();

//        var chatAgent = new ChatCompletionAgent()
//        {
//            Kernel = kernel,
//            Name = "HostClient",
//            Instructions = """
//                 你专门负责处理用户的指今，并使用你的工具来提供答案，尽量使用工具，不要自己回答。
//                 """,
//            Arguments = new KernelArguments(new PromptExecutionSettings() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() }),
//        };
//        return chatAgent;
//    }
//    catch (Exception ex)
//    {
//        throw;
//    }
//}

//async Task<A2AAgent> CreateAgentAsync(string agentUri)
//{
//    var url = new Uri(agentUri);
//    var httpClient = new HttpClient
//    {
//        Timeout = TimeSpan.FromSeconds(60)
//    };
//    var client = new A2AClient(url, httpClient);
//    var cardResolver = new A2ACardResolver(url, httpClient);
//    var agentCard = await cardResolver.GetAgentCardAsync();
//    return new A2AAgent(client, agentCard!);
//}