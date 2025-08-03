
#pragma warning disable
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.OpenAI;
using Microsoft.SemanticKernel.ChatCompletion;
using OpenAI.Files;
using System.ClientModel;
using System.Diagnostics;

var key = File.ReadAllText("C:/gpt/key.txt");
//var Client = OpenAIAssistantAgent.CreateOpenAIClient(new ApiKeyCredential(key));
var lines = File.ReadAllLines("c:/gpt/azure_key.txt");
var Client = OpenAIAssistantAgent.CreateAzureOpenAIClient(
    new ApiKeyCredential(lines[2]),new Uri(lines[1]));

var AssistantClient = Client.GetAssistantClient();


var dic = new Dictionary<string, string>();
dic.Add("sksample", bool.TrueString);
var assistant =
    await AssistantClient.CreateAssistantAsync("gpt-4.1",
       "ChartMaker",
        instructions: "Create charts as requested without explanation.",
                enableCodeInterpreter: true,
                metadata: dic);

// Create the agent
OpenAIAssistantAgent agent = new(assistant, AssistantClient);
AgentThread? agentThread = null;

// Respond to user input
try
{
    await InvokeAgentAsync(
        """
                Display this data using a bar-chart (not stacked):

                Banding  Brown Pink Yellow  Sum
                X00000   339   433     126  898
                X00300    48   421     222  691
                X12345    16   395     352  763
                Others    23   373     156  552
                Sum      426  1622     856 2904
                """);

    await InvokeAgentAsync("Can you regenerate this same chart using the category names as the bar colors?");
}
finally
{
    if (agentThread is not null)
    {
        await agentThread.DeleteAsync();
    }

    await AssistantClient.DeleteAssistantAsync(agent.Id);
}

// Local function to invoke agent and display the conversation messages.
async Task InvokeAgentAsync(string input)
{
    ChatMessageContent message = new(AuthorRole.User, input);

    Console.WriteLine(message.Content);
    await foreach (AgentResponseItem<ChatMessageContent> response in agent.InvokeAsync(message))
    {
        Console.WriteLine(response.Message.Content);
        await DownloadResponseImageAsync(response);

        agentThread = response.Thread;
    }
}


async Task DownloadResponseContentAsync(ChatMessageContent message)
{
    OpenAIFileClient fileClient = Client.GetOpenAIFileClient();

    foreach (KernelContent item in message.Items)
    {
        if (item is AnnotationContent annotation)
        {
            await DownloadFileContentAsync(fileClient, annotation.ReferenceId!);
        }
    }
}

async Task DownloadResponseImageAsync(ChatMessageContent message)
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