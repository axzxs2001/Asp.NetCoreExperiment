using Azure;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using OpenAI.Chat;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http.Headers;
using Azure;
using Azure.AI.Inference;
using OllamaSharp;

#pragma warning disable
var apiKey = File.ReadAllText("C:/gpt/deepseekkey.txt");
//await HttpInvockAsync();
//await SKInvockAsync();
await SKStreamInvockAsync();
//await FCInvockAsync();
//await HttpStreamAshnc();

async Task HttpStreamAshnc()
{


    var endpoint = new Uri("https://DeepSeek-R1-iwztj.eastus2.models.ai.azure.com");
    var credential = new AzureKeyCredential(apiKey);
    var model = "DeepSeek-R1";

    var client = new ChatCompletionsClient(
        endpoint,
        credential,
        new AzureAIInferenceClientOptions());

    var requestOptions = new ChatCompletionsOptions()
    {
        Messages =
    {
        new ChatRequestUserMessage("出几道勾股定理的题")
    },
        MaxTokens = 2048,
        Model = model
    };

    StreamingResponse<StreamingChatCompletionsUpdate> response = await client.CompleteStreamingAsync(requestOptions);
    await foreach (StreamingChatCompletionsUpdate chatUpdate in response)
    {
        if (!string.IsNullOrEmpty(chatUpdate.ContentUpdate))
        {
            System.Console.Write(chatUpdate.ContentUpdate);
        }
    }
    System.Console.WriteLine("");
}
async Task HttpInvockAsync()
{

    var handler = new HttpClientHandler()
    {
        ClientCertificateOptions = ClientCertificateOption.Manual,
        ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
    };
    using (var client = new HttpClient(handler))
    {
        var requestBody = @"{
                  ""messages"": [                 
                    {
                      ""role"": ""user"",
                      ""content"": ""你是谁？""
                    }
                  ],
                  ""max_tokens"": 2048
                }";


        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        client.BaseAddress = new Uri("https://DeepSeek-R1-iwztj.eastus2.models.ai.azure.com/chat/completions");

        var content = new StringContent(requestBody);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

        HttpResponseMessage response = await client.PostAsync("", content);

        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            var ent = System.Text.Json.JsonSerializer.Deserialize<ChatCompletionResponse>(result, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            foreach (var choice in ent.Choices)
            {
                Console.WriteLine("Result: {0}", choice.Message.Content);
            }
        }
        else
        {
            Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));
            Console.WriteLine(response.Headers.ToString());

            Console.WriteLine("错误");
        }
    }
}
async Task SKInvockAsync()
{
    var chatCompletionService = new OpenAIChatCompletionService(
        endpoint: new Uri("https://DeepSeek-R1-iwztj.eastus2.models.ai.azure.com/"),
        apiKey: apiKey,
        modelId: "deepseek-chat"
    );
    var chatHistory = new ChatHistory();
    chatHistory.AddUserMessage("你好，你是谁?");
    var reply = await chatCompletionService.GetChatMessageContentAsync(chatHistory);
    Console.WriteLine(reply);
}
async Task SKStreamInvockAsync()
{
    //不能实现
    var chatCompletionService = new OpenAIChatCompletionService(
        endpoint: new Uri("https://DeepSeek-R1-iwztj.eastus2.models.ai.azure.com/"),
        apiKey: apiKey,
        modelId: "deepseek-r1"
    );
    var chatHistory = new ChatHistory();
    chatHistory.AddUserMessage("你好，你是谁?");
    var content = chatCompletionService.GetStreamingChatMessageContentsAsync(chatHistory);
    await foreach (var reply in content)
    {
        Console.WriteLine(reply.Content);
    }
}

async Task FCInvockAsync()
{
    //不支持
    var builder = Kernel.CreateBuilder();
    var modelId = "deepseek-chat";
    var endpoint = new Uri("https://DeepSeek-R1-iwztj.eastus2.models.ai.azure.com/");
    builder.Services.AddOpenAIChatCompletion(modelId, endpoint, apiKey: apiKey);
    builder.Plugins.AddFromType<TimePlugin>();
    var kernel = builder.Build();
    var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
    var settings = new PromptExecutionSettings
    {
        FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
    };
    var prompt = "当前时间是什么";
    var chatResult = await chatCompletionService.GetChatMessageContentAsync(prompt, settings, kernel);
    Console.Write(chatResult);
}
public class TimePlugin
{
    [KernelFunction("getCurrentTime")]
    [Description("获取当前时间")]
    [return: Description("An array of lights")]
    public async Task<string> GetCurrentTime()
    {
        return DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒");
    }
}


public class ChatCompletionResponse
{
    public List<Choice> Choices { get; set; }
    public long Created { get; set; }
    public string Id { get; set; }
    public string Model { get; set; }
    public string Object { get; set; }
    public List<PromptFilterResult> PromptFilterResults { get; set; }
    public Usage Usage { get; set; }
}

public class Choice
{
    public ContentFilterResults ContentFilterResults { get; set; }
    public string FinishReason { get; set; }
    public int Index { get; set; }
    public Message Message { get; set; }
}

public class ContentFilterResults
{
    public FilterStatus Hate { get; set; }
    public FilterStatus SelfHarm { get; set; }
    public FilterStatus Sexual { get; set; }
    public FilterStatus Violence { get; set; }
}

public class FilterStatus
{
    public bool Filtered { get; set; }
    public string Severity { get; set; }
}

public class Message
{
    public string Content { get; set; }
    public string ReasoningContent { get; set; }
    public string Role { get; set; }
    public object ToolCalls { get; set; }
}

public class PromptFilterResult
{
    public int PromptIndex { get; set; }
    public ContentFilterResults ContentFilterResults { get; set; }
}

public class Usage
{
    public int CompletionTokens { get; set; }
    public int PromptTokens { get; set; }
    public object PromptTokensDetails { get; set; }
    public int TotalTokens { get; set; }
}
