using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.Agents.AI;
using Microsoft.Extensions.AI;
using OpenAI;
using System.ClientModel;
using System.ComponentModel;

var arr = File.ReadLines("C://gpt/azure_key.txt").ToArray();

var endpoint = arr[1];
var deploymentName = arr[0];
var key = arr[2];
var credential = new ApiKeyCredential(key);

await F2();

Console.ReadLine();
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