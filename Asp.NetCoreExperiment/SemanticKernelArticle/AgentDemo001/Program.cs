using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Chat;
using Microsoft.SemanticKernel.ChatCompletion;
#pragma warning disable
var modelID = "gpt-4o";
var openAIKey = File.ReadAllText("c://gpt/key.txt");
var kernel = Kernel.CreateBuilder()
           .AddOpenAIChatCompletion(modelID, openAIKey).Build();

const string WriterName = "Writer";
const string ReviewerName = "Reviewer";

var writerAgent =
   new ChatCompletionAgent()
   {
       Name = WriterName,
       Instructions = "你是一个中式的黑色笑话的作者，能写出简短的黑色笑话。如果Reviewer提出意见，你会采纳。",
       Kernel = kernel
   };

var reviewerAgent =
    new ChatCompletionAgent()
    {
        Name = ReviewerName,
        Instructions = "你是一个黑色笑话的评论师，有很严格的要求，你总会吹毛求疵，当不够好笑时，会作出评价，要求重写。",
        Kernel = kernel
    };

// Define a kernel function for the selection strategy
var selectionFunction =
    AgentGroupChat.CreatePromptFunctionForStrategy(
        $$$$"""
        确定在对话中轮到哪位参与者发言，基于最近一次发言的参与者。
        仅说明轮到哪位参与者发言的名字。
        不允许同一位参与者连续发言两次。

        只能从以下参与者中选择：

        {{{Reviewer}}}
        {{{Writer}}}
        选择下一位参与者时，始终遵循以下规则：

        在{{{Writer}}}之后，轮到{{{Reviewer}}}发言。
        在{{{Reviewer}}}之后，轮到{{{Writer}}}发言。

        History:
        {{$history}}
        """,
        safeParameterNames: "history");

// Define the selection strategy
var selectionStrategy =
 new KernelFunctionSelectionStrategy(selectionFunction, kernel)
 {
     // Always start with the writer agent.
     InitialAgent = writerAgent,
     // Parse the function response.
     ResultParser = (result) =>
     {
         Console.WriteLine(result.GetValue<string>());
         return result.GetValue<string>() ?? WriterName;
     },
     // The prompt variable name for the history argument.
     HistoryVariableName = "history",
     // Save tokens by not including the entire history in the prompt
     HistoryReducer = new ChatHistoryTruncationReducer(5),
 };

// Create a chat using the defined selection strategy.
var chat =
    new AgentGroupChat(writerAgent, reviewerAgent)
    {
        ExecutionSettings = new() { SelectionStrategy = selectionStrategy }
    };


chat.AddChatMessage(new ChatMessageContent { Role = AuthorRole.User, Content = "开始分享你的黑色笑话：" });
bool isComplete = false;
do
{
    AuthorRole? role = null;
    var input = "";
    await foreach (var content in chat.InvokeStreamingAsync())
    {
        if (role != content.Role)
        {
            role = content.Role;
            Console.WriteLine(role + ":");
        }
        Console.Write(content.Content);
        input += content.Content;
    }
    Console.WriteLine();
    chat.AddChatMessage(new ChatMessageContent(role.Value, input));
} while (!isComplete);

Console.WriteLine("   End");
Console.ReadLine();