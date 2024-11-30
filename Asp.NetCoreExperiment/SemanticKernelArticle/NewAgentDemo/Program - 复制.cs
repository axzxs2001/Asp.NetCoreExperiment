using Microsoft.SemanticKernel.Agents.Chat;
using Microsoft.SemanticKernel.Agents.OpenAI;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
using System.ClientModel;
using System.Collections.ObjectModel;
using System.Reflection.Emit;
using System;

#pragma warning disable

var modelID = "gpt-4o";
var openAIKey = File.ReadAllText("c://gpt/key.txt");

var kernel = Kernel.CreateBuilder()
           .AddOpenAIChatCompletion(modelID, openAIKey).Build();
//创建翻译家代理
var agentTranslator = await CreateTranslatorAsync(kernel);
//创建审核员代理
var agentAuditor = await CreateAuditorAsync(kernel);

//创建一个代理聊天组，先让翻译家翻译，然后审核员审核
var chat = new AgentGroupChat(agentTranslator, agentAuditor)
{
    ExecutionSettings = new()
    {
        // 这里使用了一个 TerminationStrategy 子类，当代理消息包含术语 "采用它" 时将终止。
        TerminationStrategy = new AdoptTerminationStrategy("采用它")
        {
            Agents = [agentAuditor],
            MaximumIterations = 6,
        }
    }
};

//添加聊天消息
var chatMessage = new ChatMessageContent(AuthorRole.User, File.ReadAllText("content.txt"));
chat.AddChatMessage(chatMessage);
Console.WriteLine(chatMessage);

var lastAgent = string.Empty;
Console.WriteLine();
// 这里使用了异步流来处理代理的消息。
await foreach (var response in chat.InvokeStreamingAsync())
{
    if (string.IsNullOrEmpty(response.Content))
    {
        continue;
    }
    //输入角色和代理名称
    if (!lastAgent.Equals(response.AuthorName, StringComparison.Ordinal))
    {
        Console.WriteLine($"\n# {response.Role} - {response.AuthorName ?? "*"}:");
        lastAgent = response.AuthorName ?? string.Empty;
    }
    Console.Write(response.Content);
}

//ChatMessageContent[] history = await chat.GetChatMessagesAsync().Reverse().ToArrayAsync();
//全部的内容
//for (int index = 0; index < history.Length; index++)
//{
//    Console.WriteLine(history[index]);
//}
Console.WriteLine($"\n[已完成: {chat.IsComplete}]");



async Task<ChatCompletionAgent> CreateAuditorAsync(Kernel kernel)
{
    var agentTranslator = new ChatCompletionAgent()
    {
        Instructions = """
                你是一位中日文翻译的翻译审核员，你有丰富的翻译和审核经验，对翻译质量有较高的要求，总是严格要求，反复琢磨，以求得到更为准确的翻译。
                目标是确定给定翻译否符合要求，是否采用。
                如果翻译内容可以接受并且符合您的标准，请说：采用它。
                """,
        Name = "Auditor",
        Kernel = kernel,
    };
    return agentTranslator;
}

async Task<OpenAIAssistantAgent> CreateTranslatorAsync(Kernel kernel)
{
    var agentAuditor = await OpenAIAssistantAgent.CreateAsync(
            clientProvider: OpenAIClientProvider.ForOpenAI(new ApiKeyCredential(openAIKey)),
            definition: new OpenAIAssistantDefinition(modelID)
            {
                Instructions = """
                您是一位把中文翻译成日文的翻译家。
                你会把用户的输入，全神贯注于手头的目标，翻译成准确，高质量的译文。
                完善翻译内容时，请考虑翻译 Auditor 的建议。
                """,
                Name = "JapaneseTranslator",
            },
            kernel: kernel);
    return agentAuditor;
}


class AdoptTerminationStrategy(string adoptCommand) : TerminationStrategy
{
    protected override Task<bool> ShouldAgentTerminateAsync(Agent agent, IReadOnlyList<ChatMessageContent> history, CancellationToken cancellationToken)
        => Task.FromResult(history[history.Count - 1].Content?.Contains(adoptCommand, StringComparison.OrdinalIgnoreCase) ?? false);
}