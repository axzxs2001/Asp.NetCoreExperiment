using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.Google;
using System.Text;
#pragma warning disable SKEXP0070
Console.WriteLine("============= Google AI - Gemini Chat Completion with function calling =============");

string geminiApiKey = File.ReadAllText("C:/GPT/gemini_key.txt");
string geminiModelId = "gemini-1.5-pro-latest";

if (geminiApiKey is null || geminiModelId is null)
{
    Console.WriteLine("Gemini credentials not found. Skipping example.");
    return;
}

Kernel kernel = Kernel.CreateBuilder()
    .AddGoogleAIGeminiChatCompletion(
        modelId: geminiModelId,
        apiKey: geminiApiKey)
    .Build();


await StreamingChatAsync(kernel);

async Task FuncationCall(Kernel kernel)
{
    kernel.ImportPluginFromFunctions("HelperFunctions",
        [
            kernel.CreateFunctionFromMethod(() => DateTime.UtcNow.ToString("R"), "GetCurrentUtcTime", "Retrieves the current time in UTC."),
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
                }, "Get_Weather_For_City", "Gets the current weather for the specified city"),
        ]);

    Console.WriteLine("======== Example 1: Use automated function calling with a non-streaming prompt ========");
    {
        GeminiPromptExecutionSettings settings = new() { ToolCallBehavior = GeminiToolCallBehavior.AutoInvokeKernelFunctions };
        Console.WriteLine(await kernel.InvokePromptAsync(
            "Check current UTC time, and return current weather in Paris city", new(settings)));
        Console.WriteLine();
    }
}


async Task StreamingChatAsync(Kernel kernel)
{
    Console.WriteLine("======== Streaming Chat ========");

    var chatHistory = new ChatHistory();
    var chat = kernel.GetRequiredService<IChatCompletionService>();

    // First user message
    chatHistory.AddUserMessage("你好，我正在寻找替代的咖啡冲泡方法，你能帮我吗？");
    await MessageOutputAsync(chatHistory);

    // First bot assistant message
    var streamingChat = chat.GetStreamingChatMessageContentsAsync(chatHistory);
    var reply = await MessageOutputStreamAsync(streamingChat);
    chatHistory.Add(reply);

    // Second user message
    chatHistory.AddUserMessage("给我最好的精品咖啡烘焙机。");
    await MessageOutputAsync(chatHistory);

    // Second bot assistant message
    streamingChat = chat.GetStreamingChatMessageContentsAsync(chatHistory);
    reply = await MessageOutputStreamAsync(streamingChat);
    chatHistory.Add(reply);
}


Task MessageOutputAsync(ChatHistory chatHistory)
{
    var message = chatHistory.Last();

    Console.WriteLine($"{message.Role}: {message.Content}");
    Console.WriteLine("------------------------");

    return Task.CompletedTask;
}

async Task<ChatMessageContent> MessageOutputStreamAsync(IAsyncEnumerable<StreamingChatMessageContent> streamingChat)
{
    bool first = true;
    StringBuilder messageBuilder = new();
    await foreach (var chatMessage in streamingChat)
    {
        if (first)
        {
            Console.Write($"{chatMessage.Role}: ");
            first = false;
        }

        Console.Write(chatMessage.Content);
        messageBuilder.Append(chatMessage.Content);
    }

    Console.WriteLine();
    Console.WriteLine("------------------------");
    return new ChatMessageContent(AuthorRole.Assistant, messageBuilder.ToString());
}