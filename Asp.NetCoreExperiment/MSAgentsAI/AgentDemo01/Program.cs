using Azure.AI.OpenAI;
using Azure.Identity;
using Azure.Monitor.OpenTelemetry.Exporter;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI;
using OpenTelemetry;
using OpenTelemetry.Trace;
using System.ClientModel;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
#pragma warning disable 


var arr = File.ReadLines("C://gpt/azure_key.txt").ToArray();

var endpoint = arr[1];
var deploymentName = arr[0];
var key = arr[2];
var credential = new ApiKeyCredential(key);

await F6();


Console.ReadLine();

async Task F6()
{
    var applicationInsightsConnectionString = File.ReadAllText("C://gpt/IngestionConnStr.txt");

    const string JokerName = "Joker";
    const string JokerInstructions = "You are good at telling jokes.";

    // Create TracerProvider with console exporter
    // This will output the telemetry data to the console.
    string sourceName = Guid.NewGuid().ToString("N");
    var tracerProviderBuilder = Sdk.CreateTracerProviderBuilder()
        .AddSource(sourceName)
        .AddConsoleExporter();
    if (!string.IsNullOrWhiteSpace(applicationInsightsConnectionString))
    {
        tracerProviderBuilder.AddAzureMonitorTraceExporter(options => options.ConnectionString = applicationInsightsConnectionString);
    }
    using var tracerProvider = tracerProviderBuilder.Build();

    // Create the agent, and enable OpenTelemetry instrumentation.
    AIAgent agent = new AzureOpenAIClient(new Uri(endpoint), credential)
        .GetChatClient(deploymentName)
        .CreateAIAgent(JokerInstructions, JokerName)
        .AsBuilder()
        .UseOpenTelemetry(sourceName: sourceName)
        .Build();

    // Invoke the agent and output the text result.
    Console.WriteLine(await agent.RunAsync("Tell me a joke about a pirate."));

    // Invoke the agent with streaming support.
    await foreach (var update in agent.RunStreamingAsync("Tell me a joke about a pirate."))
    {
        Console.WriteLine(update);
    }
}

async Task F5()
{
    const string JokerName = "Joker";
    const string JokerInstructions = "You are good at telling jokes.";

    // Create the agent
    AIAgent agent = new AzureOpenAIClient(
        new Uri(endpoint),
        credential)
         .GetChatClient(deploymentName)
         .CreateAIAgent(JokerInstructions, JokerName);

    // Start a new thread for the agent conversation.
    AgentThread thread = agent.GetNewThread();

    // Run the agent with a new thread.
    Console.WriteLine(await agent.RunAsync("Tell me a joke about a pirate.", thread));

    // Serialize the thread state to a JsonElement, so it can be stored for later use.
    JsonElement serializedThread = thread.Serialize();

    // Save the serialized thread to a temporary file (for demonstration purposes).
    string tempFilePath = Path.GetTempFileName();
    await File.WriteAllTextAsync(tempFilePath, JsonSerializer.Serialize(serializedThread));

    // Load the serialized thread from the temporary file (for demonstration purposes).
    JsonElement reloadedSerializedThread = JsonSerializer.Deserialize<JsonElement>(await File.ReadAllTextAsync(tempFilePath));

    // Deserialize the thread state after loading from storage.
    AgentThread resumedThread = agent.DeserializeThread(reloadedSerializedThread);

    // Run the agent again with the resumed thread.
    Console.WriteLine(await agent.RunAsync("Now tell the same joke in the voice of a pirate, and add some emojis to the joke.", resumedThread));
}

async Task F4()
{
    // Create the agent options, specifying the response format to use a JSON schema based on the PersonInfo class.
    ChatClientAgentOptions agentOptions = new(name: "HelpfulAssistant", instructions: "You are a helpful assistant.")
    {
        ChatOptions = new()
        {
            ResponseFormat = ChatResponseFormat.ForJsonSchema<PersonInfo>()
        }
    };

    // Create the agent using Azure OpenAI.
    AIAgent agent = new AzureOpenAIClient(
        new Uri(endpoint),
        credential)
            .GetChatClient(deploymentName)
            .CreateAIAgent(agentOptions);

    // Invoke the agent with some unstructured input, to extract the structured information from.
    var response = await agent.RunAsync("Please provide information about John Smith, who is a 35-year-old software engineer.");

    // Deserialize the response into the PersonInfo class.
    var personInfo = response.Deserialize<PersonInfo>(JsonSerializerOptions.Web);

    Console.WriteLine("Assistant Output:");
    Console.WriteLine($"Name: {personInfo.Name}");
    Console.WriteLine($"Age: {personInfo.Age}");
    Console.WriteLine($"Occupation: {personInfo.Occupation}");

    // Invoke the agent with some unstructured input while streaming, to extract the structured information from.
    var updates = agent.RunStreamingAsync("Please provide information about John Smith, who is a 35-year-old software engineer.");

    // Assemble all the parts of the streamed output, since we can only deserialize once we have the full json,
    // then deserialize the response into the PersonInfo class.
    personInfo = (await updates.ToAgentRunResponseAsync()).Deserialize<PersonInfo>(JsonSerializerOptions.Web);

    Console.WriteLine("Assistant Output:");
    Console.WriteLine($"Name: {personInfo.Name}");
    Console.WriteLine($"Age: {personInfo.Age}");
    Console.WriteLine($"Occupation: {personInfo.Occupation}");



}



async Task F3()
{
    [Description("Get the weather for a given location.")]
    static string GetWeather([Description("The location to get the weather for.")] string location)
    => $"The weather in {location} is cloudy with a high of 15°C.";

    // Create the chat client and agent.
    // Note that we are wrapping the function tool with ApprovalRequiredAIFunction to require user approval before invoking it.
    AIAgent agent = new AzureOpenAIClient(
        new Uri(endpoint),
        credential)
         .GetChatClient(deploymentName)
         .CreateAIAgent(instructions: "You are a helpful assistant", tools: [new ApprovalRequiredAIFunction(AIFunctionFactory.Create(GetWeather))]);

    // Call the agent and check if there are any user input requests to handle.
    AgentThread thread = agent.GetNewThread();
    var response = await agent.RunAsync("What is the weather like in Amsterdam?", thread);
    var userInputRequests = response.UserInputRequests.ToList();

    // For streaming use:
    // var updates = await agent.RunStreamingAsync("What is the weather like in Amsterdam?", thread).ToListAsync();
    // userInputRequests = updates.SelectMany(x => x.UserInputRequests).ToList();

    while (userInputRequests.Count > 0)
    {
        // Ask the user to approve each function call request.
        // For simplicity, we are assuming here that only function approval requests are being made.
        var userInputResponses = userInputRequests
            .OfType<FunctionApprovalRequestContent>()
            .Select(functionApprovalRequest =>
            {
                Console.WriteLine($"The agent would like to invoke the following function, please reply Y to approve: Name {functionApprovalRequest.FunctionCall.Name}");
                return new ChatMessage(ChatRole.User, [functionApprovalRequest.CreateResponse(Console.ReadLine()?.Equals("Y", StringComparison.OrdinalIgnoreCase) ?? false)]);
            })
            .ToList();

        // Pass the user input responses back to the agent for further processing.
        response = await agent.RunAsync(userInputResponses, thread);
        Console.WriteLine($"\nAgent: {response.Text}");
        userInputRequests = response.UserInputRequests.ToList();

        // For streaming use:
        // updates = await agent.RunStreamingAsync(userInputResponses, thread).ToListAsync();
        // userInputRequests = updates.SelectMany(x => x.UserInputRequests).ToList();
    }

    Console.WriteLine($"\nAgent: {response}");
}
async Task F2()
{
    [Description("Get the weather for a given location.")]
    static string GetWeather([Description("The location to get the weather for.")] string location)
    => $"The weather in {location} is cloudy with a high of 15°C.";

    // Create the chat client and agent, and provide the function tool to the agent.
    AIAgent agent = new AzureOpenAIClient(
        new Uri(endpoint),
        credential)
         .GetChatClient(deploymentName)
         .CreateAIAgent(instructions: "You are a helpful assistant", tools: [AIFunctionFactory.Create(GetWeather)]);

    // Non-streaming agent interaction with function tools.
    Console.WriteLine(await agent.RunAsync("What is the weather like in Amsterdam?"));

    // Streaming agent interaction with function tools.
    await foreach (var update in agent.RunStreamingAsync("What is the weather like in Amsterdam?"))
    {
        Console.Write(update);
    }
}







async Task F1()
{
    const string JokerName = "Joker";
    const string JokerInstructions = "你擅长讲笑话。";

    AIAgent agent = new AzureOpenAIClient(
        new Uri(endpoint),
        credential)
         .GetChatClient(deploymentName)
         .CreateAIAgent(JokerInstructions, JokerName);
    // 调用 agent 并输出文本结果。
    //Console.WriteLine(await agent.RunAsync("讲一个关于海盗的笑话。"));
    // 使用带流支持的调用。
    AgentThread thread = agent.GetNewThread();
    Console.WriteLine(await agent.RunAsync("给我讲一个关于海盗的笑话。", thread));
    Console.WriteLine("----------------------");
    Console.WriteLine(await agent.RunAsync("现在在笑话中加入一些表情符号，并以海盗鹦鹉的声音讲述。", thread));
    Console.WriteLine("----------------------");
    // 使用多轮会话和流式输出，线程对象保留上下文。
    thread = agent.GetNewThread();
    await foreach (var update in agent.RunStreamingAsync("给我讲一个关于海盗的笑话。", thread))
    {
        Console.Write(update);
    }
    Console.WriteLine();
    Console.WriteLine("----------------------");
    await foreach (var update in agent.RunStreamingAsync("现在在笑话中加入一些表情符号，并以海盗鹦鹉的声音讲述。", thread))
    {
        Console.Write(update);
    }
    Console.WriteLine();
    Console.WriteLine("----------------------");
    Console.ReadLine();
}


/// <summary>
/// Represents information about a person, including their name, age, and occupation, matched to the JSON schema used in the agent.
/// </summary>
[Description("Information about a person including their name, age, and occupation")]
class PersonInfo
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("age")]
    public int? Age { get; set; }

    [JsonPropertyName("occupation")]
    public string? Occupation { get; set; }
}