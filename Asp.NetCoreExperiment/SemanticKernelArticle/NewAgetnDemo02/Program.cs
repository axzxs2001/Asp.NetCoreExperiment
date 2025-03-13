
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Chat;
using Microsoft.SemanticKernel.Agents.OpenAI;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using OpenAI.Assistants;
using OpenAI.Chat;
using System.Reflection;
using System.Text.Json;
using ChatResponseFormat = OpenAI.Chat.ChatResponseFormat;
#pragma warning disable

var modelID = "gpt-4o";
var openAIKey = File.ReadAllText("c://gpt/key.txt");
var kernel = Kernel.CreateBuilder()
           .AddOpenAIChatCompletion(modelID, openAIKey).Build();

string InternalLeaderName = "InternalLeader";
string InternalLeaderInstructions =
     """
        你的工作是清晰、直接地传达当前助手的回复给用户。

        如果用户请求信息，仅重复该请求。
        如果用户提供信息，仅重复该信息。
        不要自行提出购物建议。
        """;

string InternalGiftIdeaAgentName = "InternalGiftIdeas";
string InternalGiftIdeaAgentInstructions =
       """        
        你是一名个人购物顾问，提供礼物创意。

        仅在已知以下信息时提供创意：

        赠送者与收礼者的关系
        赠送礼物的原因
        在提供创意前，需先获取缺失的信息。

        仅用名称描述礼物。

        始终立即根据反馈调整并提供更新后的建议。
        """;

string InternalGiftReviewerName = "InternalGiftReviewer";
string InternalGiftReviewerInstructions =
     """
        审核最近的购物回复。

        可以提供批评性反馈，以改进回复，但不得引入新的创意，或者说明回复是合适的。
        """;

string InnerSelectionInstructions =
      $$$"""
        
        请选择下一个发言的参与者，仅可从以下参与者中选择。
        
        - {{{InternalGiftIdeaAgentName}}}
        - {{{InternalGiftReviewerName}}}
        - {{{InternalLeaderName}}}
        
        选择下一个参与者，根据最近一位参与者的动作：

        在用户输入后，由 {{{InternalGiftIdeaAgentName}}} 进行回合。
        在 {{{InternalGiftIdeaAgentName}}} 提供创意后，由 {{{InternalGiftReviewerName}}} 进行回合。
        在 {{{InternalGiftIdeaAgentName}}} 请求额外信息后，由 {{{InternalLeaderName}}} 进行回合。
        在 {{{InternalGiftReviewerName}}} 提供反馈或指示后，由 {{{InternalGiftIdeaAgentName}}} 进行回合。
        在 {{{InternalGiftReviewerName}}} 认定 {{{InternalGiftIdeaAgentName}}} 的回应已足够后，由 {{{InternalLeaderName}}} 进行回合。
        请以 JSON 格式回复。JSON 结构仅包括：
        {
            "name": "string (被选中进行下一个回合的助手名称)",
            "reason": "string (选中该参与者的原因)"
        }
        
        History:
        {{${{{KernelFunctionSelectionStrategy.DefaultHistoryVariableName}}}}}
        """;

string OuterTerminationInstructions =
       $$$"""
        以下是你的请求的中文翻译：

        确定用户请求是否已被完全回答。

        请以 JSON 格式响应。JSON 结构仅可包括：
        {
            "isAnswered": "bool (如果用户请求已被完全回答，则为 true)",
            "reason": "string (你做出此判断的理由)"
        }
        
        History:
        {{${{{KernelFunctionTerminationStrategy.DefaultHistoryVariableName}}}}}
        """;

await NestedChatWithAggregatorAgentAsync();
Console.ReadLine();

async Task NestedChatWithAggregatorAgentAsync()
{
    Console.WriteLine($"! GPT-4o");

    OpenAIPromptExecutionSettings jsonSettings = new() { ResponseFormat = ChatResponseFormat.CreateJsonObjectFormat() };
    PromptExecutionSettings autoInvokeSettings = new() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() };

    ChatCompletionAgent internalLeaderAgent = CreateAgent(InternalLeaderName, InternalLeaderInstructions);
    ChatCompletionAgent internalGiftIdeaAgent = CreateAgent(InternalGiftIdeaAgentName, InternalGiftIdeaAgentInstructions);
    ChatCompletionAgent internalGiftReviewerAgent = CreateAgent(InternalGiftReviewerName, InternalGiftReviewerInstructions);

    KernelFunction innerSelectionFunction = KernelFunctionFactory.CreateFromPrompt(InnerSelectionInstructions, jsonSettings);
    KernelFunction outerTerminationFunction = KernelFunctionFactory.CreateFromPrompt(OuterTerminationInstructions, jsonSettings);

    AggregatorAgent personalShopperAgent =
        new(CreateChat)
        {
            Name = "私人购物顾问",
            Mode = AggregatorMode.Nested,
        };

    AgentGroupChat chat =
        new(personalShopperAgent)
        {
            ExecutionSettings =
                new()
                {
                    TerminationStrategy =
                        new KernelFunctionTerminationStrategy(outerTerminationFunction, kernel)
                        {
                            ResultParser =
                                (result) =>
                                {
                                    OuterTerminationResult? jsonResult = JsonResultTranslator.Translate<OuterTerminationResult>(result.GetValue<string>());

                                    return jsonResult?.isAnswered ?? false;
                                },
                            MaximumIterations = 5,
                        },
                }
        };

    // Invoke chat and display messages.
    Console.WriteLine("\n######################################");
    Console.WriteLine("# 动态聊天");
    Console.WriteLine("######################################");

    await InvokeChatAsync("你能提供三个独特的生日礼物创意吗？我不想要别人也会挑选的礼物。");

    await InvokeChatAsync("礼物是送给我成年的兄弟的。");

    if (!chat.IsComplete)
    {
        await InvokeChatAsync("他喜欢摄影。");
    }

    Console.WriteLine("\n\n>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
    Console.WriteLine(">>>> 聚合聊天");
    Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");

    await foreach (var message in chat.GetChatMessagesAsync(personalShopperAgent))
    {
        WriteAgentChatMessage(message);
    }

    async Task InvokeChatAsync(string input)
    {
        Microsoft.SemanticKernel.ChatMessageContent message = new(AuthorRole.User, input);
        chat.AddChatMessage(message);
        WriteAgentChatMessage(message);

        await foreach (Microsoft.SemanticKernel.ChatMessageContent response in chat.InvokeAsync(personalShopperAgent))
        {
            WriteAgentChatMessage(response);
        }

        Console.WriteLine($"\n# 是否完成: {chat.IsComplete}");
    }

    ChatCompletionAgent CreateAgent(string agentName, string agentInstructions) =>
        new()
        {
            Instructions = agentInstructions,
            Name = agentName,
            Kernel = kernel,
        };

    AgentGroupChat CreateChat() =>
            new(internalLeaderAgent, internalGiftReviewerAgent, internalGiftIdeaAgent)
            {
                ExecutionSettings =
                    new()
                    {
                        SelectionStrategy =
                            new KernelFunctionSelectionStrategy(innerSelectionFunction, kernel)
                            {
                                ResultParser =
                                    (result) =>
                                    {
                                        AgentSelectionResult? jsonResult = JsonResultTranslator.Translate<AgentSelectionResult>(result.GetValue<string>());

                                        string? agentName = string.IsNullOrWhiteSpace(jsonResult?.name) ? null : jsonResult?.name;
                                        agentName ??= InternalGiftIdeaAgentName;

                                        Console.WriteLine($"\t>>>> 内部切换: {agentName}");

                                        return agentName;
                                    }
                            },
                        TerminationStrategy =
                            new AgentTerminationStrategy()
                            {
                                Agents = [internalLeaderAgent],
                                MaximumIterations = 7,
                                AutomaticReset = true,
                            },
                    }
            };
}
void WriteAgentChatMessage(Microsoft.SemanticKernel.ChatMessageContent message)
{
    // Include ChatMessageContent.AuthorName in output, if present.
    string authorExpression = message.Role == AuthorRole.User ? string.Empty : $" - {message.AuthorName ?? "*"}";
    // Include TextContent (via ChatMessageContent.Content), if present.
    string contentExpression = string.IsNullOrWhiteSpace(message.Content) ? string.Empty : message.Content;
    bool isCode = message.Metadata?.ContainsKey(OpenAIAssistantAgent.CodeInterpreterMetadataKey) ?? false;
    string codeMarker = isCode ? "\n  [CODE]\n" : " ";
    Console.WriteLine($"\n# {message.Role}{authorExpression}:{codeMarker}{contentExpression}");

   

}
record OuterTerminationResult(bool isAnswered, string reason);

record AgentSelectionResult(string name, string reason);

class AgentTerminationStrategy : TerminationStrategy
{
    /// <inheritdoc/>
    protected override Task<bool> ShouldAgentTerminateAsync(Agent agent, IReadOnlyList<Microsoft.SemanticKernel.ChatMessageContent> history, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }
}


public static class JsonResultTranslator
{
    private const string LiteralDelimiter = "```";
    private const string JsonPrefix = "json";

    /// <summary>
    /// Utility method for extracting a JSON result from an agent response.
    /// </summary>
    /// <param name="result">A text result</param>
    /// <typeparam name="TResult">The target type of the <see cref="FunctionResult"/>.</typeparam>
    /// <returns>The JSON translated to the requested type.</returns>
    public static TResult? Translate<TResult>(string? result)
    {
        if (string.IsNullOrWhiteSpace(result))
        {
            return default;
        }

        string rawJson = ExtractJson(result);

        return JsonSerializer.Deserialize<TResult>(rawJson);
    }

    private static string ExtractJson(string result)
    {
        // Search for initial literal delimiter: ```
        int startIndex = result.IndexOf(LiteralDelimiter, System.StringComparison.Ordinal);
        if (startIndex < 0)
        {
            // No initial delimiter, return entire expression.
            return result;
        }

        startIndex += LiteralDelimiter.Length;

        // Accommodate "json" prefix, if present.
        if (JsonPrefix.Equals(result.Substring(startIndex, JsonPrefix.Length), System.StringComparison.OrdinalIgnoreCase))
        {
            startIndex += JsonPrefix.Length;
        }

        // Locate final literal delimiter
        int endIndex = result.IndexOf(LiteralDelimiter, startIndex, System.StringComparison.OrdinalIgnoreCase);
        if (endIndex < 0)
        {
            endIndex = result.Length;
        }

        // Extract JSON
        return result.Substring(startIndex, endIndex - startIndex);
    }
}