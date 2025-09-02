// Copyright (c) Microsoft. All rights reserved.

using A2A;
using Azure.AI.Agents.Persistent;
using Azure.Identity;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.A2A;
using Microsoft.SemanticKernel.Agents.AzureAI;
using Microsoft.SemanticKernel.Agents.OpenAI;
namespace A2AServer;

internal static class HostAgentFactory
{
    internal static async Task<A2AHostAgent> CreateFoundryHostAgentAsync(string agentType, string modelId, string endpoint, string assistantId, IEnumerable<KernelPlugin>? plugins = null)
    {
        var agentsClient = new PersistentAgentsClient(endpoint, new AzureCliCredential());
        PersistentAgent definition = await agentsClient.Administration.GetAgentAsync(assistantId);

        var agent = new AzureAIAgent(definition, agentsClient, plugins);

        AgentCard agentCard = agentType.ToUpperInvariant() switch
        {
            "INVOICE" => GetInvoiceAgentCard(),
            "POLICY" => GetPolicyAgentCard(),
            "LOGISTICS" => GetLogisticsAgentCard(),
            _ => throw new ArgumentException($"不支持的代理类型: {agentType}"),
        };

        return new A2AHostAgent(agent, agentCard);
    }

    internal static async Task<A2AHostAgent> CreateChatCompletionHostAgentAsync(string agentType, string modelId, string endpoint, string apiKey, string name, string instructions, IEnumerable<KernelPlugin>? plugins = null)
    {
        var builder = Kernel.CreateBuilder();
        builder.AddAzureOpenAIChatCompletion(modelId, endpoint, apiKey);
        if (plugins is not null)
        {
            foreach (var plugin in plugins)
            {
                builder.Plugins.Add(plugin);
            }
        }
        var kernel = builder.Build();

        var agent = new ChatCompletionAgent()
        {
            Kernel = kernel,
            Name = name,
            Instructions = instructions,
            Arguments = new KernelArguments(new PromptExecutionSettings() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() }),
        };

        AgentCard agentCard = agentType.ToUpperInvariant() switch
        {
            "INVOICE" => GetInvoiceAgentCard(),
            "POLICY" => GetPolicyAgentCard(),
            "LOGISTICS" => GetLogisticsAgentCard(),
            _ => throw new ArgumentException($"不支持的代理类型: {agentType}"),
        };

        return new A2AHostAgent(agent, agentCard);
    }

    #region private
    private static AgentCard GetInvoiceAgentCard()
    {
        var capabilities = new AgentCapabilities()
        {
            Streaming = false,
            PushNotifications = false,
        };

        var invoiceQuery = new AgentSkill()
        {
            Id = "id_invoice_agent",
            Name = "InvoiceAgent",
            Description = "处理与发票相关的请求。",
            Tags = ["发票", "semantic-kernel"],
            Examples =
            [
                "列出 Contoso 公司的最新发票。",
            ],
        };

        return new()
        {
            Name = "InvoiceAgent",
            Description = "处理与发票相关的请求。",
            Version = "1.0.0",
            DefaultInputModes = ["text"],
            DefaultOutputModes = ["text"],
            Capabilities = capabilities,
            Skills = [invoiceQuery],
        };
    }

    private static AgentCard GetPolicyAgentCard()
    {
        var capabilities = new AgentCapabilities()
        {
            Streaming = false,
            PushNotifications = false,
        };

        var invoiceQuery = new AgentSkill()
        {
            Id = "id_policy_agent",
            Name = "PolicyAgent",
            Description = "处理与政策和客户沟通相关的请求。",
            Tags = ["政策", "semantic-kernel"],
            Examples =
            [
                "短装的政策是什么？",
            ],
        };

        return new AgentCard()
        {
            Name = "PolicyAgent",
            Description = "处理与政策和客户沟通相关的请求。",
            Version = "1.0.0",
            DefaultInputModes = ["text"],
            DefaultOutputModes = ["text"],
            Capabilities = capabilities,
            Skills = [invoiceQuery],
        };
    }

    private static AgentCard GetLogisticsAgentCard()
    {
        var capabilities = new AgentCapabilities()
        {
            Streaming = false,
            PushNotifications = false,
        };

        var invoiceQuery = new AgentSkill()
        {
            Id = "id_invoice_agent",
            Name = "物流查询",
            Description = "处理与物流相关的请求。",
            Tags = ["物流", "semantic-kernel"],
            Examples =
            [
                "SHPMT-SAP-001的状态是什么",
            ],
        };

        return new AgentCard()
        {
            Name = "LogisticsAgent",
            Description = "处理与物流相关的请求。",
            Version = "1.0.0",
            DefaultInputModes = ["text"],
            DefaultOutputModes = ["text"],
            Capabilities = capabilities,
            Skills = [invoiceQuery],
        };
    }
    #endregion
}
