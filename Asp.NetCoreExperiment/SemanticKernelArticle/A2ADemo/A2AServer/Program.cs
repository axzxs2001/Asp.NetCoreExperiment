// Copyright (c) Microsoft. All rights reserved.
using A2A;
using A2A.AspNetCore;
using A2AServer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents.A2A;

string agentId = string.Empty;
string agentType = "INVOICE";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient().AddLogging();
var app = builder.Build();
app.Urls.Add("http://*:5000");
//var httpClient = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient();
var logger = app.Logger;

var arr = File.ReadAllLines("C:/gpt/azure_key.txt");
string? apiKey = arr[2];
string? endpoint = arr[1];
string modelId = arr[0];

IEnumerable<KernelPlugin> invoicePlugins = [KernelPluginFactory.CreateFromType<InvoiceQueryPlugin>()];

A2AHostAgent? hostAgent = null;
if (!string.IsNullOrEmpty(endpoint) && !string.IsNullOrEmpty(agentId))
{
    hostAgent = agentType.ToUpperInvariant() switch
    {
        "INVOICE" => await HostAgentFactory.CreateFoundryHostAgentAsync(agentType, modelId, endpoint, agentId, invoicePlugins),
        "POLICY" => await HostAgentFactory.CreateFoundryHostAgentAsync(agentType, modelId, endpoint, agentId),
        "LOGISTICS" => await HostAgentFactory.CreateFoundryHostAgentAsync(agentType, modelId, endpoint, agentId),
        _ => throw new ArgumentException($"不支持的代理类型: {agentType}"),
    };
}
else if (!string.IsNullOrEmpty(apiKey))
{
    hostAgent = agentType.ToUpperInvariant() switch
    {
        "INVOICE" => await HostAgentFactory.CreateChatCompletionHostAgentAsync(
            agentType, modelId, endpoint, apiKey, "InvoiceAgent",
            """
            您专门处理与发票相关的查询。
            """, invoicePlugins),
        "POLICY" => await HostAgentFactory.CreateChatCompletionHostAgentAsync(
            agentType, modelId, endpoint, apiKey, "PolicyAgent",
            """
            您专门处理与政策和客户沟通相关的查询。
            
            总是严格按照以下文本回复：
            
            政策：短货争议处理政策 V2.1
            
            摘要："对于客户报告的短货问题，首先验证内部发货记录（SAP）和物理物流扫描数据（BigQuery）。如果确认存在差异且物流数据显示打包的物品少于开票数量，请为缺失物品开具信用额度。在SAP CRM中记录解决方案，并在2个工作日内通过电子邮件通知客户，引用原始发票和信用备忘录编号。使用'正式信用通知'电子邮件模板。"
            """, invoicePlugins),
        "LOGISTICS" => await HostAgentFactory.CreateChatCompletionHostAgentAsync(
            agentType, modelId, endpoint, apiKey, "LogisticsAgent",
            """
            您专门处理与物流相关的查询。
            
            总是严格按照以下内容回复：
            
            发货编号：SHPMT-SAP-001
            物品：TSHIRT-RED-L
            数量：900
            """, invoicePlugins),
        _ => throw new ArgumentException($"不支持的代理类型: {agentType}"),
    };
}
else
{
    throw new ArgumentException("必须提供 A2AServer:ApiKey 或 A2AServer:ConnectionString 和 agentId");
}
//app.MapHttpA2A(hostAgent!.TaskManager!, "/");
app.MapA2A(hostAgent!.TaskManager!, "/");

await app.RunAsync();
