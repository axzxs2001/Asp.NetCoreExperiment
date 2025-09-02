// Copyright (c) Microsoft. All rights reserved.

using System.CommandLine;
using System.CommandLine.Invocation;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;

namespace A2A;

public static class Program
{
    public static async Task Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("回车开始：");
            Console.ReadLine();
            await RunCliAsync();
        }
    }

    #region private
    private static async System.Threading.Tasks.Task RunCliAsync()
    {
        // Set up the logging
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            builder.SetMinimumLevel(LogLevel.Information);
        });
        var logger = loggerFactory.CreateLogger("A2AClient");

      
        var arr = File.ReadAllLines("C:/gpt/azure_key.txt");
        var apiKey = arr[2];
        var modelId = arr[0];
        var endpoint = arr[1];
        var agentUrls = "http://localhost:5000/";

        // Create the Host agent
        var hostAgent = new HostClientAgent(logger);
        await hostAgent.InitializeAgentAsync(modelId, endpoint, apiKey, agentUrls!.Split(";"));
        AgentThread thread = new ChatHistoryAgentThread();
        try
        {
            while (true)
            {
                // Get user message
                Console.Write("\nUser (:q or quit to exit): ");
                string? message = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(message))
                {
                    Console.WriteLine("Request cannot be empty.");
                    continue;
                }

                if (message == ":q" || message == "quit")
                {
                    break;
                }

                await foreach (AgentResponseItem<ChatMessageContent> response in hostAgent.Agent!.InvokeAsync(message, thread))
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"\nAgent: {response.Message.Content}");
                    Console.ResetColor();

                    thread = response.Thread;
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while running the A2AClient");
            return;
        }
    }
    #endregion
}
