
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Orchestration;
using Microsoft.SemanticKernel.Agents.Orchestration.Concurrent;
using Microsoft.SemanticKernel.Agents.Orchestration.Transforms;
using Microsoft.SemanticKernel.Agents.Runtime.InProcess;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.Text.Encodings.Web;
using System.Text.Json;

#pragma warning disable
const int ResultTimeoutInSeconds = 30;
var modelID = "gpt-4.1";
var openAIKey = File.ReadAllText("c://gpt/key.txt");

var kernel = Kernel.CreateBuilder()
           .AddOpenAIChatCompletion(modelID, openAIKey).Build();
ChatCompletionAgent agent1 =
    CreateAgent(
        instructions: "你是识别文章主题的专家。给定一篇文章，请识别其主要主题。",
        description: "擅长识别文章主题的专家",
         name: "",
        kernel: kernel);
ChatCompletionAgent agent2 =
    CreateAgent(
        instructions: "你是情感分析方面的专家。给定一篇文章，请识别其情感倾向。",
        description: "情感分析专家",
        name: "",
        kernel: kernel);
ChatCompletionAgent agent3 =
    CreateAgent(
        instructions: "你是实体识别方面的专家。给定一篇文章，请提取其中的实体。",
        description: "实体识别专家",
         name: "",
        kernel: kernel);


var outputTransform = new StructuredOutputTransform<Analysis>(kernel.GetRequiredService<IChatCompletionService>(),
        new OpenAIPromptExecutionSettings { ResponseFormat = typeof(Analysis) });
var orchestration = new ConcurrentOrchestration<string, Analysis>(agent1, agent2, agent3)
{
    ResponseCallback = (response) =>
    {
        Console.WriteLine($"# {response.AuthorName} : {response.Content}");
        return ValueTask.CompletedTask;
    },
    LoggerFactory = NullLoggerFactory.Instance,
    ResultTransform = outputTransform.TransformAsync,
};

InProcessRuntime runtime = new();
await runtime.StartAsync();

string input = @"在一个黑暗的冬夜，一位幽灵在丹麦的埃尔西诺城堡的城墙上徘徊。最初被一对守夜人发现，随后由学者霍拉旭确认，这位幽灵看上去酷似最近去世的老哈姆雷特国王。而老国王的弟弟克劳狄斯则继承了王位，并娶了国王的遗孀，王后格特鲁德。当霍拉旭与守夜人将王子哈姆雷特，也就是格特鲁德与死去国王的儿子，带来见幽灵时，幽灵开口了。他宣称自己确实是哈姆雷特的父亲，并透露自己是被克劳狄斯谋杀的。幽灵命令哈姆雷特为他复仇，惩罚这个篡位并娶其妻子的男人，随后随晨曦消失。
哈姆雷特王子决定为父复仇，但由于他天性多思而沉静，他迟迟没有动手，陷入了深深的忧郁，甚至表现出疯狂的样子。克劳狄斯与格特鲁德对哈姆雷特异常的行为感到担忧，并设法找出原因。他们派遣哈姆雷特的两位朋友，罗森克兰茨与吉尔登斯吞，监视他。自负的宫廷大臣波洛涅斯认为哈姆雷特可能是因爱上他的女儿奥菲莉娅而疯了，克劳狄斯于是安排偷听哈姆雷特与奥菲莉娅的对话。但尽管哈姆雷特看起来的确疯了，他似乎并不爱奥菲莉娅：他命令她进修道院，并声称希望禁止世上的婚姻。
一群巡演演员来到埃尔西诺，哈姆雷特借机设下计策来试探他叔父的罪行。他让这些演员表演一场戏，剧情与他所设想的叔父杀害父亲的过程极其相似。如果克劳狄斯有罪，他必定会有所反应。果然，在剧中演到谋杀的一刻，克劳狄斯猛地起身离席。哈姆雷特与霍拉旭因此认定他确实有罪。哈姆雷特随即前去杀他，但发现他正在祷告。哈姆雷特认为若在祈祷中杀死克劳狄斯，会让他的灵魂升入天堂，这样的复仇太过仁慈，遂决定等待更好的时机。此时，惧怕哈姆雷特疯狂行为的克劳狄斯下令立即将他送往英格兰。
哈姆雷特去找母亲对质，此时波洛涅斯正藏身于她寝宫的帷幕后。听见幕后有动静，哈姆雷特误以为是国王藏在其中，拔剑刺去，结果杀死了波洛涅斯。为此，他被立即流放至英格兰，与他同行的仍是罗森克兰茨与吉尔登斯吞。然而，克劳狄斯的计划并不只是驱逐哈姆雷特，他交给两人密函，命令英格兰国王处死哈姆雷特。
波洛涅斯死后，奥菲莉娅因悲痛发狂，最终在河中溺亡。波洛涅斯之子雷欧提斯在法国得知噩耗后愤怒归国，克劳狄斯煽动他将父亲与妹妹之死归咎于哈姆雷特。当霍拉旭与国王收到哈姆雷特的来信，说他在赴英途中遭遇海盗袭击并已返回丹麦，克劳狄斯便借雷欧提斯之手谋杀哈姆雷特。他安排一场看似友好的击剑比赛，雷欧提斯的剑尖涂有剧毒，一旦划破皮肤便会致命。作为备选方案，克劳狄斯还准备了一杯毒酒，若哈姆雷特先得分，就让他饮下。
哈姆雷特归来之时正逢奥菲莉娅的葬礼。他悲伤至极，与雷欧提斯扭打在一起，并宣称自己一直深爱奥菲莉娅。回到城堡后，哈姆雷特告诉霍拉旭，人应当随时准备面对死亡，因为死亡无处不在。此时，滑稽的廷臣奥斯里克奉命前来安排哈姆雷特与雷欧提斯的比剑。
比剑开始，哈姆雷特先得一分，但拒绝饮下克劳狄斯递来的酒。结果王后格特鲁德误饮毒酒，很快身亡。雷欧提斯成功刺伤哈姆雷特，但也被自己的毒剑所伤。在临死前，他坦白一切是克劳狄斯的阴谋。哈姆雷特愤怒之下用毒剑刺死克劳狄斯，并逼他喝下剩余毒酒。克劳狄斯死去，哈姆雷特也在复仇后咽下最后一口气。
此时，挪威王子福丁布拉斯率军归来，早前他曾攻打波兰。英格兰的使节也抵达，报告罗森克兰茨与吉尔登斯吞已死。福丁布拉斯面对满地的尸体震惊不已，准备接管丹麦王位。霍拉旭依照哈姆雷特临终遗愿，将整个悲剧娓娓道来。福丁布拉斯命人以战士的礼仪，安葬哈姆雷特。";

OrchestrationResult<Analysis> result = await orchestration.InvokeAsync(input, runtime);

Analysis output = await result.GetValueAsync(TimeSpan.FromSeconds(ResultTimeoutInSeconds * 2));
JsonSerializerOptions s_options = new()
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
};
Console.WriteLine($"\n# RESULT:\n{JsonSerializer.Serialize(output, s_options)}");

await runtime.RunUntilIdleAsync();

ChatCompletionAgent CreateAgent(string instructions, string? description = null, string? name = null, Kernel? kernel = null)
{
    return
        new ChatCompletionAgent
        {
            Name = name,
            Description = description,
            Instructions = instructions,
            Kernel = kernel,
        };
}


class Analysis
{
    public IList<string> Themes { get; set; } = [];
    public IList<string> Sentiments { get; set; } = [];
    public IList<string> Entities { get; set; } = [];
}