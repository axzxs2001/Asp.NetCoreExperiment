using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Core;

var openAIModelId = "gpt-4";
var openAIApiKey = File.ReadAllText(@"C:\GPT\key.txt");

Kernel kernel = Kernel.CreateBuilder()
          .AddOpenAIChatCompletion(
              modelId: openAIModelId,
              apiKey: openAIApiKey)
          .Build();

// Load native plugin into the kernel function collection, sharing its functions with prompt templates
// Functions loaded here are available as "time.*"
#pragma warning disable SKEXP0050
kernel.ImportPluginFromType<TimePlugin>("time");

// Prompt Function invoking time.Date and time.Time method functions
const string FunctionDefinition = @"
Today is: {{time.Date}}
Current time is: {{time.Time}}

Answer to the following questions using JSON syntax, including the data used.
Is it morning, afternoon, evening, or night (morning/afternoon/evening/night)?
Is it weekend time (weekend/not weekend)?
";

// This allows to see the prompt before it's sent to OpenAI
Console.WriteLine("--- Rendered Prompt");
var promptTemplateFactory = new KernelPromptTemplateFactory();
var promptTemplate = promptTemplateFactory.Create(new PromptTemplateConfig(FunctionDefinition));
var renderedPrompt = await promptTemplate.RenderAsync(kernel);
Console.WriteLine(renderedPrompt);

// Run the prompt / prompt function
var kindOfDay = kernel.CreateFunctionFromPrompt(FunctionDefinition, new OpenAIPromptExecutionSettings() { MaxTokens = 100 });

// Show the result
Console.WriteLine("--- Prompt Function result");
var result = await kernel.InvokeAsync(kindOfDay);
Console.WriteLine(result.GetValue<string>());