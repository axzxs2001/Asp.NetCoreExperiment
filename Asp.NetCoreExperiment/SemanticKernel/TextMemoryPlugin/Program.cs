
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel;
using System.Threading;
using Microsoft.SemanticKernel.Plugins.Memory;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System.Text.RegularExpressions;



#pragma warning disable SKEXP0003
#pragma warning disable SKEXP0052
const string MemoryCollectionName = "aboutMe";
IMemoryStore store = new VolatileMemoryStore();
await RunWithStoreAsync(store, CancellationToken.None);

Console.ReadLine();
static async Task RunAsync(CancellationToken cancellationToken = default)
{
#pragma warning disable SKEXP0003
#pragma warning disable SKEXP0052
    IMemoryStore store = new VolatileMemoryStore();

    ///////////////////////////////////////////////////////////////////////////////////////////
    // INSTRUCTIONS: uncomment one of the following lines to select the memory store to use. //
    ///////////////////////////////////////////////////////////////////////////////////////////

    // Volatile Memory Store - an in-memory store that is not persisted


    // Sqlite Memory Store - a file-based store that persists data in a Sqlite database
    // store = await CreateSampleSqliteMemoryStoreAsync();

    // DuckDB Memory Store - a file-based store that persists data in a DuckDB database
    // store = await CreateSampleDuckDbMemoryStoreAsync();

    // MongoDB Memory Store - a store that persists data in a MongoDB database
    // store = CreateSampleMongoDBMemoryStore();

    // Azure AI Search Memory Store - a store that persists data in a hosted Azure AI Search database
    // store = CreateSampleAzureAISearchMemoryStore();

    // Qdrant Memory Store - a store that persists data in a local or remote Qdrant database
    // store = CreateSampleQdrantMemoryStore();

    // Chroma Memory Store
    // store = CreateSampleChromaMemoryStore();

    // Pinecone Memory Store - a store that persists data in a hosted Pinecone database
    // store = CreateSamplePineconeMemoryStore();

    // Weaviate Memory Store
    // store = CreateSampleWeaviateMemoryStore();

    // Redis Memory Store
    // store = await CreateSampleRedisMemoryStoreAsync();

    // Postgres Memory Store
    // store = CreateSamplePostgresMemoryStore();

    // Kusto Memory Store
    // store = CreateSampleKustoMemoryStore();

    await RunWithStoreAsync(store, cancellationToken);
}
#region
//private static async Task<IMemoryStore> CreateSampleSqliteMemoryStoreAsync()
//{
//    IMemoryStore store = await SqliteMemoryStore.ConnectAsync("memories.sqlite");
//    return store;
//}

//private static async Task<IMemoryStore> CreateSampleDuckDbMemoryStoreAsync()
//{
//    IMemoryStore store = await DuckDBMemoryStore.ConnectAsync("memories.duckdb");
//    return store;
//}

//private static IMemoryStore CreateSampleMongoDBMemoryStore()
//{
//    IMemoryStore store = new MongoDBMemoryStore(TestConfiguration.MongoDB.ConnectionString, "memoryPluginExample");
//    return store;
//}

//private static IMemoryStore CreateSampleAzureAISearchMemoryStore()
//{
//    IMemoryStore store = new AzureAISearchMemoryStore(TestConfiguration.AzureAISearch.Endpoint, TestConfiguration.AzureAISearch.ApiKey);
//    return store;
//}

//private static IMemoryStore CreateSampleChromaMemoryStore()
//{
//    IMemoryStore store = new ChromaMemoryStore(TestConfiguration.Chroma.Endpoint, ConsoleLogger.LoggerFactory);
//    return store;
//}

//private static IMemoryStore CreateSampleQdrantMemoryStore()
//{
//    IMemoryStore store = new QdrantMemoryStore(TestConfiguration.Qdrant.Endpoint, 1536, ConsoleLogger.LoggerFactory);
//    return store;
//}

//private static IMemoryStore CreateSamplePineconeMemoryStore()
//{
//    IMemoryStore store = new PineconeMemoryStore(TestConfiguration.Pinecone.Environment, TestConfiguration.Pinecone.ApiKey, ConsoleLogger.LoggerFactory);
//    return store;
//}

//private static IMemoryStore CreateSampleWeaviateMemoryStore()
//{
//    IMemoryStore store = new WeaviateMemoryStore(TestConfiguration.Weaviate.Endpoint, TestConfiguration.Weaviate.ApiKey);
//    return store;
//}

//private static async Task<IMemoryStore> CreateSampleRedisMemoryStoreAsync()
//{
//    string configuration = TestConfiguration.Redis.Configuration;
//    ConnectionMultiplexer connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(configuration);
//    IDatabase database = connectionMultiplexer.GetDatabase();
//    IMemoryStore store = new RedisMemoryStore(database, vectorSize: 1536);
//    return store;
//}

//private static IMemoryStore CreateSamplePostgresMemoryStore()
//{
//    NpgsqlDataSourceBuilder dataSourceBuilder = new(TestConfiguration.Postgres.ConnectionString);
//    dataSourceBuilder.UseVector();
//    NpgsqlDataSource dataSource = dataSourceBuilder.Build();
//    IMemoryStore store = new PostgresMemoryStore(dataSource, vectorSize: 1536, schema: "public");
//    return store;
//}

//private static IMemoryStore CreateSampleKustoMemoryStore()
//{
//    var connectionString = new Kusto.Data.KustoConnectionStringBuilder(TestConfiguration.Kusto.ConnectionString).WithAadUserPromptAuthentication();
//    IMemoryStore store = new KustoMemoryStore(connectionString, "MyDatabase");
//    return store;
//}
#endregion
static async Task RunWithStoreAsync(IMemoryStore memoryStore, CancellationToken cancellationToken)
{
#pragma warning disable SKEXP0011
    var openAIModelId = "gpt-4";
    var openAIApiKey = File.ReadAllText(@"C:\GPT\key.txt");
    var embeddingModelId = "text-embedding-ada-002";
    var kernel = Kernel.CreateBuilder()
        .AddOpenAIChatCompletion(openAIModelId, openAIApiKey)
        .AddOpenAITextEmbeddingGeneration(embeddingModelId, openAIApiKey)
        .Build();

    // Create an embedding generator to use for semantic memory.
    var embeddingGenerator = new OpenAITextEmbeddingGenerationService(embeddingModelId, openAIApiKey);

    // The combination of the text embedding generator and the memory store makes up the 'SemanticTextMemory' object used to
    // store and retrieve memories.
    SemanticTextMemory textMemory = new(memoryStore, embeddingGenerator);

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // PART 1: Store and retrieve memories using the ISemanticTextMemory (textMemory) object.
    //
    // This is a simple way to store memories from a code perspective, without using the Kernel.
    /////////////////////////////////////////////////////////////////////////////////////////////////////
    Console.WriteLine("== 第 1a 部分：通过 ISemanticTextMemory 对象保存内存 ==");

    Console.WriteLine("使用键“info1”节省内存：“我的名字是安德里亚”");
    await textMemory.SaveInformationAsync(MemoryCollectionName, id: "info1", text: "我的名字是安德里亚", cancellationToken: cancellationToken);

    Console.WriteLine("使用键“info2”节省内存：“我是一名旅游经营者”");
    await textMemory.SaveInformationAsync(MemoryCollectionName, id: "info2", text: "我是一名旅游经营者", cancellationToken: cancellationToken);

    Console.WriteLine("使用键“info3”节省内存：“自 2005 年以来我一直住在西雅图”");
    await textMemory.SaveInformationAsync(MemoryCollectionName, id: "info3", text: "我自 2005 年以来一直住在西雅图", cancellationToken: cancellationToken);

    Console.WriteLine("使用键“info4”节省内存：“自 2015 年以来，我访问了法国和意大利五次”");
    await textMemory.SaveInformationAsync(MemoryCollectionName, id: "info4", text: "自2015年以来我曾五次访问法国和意大利", cancellationToken: cancellationToken);

    // Retrieve a memory
    Console.WriteLine("== 第 1b 部分：通过 ISemanticTextMemory 对象检索内存 ==");
    MemoryQueryResult? lookup = await textMemory.GetAsync(MemoryCollectionName, "info1", cancellationToken: cancellationToken);
    Console.WriteLine("带有键“info1”的内存:" + lookup?.Metadata.Text ?? "错误：找不到内存");
    Console.WriteLine();

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // 第 2 部分：创建 TextMemoryPlugin，通过内核存储和检索内存。
    //
    // 这使得提示功能和人工智能（通过规划器）能够访问记忆
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    Console.WriteLine("== 第 2a 部分：使用 TextMemoryPlugin 和“Save”函数通过内核保存内存 ==");

    // Import the TextMemoryPlugin into the Kernel for other functions
    var memoryPlugin = kernel.ImportPluginFromObject(new TextMemoryPlugin(textMemory));

    // Save a memory with the Kernel
    Console.WriteLine("使用键“info5”节省内存：“我的家人来自纽约”");
    await kernel.InvokeAsync(memoryPlugin["Save"], new()
    {
        [TextMemoryPlugin.InputParam] = "我的家人来自纽约",
        [TextMemoryPlugin.CollectionParam] = MemoryCollectionName,
        [TextMemoryPlugin.KeyParam] = "info5",
    }, cancellationToken);

    // Retrieve a specific memory with the Kernel
    Console.WriteLine("== 第 2b 部分：使用 TextMemoryPlugin 和“Retrieve”函数通过内核检索内存 ==");
    var result = await kernel.InvokeAsync(memoryPlugin["Retrieve"], new KernelArguments()
    {
        [TextMemoryPlugin.CollectionParam] = MemoryCollectionName,
        [TextMemoryPlugin.KeyParam] = "info5"
    }, cancellationToken);

    Console.WriteLine("带有“info5”键的内存:" + result.GetValue<string>() ?? "错误：找不到内存");
    Console.WriteLine();

    /////////////////////////////////////////////////////////////////////////////////////////////////////
    // PART 3: Recall similar ideas with semantic search
    //
    // Uses AI Embeddings for fuzzy lookup of memories based on intent, rather than a specific key.
    /////////////////////////////////////////////////////////////////////////////////////////////////////

    Console.WriteLine("== 第 3 部分：使用 AI 嵌入进行Recall （相似性搜索） ==");

    Console.WriteLine("== 第 3a 部分：使用 ISemanticTextMemory 进行Recall （相似性搜索） ==");
    Console.WriteLine("问：我在哪里长大？");

    await foreach (var answer in textMemory.SearchAsync(
        collection: MemoryCollectionName,
        query: "我在哪里长大?",
        limit: 2,
        minRelevanceScore: 0.79,
        withEmbeddings: true,
        cancellationToken: cancellationToken))
    {
        Console.WriteLine($"回答: {answer.Metadata.Text}");
    }

    Console.WriteLine("==第 3b 部分：使用内核和 TextMemoryPlugin 的“Recall”功能进行 Recall（相似性搜索） ==");
    Console.WriteLine("问题:我住在哪里？");

    result = await kernel.InvokeAsync(memoryPlugin["Recall"], new()
    {
        [TextMemoryPlugin.InputParam] = "问题:我住在哪里？",
        [TextMemoryPlugin.CollectionParam] = MemoryCollectionName,
        [TextMemoryPlugin.LimitParam] = "2",
        [TextMemoryPlugin.RelevanceParam] = "0.79",
    }, cancellationToken);

    Console.WriteLine($"回答: {Regex.Unescape(result.GetValue<string>())}");
    Console.WriteLine();


    Console.WriteLine("== 第 4 部分：在提示功能中使用 TextMemoryPlugin 'Recall' 功能 ==");

    // Build a prompt function that uses memory to find facts
    const string RecallFunctionDefinition = @"
回答问题时仅考虑以下事实：

从事实开始
关于我：{{recall '我在哪里长大？'}}
关于我：{{recall '我现在住在哪里？'}}
最终事实

问题：{{$input}}

回答：
";

    var aboutMeOracle = kernel.CreateFunctionFromPrompt(RecallFunctionDefinition, new OpenAIPromptExecutionSettings() { MaxTokens = 100 });

    result = await kernel.InvokeAsync(aboutMeOracle, new()
    {
        [TextMemoryPlugin.InputParam] = "我住的我长大的同一个城镇吗？",
        [TextMemoryPlugin.CollectionParam] = MemoryCollectionName,
        [TextMemoryPlugin.LimitParam] = "2",
        [TextMemoryPlugin.RelevanceParam] = "0.79",
    }, cancellationToken);

    Console.WriteLine("问题: 我住的我长大的同一个城镇吗？");
    Console.WriteLine($"回答: {result.GetValue<string>()}");



    Console.WriteLine("== 第 5 部分：清理、删除数据库集合 ==");

    Console.WriteLine("打印数据库中的集合...");
    var collections = memoryStore.GetCollectionsAsync(cancellationToken);
    await foreach (var collection in collections)
    {
        Console.WriteLine(collection);
    }
    Console.WriteLine();

    Console.WriteLine("删除集合{0}", MemoryCollectionName);
    await memoryStore.DeleteCollectionAsync(MemoryCollectionName, cancellationToken);
    Console.WriteLine();

    Console.WriteLine($"打印数据库中的集合（删除后{MemoryCollectionName})...");
    collections = memoryStore.GetCollectionsAsync(cancellationToken);
    await foreach (var collection in collections)
    {
        Console.WriteLine(collection);
    }
}
