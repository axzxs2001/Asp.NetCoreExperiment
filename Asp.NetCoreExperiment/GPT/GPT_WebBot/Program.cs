using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Memory.Sqlite;
using Microsoft.SemanticKernel.Orchestration;
using Microsoft.SemanticKernel.SkillDefinition;
var builder = WebApplication.CreateBuilder(args);
await builder.AddEmband();
var app = builder.Build();
app.UseStaticFiles();
app.MapGet("/bot", async (IKernel kernel, SKContext context, ISKFunction semanticFunction, string ask,CancellationToken token) =>
{
    var facts = kernel.Memory.SearchAsync("gsw", ask, limit: 10, withEmbeddings: true,cancellationToken:token);
    var fact = await facts.FirstOrDefaultAsync(cancellationToken: token);
    context["fact"] = fact?.Metadata?.Text!;
    context["ask"] = ask;
    var resultContext = await semanticFunction.InvokeAsync(context);
    return resultContext.Result;
});
app.Run();
public static class BuilderExt
{
    public static async Task AddEmband(this WebApplicationBuilder builder)
    {
        var key = File.ReadAllText(@"C:\\GPT\key.txt");
        var store = Directory.GetCurrentDirectory() + "/db.sqlite";
        var kernel = Kernel.Builder                 
                   .WithOpenAITextCompletionService("text-davinci-003", key, serviceId: "gsw")
                   .WithOpenAITextEmbeddingGenerationService("text-embedding-ada-002", key, serviceId: "gsw")
                   .WithMemoryStorage(await SqliteMemoryStore.ConnectAsync(store))
                   .Build();
        const string MemoryCollectionName = "gsw";
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info0", text: "���ֽй���ΰ");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info1", text: "�Ա��У����171cm��\r\n����75ǧ��");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info2", text: "ְҵ��ũ�����ó�������");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info3", text: "��20����ֵؾ���");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info4", text: "����ס����ʮĶ��");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info5", text: "�漮ɽ��������ʡ�������������ʮĶ��");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info6", text: "�ϼ�ɽ��������ʡ�������������ʮĶ��");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info7", text: "����ɽ��������ʡ�������������ʮĶ��");
        var prompt = """
        �����𰸻��߲�֪����ʱ˵���ǳ���Ǹ����û���ҵ���Ҫ�����⣡��     
        �Ի��еĹ��ڹ���ΰ����Ϣ:
        {{ $fact }}     
        �û�: {{ $ask }}
        ������:
        """;
        var semanticFunction = kernel.CreateSemanticFunction(prompt, temperature: 0.7, topP: 0.5);
        var context = kernel.CreateNewContext();
        builder.Services.AddSingleton(kernel);
        builder.Services.AddSingleton(semanticFunction);
        builder.Services.AddSingleton(context);
    }
}