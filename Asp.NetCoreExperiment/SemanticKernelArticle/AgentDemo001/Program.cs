using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Azure.Identity;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.Chat;
//using Microsoft.SemanticKernel.Agents.History;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;

namespace AgentsSample;
#pragma warning disable
public static class Program
{
    public static async Task Main()
    {


        Console.WriteLine("Creating kernel...");
        var modelID = "gpt-4o";
        var openAIKey = File.ReadAllText("c://gpt/key.txt");
        var kernel = Kernel.CreateBuilder()
                   .AddOpenAIChatCompletion(modelID, openAIKey).Build();



        Kernel toolKernel = kernel.Clone();
        toolKernel.Plugins.AddFromType<ClipboardAccess>();


        Console.WriteLine("Defining agents...");

        const string ReviewerName = "Reviewer";
        const string WriterName = "Writer";

        ChatCompletionAgent agentReviewer =
            new()
            {
                Name = ReviewerName,
                Instructions =
                    """
                    你的职责是审核并识别用户提供的内容中可改进的地方。
                    如果用户针对已提供的内容提出了输入或修改方向，需明确说明应如何根据该输入进行调整。
                    绝不直接进行修改或提供示例。
                    一旦用户在后续回复中更新了内容，你需要重新审核，直到内容达到满意为止。
                    当内容达到满意标准时，需使用可用工具将其复制到剪贴板，并告知用户。

                    规则：
                    - 仅提出具体且可执行的修改建议。
                    - 确认之前的建议已被采纳或处理。
                    - 绝不重复之前的建议。
                    """,
                Kernel = toolKernel,
                Arguments = new KernelArguments(new AzureOpenAIPromptExecutionSettings() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() })
            };

        ChatCompletionAgent agentWriter =
            new()
            {
                Name = WriterName,
                Instructions =
                    """
                    你的唯一职责是根据审核建议完整重写内容。

                    规则：
                    - 必须完全执行所有审核意见和指示。
                    - 必须整体重写内容，不作任何解释。
                    - 禁止与用户交流或回应。
                    """,
                Kernel = kernel,
            };

        KernelFunction selectionFunction =
            AgentGroupChat.CreatePromptFunctionForStrategy(
                $$$"""
                请审阅提供的 RESPONSE，并选择下一个参与者。
                仅陈述所选参与者的姓名，不作任何解释。
                不得选择在 RESPONSE 中提到的参与者。

                仅可从以下参与者中选择：
                - {{{ReviewerName}}}
                - {{{WriterName}}}

                选择下一个参与者时必须始终遵循以下规则：
                - 如果 RESPONSE 是用户输入，则轮到 {{{ReviewerName}}}。
                - 如果 RESPONSE 来自 {{{ReviewerName}}}，则轮到 {{{WriterName}}}。
                - 如果 RESPONSE 来自 {{{WriterName}}}，则轮到 {{{ReviewerName}}}。

                RESPONSE:
                {{$lastmessage}}
                """,
                safeParameterNames: "lastmessage");

        const string TerminationToken = "yes";

        KernelFunction terminationFunction =
            AgentGroupChat.CreatePromptFunctionForStrategy(
                $$$"""
                请审阅 RESPONSE 并判断内容是否已被认为满意。
                如果内容满意，仅回复一个单词，不作任何解释：{{{TerminationToken}}}。
                如果有具体修改建议，说明内容尚不满意。
                如果未提出任何修改建议，则视为满意。

                RESPONSE:
                {{$lastmessage}}
                """,
                safeParameterNames: "lastmessage");

        ChatHistoryTruncationReducer historyReducer = new(1);

        AgentGroupChat chat =
            new(agentReviewer, agentWriter)
            {
                ExecutionSettings = new AgentGroupChatSettings
                {
                    SelectionStrategy =
                        new KernelFunctionSelectionStrategy(selectionFunction, kernel)
                        {
                            // Always start with the editor agent.
                            //始终由编辑代理（editor agent）开始。
                            InitialAgent = agentReviewer,
                            // Save tokens by only including the final response
                            // 通过仅包含最终响应来保存令牌
                            HistoryReducer = historyReducer,
                            // The prompt variable name for the history argument.
                            // 历史参数的提示变量名称。
                            HistoryVariableName = "lastmessage",
                            // Returns the entire result value as a string.
                            // 将整个结果值作为字符串返回。
                            ResultParser = (result) => result.GetValue<string>() ?? agentReviewer.Name
                        },
                    TerminationStrategy =
                        new KernelFunctionTerminationStrategy(terminationFunction, kernel)
                        {
                            // Only evaluate for editor's response
                            // 仅评估编辑器的响应
                            Agents = [agentReviewer],
                            // Save tokens by only including the final response
                            // 通过仅包含最终响应来保存令牌
                            HistoryReducer = historyReducer,
                            // The prompt variable name for the history argument.
                            // 历史参数的提示变量名称。
                            HistoryVariableName = "lastmessage",
                            // Limit total number of turns
                            // 限制总轮数
                            MaximumIterations = 12,
                            // Customer result parser to determine if the response is "yes"
                            // 客户结果解析器，用于确定响应是否为“yes” 
                            ResultParser = (result) => result.GetValue<string>()?.Contains(TerminationToken, StringComparison.OrdinalIgnoreCase) ?? false
                        }
                }
            };

        Console.WriteLine("Ready!");

        bool isComplete = false;
        do
        {
            Console.WriteLine();
            Console.Write("> ");
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }
            input = input.Trim();
            chat.AddChatMessage(new ChatMessageContent(AuthorRole.User, input));
            chat.IsComplete = false;

            try
            {
                string? role = null;
                await foreach (var response in chat.InvokeStreamingAsync())
                {
                    if (role != response.AuthorName)
                    {
                        Console.WriteLine();
                        role = response.AuthorName;
                        Console.Write(response.AuthorName + ">>>");
                    }
                    Console.Write($"{response.Content}");
                }
            }
            catch (HttpOperationException exception)
            {
                Console.WriteLine(exception.Message);
                if (exception.InnerException != null)
                {
                    Console.WriteLine(exception.InnerException.Message);
                    if (exception.InnerException.Data.Count > 0)
                    {
                        Console.WriteLine(JsonSerializer.Serialize(exception.InnerException.Data, new JsonSerializerOptions() { WriteIndented = true }));
                    }
                }
            }
        } while (!isComplete);
    }

    private sealed class ClipboardAccess
    {
        [KernelFunction]
        [Description("复制内容到剪切板.")]
        public static void SetClipboard(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return;
            }

            using Process clipProcess = Process.Start(
                new ProcessStartInfo
                {
                    FileName = "clip",
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                });

            clipProcess.StandardInput.Write(content);
            clipProcess.StandardInput.Close();
        }
    }
}