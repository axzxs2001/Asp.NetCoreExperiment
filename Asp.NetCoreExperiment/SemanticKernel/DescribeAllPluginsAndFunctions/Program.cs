using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.ComponentModel;
using static System.Console;

var openAIModelId = "gpt-4";
var openAIApiKey = File.ReadAllText(@"C:\GPT\key.txt");

var kernel = Kernel.CreateBuilder()
           .AddOpenAIChatCompletion(
               modelId: openAIModelId,
               apiKey: openAIApiKey)
           .Build();

// Import a native plugin
kernel.ImportPluginFromType<StaticTextPlugin>();

// Import another native plugin
//kernel.ImportPluginFromType<TextPlugin>("AnotherTextPlugin");

//// Import a semantic plugin
//string folder = RepoFiles.SamplePluginsPath();
//kernel.ImportPluginFromPromptDirectory(Path.Combine(folder, "SummarizePlugin"));

// Define a prompt function inline, without naming
var sFun1 = kernel.CreateFunctionFromPrompt("给我讲一个关于{{$input}}的笑话", new OpenAIPromptExecutionSettings() { MaxTokens = 150 });
PrintFunction(sFun1.Metadata);

// Define a prompt function inline, with plugin name
var sFun2 = kernel.CreateFunctionFromPrompt(
    "用 {{$language}} 语言，写一个关于{{$input}}的小说。",
    new OpenAIPromptExecutionSettings() { MaxTokens = 150 },
    functionName: "Novel",
    description: "写一个睡前故事");
PrintFunction(sFun2.Metadata);

var functions = kernel.Plugins.GetFunctionsMetadata();

WriteLine("**********************************************");
WriteLine("****** Registered plugins and functions ******");
WriteLine("**********************************************");
WriteLine();

foreach (KernelFunctionMetadata func in functions)
{
    PrintFunction(func);
}


void PrintFunction(KernelFunctionMetadata func)
{
    WriteLine($"Plugin: {func.PluginName}");
    WriteLine($"   {func.Name}: {func.Description}");

    if (func.Parameters.Count > 0)
    {
        WriteLine("      Params:");
        foreach (var p in func.Parameters)
        {
            WriteLine($"      - {p.Name}: {p.Description}");
            WriteLine($"        default: '{p.DefaultValue}'");
        }
    }

    WriteLine();
}

public sealed class StaticTextPlugin
{
    [KernelFunction, Description("Change all string chars to uppercase")]
    public static string Uppercase([Description("Text to uppercase")] string input) =>
        input.ToUpperInvariant();

    [KernelFunction, Description("Append the day variable")]
    public static string AppendDay(
        [Description("Text to append to")] string input,
        [Description("Value of the day to append")] string day) =>
        input + day;
}