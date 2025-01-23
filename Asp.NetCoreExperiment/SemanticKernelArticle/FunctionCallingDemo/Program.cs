using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Plugins.Web;



var key = File.ReadAllText(@"C:\GPT\key.txt");
var builder = Kernel.CreateBuilder();
builder.Services.AddOpenAIChatCompletion("gpt-4o", key);

//connectionString
//SQL
//QueryParameterJsonSchema
//FunctionPrompt
//ResultPrompt

const string p1Schema = """
            {
              "$schema": "http://json-schema.org/draft-07/schema#",
              "type": "object",
              "properties": {
                 "category": {
                    "type": "string",
                    "description": "订单类别"
                 },
                 "date": {
                     "type": "string",
                    "description": "订单日期"
                  }
               },
            "required": ["category"]
            }
            """;

var function = KernelFunctionFactory.CreateFromMethod(
           method: Select,
           functionName: "QueryOrder",
           description: "查询订单",
           parameters: new KernelParameterMetadata[] {
               new KernelParameterMetadata("pars")
               {
                   Schema = KernelJsonSchema.Parse(p1Schema)
               },
               new KernelParameterMetadata("appkey")
               {
                   Description = "appkey",
                   ParameterType = typeof(string),
                   DefaultValue="123"
               }
           },
           returnParameter: new KernelReturnParameterMetadata()
           {
               Description = "订单结果",
               ParameterType = typeof(string)
           });

builder.Plugins.AddFromFunctions("QueryOrder", new KernelFunction[] { function });
builder.Plugins.AddFromFunctions("CurrentDate", new KernelFunction[] {
    KernelFunctionFactory.CreateFromMethod(()=>DateTime .Now,"CurrentDate","获取当前日期")
});
var kernel = builder.Build();

ChatHistory history = [];

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
var openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};


while (true)
{
    Console.Write("用户 > ");
    history.AddUserMessage(Console.ReadLine()!);
    var result = chatCompletionService.GetStreamingChatMessageContentsAsync(
        history,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);
    Console.Write("助理 > ");
    var contents = "";
    AuthorRole role = AuthorRole.Assistant;
    await foreach (var item in result)
    {
        item.Role = role;
        contents += item.Content;
        Console.Write(item.Content);
    }
    Console.WriteLine();
    history.AddMessage(role, contents);
}

async Task<string> Select(string pars, string appkey)
{
    Console.WriteLine($"APPKey={appkey}");
    await Task.Delay(10);
    // Console.WriteLine(pars);
    var dic = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(pars);
    if (dic != null)
    {
        foreach (var item in dic)
        {
            Console.WriteLine(item.Key + " : " + item.Value);
        }
    }
    return $"汾酒：3000件";
}
