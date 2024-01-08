using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Memory;
const string MemoryCollectionName = "SKGitHub";


await OpenAIChatSampleAsync();
Console.ReadLine();
static async Task OpenAIChatSampleAsync()
{
    var key = File.ReadAllText(@"C:\GPT\key.txt");
    //var chatModelId = "gpt-4";
    //OpenAIChatCompletionService chatCompletionService = new(chatModelId, key);

#pragma warning disable SKEXP0003
#pragma warning disable SKEXP0011
#pragma warning disable SKEXP0052
    var memoryWithCustomDb = new MemoryBuilder()
        .WithOpenAITextEmbeddingGeneration("text-embedding-ada-002", key)
        .WithMemoryStore(new VolatileMemoryStore())
        .Build();
    await RunExampleAsync(memoryWithCustomDb);


}


static async Task RunExampleAsync(ISemanticTextMemory memory)
{
    await StoreMemoryAsync(memory);

    await SearchMemoryAsync(memory, "你多大了?");

    await SearchMemoryAsync(memory, "你叫什么?");


}

static async Task SearchMemoryAsync(ISemanticTextMemory memory, string query)
{
    Console.WriteLine("\nQuery: " + query + "\n");

    var memoryResults = memory.SearchAsync(MemoryCollectionName, query, limit: 2, minRelevanceScore: 0.5);

    int i = 0;
    await foreach (MemoryQueryResult memoryResult in memoryResults)
    {
        Console.WriteLine($"Result {++i}:");
        Console.WriteLine("  URL:     : " + memoryResult.Metadata.Id);
        Console.WriteLine("  Title    : " + memoryResult.Metadata.Description);
        Console.WriteLine("  Relevance: " + memoryResult.Relevance);
        Console.WriteLine();
    }

    Console.WriteLine("----------------------");
}

static async Task StoreMemoryAsync(ISemanticTextMemory memory)
{
    var githubFiles = SampleData();
    var i = 0;
    foreach (var entry in githubFiles)
    {
        await memory.SaveReferenceAsync(
            collection: MemoryCollectionName,
            externalSourceName: "GitHub",
            externalId: entry.Key,
            description: entry.Value,
            text: entry.Value);

        Console.Write($" #{++i} saved.");
    }

    Console.WriteLine("\n----------------------");
}

static Dictionary<string, string> SampleData()
{
    return new Dictionary<string, string>
    {
        ["姓名"] = "说明：我叫桂素伟",
        ["年龄"] = "说明：今年21岁"
        //["https://github.com/microsoft/semantic-kernel/blob/main/README.md"]
        //    = "README: Installation, getting started, and how to contribute",
        //["https://github.com/microsoft/semantic-kernel/blob/main/dotnet/notebooks/02-running-prompts-from-file.ipynb"]
        //    = "Jupyter notebook describing how to pass prompts from a file to a semantic plugin or function",
        //["https://github.com/microsoft/semantic-kernel/blob/main/dotnet/notebooks//00-getting-started.ipynb"]
        //    = "Jupyter notebook describing how to get started with the Semantic Kernel",
        //["https://github.com/microsoft/semantic-kernel/tree/main/samples/plugins/ChatPlugin/ChatGPT"]
        //    = "Sample demonstrating how to create a chat plugin interfacing with ChatGPT",
        //["https://github.com/microsoft/semantic-kernel/blob/main/dotnet/src/SemanticKernel/Memory/VolatileMemoryStore.cs"]
        //    = "C# class that defines a volatile embedding store",
    };
}

