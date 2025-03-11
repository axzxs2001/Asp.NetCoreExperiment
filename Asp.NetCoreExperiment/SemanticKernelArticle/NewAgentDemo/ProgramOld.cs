//using Microsoft.SemanticKernel.Agents.Chat;
//using Microsoft.SemanticKernel.Agents.OpenAI;
//using Microsoft.SemanticKernel.Agents;
//using Microsoft.SemanticKernel.ChatCompletion;
//using Microsoft.SemanticKernel;
//using System.ClientModel;
//using System.Collections.ObjectModel;

//#pragma warning disable 

//string ReviewerName = "翻译审核员";
//string ReviewerInstructions =
//   """
//        你是一位中日文翻译的翻译审核员，你有丰富的翻译和审核经验，对翻译质量有较高的要求，总是严格要求，反复琢磨，以求得到更为准确的翻译。
//        目标是确定给定翻译否符合要求，是否采用。如果不符合要求，提出你的建议给翻译家，但不要把翻译内容给对方。
//        如果翻译内容可以接受并且符合您的标准，请说：采用它。
//        """;

//string CopyWriterName = "翻译家";
//string CopyWriterInstructions =
//   """
//        您是一位中文日文的翻译家，以严谨闻名。
//        你会把用户的输入，全神贯注于手头的目标，翻译成准确，高质量的译文。
//        完善翻译内容时，请考虑翻译审核员的建议。
//        """;
//var modelID = "gpt-4o";
//var openAIKey = File.ReadAllText("c://gpt/key.txt");

//var kernel = Kernel.CreateBuilder()
//           .AddOpenAIChatCompletion(modelID, openAIKey).Build();

//var agentReviewer = new ChatCompletionAgent()
//{
//    Instructions = ReviewerInstructions,
//    Name = ReviewerName,
//    Kernel = kernel,
//};


//ReadOnlyDictionary<string, string> AssistantSampleMetadata =
//        new(new Dictionary<string, string>
//        {
//            {  "sksample", bool.TrueString }
//        });

//var agentWriter =
//    await OpenAIAssistantAgent.CreateAsync(
//        clientProvider: OpenAIClientProvider.ForOpenAI(new ApiKeyCredential(openAIKey)),
//        definition: new OpenAIAssistantDefinition(modelID)
//        {
//            Instructions = CopyWriterInstructions,
//            Name = CopyWriterName,
//            Metadata = AssistantSampleMetadata
//        },
//        kernel: kernel);

//// Create a chat for agent interaction.
//AgentGroupChat chat =
//    new(agentWriter, agentReviewer)
//    {
//        ExecutionSettings =
//            new()
//            {
//                // Here a TerminationStrategy subclass is used that will terminate when
//                // an assistant message contains the term "approve".
//                TerminationStrategy = new ApprovalTerminationStrategy()
//                {
//                    // Only the art-director may approve.
//                    Agents = [agentReviewer],
//                    // Limit total number of turns
//                    MaximumIterations = 10,
//                }
//            }
//    };

//// Invoke chat and display messages.
//ChatMessageContent input = new(AuthorRole.User, File.ReadAllText("content.txt"));
//chat.AddChatMessage(input);
//Console.WriteLine(input);

//string lastAgent = string.Empty;
//await foreach (StreamingChatMessageContent response in chat.InvokeStreamingAsync())
//{
//    if (string.IsNullOrEmpty(response.Content))
//    {
//        continue;
//    }

//    if (!lastAgent.Equals(response.AuthorName, StringComparison.Ordinal))
//    {
//        Console.WriteLine($"\n# {response.Role} - {response.AuthorName ?? "*"}:");
//        lastAgent = response.AuthorName ?? string.Empty;
//    }

//    Console.WriteLine($"\t > streamed: '{response.Content}'");
//}

//// Display the chat history.
//Console.WriteLine("================================");
//Console.WriteLine("CHAT HISTORY");
//Console.WriteLine("================================");

//ChatMessageContent[] history = await chat.GetChatMessagesAsync().Reverse().ToArrayAsync();

//for (int index = 0; index < history.Length; index++)
//{
//    Console.WriteLine(history[index]);
//}

//Console.WriteLine($"\n[IS COMPLETED: {chat.IsComplete}]");


//sealed class ApprovalTerminationStrategy : TerminationStrategy
//{
//    // Terminate when the final message contains the term "approve"
//    protected override Task<bool> ShouldAgentTerminateAsync(Agent agent, IReadOnlyList<ChatMessageContent> history, CancellationToken cancellationToken)
//        => Task.FromResult(history[history.Count - 1].Content?.Contains("采用它", StringComparison.OrdinalIgnoreCase) ?? false);
//}