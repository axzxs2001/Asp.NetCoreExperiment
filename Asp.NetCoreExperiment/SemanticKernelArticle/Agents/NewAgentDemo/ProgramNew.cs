using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Chat;
using Microsoft.SemanticKernel.Agents.OpenAI;
using Microsoft.SemanticKernel.ChatCompletion;
using OpenAI;
using OpenAI.Assistants;
using System;
using System.ClientModel;
using System.Collections.ObjectModel;
using System.Reflection.Emit;

#pragma warning disable

var modelID = "gpt-4o";
var openAIKey = File.ReadAllText("c://gpt/key.txt");

var kernel = Kernel.CreateBuilder()
           .AddOpenAIChatCompletion(modelID, openAIKey).Build();


string ReviewerName = "ArtDirector";
 string ReviewerInstructions =
    """
        你是一位艺术总监，对文案写作有自己的见解，这些见解源自你对大卫·奥格威（David Ogilvy）的热爱。
        你的目标是判断所提供的文案是否可以印刷出版。
        如果文案合格，请直接批准。
        如果文案不合格，请提供改进的见解，但不要提供示例。
        """;
string CopyWriterName = "CopyWriter";
string CopyWriterInstructions =
    """
        你是一名拥有十年经验的文案撰写人，以简洁和冷幽默著称。
        你的目标是作为该领域的专家，精炼并选出唯一最佳的文案。
        每次回复只提供一个最终提案。
        你专注于目标，不浪费时间闲聊。
        在优化创意时，考虑建议并加以打磨。
        """;


var agentReviewer = await CreateAgentReviewerAsync(kernel);
var agentWriter = await CreateAgentWriterAsync(kernel);

AgentGroupChat chat =
           new(agentWriter, agentReviewer)
           {
               ExecutionSettings =
                   new()
                   {                      
                       TerminationStrategy =
                           new ApprovalTerminationStrategy()
                           {                               
                               Agents = [agentReviewer],                          
                               MaximumIterations = 50,
                           }
                   },    
           };


ChatMessageContent input = new(AuthorRole.User, "概念：由蛋盒制作的地图。");
chat.AddChatMessage(input);


string lastAgent = string.Empty;
await foreach (StreamingChatMessageContent response in chat.InvokeStreamingAsync())
{
    if (string.IsNullOrEmpty(response.Content))
    {
        continue;
    }
    if (!lastAgent.Equals(response.AuthorName, StringComparison.Ordinal))
    {
        Console.WriteLine($"\n# {response.Role} - {response.AuthorName ?? "*"}:");
        lastAgent = response.AuthorName ?? string.Empty;
    }
    Console.Write($"{response.Content}");
}


Console.WriteLine($"\n[完成: {chat.IsComplete}]");


async Task<ChatCompletionAgent> CreateAgentWriterAsync(Kernel kernel)
{
    var agentTranslator = new ChatCompletionAgent()
    {
        Instructions = ReviewerInstructions,
        Name = ReviewerName,
        Kernel = kernel,
    };
    return agentTranslator;
}

async Task<OpenAIAssistantAgent> CreateAgentReviewerAsync(Kernel kernel)
{
    ReadOnlyDictionary<string, string> SampleMetadata =
       new(new Dictionary<string, string>
       {
            { "sksample", bool.TrueString }
       });
    var client = new OpenAIClient(openAIKey);
    var AssistantClient = client.GetAssistantClient();
    var OpenAIAssistantClient = client.GetAssistantClient();
    var assistant =
         await OpenAIAssistantClient.CreateAssistantAsync(
             modelID,
             instructions: CopyWriterInstructions,
                name: CopyWriterName,
                metadata: SampleMetadata
            );
    return new OpenAIAssistantAgent(assistant, AssistantClient); 
}


 class ApprovalTerminationStrategy : TerminationStrategy
{   
    protected override Task<bool> ShouldAgentTerminateAsync(Agent agent, IReadOnlyList<ChatMessageContent> history, CancellationToken cancellationToken)
        => Task.FromResult(history[history.Count-2].Content?.Contains("批准", StringComparison.OrdinalIgnoreCase) ?? false);
}