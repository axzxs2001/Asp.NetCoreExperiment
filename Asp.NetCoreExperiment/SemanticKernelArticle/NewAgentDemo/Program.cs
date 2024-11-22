using Microsoft.SemanticKernel.Agents.Chat;
using Microsoft.SemanticKernel.Agents.OpenAI;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
using System.ClientModel;
using System.Collections.ObjectModel;

#pragma warning disable 

string ReviewerName = "ArtDirector";
string ReviewerInstructions =
   """
        你是一位热爱大卫·奥格威（David Ogilvy）的艺术总监，对文案撰写有独到见解。
        目标是判断给定的文案是否可以用于印刷。
        如果可以，请说明“已批准”。
        如果不可以，请提供如何改进建议文案的见解，但不提供具体例子。
        """;

string CopyWriterName = "CopyWriter";
string CopyWriterInstructions =
   """
        你是一位拥有十年经验的文案撰写专家，以简洁和冷幽默著称。
        目标是以专家的角度改进并确定唯一最佳的文案。
        每次回应只提供一个提案。
        专注于手头的目标，不浪费时间闲聊。
        在改进创意时，请考虑提出的建议。
        """;
var modelID = "gpt-4o";
var openAIKey = File.ReadAllText("c://gpt/key.txt");

var kernel = Kernel.CreateBuilder()
           .AddOpenAIChatCompletion(modelID, openAIKey).Build();

var agentReviewer = new ChatCompletionAgent()
{
    Instructions = ReviewerInstructions,
    Name = ReviewerName,
    Kernel = kernel,
};


ReadOnlyDictionary<string, string> AssistantSampleMetadata =
        new(new Dictionary<string, string>
        {
            {  "sksample", bool.TrueString }
        });

var agentWriter =
    await OpenAIAssistantAgent.CreateAsync(
        clientProvider: OpenAIClientProvider.ForOpenAI(new ApiKeyCredential(openAIKey)),
        definition: new OpenAIAssistantDefinition(modelID)
        {
            Instructions = CopyWriterInstructions,
            Name = CopyWriterName,
            Metadata = AssistantSampleMetadata
        },
        kernel: kernel);

// Create a chat for agent interaction.
AgentGroupChat chat =
    new(agentWriter, agentReviewer)
    {
        ExecutionSettings =
            new()
            {
                // Here a TerminationStrategy subclass is used that will terminate when
                // an assistant message contains the term "approve".
                TerminationStrategy = new ApprovalTerminationStrategy()
                {
                    // Only the art-director may approve.
                    Agents = [agentReviewer],
                    // Limit total number of turns
                    MaximumIterations = 10,
                }
            }
    };

// Invoke chat and display messages.
ChatMessageContent input = new(AuthorRole.User, "concept:用鸡蛋盒制作的地图。");
chat.AddChatMessage(input);
Console.WriteLine(input);

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

    Console.WriteLine($"\t > streamed: '{response.Content}'");
}

// Display the chat history.
Console.WriteLine("================================");
Console.WriteLine("CHAT HISTORY");
Console.WriteLine("================================");

ChatMessageContent[] history = await chat.GetChatMessagesAsync().Reverse().ToArrayAsync();

for (int index = 0; index < history.Length; index++)
{
    Console.WriteLine(history[index]);
}

Console.WriteLine($"\n[IS COMPLETED: {chat.IsComplete}]");


sealed class ApprovalTerminationStrategy : TerminationStrategy
{
    // Terminate when the final message contains the term "approve"
    protected override Task<bool> ShouldAgentTerminateAsync(Agent agent, IReadOnlyList<ChatMessageContent> history, CancellationToken cancellationToken)
        => Task.FromResult(history[history.Count - 1].Content?.Contains("approve", StringComparison.OrdinalIgnoreCase) ?? false);
}