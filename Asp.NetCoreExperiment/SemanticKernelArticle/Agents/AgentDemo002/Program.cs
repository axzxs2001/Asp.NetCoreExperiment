using Azure.AI.OpenAI;
using Azure.Identity;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents.OpenAI;
using Microsoft.SemanticKernel.ChatCompletion;
using OpenAI;
using OpenAI.Assistants;
using OpenAI.Files;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AgentsSample;
#pragma warning disable
public static class Program
{
    public static async Task Main()
    {
     

        // Initialize the clients
       // AzureOpenAIClient client = OpenAIAssistantAgent.CreateAzureOpenAIClient(new AzureCliCredential(), new Uri(settings.AzureOpenAI.Endpoint));
       var openAIKey=File.ReadAllText("c://gpt/key.txt");
        var client = new OpenAIClient(openAIKey);

        //OpenAIClient client = OpenAIAssistantAgent.CreateOpenAIClient(new ApiKeyCredential(settings.OpenAI.ApiKey)));
        AssistantClient assistantClient = client.GetAssistantClient();
        OpenAIFileClient fileClient = client.GetOpenAIFileClient();

        // Upload files
        Console.WriteLine("Uploading files...");       
        OpenAIFile fileDataCountryDetail = await fileClient.UploadFileAsync("PopulationByAdmin1.csv", FileUploadPurpose.Assistants);
        OpenAIFile fileDataCountryList = await fileClient.UploadFileAsync("PopulationByCountry.csv", FileUploadPurpose.Assistants);

        // Define assistant
        Console.WriteLine("Defining assistant...");
        Assistant assistant =
            await assistantClient.CreateAssistantAsync(
                "gpt-4o",
                name: "SampleAssistantAgent",
                instructions:
                        """
                        分析可用数据以回答用户的问题。  
                        始终使用 Markdown 格式化响应内容。  
                        对于任何列表或表格，始终使用从 1 开始的数字索引。  
                        始终按升序对列表进行排序。
                        """,
                enableCodeInterpreter: true,
                codeInterpreterFileIds: [fileDataCountryList.Id, fileDataCountryDetail.Id]);

        // Create agent
        OpenAIAssistantAgent agent = new(assistant, assistantClient);

        // Create the conversation thread
        Console.WriteLine("Creating thread...");
        var thread = (await assistantClient.CreateThreadAsync()).Value;

        Console.WriteLine("Ready!");

        try
        {
            bool isComplete = false;
            List<string> fileIds = [];
            do
            {
                Console.WriteLine();
                Console.Write("> ");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    continue;
                }
                if (input.Trim().Equals("EXIT", StringComparison.OrdinalIgnoreCase))
                {
                    isComplete = true;
                    break;
                }

                await agent.AddChatMessageAsync(thread.Id, new ChatMessageContent(AuthorRole.User, input));

                Console.WriteLine();

                bool isCode = false;
                await foreach (StreamingChatMessageContent response in agent.InvokeStreamingAsync(thread.Id))
                {
                    if (isCode != (response.Metadata?.ContainsKey(OpenAIAssistantAgent.CodeInterpreterMetadataKey) ?? false))
                    {
                        Console.WriteLine();
                        isCode = !isCode;
                    }

                    // Display response.
                    Console.Write($"{response.Content}");

                    // Capture file IDs for downloading.
                    fileIds.AddRange(response.Items.OfType<StreamingFileReferenceContent>().Select(item => item.FileId));
                }
                Console.WriteLine();

                // Download any files referenced in the response.
                await DownloadResponseImageAsync(fileClient, fileIds);
                fileIds.Clear();

            } while (!isComplete);
        }
        finally
        {
            Console.WriteLine();
            Console.WriteLine("Cleaning-up...");
            await Task.WhenAll(
                [
                    assistantClient.DeleteThreadAsync(thread.Id),
                    assistantClient.DeleteAssistantAsync(assistant.Id),
                    fileClient.DeleteFileAsync(fileDataCountryList.Id),
                    fileClient.DeleteFileAsync(fileDataCountryDetail.Id),
                ]);
        }
    }

    private static async Task DownloadResponseImageAsync(OpenAIFileClient client, ICollection<string> fileIds)
    {
        if (fileIds.Count > 0)
        {
            Console.WriteLine();
            foreach (string fileId in fileIds)
            {
                await DownloadFileContentAsync(client, fileId, launchViewer: true);
            }
        }
    }

    private static async Task DownloadFileContentAsync(OpenAIFileClient client, string fileId, bool launchViewer = false)
    {
        OpenAIFile fileInfo = client.GetFile(fileId);
        if (fileInfo.Purpose == FilePurpose.AssistantsOutput)
        {
            string filePath =
                Path.Combine(
                    Path.GetTempPath(),
                    Path.GetFileName(Path.ChangeExtension(fileInfo.Filename, ".png")));

            BinaryData content = await client.DownloadFileAsync(fileId);
            await using FileStream fileStream = new(filePath, FileMode.CreateNew);
            await content.ToStream().CopyToAsync(fileStream);
            Console.WriteLine($"File saved to: {filePath}.");

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
}