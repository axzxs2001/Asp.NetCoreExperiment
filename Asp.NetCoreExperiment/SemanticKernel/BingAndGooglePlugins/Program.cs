using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Web.Bing;
using Microsoft.SemanticKernel.Plugins.Web;
using Microsoft.SemanticKernel.Plugins.Web.Google;

await RunAsync();
static async Task RunAsync()
{

    var openAIModelId = "gpt-4";
    var openAIApiKey = File.ReadAllText(@"C:\GPT\key.txt");
   
    if (openAIModelId == null || openAIApiKey == null)
    {
        Console.WriteLine("未找到 OpenAI 凭据。 跳过示例。");
        return;
    }

    Kernel kernel = Kernel.CreateBuilder()
        .AddOpenAIChatCompletion(
            modelId: openAIModelId,
            apiKey: openAIApiKey)
        .Build();

    //// Load Bing plugin
    //string bingApiKey =File.ReadAllText(@"C:\GPT\bingkey.txt");
    //if (bingApiKey == null)
    //{
    //    Console.WriteLine("未找到 Bing 凭据。 跳过示例。");
    //}
    //else
    //{
    //    #pragma warning disable SKEXP0054
    //    var bingConnector = new BingConnector(bingApiKey);
    //    var bing = new WebSearchEnginePlugin(bingConnector);
    //    kernel.ImportPluginFromObject(bing, "bing");
    //    await Example1Async(kernel, "bing");
    //    await Example2Async(kernel);
    //}

    // Load Google plugin
    string googleApiKey = " ";
    string googleSearchEngineId = "My Project";

    if (googleApiKey == null || googleSearchEngineId == null)
    {
        Console.WriteLine("Google credentials not found. Skipping example.");
    }
    else
    {
#pragma warning disable SKEXP0054
        using var googleConnector = new GoogleConnector(
            apiKey: googleApiKey,
            searchEngineId: googleSearchEngineId);
        var google = new WebSearchEnginePlugin(googleConnector);
        kernel.ImportPluginFromObject(new WebSearchEnginePlugin(googleConnector), "google");
        await Example1Async(kernel, "google");
    }
}

static async Task Example1Async(Kernel kernel, string searchPluginName)
{
    Console.WriteLine("======== Bing and Google Search Plugins ========");

    // Run
    var question = "世界上最大的建筑是什么？";
    var function = kernel.Plugins[searchPluginName]["search"];
    var result = await kernel.InvokeAsync(function, new() { ["query"] = question });

    Console.WriteLine(question);
    Console.WriteLine($"----{searchPluginName}----");
    Console.WriteLine(result.GetValue<string>());

 
}

static async Task Example2Async(Kernel kernel)
{
    Console.WriteLine("======== Use Search Plugin to answer user questions ========");

    const string SemanticFunction = @"仅当您了解事实或已提供信息时才回答问题。
当您没有足够的信息时，您可以回复一个命令列表来查找所需的信息。
回答多个问题时，请使用要点列表。
注意：确保使用反斜杠字符转义单引号和双引号。

[可用命令]
- 必应搜索

[提供的信息]
{{ $externalInformation }}

[示例1]
问：意大利最大的湖是哪个？
答：加尔达湖，又称加尔达湖。

[示例2]
问：意大利最大的湖是哪个？ 最小的正数是多少？
回答：
* 加尔达湖，也称为加尔达湖。
* 最小的正数是 1。

[实施例3]
问：法拉利的股价是多少？ 目前世界排名第一的女子网球运动员是谁？
回答：
{{ '{{' }} bing.search ""法拉利股票价格是多少？"" {{ '}}' }}.
{{ '{{' }} bing.search ""谁是目前世界排名第一的女子网球运动员？"" {{ '}}' }}.

[示例结束]

[任务]
问题：{{ $question }}。
回答： ";

    var question = "Who is the most followed person on TikTok right now? What's the exchange rate EUR:USD?";
    Console.WriteLine(question);

    var oracle = kernel.CreateFunctionFromPrompt(SemanticFunction, new OpenAIPromptExecutionSettings() { MaxTokens = 150, Temperature = 0, TopP = 1 });

    var answer = await kernel.InvokeAsync(oracle, new KernelArguments()
    {
        ["question"] = question,
        ["externalInformation"] = string.Empty
    });

    var result = answer.GetValue<string>()!;

    // If the answer contains commands, execute them using the prompt renderer.
    if (result.Contains("bing.search", StringComparison.OrdinalIgnoreCase))
    {
        var promptTemplateFactory = new KernelPromptTemplateFactory();
        var promptTemplate = promptTemplateFactory.Create(new PromptTemplateConfig(result));

        Console.WriteLine("---- Fetching information from Bing...");
        var information = await promptTemplate.RenderAsync(kernel);

        Console.WriteLine("Information found:");
        Console.WriteLine(information);

        // Run the prompt function again, now including information from Bing
        answer = await kernel.InvokeAsync(oracle, new KernelArguments()
        {
            ["question"] = question,
            // The rendered prompt contains the information retrieved from search engines
            ["externalInformation"] = information
        });
    }
    else
    {
        Console.WriteLine("AI had all the information, no need to query Bing.");
    }

    Console.WriteLine("---- ANSWER:");
    Console.WriteLine(answer.GetValue<string>());

}