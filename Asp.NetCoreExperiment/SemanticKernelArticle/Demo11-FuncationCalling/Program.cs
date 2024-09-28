using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System;
using System.Globalization;
using System.Text;
#pragma warning disable SKEXP0001


var chatModelId = "gpt-4o";
var key = File.ReadAllText(@"C:\GPT\key.txt");

var builder = Kernel.CreateBuilder();
builder.AddOpenAIChatCompletion(chatModelId, key);
Kernel kernel = builder.Build();

kernel.ImportPluginFromFunctions("HelperFunctions",
[
    kernel.CreateFunctionFromMethod(GetChineseDay, "GetChineseDay", "返回中国的农历")
]);


//await Call1();
await Call2();
//await Call3();
//await Call4();

async Task Call1()
{
    Console.WriteLine("-----------------Call1 开始---------------------");
    var settings = new OpenAIPromptExecutionSettings() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };
    await foreach (var content in kernel.InvokePromptStreamingAsync("现在离吃月饼还有多少天？", new(settings)))
    {
        Console.Write(content);
    }
    Console.WriteLine();
    Console.WriteLine("-----------------Call1 结束---------------------");
}
async Task Call2()
{
    Console.WriteLine("-----------------Call2 开始---------------------");
    var settings = new OpenAIPromptExecutionSettings() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };
    var chat = kernel.GetRequiredService<IChatCompletionService>();
    var chatHistory = new ChatHistory("中国农历的表示方式是：九月初三，十二月二十三，请用这种表示方式表求农历日期。");
    chatHistory.AddUserMessage("现在离吃月饼还有多少天？");
    var contentBuilder = new StringBuilder();
    await foreach (var streamingContent in chat.GetStreamingChatMessageContentsAsync(chatHistory, settings, kernel))
    {
        if (streamingContent.Content is not null)
        {
            Console.Write(streamingContent.Content);
            contentBuilder.Append(streamingContent.Content);
        }
    }
    chatHistory.AddAssistantMessage(contentBuilder.ToString());
    Console.WriteLine();
    Console.WriteLine("-----------------Call2 结束---------------------");
}
async Task Call3()
{
    Console.WriteLine("-----------------Call3 开始---------------------");
    var settings = new OpenAIPromptExecutionSettings() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };
    var chat = kernel.GetRequiredService<IChatCompletionService>();
    var chatHistory = new ChatHistory();
    chatHistory.AddUserMessage("现在离吃月饼还有多少天？");
    while (true)
    {
        AuthorRole? authorRole = null;
        var fccBuilder = new FunctionCallContentBuilder();
        await foreach (var streamingContent in chat.GetStreamingChatMessageContentsAsync(chatHistory, settings, kernel))
        {
            if (streamingContent.Content is not null)
            {
                Console.Write(streamingContent.Content);
            }
            authorRole ??= streamingContent.Role;
            fccBuilder.Append(streamingContent);
        }
        Console.WriteLine();
        var functionCalls = fccBuilder.Build();
        if (!functionCalls.Any())
        {
            break;
        }
        var fcContent = new ChatMessageContent(role: authorRole ?? default, content: null);
        chatHistory.Add(fcContent);

        foreach (var functionCall in functionCalls)
        {
            fcContent.Items.Add(functionCall);
            var functionResult = await functionCall.InvokeAsync(kernel);
            Console.WriteLine($"FunctionName：{functionResult.FunctionName}，Return：{functionResult.InnerContent}");
            chatHistory.Add(functionResult.ToChatMessage());
        }
        Console.WriteLine();
    }
    Console.WriteLine();
    Console.WriteLine("-----------------Call3 结束---------------------");
}
async Task Call4()
{
    Console.WriteLine("-----------------Call4 开始---------------------");
    var chat = kernel.GetRequiredService<IChatCompletionService>();
    var settings = new OpenAIPromptExecutionSettings() { ToolCallBehavior = ToolCallBehavior.EnableKernelFunctions };
    var chatHistory = new ChatHistory();
    chatHistory.AddUserMessage("现在离吃月饼还有多少天？");
    while (true)
    {
        AuthorRole? authorRole = null;
        var fccBuilder = new FunctionCallContentBuilder();
        await foreach (var streamingContent in chat.GetStreamingChatMessageContentsAsync(chatHistory, settings, kernel))
        {
            if (streamingContent.Content is not null)
            {
                Console.Write(streamingContent.Content);
            }
            authorRole ??= streamingContent.Role;
            fccBuilder.Append(streamingContent);
        }
        Console.WriteLine();
        var functionCalls = fccBuilder.Build();
        if (!functionCalls.Any())
        {
            break;
        }
        var fcContent = new ChatMessageContent(role: authorRole ?? default, content: null);
        chatHistory.Add(fcContent);

        foreach (var functionCall in functionCalls)
        {
            fcContent.Items.Add(functionCall);
            var functionResult = await functionCall.InvokeAsync(kernel);
            Console.WriteLine($"FunctionName：{functionResult.FunctionName}，Return：{functionResult.InnerContent}");
            chatHistory.Add(functionResult.ToChatMessage());
        }
        Console.WriteLine();
    }
    Console.WriteLine();
    Console.WriteLine("-----------------Call4 结束---------------------");
}

string GetChineseDay()
{
    var chineseCalendar = new ChineseLunisolarCalendar();
    var today = DateTime.Now;
    int lunarYear = chineseCalendar.GetYear(today);
    int lunarMonth = chineseCalendar.GetMonth(today);
    int lunarDay = chineseCalendar.GetDayOfMonth(today);
    bool isLeapMonth = chineseCalendar.IsLeapMonth(lunarYear, lunarMonth);
    Console.WriteLine("-------GetChineseDay--------");
    return $"农历日期: {lunarYear}年 {(isLeapMonth ? "闰" : "")}{lunarMonth}月 {lunarDay}日";
}

Console.WriteLine();

//await RunNonStreamingPromptWithAutoFunctionCallingAsync();
//await RunStreamingPromptAutoFunctionCallingAsync();
//await RunNonStreamingChatAPIWithManualFunctionCallingAsync();
//await RunStreamingChatAPIWithManualFunctionCallingAsync();
//await RunNonStreamingPromptWithSimulatedFunctionAsync();
//await RunStreamingChatWithAutoFunctionCallingAsync();

async Task RunNonStreamingPromptWithAutoFunctionCallingAsync()
{
    Console.WriteLine("Auto function calling with a non-streaming prompt.");

    Kernel kernel = CreateKernel();

    OpenAIPromptExecutionSettings settings = new() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };

    Console.WriteLine(await kernel.InvokePromptAsync("鉴于当前的时间和天气，波士顿的天空可能是什么颜色？", new(settings)));
    Console.WriteLine("--------------------------------------");
}

async Task RunStreamingPromptAutoFunctionCallingAsync()
{
    Console.WriteLine("Auto function calling with a streaming prompt.");

    Kernel kernel = CreateKernel();

    OpenAIPromptExecutionSettings settings = new() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };

    await foreach (StreamingKernelContent update in kernel.InvokePromptStreamingAsync("鉴于当前的时间和天气，波士顿的天空可能是什么颜色？", new(settings)))
    {
        Console.Write(update);
    }
    Console.WriteLine();
    Console.WriteLine("--------------------------------------");
}

async Task RunNonStreamingChatAPIWithManualFunctionCallingAsync()
{
    Console.WriteLine("Manual function calling with a non-streaming prompt.");
    Kernel kernel = CreateKernel();
    IChatCompletionService chat = kernel.GetRequiredService<IChatCompletionService>();
    OpenAIPromptExecutionSettings settings = new() { ToolCallBehavior = ToolCallBehavior.EnableKernelFunctions };

    ChatHistory chatHistory = new();
    chatHistory.AddUserMessage("鉴于当前的时间和天气，波士顿的天空可能是什么颜色？");

    while (true)
    {
        ChatMessageContent result = await chat.GetChatMessageContentAsync(chatHistory, settings, kernel);
        if (result.Content is not null)
        {
            Console.Write(result.Content);
        }
        IEnumerable<FunctionCallContent> functionCalls = FunctionCallContent.GetFunctionCalls(result);
        if (!functionCalls.Any())
        {
            break;
        }
        chatHistory.Add(result);
        foreach (FunctionCallContent functionCall in functionCalls)
        {
            try
            {
                FunctionResultContent resultContent = await functionCall.InvokeAsync(kernel);
                Console.WriteLine($"FunctionName：{resultContent.FunctionName}，Return：{resultContent.InnerContent}");
                chatHistory.Add(resultContent.ToChatMessage());
            }
            catch (Exception ex)
            {
                chatHistory.Add(new FunctionResultContent(functionCall, ex).ToChatMessage());
                // or
                //chatHistory.Add(new FunctionResultContent(functionCall, "Error details that LLM can reason about.").ToChatMessage());
            }
        }
        Console.WriteLine();
    }
    Console.WriteLine();
    Console.WriteLine("--------------------------------------");
}

async Task RunStreamingChatAPIWithManualFunctionCallingAsync()
{
    Console.WriteLine("Manual function calling with a streaming prompt.");
    Kernel kernel = CreateKernel();

    IChatCompletionService chat = kernel.GetRequiredService<IChatCompletionService>();
    OpenAIPromptExecutionSettings settings = new() { ToolCallBehavior = ToolCallBehavior.EnableKernelFunctions };
    ChatHistory chatHistory = new();
    chatHistory.AddUserMessage("鉴于当前的时间和天气，波士顿的天空可能是什么颜色？");

    while (true)
    {
        AuthorRole? authorRole = null;
        var fccBuilder = new FunctionCallContentBuilder();
        await foreach (var streamingContent in chat.GetStreamingChatMessageContentsAsync(chatHistory, settings, kernel))
        {
            if (streamingContent.Content is not null)
            {
                Console.Write(streamingContent.Content);
            }
            authorRole ??= streamingContent.Role;
            fccBuilder.Append(streamingContent);
        }
        var functionCalls = fccBuilder.Build();
        if (!functionCalls.Any())
        {
            break;
        }
        var fcContent = new ChatMessageContent(role: authorRole ?? default, content: null);
        chatHistory.Add(fcContent);

        foreach (var functionCall in functionCalls)
        {
            fcContent.Items.Add(functionCall);
            var functionResult = await functionCall.InvokeAsync(kernel);
            Console.WriteLine($"FunctionName：{functionResult.FunctionName}，Return：{functionResult.InnerContent}");
            chatHistory.Add(functionResult.ToChatMessage());
        }
        Console.WriteLine();
    }
    Console.WriteLine();
    Console.WriteLine("--------------------------------------");
}

async Task RunNonStreamingPromptWithSimulatedFunctionAsync()
{
    Console.WriteLine("Simulated function calling with a non-streaming prompt.");

    Kernel kernel = CreateKernel();

    IChatCompletionService chat = kernel.GetRequiredService<IChatCompletionService>();

    OpenAIPromptExecutionSettings settings = new() { ToolCallBehavior = ToolCallBehavior.EnableKernelFunctions };

    ChatHistory chatHistory = new();
    chatHistory.AddUserMessage("鉴于当前的时间和天气，波士顿的天空可能是什么颜色？");

    while (true)
    {
        ChatMessageContent result = await chat.GetChatMessageContentAsync(chatHistory, settings, kernel);
        if (result.Content is not null)
        {
            Console.Write(result.Content);
        }

        chatHistory.Add(result);
        IEnumerable<FunctionCallContent> functionCalls = FunctionCallContent.GetFunctionCalls(result);
        if (!functionCalls.Any())
        {
            break;
        }
        foreach (FunctionCallContent functionCall in functionCalls)
        {
            FunctionResultContent resultContent = await functionCall.InvokeAsync(kernel); // Executing each function.

            chatHistory.Add(resultContent.ToChatMessage());
        }
        FunctionCallContent simulatedFunctionCall = new("weather-alert", id: "call_123");
        result.Items.Add(simulatedFunctionCall);

        string simulatedFunctionResult = "A Tornado Watch has been issued, with potential for severe thunderstorms causing unusual sky colors like green, yellow, or dark gray. Stay informed and follow safety instructions from authorities.";
        chatHistory.Add(new FunctionResultContent(simulatedFunctionCall, simulatedFunctionResult).ToChatMessage());

        Console.WriteLine();
    }
    Console.WriteLine();
    Console.WriteLine("--------------------------------------");
}

async Task RunStreamingChatWithAutoFunctionCallingAsync()
{
    Console.WriteLine("Auto function calling with a streaming chat");

    Kernel kernel = CreateKernel();

    OpenAIPromptExecutionSettings settings = new() { ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };
    IChatCompletionService chat = kernel.GetRequiredService<IChatCompletionService>();
    ChatHistory chatHistory = new();
    int iteration = 0;

    while (true)
    {
        Console.Write("Question (Type \"quit\" to leave): ");

        string question = iteration == 0 ? "鉴于当前的时间和天气，波士顿的天空可能是什么颜色？" : "quit";
        if (question == "quit")
        {
            break;
        }
        chatHistory.AddUserMessage(question);
        StringBuilder sb = new();
        await foreach (var update in chat.GetStreamingChatMessageContentsAsync(chatHistory, settings, kernel))
        {
            if (update.Content is not null)
            {
                Console.Write(update.Content);
                sb.Append(update.Content);
            }
        }
        chatHistory.AddAssistantMessage(sb.ToString());
        Console.WriteLine();
        iteration++;
    }
    Console.WriteLine();
    Console.WriteLine("--------------------------------------");
}

Kernel CreateKernel()
{
    var chatModelId = "gpt-4o";
    var key = File.ReadAllText(@"C:\GPT\key.txt");

    var builder = Kernel.CreateBuilder();
    builder.AddOpenAIChatCompletion(chatModelId, key);
    Kernel kernel = builder.Build();

    kernel.ImportPluginFromFunctions("HelperFunctions",
    [
       // kernel.CreateFunctionFromMethod(() => DateTime.UtcNow.ToString("R"), "GetCurrentUtcTime", "Retrieves the current time in UTC."),
            kernel.CreateFunctionFromMethod((string cityName) =>
                cityName switch
                {
                    "Boston" => "61 and rainy",
                    "London" => "55 and cloudy",
                    "Miami" => "80 and sunny",
                    "Paris" => "60 and rainy",
                    "Tokyo" => "50 and sunny",
                    "Sydney" => "75 and sunny",
                    "Tel Aviv" => "80 and sunny",
                    _ => "31 and snowing",
                }, "GetWeatherForCity", "Gets the current weather for the specified city"),
        ]);

    return kernel;
}
