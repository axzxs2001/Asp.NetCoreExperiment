using Azure.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.OpenApi;
using SharpYaml.Tokens;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;
#pragma warning disable 
var apikey = File.ReadAllText("c:/gpt/key.txt");
using HttpClient httpClient = new();

var kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddOpenAIChatCompletion("gpt-4o", apiKey: apikey);
var kernel = kernelBuilder.Build();

var pluginArr = new List<PluginSetting>
{
    new PluginSetting{PluginName="OrderService",UriString="http://localhost:5000/openapi/v1.json"}
};
var token = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJhQGEuY29tIiwibmJmIjoxNzM2MjMwODI1LCJleHAiOjE3NTk1NjQxNTgsImlzcyI6Imh0dHBzOi8vd3d3Lmp1c3QtYWdpLmNvbS9zbWFydGZpbGwiLCJhdWQiOiJodHRwczovL3d3dy5qdXN0LWFnaS5jb20vc21hcnRmaWxsIn0.TxhOMBkwWADihneRnx_OI7rwz_y9vEGMnO8ZjJgL5nwmji8hrlb8GhXKPEQX2Ueol2kCFApcKhoF2kYTKaUwvQ";
BearerAuthenticationProviderWithCancellationToken authenticationProvider = new(() => Task.FromResult(token));
foreach (var pluginItem in pluginArr)
{
    var plugin = await OpenApiKernelPluginFactory.CreateFromOpenApiAsync(pluginName: pluginItem.PluginName,
        uri: new Uri(pluginItem.UriString),
        executionParameters: new OpenApiFunctionExecutionParameters(httpClient)
        {
            IgnoreNonCompliantErrors = true,
            //OperationsToExclude = new List<string> { "GET /token" },
            EnableDynamicPayload = false,
            AuthCallback = authenticationProvider.AuthenticateRequestAsync,
            ServerUrlOverride=new Uri("http://localhost:5000"), 
        });
    kernel.Plugins.Add(plugin);
}
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
var openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
};
while (true)
{
    Console.WriteLine("1、查询订单    2、添加订单    3、");

    var sn = Console.ReadLine();
    switch (sn)
    {
        case "1":
            Console.WriteLine("用户 > 查询一下订单，然后总汇订单的总金额 ");
            var result1 = await chatCompletionService.GetChatMessageContentAsync(
                "总汇一下订单的总金额",
                executionSettings: openAIPromptExecutionSettings,
                kernel: kernel);
            Console.WriteLine("助理 > " + result1);
            break;
        case "2":
            Console.WriteLine("用户 > 添加5个10块钱的苹果订单 ");
            var result2 = await chatCompletionService.GetChatMessageContentAsync(
                "添加5个苹果，每个10块钱的订单",
                executionSettings: openAIPromptExecutionSettings,
                kernel: kernel);
            Console.WriteLine("助理 > " + result2);
            break;
    }
}

public class PluginSetting
{
    public string PluginName { get; set; }
    public string UriString { get; set; }
}

public class BearerAuthenticationProviderWithCancellationToken(Func<Task<string>> bearerToken)
{
    private readonly Func<Task<string>> _bearerToken = bearerToken;

    /// <summary>
    /// Applies the token to the provided HTTP request message.
    /// </summary>
    /// <param name="request">The HTTP request message.</param>
    /// <param name="cancellationToken"></param>
    public async Task AuthenticateRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
    {
        var token = await this._bearerToken().ConfigureAwait(false);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}