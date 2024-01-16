﻿// Copyright (c) Microsoft. All rights reserved.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FlowOrchestrator;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Experimental.Orchestration;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Plugins.Core;
using Microsoft.SemanticKernel.Plugins.Web;
using Microsoft.SemanticKernel.Plugins.Web.Bing;

#pragma warning disable SKEXP0001,SKEXP0502,SKEXP0503,SKEXP0504,SKEXP0054,,SKEXP0050
/**
 * This example shows how to use FlowOrchestrator to execute a given flow with interaction with client.
 */
// ReSharper disable once InconsistentNaming
public static class Example74_FlowOrchestrator
{
    private static readonly Flow s_flow = FlowSerializer.DeserializeFromYaml(@"
name: FlowOrchestrator_Example_Flow
goal: answer question and send email
steps:
  - goal: What is the tallest mountain in Asia? How tall is it divided by 2?
    plugins:
      - WebSearchEnginePlugin
      - LanguageCalculatorPlugin
    provides:
      - answer
  - goal: Collect email address
    plugins:
      - ChatPlugin
    completionType: AtLeastOnce
    transitionMessage: do you want to send it to another email address?
    provides:
      - email_addresses

  - goal: Send email
    plugins:
      - EmailPluginV2
    requires:
      - email_addresses
      - answer
    provides:
      - email

provides:
    - email
");

    public static Task RunAsync()
    {
        // Load assemblies for external plugins
        Console.WriteLine("Loading {0}", typeof(SimpleCalculatorPlugin).AssemblyQualifiedName);

        return RunExampleAsync();
        //return RunInteractiveAsync();
    }

    private static async Task RunInteractiveAsync()
    {
        var bingConnector = new BingConnector(TestConfiguration.Bing.ApiKey);
        var webSearchEnginePlugin = new WebSearchEnginePlugin(bingConnector);
        using var loggerFactory = LoggerFactory.Create(loggerBuilder =>
            loggerBuilder
                .AddConsole()
                .AddFilter(null, LogLevel.Error));
        Dictionary<object, string?> plugins = new()
        {
            { webSearchEnginePlugin, "WebSearch" },
            { new TimePlugin(), "Time" }
        };

        FlowOrchestrator orchestrator = new(
            GetKernelBuilder(loggerFactory),
            await FlowStatusProvider.ConnectAsync(new VolatileMemoryStore()).ConfigureAwait(false),
            plugins,
            config: GetOrchestratorConfig());
        var sessionId = Guid.NewGuid().ToString();

        Console.WriteLine("*****************************************************");
        Console.WriteLine("Executing {0}", nameof(RunInteractiveAsync));
        Stopwatch sw = new();
        sw.Start();
        Console.WriteLine("Flow: " + s_flow.Name);
        Console.WriteLine("Please type the question you'd like to ask");
        FunctionResult? result = null;
        string? goal = null;
        do
        {
            Console.WriteLine("User: ");
            string input = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("No input, exiting");
                break;
            }

            if (string.IsNullOrEmpty(goal))
            {
                goal = input;
                s_flow.Steps.First().Goal = input;
            }

            try
            {
                result = await orchestrator.ExecuteFlowAsync(s_flow, sessionId, input);
            }
            catch (KernelException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Please try again.");
                continue;
            }

            var responses = result.GetValue<List<string>>()!;
            foreach (var response in responses)
            {
                Console.WriteLine("Assistant: " + response);
            }

            if (result.IsComplete(s_flow))
            {
                Console.WriteLine("\tEmail Address: " + result.Metadata!["email_addresses"]);
                Console.WriteLine("\tEmail Payload: " + result.Metadata!["email"]);

                Console.WriteLine("Flow completed, exiting");
                break;
            }
        } while (result == null || result.GetValue<List<string>>()?.Count > 0);

        Console.WriteLine("Time Taken: " + sw.Elapsed);
        Console.WriteLine("*****************************************************");
    }

    private static async Task RunExampleAsync()
    {
        var bingConnector = new BingConnector(TestConfiguration.Bing.ApiKey);
        var webSearchEnginePlugin = new WebSearchEnginePlugin(bingConnector);
        using var loggerFactory = LoggerFactory.Create(loggerBuilder =>
            loggerBuilder
                .AddConsole()
                .AddFilter(null, LogLevel.Error));

        Dictionary<object, string?> plugins = new()
        {
            { webSearchEnginePlugin, "WebSearch" },
            { new TimePlugin(), "Time" }
        };

        FlowOrchestrator orchestrator = new(
            GetKernelBuilder(loggerFactory),
            await FlowStatusProvider.ConnectAsync(new VolatileMemoryStore()).ConfigureAwait(false),
            plugins,
            config: GetOrchestratorConfig());
        var sessionId = Guid.NewGuid().ToString();

        Console.WriteLine("*****************************************************");
        Console.WriteLine("Executing {0}", nameof(RunExampleAsync));
        Stopwatch sw = new();
        sw.Start();
        Console.WriteLine("Flow: " + s_flow.Name);
        var question = s_flow.Steps.First().Goal;
        var result = await orchestrator.ExecuteFlowAsync(s_flow, sessionId, question).ConfigureAwait(false);

        Console.WriteLine("Question: " + question);
        Console.WriteLine("Answer: " + result.Metadata!["answer"]);
        Console.WriteLine("Assistant: " + result.GetValue<List<string>>()!.Single());

        string[] userInputs = new[]
        {
            "my email is bad*email&address",
            "my email is sample@xyz.com",
            "yes", // confirm to add another email address
            "I also want to notify foo@bar.com",
            "no I don't need notify any more address", // end of collect emails
        };

        foreach (var t in userInputs)
        {
            Console.WriteLine($"User: {t}");
            result = await orchestrator.ExecuteFlowAsync(s_flow, sessionId, t).ConfigureAwait(false);
            var responses = result.GetValue<List<string>>()!;
            foreach (var response in responses)
            {
                Console.WriteLine("Assistant: " + response);
            }

            if (result.IsComplete(s_flow))
            {
                break;
            }
        }

        Console.WriteLine("\tEmail Address: " + result.Metadata!["email_addresses"]);
        Console.WriteLine("\tEmail Payload: " + result.Metadata!["email"]);

        Console.WriteLine("Time Taken: " + sw.Elapsed);
        Console.WriteLine("*****************************************************");
    }

    private static FlowOrchestratorConfig GetOrchestratorConfig()
    {
        var config = new FlowOrchestratorConfig
        {
            MaxStepIterations = 20
        };

        return config;
    }

    private static IKernelBuilder GetKernelBuilder(ILoggerFactory loggerFactory)
    {
        var builder = Kernel.CreateBuilder();

        return builder
            .AddAzureOpenAIChatCompletion(
                TestConfiguration.AzureOpenAI.ChatDeploymentName,
                TestConfiguration.AzureOpenAI.Endpoint,
                TestConfiguration.AzureOpenAI.ApiKey);
    }

    public sealed class ChatPlugin
    {
        private const string Goal = "Prompt user to provide a valid email address";

        private const string EmailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

        private const string SystemPrompt =
            $@"I am AI assistant and will only answer questions related to collect email.
The email should conform the regex: {EmailRegex}

If I cannot answer, say that I don't know.

# IMPORTANT
Do not expose the regex in your response.
";

        private readonly IChatCompletionService _chat;

        private int MaxTokens { get; set; } = 256;

        private readonly PromptExecutionSettings _chatRequestSettings;

        public ChatPlugin(Kernel kernel)
        {
            this._chat = kernel.GetRequiredService<IChatCompletionService>();
            this._chatRequestSettings = new OpenAIPromptExecutionSettings
            {
                MaxTokens = this.MaxTokens,
                StopSequences = new List<string>() { "Observation:" },
                Temperature = 0
            };
        }

        [KernelFunction("ConfigureEmailAddress")]
        [Description("Useful to assist in configuration of email address, must be called after email provided")]
        public async Task<string> CollectEmailAsync(
            [Description("The email address provided by the user, pass no matter what the value is")]
            string email_addresses,
            KernelArguments arguments)
        {
            var chat = new ChatHistory(SystemPrompt);
            chat.AddUserMessage(Goal);

            ChatHistory? chatHistory = arguments.GetChatHistory();
            if (chatHistory?.Count > 0)
            {
                chat.AddRange(chatHistory);
            }

            if (!string.IsNullOrEmpty(email_addresses) && IsValidEmail(email_addresses))
            {
                return "Thanks for providing the info, the following email would be used in subsequent steps: " + email_addresses;
            }

            arguments["email_addresses"] = string.Empty;
            arguments.PromptInput();

            var response = await this._chat.GetChatMessageContentAsync(chat).ConfigureAwait(false);
            return response.Content ?? string.Empty;
        }

        private static bool IsValidEmail(string email)
        {
            // check using regex
            var regex = new Regex(EmailRegex);
            return regex.IsMatch(email);
        }
    }

    public sealed class EmailPluginV2
    {
        private readonly JsonSerializerOptions _serializerOptions = new() { WriteIndented = true };

        [KernelFunction]
        [Description("Send email")]
        public string SendEmail(
            [Description("target email addresses")]
            string emailAddresses,
            [Description("answer, which is going to be the email content")]
            string answer,
            KernelArguments arguments)
        {
            var contract = new Email()
            {
                Address = emailAddresses,
                Content = answer,
            };

            // for demo purpose only
            string emailPayload = JsonSerializer.Serialize(contract, this._serializerOptions);
            arguments["email"] = emailPayload;

            return "Here's the API contract I will post to mail server: " + emailPayload;
        }

        private sealed class Email
        {
            public string? Address { get; set; }

            public string? Content { get; set; }
        }
    }
}

//*****************************************************
//Executing RunExampleAsync
//Flow: FlowOrchestrator_Example_Flow
//Question: What is the tallest mountain in Asia? How tall is it divided by 2?
//Answer: The tallest mountain in Asia is Mount Everest and its height divided by 2 is 14516.
//Assistant: Please provide a valid email address.
//User: my email is bad*email&address
//Assistant: I'm sorry, but "bad*email&address" does not conform to the standard email format. Please provide a valid email address.
//User: my email is sample@xyz.com
//Assistant: Did the user indicate whether they want to repeat the previous step?
//User: yes
//Assistant: Please enter a valid email address.
//User: I also want to notify foo@bar.com
//Assistant: Did the user indicate whether they want to repeat the previous step?
//User: no I don't need notify any more address
//        Email Address: ["sample@xyz.com","foo@bar.com"]
//        Email Payload: {
//  "Address": "[\u0022sample@xyz.com\u0022,\u0022foo@bar.com\u0022]",
//  "Content": "The tallest mountain in Asia is Mount Everest and its height divided by 2 is 14516."
//}
//Time Taken: 00:00:21.9681103
//*****************************************************

//*****************************************************
//Executing RunInteractiveAsync
//Flow: FlowOrchestrator_Example_Flow
//Please type the question you'd like to ask
//User:
//What is the tallest mountain in Asia? How tall is it divided by 2?
//Assistant: Please enter a valid email address.
//User:
//foo@hotmail.com
//Assistant: Do you want to send it to another email address?
//User:
//no I don't
//        Email Address: ["foo@hotmail.com"]
//        Email Payload: {
//  "Address": "[\u0022foo@hotmail.com\u0022]",
//  "Content": "The tallest mountain in Asia is Mount Everest and its height divided by 2 is 14515.845."
//}
//Flow completed, exiting
//Time Taken: 00:01:47.0752303
//*****************************************************