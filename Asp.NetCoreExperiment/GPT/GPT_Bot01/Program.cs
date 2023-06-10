
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Memory.Sqlite;
using Microsoft.SemanticKernel.CoreSkills;
using Microsoft.SemanticKernel.Memory;
using System.Threading.Tasks;

var key = File.ReadAllText(@"C:\\GPT\key.txt");
var store = Directory.GetCurrentDirectory() + "/db.sqlite";
var kernel = Kernel.Builder           
           .WithOpenAITextCompletionService("text-davinci-003", key, serviceId: "gsw")
           .WithOpenAITextEmbeddingGenerationService("text-embedding-ada-002", key, serviceId: "gsw")
           .WithMemoryStorage(await SqliteMemoryStore.ConnectAsync(store))
           .Build();

const string MemoryCollectionName = "gsw";
await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info0", text: "名字叫桂素伟");
await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info1", text: "性别男，身高171cm，体重75千克");
await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info2", text: "职业是农民，他擅长种茄子");
await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info3", text: "有20年的种地经验");
await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info4", text: "现在住在五十亩村");
await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info5", text: "祖籍山西长治市省黎城县西井镇五十亩村");
await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info6", text: "老家山西长治市省黎城县西井镇五十亩村");
await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info7", text: "来自山西长治市省黎城县西井镇五十亩村");

await Bot1(key, store, kernel);
static async Task Bot1(string key, string store, IKernel kernel)
{
    var prompt = """
    给出答案或者不知道答案时说“非常抱歉，我没有找到你要的问题！”     
    对话中的关于桂素伟的信息:
    {{ $fact }}     
    用户: {{ $ask }}
    机器人:
    """;
    var semanticFunction = kernel.CreateSemanticFunction(prompt, temperature: 0.7, topP: 0.5);
    var context = kernel.CreateNewContext();
    while (true)
    {
        Console.WriteLine("请输入问题：");
        var ask = Console.ReadLine();
        var facts = kernel.Memory.SearchAsync(MemoryCollectionName, ask, limit: 10, withEmbeddings: true);
        var fact= facts.ToBlockingEnumerable().FirstOrDefault();  
        //var fact = await facts.FirstOrDefaultAsync();
        context["fact"] = fact?.Metadata?.Text!;
        context["ask"] = ask;
        var resultContext = await semanticFunction.InvokeAsync(context);
        Console.WriteLine($"Bot:{resultContext.Result}");
    }
}

static async Task Bot2(string key, string store, IKernel kernel)
{

    kernel.ImportSkill(new TextMemorySkill());
    var prompt = """
    给出答案或者不知道答案时说“非常抱歉，我没有找到你要的问题！”
    对话中的关于桂素伟的信息:
    {{ recall $ask }}
    用户: {{ $ask }}
    机器人:
    """;
    var semanticFunction = kernel.CreateSemanticFunction(prompt, temperature: 0.7, topP: 0.5);
    var context = kernel.CreateNewContext();
    context[TextMemorySkill.CollectionParam] = MemoryCollectionName;
    while (true)
    {
        Console.WriteLine("请输入问题：");
        context["ask"] = Console.ReadLine();
        var resultContext = await semanticFunction.InvokeAsync(context);
        Console.WriteLine($"Bot:{resultContext.Result}");
    }
}