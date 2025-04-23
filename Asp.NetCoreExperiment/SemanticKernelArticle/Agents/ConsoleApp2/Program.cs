using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.OpenAI;
using Microsoft.SemanticKernel.ChatCompletion;
using OpenAI.Assistants;
using OpenAI.Chat;
using OpenAI.Files;
using System.ClientModel;
using System.Diagnostics;

#pragma warning disable
string AnalystName = "Analyst";
string AnalystInstructions = "Create charts as requested without explanation.";

string SummarizerName = "Summarizer";
string SummarizerInstructions = "Summarize the entire conversation for the user in natural language.";

var modeid = "gpt-4.1";
var key = File.ReadAllText("c://gpt/key.txt");
var Client =OpenAIAssistantAgent.CreateOpenAIClient(new ApiKeyCredential(key));

var AssistantClient = Client.GetAssistantClient();
Assistant assistant =
           await AssistantClient.CreateAssistantAsync(
               modeid,
               name: AnalystName,
               instructions: AnalystInstructions,
               enableCodeInterpreter: true,
               metadata: new Dictionary<string, string>
               {{ "sksample", bool.TrueString }});

// Create the agent
OpenAIAssistantAgent analystAgent = new(assistant, AssistantClient);

var kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(modeid, key)
            .Build();
ChatCompletionAgent summaryAgent =
    new()
    {
        Instructions = SummarizerInstructions,
        Name = SummarizerName,
        Kernel = kernel,
    };

// Create a chat for agent interaction.
AgentGroupChat chat = new();

// Respond to user input
try
{
    await InvokeAgentAsync(
        analystAgent,
        """
                Graph the percentage of storm events by state using a pie chart:

                State, StormCount
                TEXAS, 4701
                KANSAS, 3166
                IOWA, 2337
                ILLINOIS, 2022
                MISSOURI, 2016
                GEORGIA, 1983
                MINNESOTA, 1881
                WISCONSIN, 1850
                NEBRASKA, 1766
                NEW YORK, 1750
                """);

    await InvokeAgentAsync(summaryAgent);
    Console.ReadLine();
}
finally
{
    await AssistantClient.DeleteAssistantAsync(analystAgent.Id);
}

// Local function to invoke agent and display the conversation messages.
async Task InvokeAgentAsync(Microsoft.SemanticKernel.Agents.Agent agent, string? input = null)
{
    if (!string.IsNullOrWhiteSpace(input))
    {
        Microsoft.SemanticKernel.ChatMessageContent message = new(AuthorRole.User, input);
        chat.AddChatMessage(message);
        WriteAgentChatMessage(message);
    }

    await foreach (Microsoft.SemanticKernel.ChatMessageContent response in chat.InvokeAsync(agent))
    {
        WriteAgentChatMessage(response);
        await DownloadResponseImageAsync(response);
    }
}

 async Task DownloadResponseImageAsync(Microsoft.SemanticKernel.ChatMessageContent message)
{
    OpenAIFileClient fileClient = Client.GetOpenAIFileClient();

    foreach (KernelContent item in message.Items)
    {
        if (item is FileReferenceContent fileReference)
        {
            await DownloadFileContentAsync(fileClient, fileReference.FileId, launchViewer: true);
        }
    }
}

async Task DownloadResponseContentAsync(Microsoft.SemanticKernel.ChatMessageContent message)
{
    OpenAIFileClient fileClient = Client.GetOpenAIFileClient();

    foreach (KernelContent item in message.Items)
    {
        if (item is AnnotationContent annotation)
        {
            await DownloadFileContentAsync(fileClient, annotation.FileId!);
        }
    }
}

async Task DownloadFileContentAsync(OpenAIFileClient fileClient, string fileId, bool launchViewer = false)
{
    OpenAIFile fileInfo = fileClient.GetFile(fileId);
    if (fileInfo.Purpose == FilePurpose.AssistantsOutput)
    {
        string filePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(fileInfo.Filename));
        if (launchViewer)
        {
            filePath = Path.ChangeExtension(filePath, ".png");
        }

        BinaryData content = await fileClient.DownloadFileAsync(fileId);
        File.WriteAllBytes(filePath, content.ToArray());
        Console.WriteLine($"  File #{fileId} saved to: {filePath}");

        if (launchViewer)
        {
            Process.Start(
                new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/C start {filePath}"
                });
        }
    }
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

    // Provide visibility for inner content (that isn't TextContent).
    foreach (KernelContent item in message.Items)
    {
        if (item is AnnotationContent annotation)
        {
            Console.WriteLine($"  [{item.GetType().Name}] {annotation.Quote}: File #{annotation.FileId}");
        }
        else if (item is FileReferenceContent fileReference)
        {
            Console.WriteLine($"  [{item.GetType().Name}] File #{fileReference.FileId}");
        }
        else if (item is ImageContent image)
        {
            Console.WriteLine($"  [{item.GetType().Name}] {image.Uri?.ToString() ?? image.DataUri ?? $"{image.Data?.Length} bytes"}");
        }
        else if (item is FunctionCallContent functionCall)
        {
            Console.WriteLine($"  [{item.GetType().Name}] {functionCall.Id}");
        }
        else if (item is FunctionResultContent functionResult)
        {
            Console.WriteLine($"  [{item.GetType().Name}] {functionResult.CallId} - {System.Text.Json.JsonSerializer.Serialize(functionResult.Result) ?? "*"}");
        }
    }

    if (message.Metadata?.TryGetValue("Usage", out object? usage) ?? false)
    {
        if (usage is RunStepTokenUsage assistantUsage)
        {
            WriteUsage(assistantUsage.TotalTokenCount, assistantUsage.InputTokenCount, assistantUsage.OutputTokenCount);
        }
        else if (usage is RunStepCompletionUsage agentUsage)
        {
            WriteUsage(agentUsage.TotalTokens, agentUsage.PromptTokens, agentUsage.CompletionTokens);
        }
        else if (usage is ChatTokenUsage chatUsage)
        {
            WriteUsage(chatUsage.TotalTokenCount, chatUsage.InputTokenCount, chatUsage.OutputTokenCount);
        }
    }

    void WriteUsage(long totalTokens, long inputTokens, long outputTokens)
    {
        Console.WriteLine($"  [Usage] Tokens: {totalTokens}, Input: {inputTokens}, Output: {outputTokens}");
    }
}