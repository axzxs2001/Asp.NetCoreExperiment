using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;

var chatModelId = "gpt-4";
var key = File.ReadAllText(@"C:\GPT\key.txt");


Kernel kernel = Kernel.CreateBuilder()
           .AddOpenAIChatCompletion(
               modelId: chatModelId,
               apiKey: key)
           .Build();

// Function defined using few-shot design pattern
string promptTemplate = @"
Generate a creative reason or excuse for the given event.
Be creative and be funny. Let your imagination run wild.

Event: I am running late.
Excuse: I was being held ransom by giraffe gangsters.

Event: I haven't been to the gym for a year
Excuse: I've been too busy training my pet dragon.

Event: {{$input}}
";

var excuseFunction = kernel.CreateFunctionFromPrompt(promptTemplate, new OpenAIPromptExecutionSettings() { MaxTokens = 100, Temperature = 0.4, TopP = 1 });

var result = await kernel.InvokeAsync(excuseFunction, new() { ["input"] = "I missed the F1 final race" });
Console.WriteLine(result.GetValue<string>());

result = await kernel.InvokeAsync(excuseFunction, new() { ["input"] = "sorry I forgot your birthday" });
Console.WriteLine(result.GetValue<string>());

var fixedFunction = kernel.CreateFunctionFromPrompt($"Translate this date {DateTimeOffset.Now:f} to French format", new OpenAIPromptExecutionSettings() { MaxTokens = 100 });

result = await kernel.InvokeAsync(fixedFunction);
Console.WriteLine(result.GetValue<string>());