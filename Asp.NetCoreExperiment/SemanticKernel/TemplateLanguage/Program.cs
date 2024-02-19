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
今天是: {{time.Date}}
当前时间是: {{time.Time}}

使用 JSON 语法回答以下问题，包括所使用的数据。
是早上、下午、晚上还是晚上（早上/下午/晚上/晚上）？
现在是周末时间（周末/非周末）吗？
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