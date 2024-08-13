using Microsoft.SemanticKernel.Experimental.Agents;
using System.Text;

#pragma warning disable SKEXP0101
namespace Demo08_Agent
{
    internal class Program
    {
        static string key;
        static string chatModelId;
        static List<IAgent> agents;
        static async Task Main(string[] args)
        {
            key = File.ReadAllText(@"C:\GPT\key.txt");
            chatModelId = "gpt-4-turbo";
            agents = new List<IAgent>();
            Console.WriteLine("======== 开始协作 ========");
            IAgentThread? agentThread = null;
            try
            {
                // 创建文案代理来产生想法
                var translator = await CreateTranslatorAsync();
                // 创建艺术总监代理来审查想法、提供反馈和最终批准
                var auditor = await CreateAuditorAsync();

                // 创建协作线程，两个代理都向其中添加消息。
                agentThread = await translator.NewThreadAsync();

                var prompt = new StringBuilder();
                prompt.AppendLine("把下面中文翻译成日文：");
                prompt.AppendLine("-----------------");
                prompt.AppendLine(File.ReadAllText("content.txt"));
                var contnet = "";
                // 添加用户留言
                var messageUser = await agentThread.AddUserMessageAsync(prompt.ToString());
                DisplayMessages(null, ConsoleColor.White, messageUser);
                var times = 1;
                bool isComplete = false;
                do
                {
                    times++;
                    // 启动高程工作
                    var agentMessages = await agentThread.InvokeAsync(translator).ToArrayAsync();
                    DisplayMessages(translator, ConsoleColor.Green, agentMessages);

                    // 启动架构工作
                    agentMessages = await agentThread.InvokeAsync(auditor).ToArrayAsync();
                    DisplayMessages(auditor, ConsoleColor.Yellow, agentMessages);

                    // 评估是否达到目标。
                    if (agentMessages.First().Content.Contains("采用它", StringComparison.OrdinalIgnoreCase))
                    {
                        isComplete = true;
                    }
                    if (times > 3)
                    {
                        isComplete = true;
                    }
                }
                while (!isComplete);
            }
            finally
            {
                // 清理
                await Task.WhenAll(agents.Select(a => a.DeleteAsync()));
            }


        }
        static async Task<IAgent> CreateTranslatorAsync()
        {
            var agent = await new AgentBuilder()
                        .WithOpenAIChatCompletion(chatModelId, key)
                        .WithInstructions("您是一位中文日文的翻译家，以严谨闻名。 你全神贯注于手头的目标。翻译成准确，高质量的译文。完善翻译内容时，请考虑翻译审核员的建议。")
                        .WithName("翻译家")
                        .WithDescription("翻译家")
                        .BuildAsync();
            return agent;

        }

        static async Task<IAgent> CreateAuditorAsync()
        {
            var agent = await new AgentBuilder()
                        .WithOpenAIChatCompletion(chatModelId, key)
                        .WithInstructions("你是一位中日文翻译的翻译审核员，你有丰富的翻译和审核经验，对翻译质量有较高的要求，总是严格要求，反复琢磨，以求得到更为准确的翻译。目标是确定给定翻译否符合要求，是否采用。如果不符合要求，提出你的建议给翻译家，但不要把翻译内容给对方。如果翻译内容可以接受并且符合您的标准，请说：采用它。")
                        .WithName("翻译审核员")
                        .WithDescription("翻译审核员")
                        .BuildAsync();
            return agent;
        }

        static void DisplayMessages(IAgent? agent = null, ConsoleColor color = ConsoleColor.White, params IEnumerable<IChatMessage> messages)
        {
            foreach (var message in messages)
            {
                Console.ResetColor();
                Console.ForegroundColor = color;
               // Console.WriteLine($"[{message.Id}]");
                if (agent != null)
                {
                    Console.WriteLine($"{agent.Name}（{message.Role}）: {message.Content}");
                }
                else
                {
                    Console.WriteLine($"（{message.Role}）: {message.Content}");
                }
                Console.ResetColor();
            }
        }
    }
}
