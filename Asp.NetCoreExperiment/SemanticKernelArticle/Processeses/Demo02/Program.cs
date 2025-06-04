using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Process.Tools;
using PuppeteerSharp;
using System.Reflection;

#pragma warning disable


var key = File.ReadAllText("c:/gpt/key.txt");

Kernel kernel = Kernel.CreateBuilder()
    .AddOpenAIChatCompletion(
        modelId: "gpt-4.1",
        apiKey: key)
    .Build();

// 创建一个将与聊天完成服务交互的进程
ProcessBuilder process = new("ChatBot");
var introStep = process.AddStepFromType<IntroStep>();
var userInputStep = process.AddStepFromType<ChatUserInputStep>();
var responseStep = process.AddStepFromType<ChatBotResponseStep>();

// 定义进程接收外部事件时的行为
process
    .OnInputEvent(ChatBotEvents.StartProcess)
    .SendEventTo(new ProcessFunctionTargetBuilder(introStep));

// 当介绍完成时，通知用户输入步骤
introStep
    .OnFunctionResult()
    .SendEventTo(new ProcessFunctionTargetBuilder(userInputStep));

// 当用户输入步骤发出退出事件时，将其发送到结束步骤
userInputStep
    .OnEvent(ChatBotEvents.Exit)
    .StopProcess();

// 当用户输入步骤发出用户输入事件时，将其发送到助手响应步骤
userInputStep
    .OnEvent(CommonEvents.UserInputReceived)
    .SendEventTo(new ProcessFunctionTargetBuilder(responseStep, parameterName: "userMessage"));

// 当助手响应步骤发出响应时，将其发送到用户输入步骤
responseStep
    .OnEvent(ChatBotEvents.AssistantResponseGenerated)
    .SendEventTo(new ProcessFunctionTargetBuilder(userInputStep));

// 构建进程以获取可以启动的句柄
KernelProcess kernelProcess = process.Build();

// 为进程生成Mermaid图表并打印到控制台
string mermaidGraph = kernelProcess.ToMermaid();
Console.WriteLine($"=== 开始 - '{process.Name}'的Mermaid图表 ===");
Console.WriteLine(mermaidGraph);
Console.WriteLine($"=== 结束 - '{process.Name}'的Mermaid图表 ===");

// 从Mermaid图表生成图像
string generatedImagePath = await MermaidRenderer.GenerateMermaidImageAsync(mermaidGraph, "ChatBotProcess.png");
Console.WriteLine($"图表生成于: {generatedImagePath}");

// 使用初始外部事件启动进程
await using var runningProcess = await kernelProcess.StartAsync(kernel, new KernelProcessEvent() { Id = ChatBotEvents.StartProcess, Data = null });




/// <summary>
/// 进程步骤的最简单实现。IntroStep
/// </summary>
class IntroStep : KernelProcessStep
{
    /// <summary>
    /// 向控制台打印介绍消息。
    /// </summary>
    [KernelFunction]
    public void PrintIntroMessage()
    {
        System.Console.WriteLine("Welcome to Processes in Semantic Kernel.\n");
    }
}

/// <summary>
/// 引出用户输入的步骤。
/// </summary>
class ChatUserInputStep : ScriptedUserInputStep
{
    public override void PopulateUserInputs(UserInputState state)
    {
        state.UserInputs.Add("你好");
        state.UserInputs.Add("最高的山有多高？");
        state.UserInputs.Add("最深的山谷有多深？");
        state.UserInputs.Add("最宽的河有多宽？");
        state.UserInputs.Add("exit");
        state.UserInputs.Add("这段文字会被忽略，因为此时已经满足退出处理的条件。");
    }

    public override async ValueTask GetUserInputAsync(KernelProcessStepContext context)
    {
        var userMessage = this.GetNextUserMessage();

        if (string.Equals(userMessage, "exit", StringComparison.OrdinalIgnoreCase))
        {
            // 满足退出条件，发出退出事件
            await context.EmitEventAsync(new() { Id = ChatBotEvents.Exit, Data = userMessage });
            return;
        }

        // 发出用户输入接收事件
        await context.EmitEventAsync(new() { Id = CommonEvents.UserInputReceived, Data = userMessage });
    }
}

/// <summary>
/// 从前一步骤获取用户输入并从聊天完成服务生成响应的步骤。
/// </summary>
class ChatBotResponseStep : KernelProcessStep<ChatBotState>
{
    public static class ProcessFunctions
    {
        public const string GetChatResponse = nameof(GetChatResponse);
    }

    /// <summary>
    /// 聊天机器人响应步骤的内部状态对象。
    /// </summary>
    internal ChatBotState? _state;

    /// <summary>
    /// ActivateAsync是初始化步骤状态对象的地方。
    /// </summary>
    /// <param name="state">一个<see cref="ChatBotState"/>的实例</param>
    /// <returns>一个<see cref="ValueTask"/></returns>
    public override ValueTask ActivateAsync(KernelProcessStepState<ChatBotState> state)
    {
        _state = state.State;
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// 从聊天完成服务生成响应。
    /// </summary>
    /// <param name="context">当前步骤和进程的上下文。<see cref="KernelProcessStepContext"/></param>
    /// <param name="userMessage">来自前一步骤的用户消息。</param>
    /// <param name="_kernel">一个<see cref="Kernel"/>实例。</param>
    /// <returns></returns>
    [KernelFunction(ProcessFunctions.GetChatResponse)]
    public async Task GetChatResponseAsync(KernelProcessStepContext context, string userMessage, Kernel _kernel)
    {
        _state!.ChatMessages.Add(new(AuthorRole.User, userMessage));
        IChatCompletionService chatService = _kernel.Services.GetRequiredService<IChatCompletionService>();
        ChatMessageContent response = await chatService.GetChatMessageContentAsync(_state.ChatMessages).ConfigureAwait(false);
        if (response == null)
        {
            throw new InvalidOperationException("无法从聊天完成服务获取响应。");
        }

        System.Console.ForegroundColor = ConsoleColor.Yellow;
        System.Console.WriteLine($"ASSISTANT: {response.Content}");
        System.Console.ResetColor();

        // 使用响应更新状态
        _state.ChatMessages.Add(response);

        // 发出事件：助手响应
        await context.EmitEventAsync(new KernelProcessEvent { Id = ChatBotEvents.AssistantResponseGenerated, Data = response });
    }
}

/// <summary>
/// <see cref="ChatBotResponseStep"/>的状态对象。
/// </summary>
class ChatBotState
{
    internal ChatHistory ChatMessages { get; } = new();
}

/// <summary>
/// 定义聊天机器人进程可以发出的事件的类。这不是必需的，
/// 但用于确保事件名称的一致性。
/// </summary>
class ChatBotEvents
{
    public const string StartProcess = "startProcess";
    public const string IntroComplete = "introComplete";
    public const string AssistantResponseGenerated = "assistantResponseGenerated";
    public const string Exit = "exit";
}


public class ScriptedUserInputStep : KernelProcessStep<UserInputState>
{
    public static class ProcessStepFunctions
    {
        public const string GetUserInput = nameof(GetUserInput);
    }

    protected bool SuppressOutput { get; init; }

    /// <summary>
    /// 用户输入步骤的状态对象。此对象保存用户输入和当前输入索引。
    /// </summary>
    private UserInputState? _state;

    /// <summary>
    /// 用户可以重写的方法，用于填充自定义用户消息
    /// </summary>
    /// <param name="state">步骤的初始化状态对象。</param>
    public virtual void PopulateUserInputs(UserInputState state)
    {
        return;
    }

    /// <summary>
    /// 通过初始化状态对象激活用户输入步骤。此方法在进程启动时调用，
    /// 并在调用任何KernelFunctions之前调用。
    /// </summary>
    /// <param name="state">步骤的状态对象。</param>
    /// <returns>一个<see cref="ValueTask"/></returns>
    public override ValueTask ActivateAsync(KernelProcessStepState<UserInputState> state)
    {
        _state = state.State;

        PopulateUserInputs(_state!);

        return ValueTask.CompletedTask;
    }

    internal string GetNextUserMessage()
    {
        if (_state != null && _state.CurrentInputIndex >= 0 && _state.CurrentInputIndex <this._state.UserInputs.Count)
        {
            var userMessage = this._state!.UserInputs[_state.CurrentInputIndex];
            _state.CurrentInputIndex++;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"USER: {userMessage}");
            Console.ResetColor();

            return userMessage;
        }

        Console.WriteLine("SCRIPTED_USER_INPUT: 没有更多预设的用户消息，返回空字符串作为用户消息");
        return string.Empty;
    }

    /// <summary>
    /// 获取用户输入。
    /// 可以重写以自定义要发出的输出事件
    /// </summary>
    /// <param name="context">一个<see cref="KernelProcessStepContext"/>实例，
    /// 可用于从KernelFunction内部发出事件。</param>
    /// <returns>一个<see cref="ValueTask"/></returns>
    [KernelFunction(ProcessStepFunctions.GetUserInput)]
    public virtual async ValueTask GetUserInputAsync(KernelProcessStepContext context)
    {
        var userMessage = this.GetNextUserMessage();
        // 发出用户输入
        if (string.IsNullOrEmpty(userMessage))
        {
            await context.EmitEventAsync(new() { Id = CommonEvents.Exit });
            return;
        }

        await context.EmitEventAsync(new() { Id = CommonEvents.UserInputReceived, Data = userMessage });
    }
}

/// <summary>
/// <see cref="ScriptedUserInputStep"/>的状态对象
/// </summary>
public record UserInputState
{
    public List<string> UserInputs { get; init; } = [];

    public int CurrentInputIndex { get; set; } = 0;
}


static class CommonEvents
{
    public const string UserInputReceived = nameof(UserInputReceived);
    public const string CompletionResponseGenerated = nameof(CompletionResponseGenerated);
    public const string WelcomeDone = nameof(WelcomeDone);
    public const string AStepDone = nameof(AStepDone);
    public const string BStepDone = nameof(BStepDone);
    public const string CStepDone = nameof(CStepDone);
    public const string StartARequested = nameof(StartARequested);
    public const string StartBRequested = nameof(StartBRequested);
    public const string ExitRequested = nameof(ExitRequested);
    public const string StartProcess = nameof(StartProcess);
    public static readonly string Exit = nameof(Exit);
}


public static class MermaidRenderer
{
    /// <summary>
    /// 从提供的Mermaid代码生成Mermaid图表图像。
    /// </summary>
    /// <param name="mermaidCode"></param>
    /// <param name="filenameOrPath"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public static async Task<string> GenerateMermaidImageAsync(string mermaidCode, string filenameOrPath)
    {
        // 确保文件名具有正确的.png扩展名
        if (!filenameOrPath.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("文件名必须具有.png扩展名。", nameof(filenameOrPath));
        }

        string outputFilePath;

        // 检查用户是否提供了绝对路径
        if (Path.IsPathRooted(filenameOrPath))
        {
            // 使用提供的绝对路径
            outputFilePath = filenameOrPath;

            // 确保目录存在
            string directoryPath = Path.GetDirectoryName(outputFilePath)
                ?? throw new InvalidOperationException("无法确定目录路径。");
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException($"目录'{directoryPath}'不存在。");
            }
        }
        else
        {
            // 对相对路径使用程序集的目录
            string? assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (assemblyPath == null)
            {
                throw new InvalidOperationException("无法确定程序集路径。");
            }

            string outputPath = Path.Combine(assemblyPath, "output");
            Directory.CreateDirectory(outputPath); // 确保输出目录存在
            outputFilePath = Path.Combine(outputPath, filenameOrPath);
        }

        // 如果尚未安装Chromium，则下载它
        BrowserFetcher browserFetcher = new();
        browserFetcher.Browser = SupportedBrowser.Chrome;
        await browserFetcher.DownloadAsync();

        // 使用Mermaid.js CDN定义HTML模板
        string htmlContent = $@"
        <html>
            <head>
                <style>
                    body {{
                        display: flex;
                        align-items: center;
                        justify-content: center;
                        margin: 0;
                        height: 100vh;
                    }}
                </style>
                <script type=""module"">
                    import mermaid from 'https://cdn.jsdelivr.net/npm/mermaid@10/dist/mermaid.esm.min.mjs';
                    mermaid.initialize({{ startOnLoad: true,
                       fontSize: 24,
                        securityLevel: 'loose',
                        flowchart: {{
                            htmlLabels: true,
                            curve: 'basis',
                            useMaxWidth: false,
                            diagramPadding: 50,
                            nodeSpacing: 90,
                            rankSpacing: 120
                        }}
                   }});
                </script>
            </head>
            <body>
                <div class=""mermaid"">
                    {mermaidCode}
                </div>
            </body>
        </html>";

        // 创建包含Mermaid代码的临时HTML文件
        string tempHtmlFile = Path.Combine(Path.GetTempPath(), "mermaid_temp.html");
        try
        {
            await File.WriteAllTextAsync(tempHtmlFile, htmlContent);
            //File.WriteAllText(Path.GetFileName(tempHtmlFile), htmlContent);
            // 使用无头浏览器启动Puppeteer-Sharp来渲染Mermaid图表
            using (var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true }))
            using (var page = await browser.NewPageAsync())
            {
                // 设置页面视口大小为更大的尺寸
                await page.SetViewportAsync(new ViewPortOptions
                {
                    Width = 1600,
                    Height = 1200
                });
                await page.GoToAsync($"file://{tempHtmlFile}");
                await page.WaitForSelectorAsync(".mermaid"); // 等待Mermaid渲染

                await page.ScreenshotAsync(outputFilePath, new ScreenshotOptions { FullPage = true });
            }
        }
        catch (IOException ex)
        {
            throw new IOException("访问文件时发生错误。", ex);
        }
        catch (Exception ex) // 捕获可能发生的任何其他异常  
        {
            throw new InvalidOperationException(
                "Mermaid图表渲染过程中发生意外错误。", ex);
        }
        finally
        {
            // 清理临时HTML文件  
            if (File.Exists(tempHtmlFile))
            {
                File.Delete(tempHtmlFile);
            }
        }

        return outputFilePath;
    }
}