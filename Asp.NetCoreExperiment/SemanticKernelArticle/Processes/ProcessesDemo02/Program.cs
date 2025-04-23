using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel;
using Microsoft.Extensions.DependencyInjection;

#pragma warning disable SKEXP0080 

var step01 = new Step01_Processes();
await step01.UseSimpleProcessAsync();

public class Step01_Processes
{
    /// <summary>
    /// 演示如何创建一个具有多个步骤的简单流程，接受用户输入，与聊天完成服务交互，并演示流程中的循环。
    /// </summary>
    /// <returns>A <see cref="Task"/></returns>

    public async Task UseSimpleProcessAsync()
    {
        // Create a kernel with a chat completion service
        //翻译上面的这行
        //使用聊天完成服务创建一个内核
        Kernel kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
                modelId: "gpt-4o",
                apiKey: File.ReadAllText("c:/gpt/key.txt"))
            .Build();

        // Create a process that will interact with the chat completion service
        // //翻译上面的这行
        //创建一个将与聊天完成服务交互的流程
        ProcessBuilder process = new("ChatBot");
        var introStep = process.AddStepFromType<IntroStep>();
        var userInputStep = process.AddStepFromType<ChatUserInputStep>();
        var responseStep = process.AddStepFromType<ChatBotResponseStep>();

          //定义流程接收外部事件时的行为
        process
            .OnInputEvent(ChatBotEvents.StartProcess)
            .SendEventTo(new ProcessFunctionTargetBuilder(introStep));


        //当介绍完成时，通知userInput步骤
        introStep
            .OnFunctionResult()
            .SendEventTo(new ProcessFunctionTargetBuilder(userInputStep));

        //当userInput步骤发出退出事件时，将其发送到结束步骤
        userInputStep
            .OnEvent(ChatBotEvents.Exit)
            .StopProcess();

        //当userInput步骤发出用户输入事件时，将其发送到assistantResponse步骤
        userInputStep
            .OnEvent(CommonEvents.UserInputReceived)
            .SendEventTo(new ProcessFunctionTargetBuilder(responseStep, parameterName: "userMessage"));

        //当assistantResponse步骤发出响应时，将其发送到userInput步骤
        responseStep
            .OnEvent(ChatBotEvents.AssistantResponseGenerated)
            .SendEventTo(new ProcessFunctionTargetBuilder(userInputStep));

        //构建流程以获得可以启动的句柄
        KernelProcess kernelProcess = process.Build();

       //使用初始外部事件启动流程
        using var runningProcess = await kernelProcess.StartAsync(kernel, new KernelProcessEvent() { Id = ChatBotEvents.StartProcess, Data = null });
    }

    /// <summary>
    /// 流程步骤的最简单实现。IntroStep
    /// </summary>
    private sealed class IntroStep : KernelProcessStep
    {
        /// <summary>
        /// 将介绍消息打印到控制台。
        /// </summary>
        [KernelFunction]
        public void PrintIntroMessage()
        {
            System.Console.WriteLine("欢迎来到语义内核中的进程。\n");
        }
    }

    /// <summary>
    /// 引出用户输入的步骤。
    /// </summary>
    private sealed class ChatUserInputStep : ScriptedUserInputStep
    {
        public override void PopulateUserInputs(UserInputState state)
        {
            state.UserInputs.Add("你好");
            state.UserInputs.Add("最高的山有多高？");
            state.UserInputs.Add("最低谷有多低？");
            state.UserInputs.Add("最宽的河流有多宽？");
            state.UserInputs.Add("退出");
            state.UserInputs.Add("此文本将被忽略，因为此时退出流程条件已经满足。");
        }

        public override async ValueTask GetUserInputAsync(KernelProcessStepContext context)
        {
            var userMessage = this.GetNextUserMessage();

            if (string.Equals(userMessage, "退出", StringComparison.OrdinalIgnoreCase))
            {
                // 满足退出条件，发出退出事件
                await context.EmitEventAsync(new() { Id = ChatBotEvents.Exit, Data = userMessage });
                return;
            }

            // 发出 userInputReceived 事件
            await context.EmitEventAsync(new() { Id = CommonEvents.UserInputReceived, Data = userMessage });
        }
    }

    /// <summary>
    ///此步骤获取上一步中的用户输入并从聊天完成服务生成响应。
    /// </summary>
    private sealed class ChatBotResponseStep : KernelProcessStep<ChatBotState>
    {
        public static class Functions
        {
            public const string GetChatResponse = nameof(GetChatResponse);
        }

        /// <summary>
        /// 聊天机器人响应步骤的内部状态对象。
        /// </summary>
        internal ChatBotState? _state;

        /// <summary>
        /// ActivateAsync 是初始化步骤的状态对象的地方。
        /// </summary>
        /// <param name="state">一个例子 <see cref="ChatBotState"/></param>
        /// <returns>A <see cref="ValueTask"/></returns>
        public override ValueTask ActivateAsync(KernelProcessStepState<ChatBotState> state)
        {
            _state = state.State;
            return ValueTask.CompletedTask;
        }

        /// <summary>
        /// 从聊天完成服务生成响应。
        /// </summary>
        /// <param name="context">当前步骤和流程的上下文。 <see cref="KernelProcessStepContext"/></param>
        /// <param name="userMessage">来自上一步的用户消息。</param>
        /// <param name="_kernel">一个实例。<see cref="Kernel"/> </param>
        /// <returns></returns>
        [KernelFunction(Functions.GetChatResponse)]
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

            // 发出事件：assistantResponse
            await context.EmitEventAsync(new KernelProcessEvent { Id = ChatBotEvents.AssistantResponseGenerated, Data = response });
        }
    }

    /// <summary>
    /// see cref="ChatBotResponseStep"/> 的状态对象。
    /// </summary>
    private sealed class ChatBotState
    {
        internal ChatHistory ChatMessages { get; } = new();
    }

    /// <summary>
    /// 定义聊天机器人进程可以发出的事件的类。这不是必需的，但用于确保事件名称一致。
    /// </summary>
    private static class ChatBotEvents
    {
        public const string StartProcess = "startProcess";
        public const string IntroComplete = "introComplete";
        public const string AssistantResponseGenerated = "assistantResponseGenerated";
        public const string Exit = "exit";
    }
}

public class ScriptedUserInputStep : KernelProcessStep<UserInputState>
{
    public static class Functions
    {
        public const string GetUserInput = nameof(GetUserInput);
    }

    protected bool SuppressOutput { get; init; }

    /// <summary>
    /// 用户输入步骤的状态对象。此对象保存用户输入和当前输入索引。
    /// </summary>
    private UserInputState? _state;

    /// <summary>
    /// 该方法将被用户覆盖，以填充自定义用户消息
    /// </summary>
    /// <param name="state">该步骤的初始化状态对象。</param>
    public virtual void PopulateUserInputs(UserInputState state)
    {
        return;
    }

    /// <summary>
    /// 通过初始化状态对象激活用户输入步骤。此方法在进程启动时且在调用任何 KernelFunctions 之前调用。
    /// </summary>
    /// <param name="state">该步骤的状态对象。</param>
    /// <returns>A <see cref="ValueTask"/></returns>
    public override ValueTask ActivateAsync(KernelProcessStepState<UserInputState> state)
    {
        _state = state.State;

        PopulateUserInputs(_state!);

        return ValueTask.CompletedTask;
    }

    internal string GetNextUserMessage()
    {
        if (_state != null && _state.CurrentInputIndex >= 0 && _state.CurrentInputIndex < this._state.UserInputs.Count)
        {
            var userMessage = this._state!.UserInputs[_state.CurrentInputIndex];
            _state.CurrentInputIndex++;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"USER: {userMessage}");
            Console.ResetColor();

            return userMessage;
        }

        Console.WriteLine("SCRIPTED_USER_INPUT：没有定义更多脚本用户消息，返回空字符串作为用户消息");
        return string.Empty;
    }

    /// <summary>
    /// 获取用户输入。可以重写以自定义要发出的输出事件
    /// </summary>
    /// <param name="context"><see cref="KernelProcessStepContext"/> 的实例，可用于从 KernelFunction 内部发出事件。</param>
    /// <returns>A <see cref="ValueTask"/></returns>
    [KernelFunction(Functions.GetUserInput)]
    public virtual async ValueTask GetUserInputAsync(KernelProcessStepContext context)
    {
        var userMessage = this.GetNextUserMessage();
        //发出用户输入
        if (string.IsNullOrEmpty(userMessage))
        {
            await context.EmitEventAsync(new() { Id = CommonEvents.Exit });
            return;
        }

        await context.EmitEventAsync(new() { Id = CommonEvents.UserInputReceived, Data = userMessage });
    }
}

/// <summary>
/// <see cref="ScriptedUserInputStep"/> 的状态对象
/// </summary>
public record UserInputState
{
    public List<string> UserInputs { get; init; } = [];

    public int CurrentInputIndex { get; set; } = 0;
}
public static class CommonEvents
{
    public static readonly string UserInputReceived = nameof(UserInputReceived);
    public static readonly string UserInputComplete = nameof(UserInputComplete);
    public static readonly string AssistantResponseGenerated = nameof(AssistantResponseGenerated);
    public static readonly string Exit = nameof(Exit);
}