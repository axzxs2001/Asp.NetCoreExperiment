
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using System.Collections;



var openAIModelId = "gpt-4-0125-preview";
var openAIApiKey = File.ReadAllText(@"C:\GPT\key.txt");


/*
 * Example: normally you would place prompt templates in a folder to separate
 *          C# code from natural language code, but you can also define a semantic
 *          function inline if you like.
 */

Kernel kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(
        modelId: openAIModelId,
        apiKey: openAIApiKey)
    .Build();

// Function defined using few-shot design pattern
string promptTemplate = @"
为给定的事件提出一个有创意的理由或借口。
要有创意并且有趣。 让想象力自由驰骋。

事件：我迟到了。
借口：我被长颈鹿歹徒勒索赎金。

事件：我已经一年没去健身房了
借口：我一直忙着训练我的宠物龙。

事件: {{$input}}
";

var excuseFunction = kernel.CreateFunctionFromPrompt(promptTemplate, new OpenAIPromptExecutionSettings() { MaxTokens = 100, Temperature = 0.4, TopP = 1 });

var result = await kernel.InvokeAsync(excuseFunction, new() { ["input"] = "我错过了 F1 决赛" });
foreach (var (k, v) in result.Metadata)
{
    Console.WriteLine(k + " : " + v);
}

Console.WriteLine(result.GetValue<string>());

result = await kernel.InvokeAsync(excuseFunction, new() { ["input"] = "抱歉我忘记了你的生日" });
Console.WriteLine(result.GetValue<string>());

var fixedFunction = kernel.CreateFunctionFromPrompt($"转换这个日期 {DateTimeOffset.Now:f} 成法国时间格式", new OpenAIPromptExecutionSettings() { MaxTokens = 100 });

result = await kernel.InvokeAsync(fixedFunction);
Console.WriteLine(result.GetValue<string>());