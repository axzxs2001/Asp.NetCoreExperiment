
using Microsoft.SemanticKernel.TextGeneration;
using Microsoft.SemanticKernel;
using static System.Console;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

await CustomTextGenerationWithKernelFunctionAsync();
await CustomTextGenerationAsync();
await CustomTextGenerationStreamAsync();

async Task CustomTextGenerationWithKernelFunctionAsync()
{
    var openAIModelId = "gpt-4";
    var openAIApiKey = File.ReadAllText(@"C:\GPT\key.txt");
    IKernelBuilder builder = Kernel.CreateBuilder();
    // Add your text generation service as a singleton instance
    builder.Services.AddKeyedSingleton<ITextGenerationService>("myService1", new MyTextGenerationService());
    // Add your text generation service as a factory method
    builder.Services.AddKeyedSingleton<ITextGenerationService>("myService2", (_, _) => new MyTextGenerationService());
    Kernel kernel = builder.Build();

    const string FunctionDefinition = "写一段话 {{$input}}";
    var paragraphWritingFunction = kernel.CreateFunctionFromPrompt(FunctionDefinition);

    const string Input = "为什么人工智能很棒";
    WriteLine($"方法参数: {Input}\n");
    var result = await paragraphWritingFunction.InvokeAsync(kernel, new() { ["input"] = Input });

    WriteLine(result);
}

async Task CustomTextGenerationAsync()
{
    WriteLine("\n======== Custom LLM  - Text Completion - Raw ========");

    const string Prompt = "写一篇文章来说明人工智能为何如此出色。";
    var completionService = new MyTextGenerationService();

    WriteLine($"Prompt: {Prompt}\n");
    var result = await completionService.GetTextContentAsync(Prompt);

    WriteLine(result);
}
async Task CustomTextGenerationStreamAsync()
{
    WriteLine("\n======== Custom LLM  - Text Completion - Raw Streaming ========");

    const string Prompt = "写一篇文章来说明人工智能为何如此出色。";
    var completionService = new MyTextGenerationService();

    WriteLine($"Prompt: {Prompt}\n");
    await foreach (var message in completionService.GetStreamingTextContentsAsync(Prompt))
    {
        Write(message);
    }

    WriteLine();
}

sealed class MyTextGenerationService : ITextGenerationService
{
    private const string LLMResultText = @"....自定义模型的输出...示例：人工智能很棒，因为它可以帮助我们解决复杂的问题，增强我们的创造力，并在很多方面改善我们的生活。人工智能可以执行太困难的任务，对人类来说乏味或危险的，例如诊断疾病、检测欺诈或探索空间。 人工智能还可以增强我们的能力并激励我们创造新的形式艺术、音乐或文学。 人工智能还可以通过以下方式改善我们的福祉和幸福感提供个性化的建议、娱乐和帮助。 人工智能太棒了。";

    public IReadOnlyDictionary<string, object?> Attributes => new Dictionary<string, object?>();

    public async IAsyncEnumerable<StreamingTextContent> GetStreamingTextContentsAsync(string prompt, PromptExecutionSettings? executionSettings = null, Kernel? kernel = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        foreach (var word in LLMResultText.ToArray())
        {
            await Task.Delay(50, cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();

            yield return new StreamingTextContent($"{word}");
        }
    }

    public Task<IReadOnlyList<TextContent>> GetTextContentsAsync(string prompt, PromptExecutionSettings? executionSettings = null, Kernel? kernel = null, CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IReadOnlyList<TextContent>>(new List<TextContent>
            {
                new(LLMResultText)
            });
    }
}

