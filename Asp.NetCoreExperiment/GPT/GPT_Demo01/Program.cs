using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AI.OpenAI.TextEmbedding;
using Microsoft.SemanticKernel.CoreSkills;
using Microsoft.SemanticKernel.Memory;
using Microsoft.SemanticKernel.Planning;
using Microsoft.SemanticKernel.SemanticFunctions;
using Microsoft.SemanticKernel.SkillDefinition;
using System;
using System.Runtime.InteropServices;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

var key = File.ReadAllText(@"C:\\GPT\key.txt");
//await Demo.Chat(key);

//await LinkDemo.LinkerAsync(key);
//await PlanDemo.StarPlan(key);
await Demo.Chat(key);


public class PlanDemo
{
    public static async Task StarPlan(string key)
    {
        var kernel = Kernel.Builder
            .Configure(c =>
            {
                c.AddOpenAITextCompletionService("text-davinci-003", apiKey: key, serviceId: "openai");
            })
            .Build();


        var planner = new SequentialPlanner(kernel);
        var plan = await planner.CreatePlanAsync("执行TextAddSkill");

        var skills = kernel.ImportSkill(new TextAddSkill(), nameof(TextAddSkill));
        plan.AddSteps(skills.Values.ToArray());

        var prompt = $$"""
        请给出下周语句中所包含的日期或者日期段的开始日期和结束日期。今天是{{DateTime.Now}}
        +++++
        {{"{{$input}}"}}
        +++++
        """;
        var summaryFunction = kernel.CreateSemanticFunction(prompt, new PromptTemplateConfig(), skillName: "sem");
        plan.AddSteps(summaryFunction);


        var input = "明天是什么日子?";
        #region 分步
        var maxSteps = 10;
        try
        {
            for (int step = 1; plan.HasNextStep && step < maxSteps; step++)
            {
                if (string.IsNullOrEmpty(input))
                {
                    await plan.InvokeNextStepAsync(kernel.CreateNewContext());
                }
                else
                {
                    plan = await kernel.StepAsync(input, plan);
                }
                if (!plan.HasNextStep)
                {
                    Console.WriteLine($"Step {step} - COMPLETE!");
                    Console.WriteLine(plan.State.ToString());
                    break;
                }
                Console.WriteLine($"Step {step} - Results so far:");
                Console.WriteLine(plan.State.ToString());
                input = plan.State.ToString();
            }
        }
        catch (KernelException e)
        {
            Console.WriteLine($"Step - Execution failed:");
            Console.WriteLine(e.Message);
        }
        return;
        #endregion
        #region 全部
        var resultContext = await kernel.RunAsync(input, plan);
        Console.WriteLine(resultContext.Result);
        Console.WriteLine("---------------------");
        #endregion
    }
    public class TextAddSkill
    {
        [SKFunction("add time1")]
        public string AddData1(string text)
        {
            return text + $"++++ 下周";
        }
        [SKFunction("add time")]
        public string AddData(string text)
        {
            return text + $" ++++ 下个月";
        }
    }
}


public class ConnectorDemo
{
    public void F()
    {
        // httpconnector
    }

}

public class LinkDemo
{
    public static async Task LinkerAsync(string key)
    {
        var kernel = Kernel.Builder
            .Configure(c =>
            {
                c.AddOpenAITextCompletionService("text-davinci-003", key, serviceId: "openai");
            })
            .Build();
        var prompt = $$"""
        请给出下周语句中所包含的日期或者日期段的开始日期和结束日期。今天是{{DateTime.Now}}
        +++++
        {{"{{$input}}"}}
        +++++
        """;
        var summaryFunction = kernel.CreateSemanticFunction(prompt, new PromptTemplateConfig());


        var textSkill = kernel.ImportSkill(new TextSkill(), nameof(TextSkill));
        var uppercaseFunction = textSkill[nameof(TextSkill.AddData)];

        while (true)
        {
            Console.WriteLine("请输入问题：");
            var input = Console.ReadLine();
            var resultContext = await kernel.RunAsync(input, summaryFunction, uppercaseFunction);
            Console.WriteLine($"结果：{resultContext.Result}");
        }

    }

    public class TextSkill
    {
        [SKFunction("Convert a string to uppercase.")]
        public string AddData(string text)
        {
            return text + $"|||{DateTime.Now}";
        }
    }
}


public class DateSkill
{
    [SKFunction("查询日期段数据")]
    public async Task GGG(string data)
    {
        var key = File.ReadAllText(@"C:\\GPT\key.txt");
        var arr = await GetDates(key, data);
        foreach (var line in arr)
        {
            Console.WriteLine(line);
        }

        if (arr.Length == 1)
        {
            arr = arr[0].Split("至");
        }
        if (arr.Length == 2)
        {
            var json = $$"""
    [
      {
        "date":{{arr[0]}},
        "item_id": 1,
        "item_name": "Product A",
        "item_quantity": 2,
        "item_price": 9.99
      },
      {
        "date":{{arr[1]}},
        "item_id": 2,
        "item_name": "Product B",
        "item_quantity": 1,
        "item_price": 19.99
      }
    ]
    """;
            Console.WriteLine(await GetTable(key, json));
        }
    }
    async Task<string[]> GetDates(string key, string data)
    {
        var kernel = Kernel.Builder
            .Configure(c =>
            {
                c.AddOpenAIChatCompletionService("openai", "gpt-4", key);
            })
            .Build();

        var chatGPT = kernel.GetService<IChatCompletion>();
        var chatHistory = (OpenAIChatHistory)chatGPT.CreateNewChat($"今天是{DateTime.Now}。");
        chatHistory.AddUserMessage("请分多行给出下面句子中的日期或时间。如果遇到今年，去年，本月等信息，转换成一个开始日期和结束日期，并且分行显示。");


        chatHistory.AddUserMessage(data);
        var cfg = new ChatRequestSettings();
        var reply = await chatGPT.GenerateMessageAsync(chatHistory, cfg);
        chatHistory.AddAssistantMessage(reply);
        return reply.Split('\r', '\n');

    }
    async Task<string> GetTable(string key, string json)
    {
        var kernel = Kernel.Builder
            .Configure(c =>
            {
                c.AddOpenAIChatCompletionService("openai", "gpt-4", key);
            })
            .Build();

        var chatGPT = kernel.GetService<IChatCompletion>();
        var chatHistory = (OpenAIChatHistory)chatGPT.CreateNewChat($"请把下面的json转成表格输出：");
        chatHistory.AddUserMessage(json);

        var cfg = new ChatRequestSettings();
        var reply = await chatGPT.GenerateMessageAsync(chatHistory, cfg);
        return reply;
    }
}
public class Demo
{
    //await BotAsync(key);
    public static async Task Bot2Async(string key)
    {
        var kernel = Kernel.Builder
            .Configure(c =>
            {
                c.AddOpenAITextCompletionService("text-davinci-003", key, serviceId: "openai");
                c.AddOpenAITextEmbeddingGenerationService("text-embedding-ada-002", key, serviceId: "openai");
            })
            .WithMemoryStorage(new VolatileMemoryStore())
            .Build();
        const string MemoryCollectionName = "aboutMe";

        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info1", text: "桂素伟，性别男，身高171cm，体重75千克");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info2", text: "桂素伟的职业是农民，他擅长种茄子");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info3", text: "桂素伟有20年的种地经验");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info4", text: "桂素伟现在住在五十亩村");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info5", text: "我叫桂素伟1");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info6", text: "我叫桂素伟2");

        kernel.ImportSkill(new TextMemorySkill());

        var prompt =
        """
给出答案或者不知道答案时说“非常抱歉，我没有找到你要的问题！”
 
对话中的关于桂素伟的信息:
{{ recall $ask }}
 
User: {{ $ask }}
ChatBot:
""";

        var semanticFunction = kernel.CreateSemanticFunction(prompt, temperature: 0.7, topP: 0.5);
        var context = kernel.CreateNewContext();
        context[TextMemorySkill.CollectionParam] = MemoryCollectionName;
        while (true)
        {
            Console.WriteLine("请输入问题：");
            var ask = Console.ReadLine();
            context["ask"] = ask;
            var resultContext = await semanticFunction.InvokeAsync(context);
            Console.WriteLine($"Bot:{resultContext.Result}");
        }
    }
    public static async Task Bot1Async(string key)
    {
        var kernel = Kernel.Builder
            .Configure(c =>
            {
                c.AddOpenAITextCompletionService("text-davinci-003", key, serviceId: "openai");
                c.AddOpenAITextEmbeddingGenerationService("text-embedding-ada-002", key, serviceId: "openai");
            })
            .WithMemoryStorage(new VolatileMemoryStore())
            .Build();
        const string MemoryCollectionName = "aboutMe";

        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info1", text: "桂素伟，性别男，身高171cm，体重75千克");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info2", text: "桂素伟的职业是农民，他擅长种茄子");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info3", text: "桂素伟有20年的种地经验");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info4", text: "桂素伟现在住在五十亩村");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info5", text: "我叫桂素伟1");
        await kernel.Memory.SaveInformationAsync(MemoryCollectionName, id: "info6", text: "我叫桂素伟2");

        var prompt =
        """
给出答案或者不知道答案时说“非常抱歉，我没有找到你要的问题！”
 
对话中的关于桂素伟的信息:
{{ $fact }}
 
User: {{ $ask }}
ChatBot:
""";

        var semanticFunction = kernel.CreateSemanticFunction(prompt, temperature: 0.7, topP: 0.5);
        while (true)
        {
            Console.WriteLine("请输入问题：");
            var ask = Console.ReadLine();
            var facts = kernel.Memory.SearchAsync(MemoryCollectionName, ask, limit: 10, withEmbeddings: true);
            var factContent = "";
            await facts.ForEachAsync(res =>
            {
                // Console.WriteLine($"全部:{res.Metadata.Text},{res.Relevance},{JsonSerializer.Serialize(res.Metadata)}");
                if (res.Relevance > 0.7d)
                {
                    factContent += res.Metadata.Text + "\r\n";
                }
            });
            Console.WriteLine(factContent);
            var fact = await facts.FirstOrDefaultAsync();
            var context = kernel.CreateNewContext();
            // context["fact"] = factContent;
            context["fact"] = fact?.Metadata?.Text;
            context["ask"] = ask;

            var resultContext = await semanticFunction.InvokeAsync(context);
            Console.WriteLine($"Bot:{resultContext.Result}");
        }
    }
    //await Chat(key);    //聊天
    public static async Task Chat(string key)
    {
        var kernel = Kernel.Builder
            .Configure(c =>
            {
                c.AddOpenAIChatCompletionService("gpt-4", key, serviceId: "davinci-openai");
            })
            .Build();

        var chatGPT = kernel.GetService<IChatCompletion>();
        var chatHistory = (OpenAIChatHistory)chatGPT.CreateNewChat("你是一个.net专家");
        //while (true)
        //{
        //    Console.WriteLine("输入问题：");

        //    chatHistory.AddUserMessage(Console.ReadLine());
        //    var cfg = new ChatRequestSettings();
        //    var reply = await chatGPT.GenerateMessageAsync(chatHistory, cfg);
        //    chatHistory.AddAssistantMessage(reply);

        //    Console.WriteLine("聊天内容:");
        //    Console.WriteLine("------------------------");
        //    foreach (var message in chatHistory.Messages)
        //    {

        //        Console.WriteLine($"{message.AuthorRole}: {message.Content}");
        //        Console.WriteLine("------------------------");
        //    }
        //}
        while (true)
        {
            Console.WriteLine("输入问题：");

            chatHistory.AddUserMessage(Console.ReadLine());
            var cfg = new ChatRequestSettings();
            var reply = chatGPT.GenerateMessageStreamAsync(chatHistory, cfg);
            var ccc = "";
            await reply.ForEachAwaitAsync(async s =>
             {
                 Console.Out.WriteAsync(s);
                 //Console.WriteLine($"分段：{s}");
                 ccc += s;

             });
            await reply.ForEachAsync((s, i) =>
              {
                  Console.Out.WriteAsync(s);
                  //Console.WriteLine($"分段：{s}");
                  ccc += s;

              });
            chatHistory.AddAssistantMessage(ccc);

            //Console.WriteLine("聊天内容:");
            //Console.WriteLine("------------------------");
            //foreach (var message in chatHistory.Messages)
            //{

            //    Console.WriteLine($"{message.AuthorRole}: {message.Content}");
            //    Console.WriteLine("------------------------");
            //}
        }

    }
}





