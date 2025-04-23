using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.ComponentModel;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
#pragma warning disable SKEXP0080
var step2a = new Step02a_AccountOpening();
await step2a.UseAccountOpeningProcessFailureDueToCreditScoreFailureAsync();
public class Step02a_AccountOpening
{
    // 目标 Open AI 服务
    protected bool ForceOpenAI => true;

    private KernelProcess SetupAccountOpeningProcess<TUserInputStep>() where TUserInputStep : ScriptedUserInputStep
    {
        ProcessBuilder process = new("AccountOpeningProcess");
        var newCustomerFormStep = process.AddStepFromType<CompleteNewCustomerFormStep>();
        var userInputStep = process.AddStepFromType<TUserInputStep>();
        var displayAssistantMessageStep = process.AddStepFromType<DisplayAssistantMessageStep>();
        var customerCreditCheckStep = process.AddStepFromType<CreditScoreCheckStep>();
        var fraudDetectionCheckStep = process.AddStepFromType<FraudDetectionStep>();
        var mailServiceStep = process.AddStepFromType<MailServiceStep>();
        var coreSystemRecordCreationStep = process.AddStepFromType<NewAccountStep>();
        var marketingRecordCreationStep = process.AddStepFromType<NewMarketingEntryStep>();
        var crmRecordStep = process.AddStepFromType<CRMRecordCreationStep>();
        var welcomePacketStep = process.AddStepFromType<WelcomePacketStep>();

        process.OnInputEvent(AccountOpeningEvents.StartProcess)
            .SendEventTo(new ProcessFunctionTargetBuilder(newCustomerFormStep, CompleteNewCustomerFormStep.Functions.NewAccountWelcome));

        // 当欢迎消息生成时，发送消息到 displayAssistantMessageStep
        newCustomerFormStep
            .OnEvent(AccountOpeningEvents.NewCustomerFormWelcomeMessageComplete)
            .SendEventTo(new ProcessFunctionTargetBuilder(displayAssistantMessageStep, DisplayAssistantMessageStep.Functions.DisplayAssistantMessage));

        // 当 userInputStep 发出用户输入事件时，将其发送到 newCustomerFormStep
        // 当步骤有多个公共函数时，函数名称是必要的，例如 CompleteNewCustomerFormStep: NewAccountWelcome 和 NewAccountProcessUserInfo
        userInputStep
            .OnEvent(CommonEvents.UserInputReceived)
            .SendEventTo(new ProcessFunctionTargetBuilder(newCustomerFormStep, CompleteNewCustomerFormStep.Functions.NewAccountProcessUserInfo, "userMessage"));

        userInputStep
            .OnEvent(CommonEvents.Exit)
            .StopProcess();

        // 当 newCustomerFormStep 发出需要更多详细信息时，发送消息到 displayAssistantMessageStep
        newCustomerFormStep
            .OnEvent(AccountOpeningEvents.NewCustomerFormNeedsMoreDetails)
            .SendEventTo(new ProcessFunctionTargetBuilder(displayAssistantMessageStep, DisplayAssistantMessageStep.Functions.DisplayAssistantMessage));

        // 在显示任何助手消息后，预期用户输入，下一步是 userInputStep
        displayAssistantMessageStep
            .OnEvent(CommonEvents.AssistantResponseGenerated)
            .SendEventTo(new ProcessFunctionTargetBuilder(userInputStep, ScriptedUserInputStep.Functions.GetUserInput));

        // 当 newCustomerFormStep 完成时...
        newCustomerFormStep
            .OnEvent(AccountOpeningEvents.NewCustomerFormCompleted)
            // 信息传递到核心系统记录创建步骤
            .SendEventTo(new ProcessFunctionTargetBuilder(customerCreditCheckStep, functionName: CreditScoreCheckStep.Functions.DetermineCreditScore, parameterName: "customerDetails"))
            // 信息传递到欺诈检测步骤进行验证
            .SendEventTo(new ProcessFunctionTargetBuilder(fraudDetectionCheckStep, functionName: FraudDetectionStep.Functions.FraudDetectionCheck, parameterName: "customerDetails"))
            // 信息传递到核心系统记录创建步骤
            .SendEventTo(new ProcessFunctionTargetBuilder(coreSystemRecordCreationStep, functionName: NewAccountStep.Functions.CreateNewAccount, parameterName: "customerDetails"));

        // 当 newCustomerFormStep 完成时，用户与用户的交互记录传递到核心系统记录创建步骤
        newCustomerFormStep
            .OnEvent(AccountOpeningEvents.CustomerInteractionTranscriptReady)
            .SendEventTo(new ProcessFunctionTargetBuilder(coreSystemRecordCreationStep, functionName: NewAccountStep.Functions.CreateNewAccount, parameterName: "interactionTranscript"));

        // 当 creditScoreCheckStep 结果为拒绝时，信息传递到 mailServiceStep 通知用户申请状态和原因
        customerCreditCheckStep
            .OnEvent(AccountOpeningEvents.CreditScoreCheckRejected)
            .SendEventTo(new ProcessFunctionTargetBuilder(mailServiceStep, functionName: MailServiceStep.Functions.SendMailToUserWithDetails, parameterName: "message"));

        // 当 creditScoreCheckStep 结果为批准时，信息传递到欺诈检测步骤以启动此步骤
        customerCreditCheckStep
            .OnEvent(AccountOpeningEvents.CreditScoreCheckApproved)
            .SendEventTo(new ProcessFunctionTargetBuilder(fraudDetectionCheckStep, functionName: FraudDetectionStep.Functions.FraudDetectionCheck, parameterName: "previousCheckSucceeded"));

        // 当 fraudDetectionCheckStep 失败时，信息传递到 mailServiceStep 通知用户申请状态和原因
        fraudDetectionCheckStep
            .OnEvent(AccountOpeningEvents.FraudDetectionCheckFailed)
            .SendEventTo(new ProcessFunctionTargetBuilder(mailServiceStep, functionName: MailServiceStep.Functions.SendMailToUserWithDetails, parameterName: "message"));

        // 当 fraudDetectionCheckStep 通过时，信息传递到核心系统记录创建步骤以启动此步骤
        fraudDetectionCheckStep
            .OnEvent(AccountOpeningEvents.FraudDetectionCheckPassed)
            .SendEventTo(new ProcessFunctionTargetBuilder(coreSystemRecordCreationStep, functionName: NewAccountStep.Functions.CreateNewAccount, parameterName: "previousCheckSucceeded"));

        // 当 coreSystemRecordCreationStep 成功创建新 accountId 时，它将通过 marketingRecordCreationStep 触发创建新营销条目
        coreSystemRecordCreationStep
            .OnEvent(AccountOpeningEvents.NewMarketingRecordInfoReady)
            .SendEventTo(new ProcessFunctionTargetBuilder(marketingRecordCreationStep, functionName: NewMarketingEntryStep.Functions.CreateNewMarketingEntry, parameterName: "userDetails"));

        // 当 coreSystemRecordCreationStep 成功创建新 accountId 时，它将通过 crmRecordStep 触发创建新 CRM 条目
        coreSystemRecordCreationStep
            .OnEvent(AccountOpeningEvents.CRMRecordInfoReady)
            .SendEventTo(new ProcessFunctionTargetBuilder(crmRecordStep, functionName: CRMRecordCreationStep.Functions.CreateCRMEntry, parameterName: "userInteractionDetails"));

        // 当 coreSystemRecordCreationStep 成功创建新 accountId 时，它将传递 account 信息详细信息到 welcomePacketStep
        coreSystemRecordCreationStep
            .OnEvent(AccountOpeningEvents.NewAccountDetailsReady)
            .SendEventTo(new ProcessFunctionTargetBuilder(welcomePacketStep, parameterName: "accountDetails"));

        // 当 marketingRecordCreationStep 成功创建新营销条目时，它将通知 welcomePacketStep 它已准备好
        marketingRecordCreationStep
            .OnEvent(AccountOpeningEvents.NewMarketingEntryCreated)
            .SendEventTo(new ProcessFunctionTargetBuilder(welcomePacketStep, parameterName: "marketingEntryCreated"));

        // 当 crmRecordStep 成功创建新 CRM 条目时，它将通知 welcomePacketStep 它已准备好
        crmRecordStep
            .OnEvent(AccountOpeningEvents.CRMRecordInfoEntryCreated)
            .SendEventTo(new ProcessFunctionTargetBuilder(welcomePacketStep, parameterName: "crmRecordCreated"));

        // 在 crmRecord 和 marketing 创建后，创建欢迎包，然后通过 mailServiceStep 向用户发送信息
        welcomePacketStep
            .OnEvent(AccountOpeningEvents.WelcomePacketCreated)
            .SendEventTo(new ProcessFunctionTargetBuilder(mailServiceStep, functionName: MailServiceStep.Functions.SendMailToUserWithDetails, parameterName: "message"));

        // 所有可能的路径最终都会通过 mailServiceStep 完成通知用户关于账户创建决定
        mailServiceStep
            .OnEvent(AccountOpeningEvents.MailServiceSent)
            .StopProcess();

        KernelProcess kernelProcess = process.Build();

        return kernelProcess;
    }

    /// <summary>
    /// 此测试使用特定的 userId 和 DOB，使信用评分和欺诈检测通过
    /// </summary>
    public async Task UseAccountOpeningProcessSuccessfulInteractionAsync()
    {
        Kernel kernel = CreateKernelWithChatCompletion();
        KernelProcess kernelProcess = SetupAccountOpeningProcess<UserInputSuccessfulInteractionStep>();
        using var runningProcess = await kernelProcess.StartAsync(kernel, new KernelProcessEvent() { Id = AccountOpeningEvents.StartProcess, Data = null });
    }

    /// <summary>
    /// 此测试使用特定的 DOB，使信用评分失败
    /// </summary>
    public async Task UseAccountOpeningProcessFailureDueToCreditScoreFailureAsync()
    {
        Kernel kernel = CreateKernelWithChatCompletion();
        KernelProcess kernelProcess = SetupAccountOpeningProcess<UserInputCreditScoreFailureInteractionStep>();
        using var runningProcess = await kernelProcess.StartAsync(kernel, new KernelProcessEvent() { Id = AccountOpeningEvents.StartProcess, Data = null });
    }

    private Kernel CreateKernelWithChatCompletion()
    {
        Kernel kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
                modelId: "gpt-4o",
                apiKey: File.ReadAllText("c:/gpt/key.txt"))
            .Build();
        return kernel;
    }

    /// <summary>
    /// 此测试使用特定的 userId，使欺诈检测失败
    /// </summary>
    public async Task UseAccountOpeningProcessFailureDueToFraudFailureAsync()
    {
        Kernel kernel = CreateKernelWithChatCompletion();
        KernelProcess kernelProcess = SetupAccountOpeningProcess<UserInputFraudFailureInteractionStep>();
        using var runningProcess = await kernelProcess.StartAsync(kernel, new KernelProcessEvent() { Id = AccountOpeningEvents.StartProcess, Data = null });
    }
}

#region Process
public static class NewAccountCreationProcess
{
    public static ProcessBuilder CreateProcess()
    {
        ProcessBuilder process = new("AccountCreationProcess");

        var coreSystemRecordCreationStep = process.AddStepFromType<NewAccountStep>();
        var marketingRecordCreationStep = process.AddStepFromType<NewMarketingEntryStep>();
        var crmRecordStep = process.AddStepFromType<CRMRecordCreationStep>();
        var welcomePacketStep = process.AddStepFromType<WelcomePacketStep>();

        // 当 newCustomerForm 完成时...
        process
            .OnInputEvent(AccountOpeningEvents.NewCustomerFormCompleted)
            // 信息传递到核心系统记录创建步骤
            .SendEventTo(new ProcessFunctionTargetBuilder(coreSystemRecordCreationStep, functionName: NewAccountStep.Functions.CreateNewAccount, parameterName: "customerDetails"));

        // 当 newCustomerForm 完成时，用户与用户的交互记录传递到核心系统记录创建步骤
        process
            .OnInputEvent(AccountOpeningEvents.CustomerInteractionTranscriptReady)
            .SendEventTo(new ProcessFunctionTargetBuilder(coreSystemRecordCreationStep, functionName: NewAccountStep.Functions.CreateNewAccount, parameterName: "interactionTranscript"));

        // 当欺诈检测步骤通过时，信息传递到核心系统记录创建步骤以启动此步骤
        process
            .OnInputEvent(AccountOpeningEvents.NewAccountVerificationCheckPassed)
            .SendEventTo(new ProcessFunctionTargetBuilder(coreSystemRecordCreationStep, functionName: NewAccountStep.Functions.CreateNewAccount, parameterName: "previousCheckSucceeded"));

        // 当 coreSystemRecordCreationStep 成功创建新 accountId 时，它将通过 marketingRecordCreationStep 触发创建新营销条目
        coreSystemRecordCreationStep
            .OnEvent(AccountOpeningEvents.NewMarketingRecordInfoReady)
            .SendEventTo(new ProcessFunctionTargetBuilder(marketingRecordCreationStep, functionName: NewMarketingEntryStep.Functions.CreateNewMarketingEntry, parameterName: "userDetails"));

        // 当 coreSystemRecordCreationStep 成功创建新 accountId 时，它将通过 crmRecordStep 触发创建新 CRM 条目
        coreSystemRecordCreationStep
            .OnEvent(AccountOpeningEvents.CRMRecordInfoReady)
            .SendEventTo(new ProcessFunctionTargetBuilder(crmRecordStep, functionName: CRMRecordCreationStep.Functions.CreateCRMEntry, parameterName: "userInteractionDetails"));

        // 当 coreSystemRecordCreationStep 成功创建新 accountId 时，它将传递 account 信息详细信息到 welcomePacketStep
        coreSystemRecordCreationStep
            .OnEvent(AccountOpeningEvents.NewAccountDetailsReady)
            .SendEventTo(new ProcessFunctionTargetBuilder(welcomePacketStep, parameterName: "accountDetails"));

        // 当 marketingRecordCreationStep 成功创建新营销条目时，它将通知 welcomePacketStep 它已准备好
        marketingRecordCreationStep
            .OnEvent(AccountOpeningEvents.NewMarketingEntryCreated)
            .SendEventTo(new ProcessFunctionTargetBuilder(welcomePacketStep, parameterName: "marketingEntryCreated"));

        // 当 crmRecordStep 成功创建新 CRM 条目时，它将通知 welcomePacketStep 它已准备好
        crmRecordStep
            .OnEvent(AccountOpeningEvents.CRMRecordInfoEntryCreated)
            .SendEventTo(new ProcessFunctionTargetBuilder(welcomePacketStep, parameterName: "crmRecordCreated"));

        return process;
    }
}
public static class NewAccountVerificationProcess
{
    public static ProcessBuilder CreateProcess()
    {
        ProcessBuilder process = new("AccountVerificationProcess");

        var customerCreditCheckStep = process.AddStepFromType<CreditScoreCheckStep>();
        var fraudDetectionCheckStep = process.AddStepFromType<FraudDetectionStep>();

        // 当 newCustomerForm 完成时...
        process
            .OnInputEvent(AccountOpeningEvents.NewCustomerFormCompleted)
            // 信息传递到核心系统记录创建步骤
            .SendEventTo(new ProcessFunctionTargetBuilder(customerCreditCheckStep, functionName: CreditScoreCheckStep.Functions.DetermineCreditScore, parameterName: "customerDetails"))
            // 信息传递到欺诈检测步骤进行验证
            .SendEventTo(new ProcessFunctionTargetBuilder(fraudDetectionCheckStep, functionName: FraudDetectionStep.Functions.FraudDetectionCheck, parameterName: "customerDetails"));

        // 当 creditScoreCheckStep 结果为批准时，信息传递到欺诈检测步骤以启动此步骤
        customerCreditCheckStep
            .OnEvent(AccountOpeningEvents.CreditScoreCheckApproved)
            .SendEventTo(new ProcessFunctionTargetBuilder(fraudDetectionCheckStep, functionName: FraudDetectionStep.Functions.FraudDetectionCheck, parameterName: "previousCheckSucceeded"));

        return process;
    }
}

#endregion

#region 步骤
public static class CommonEvents
{
    public static readonly string UserInputReceived = nameof(UserInputReceived);
    public static readonly string UserInputComplete = nameof(UserInputComplete);
    public static readonly string AssistantResponseGenerated = nameof(AssistantResponseGenerated);
    public static readonly string Exit = nameof(Exit);
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
            Console.WriteLine($"用户: {userMessage}");
            Console.ResetColor();

            return userMessage;
        }

        Console.WriteLine("脚本用户输入: 没有更多的脚本用户消息定义，返回空字符串作为用户消息");
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
        // 发出用户输入
        if (string.IsNullOrEmpty(userMessage))
        {
            await context.EmitEventAsync(new() { Id = CommonEvents.Exit });
            return;
        }

        await context.EmitEventAsync(new() { Id = CommonEvents.UserInputReceived, Data = userMessage });
    }
}
public class DisplayAssistantMessageStep : KernelProcessStep
{
    public static class Functions
    {
        public const string DisplayAssistantMessage = nameof(DisplayAssistantMessage);
    }

    [KernelFunction(Functions.DisplayAssistantMessage)]
    public async ValueTask DisplayAssistantMessageAsync(KernelProcessStepContext context, string assistantMessage)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"助手: {assistantMessage}\n");
        Console.ResetColor();

        // 发出助手消息生成事件
        await context.EmitEventAsync(new() { Id = CommonEvents.AssistantResponseGenerated, Data = assistantMessage });
    }
}
public record UserInputState
{
    public List<string> UserInputs { get; init; } = [];

    public int CurrentInputIndex { get; set; } = 0;
}
public sealed class UserInputCreditScoreFailureInteractionStep : ScriptedUserInputStep
{
    public override void PopulateUserInputs(UserInputState state)
    {
        state.UserInputs.Add("我想开一个账户");
        state.UserInputs.Add("我的名字是 John Contoso，出生日期 01/01/1990");
        state.UserInputs.Add("我住在华盛顿，我的电话号码是 222-222-1234");
        state.UserInputs.Add("我的用户 ID 是 987-654-3210");
        state.UserInputs.Add("我的电子邮件是 john.contoso@contoso.com，你还需要什么？");
    }
}
public sealed class UserInputFraudFailureInteractionStep : ScriptedUserInputStep
{
    public override void PopulateUserInputs(UserInputState state)
    {
        state.UserInputs.Add("我想开一个账户");
        state.UserInputs.Add("我的名字是 John Contoso，出生日期 02/03/1990");
        state.UserInputs.Add("我住在华盛顿，我的电话号码是 222-222-1234");
        state.UserInputs.Add("我的用户 ID 是 123-456-7890");
        state.UserInputs.Add("我的电子邮件是 john.contoso@contoso.com，你还需要什么？");
    }
}
public sealed class UserInputSuccessfulInteractionStep : ScriptedUserInputStep
{
    public override void PopulateUserInputs(UserInputState state)
    {
        state.UserInputs.Add("我想开一个账户");
        state.UserInputs.Add("我的名字是 John Contoso，出生日期 02/03/1990");
        state.UserInputs.Add("我住在华盛顿，我的电话号码是 222-222-1234");
        state.UserInputs.Add("我的用户 ID 是 987-654-3210");
        state.UserInputs.Add("我的电子邮件是 john.contoso@contoso.com，你还需要什么？");
    }
}
public class CRMRecordCreationStep : KernelProcessStep
{
    public static class Functions
    {
        public const string CreateCRMEntry = nameof(CreateCRMEntry);
    }

    [KernelFunction(Functions.CreateCRMEntry)]
    public async Task CreateCRMEntryAsync(KernelProcessStepContext context, AccountUserInteractionDetails userInteractionDetails, Kernel _kernel)
    {
        Console.WriteLine($"[CRM 条目创建] 新账户 {userInteractionDetails.AccountId} 已创建");

        // 调用 API 创建新 CRM 条目的占位符
        await context.EmitEventAsync(new() { Id = AccountOpeningEvents.CRMRecordInfoEntryCreated, Data = true });
    }
}
public class CompleteNewCustomerFormStep : KernelProcessStep<NewCustomerFormState>
{
    public static class Functions
    {
        public const string NewAccountProcessUserInfo = nameof(NewAccountProcessUserInfo);
        public const string NewAccountWelcome = nameof(NewAccountWelcome);
    }

    internal NewCustomerFormState? _state;

    internal string _formCompletionSystemPrompt = """
        目标是填写表单所需的所有字段。
        用户可以在一条消息中提供多个字段的信息。
        用户需要填写表单，表单的所有字段都是必需的

        <当前表单状态>
        {{current_form_state}}
        <当前表单状态>

        指导：
        - 如果有缺失的详细信息，请给用户一个有用的消息，帮助填写剩余的字段。
        - 你的目标是帮助用户提供当前表单上的缺失详细信息。
        - 鼓励用户提供剩余的详细信息，如果必要的话提供示例。
        - 值为 '未回答' 的字段需要用户回答。
        - 如果用户没有提供预期格式，请正确格式化电话号码和用户 ID。
        - 如果用户没有在电话号码中使用括号，请添加它们。
        - 对于日期字段，如果日期格式不清楚，请先与用户确认。例如 02/03 03/02 可能是 3 月 2 日或 2 月 3 日。
        """;

    internal string _welcomeMessage = """
        你好，我可以帮助你填写开设新账户所需的信息。
        请提供一些个人信息，如名字和姓氏，以便开始。
        """;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.Never
    };

    public override ValueTask ActivateAsync(KernelProcessStepState<NewCustomerFormState> state)
    {
        _state = state.State;
        return ValueTask.CompletedTask;
    }

    [KernelFunction(Functions.NewAccountWelcome)]
    public async Task NewAccountWelcomeMessageAsync(KernelProcessStepContext context, Kernel _kernel)
    {
        _state?.conversation.Add(new ChatMessageContent { Role = AuthorRole.Assistant, Content = _welcomeMessage });
        await context.EmitEventAsync(new() { Id = AccountOpeningEvents.NewCustomerFormWelcomeMessageComplete, Data = _welcomeMessage });
    }

    private Kernel CreateNewCustomerFormKernel(Kernel _baseKernel)
    {
        // 创建另一个内核，仅使用私有函数来填写新客户表单
        Kernel kernel = new(_baseKernel.Services);
        kernel.ImportPluginFromFunctions("FillForm", [
            KernelFunctionFactory.CreateFromMethod(OnUserProvidedFirstName, functionName: nameof(OnUserProvidedFirstName)),
            KernelFunctionFactory.CreateFromMethod(OnUserProvidedLastName, functionName: nameof(OnUserProvidedLastName)),
            KernelFunctionFactory.CreateFromMethod(OnUserProvidedDOBDetails, functionName: nameof(OnUserProvidedDOBDetails)),
            KernelFunctionFactory.CreateFromMethod(OnUserProvidedStateOfResidence, functionName: nameof(OnUserProvidedStateOfResidence)),
            KernelFunctionFactory.CreateFromMethod(OnUserProvidedPhoneNumber, functionName: nameof(OnUserProvidedPhoneNumber)),
            KernelFunctionFactory.CreateFromMethod(OnUserProvidedUserId, functionName: nameof(OnUserProvidedUserId)),
            KernelFunctionFactory.CreateFromMethod(OnUserProvidedEmailAddress, functionName: nameof(OnUserProvidedEmailAddress)),
        ]);

        return kernel;
    }

    [KernelFunction(Functions.NewAccountProcessUserInfo)]
    public async Task CompleteNewCustomerFormAsync(KernelProcessStepContext context, string userMessage, Kernel _kernel)
    {
        // 记录所有用户交互
        _state?.conversation.Add(new ChatMessageContent { Role = AuthorRole.User, Content = userMessage });

        Kernel kernel = CreateNewCustomerFormKernel(_kernel);

        OpenAIPromptExecutionSettings settings = new()
        {
            ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions,
            Temperature = 0.7,
            MaxTokens = 2048
        };

        ChatHistory chatHistory = new();
        chatHistory.AddSystemMessage(_formCompletionSystemPrompt
            .Replace("{{current_form_state}}", JsonSerializer.Serialize(_state!.newCustomerForm.CopyWithDefaultValues(), _jsonOptions)));
        chatHistory.AddRange(_state.conversation);
        IChatCompletionService chatService = kernel.Services.GetRequiredService<IChatCompletionService>();
        ChatMessageContent response = await chatService.GetChatMessageContentAsync(chatHistory, settings, kernel).ConfigureAwait(false);
        var assistantResponse = "";

        if (response != null)
        {
            assistantResponse = response.Items[0].ToString();
            // 记录所有助手交互
            _state?.conversation.Add(new ChatMessageContent { Role = AuthorRole.Assistant, Content = assistantResponse });
        }

        if (_state?.newCustomerForm != null && _state.newCustomerForm.IsFormCompleted())
        {
            Console.WriteLine($"[新用户表单完成]: {JsonSerializer.Serialize(_state?.newCustomerForm)}");
            // 所有用户信息已收集，继续下一步
            await context.EmitEventAsync(new() { Id = AccountOpeningEvents.NewCustomerFormCompleted, Data = _state?.newCustomerForm, Visibility = KernelProcessEventVisibility.Public });
            await context.EmitEventAsync(new() { Id = AccountOpeningEvents.CustomerInteractionTranscriptReady, Data = _state?.conversation, Visibility = KernelProcessEventVisibility.Public });
            return;
        }

        // 发出事件：NewCustomerFormNeedsMoreDetails
        await context.EmitEventAsync(new() { Id = AccountOpeningEvents.NewCustomerFormNeedsMoreDetails, Data = assistantResponse });
    }

    [Description("用户提供的名字详细信息")]
    private Task OnUserProvidedFirstName(string firstName)
    {
        if (!string.IsNullOrEmpty(firstName) && _state != null)
        {
            _state.newCustomerForm.UserFirstName = firstName;
        }

        return Task.CompletedTask;
    }

    [Description("用户提供的姓氏详细信息")]
    private Task OnUserProvidedLastName(string lastName)
    {
        if (!string.IsNullOrEmpty(lastName) && _state != null)
        {
            _state.newCustomerForm.UserLastName = lastName;
        }

        return Task.CompletedTask;
    }

    [Description("用户提供的居住州详细信息，必须是 2 个字母的大写州缩写格式")]
    private Task OnUserProvidedStateOfResidence(string stateAbbreviation)
    {
        if (!string.IsNullOrEmpty(stateAbbreviation) && _state != null)
        {
            _state.newCustomerForm.UserState = stateAbbreviation;
        }

        return Task.CompletedTask;
    }

    [Description("用户提供的出生日期详细信息，必须是 MM/DD/YYYY 格式")]
    private Task OnUserProvidedDOBDetails(string date)
    {
        if (!string.IsNullOrEmpty(date) && _state != null)
        {
            _state.newCustomerForm.UserDateOfBirth = date;
        }

        return Task.CompletedTask;
    }

    [Description("用户提供的电话号码详细信息，必须是 (\\d{3})-\\d{3}-\\d{4} 格式")]
    private Task OnUserProvidedPhoneNumber(string phoneNumber)
    {
        if (!string.IsNullOrEmpty(phoneNumber) && _state != null)
        {
            _state.newCustomerForm.UserPhoneNumber = phoneNumber;
        }

        return Task.CompletedTask;
    }

    [Description("用户提供的用户 ID 详细信息，必须是 \\d{3}-\\d{3}-\\d{4} 格式")]
    private Task OnUserProvidedUserId(string userId)
    {
        if (!string.IsNullOrEmpty(userId) && _state != null)
        {
            _state.newCustomerForm.UserId = userId;
        }

        return Task.CompletedTask;
    }

    [Description("用户提供的电子邮件地址，必须是有效的电子邮件格式")]
    private Task OnUserProvidedEmailAddress(string emailAddress)
    {
        if (!string.IsNullOrEmpty(emailAddress) && _state != null)
        {
            _state.newCustomerForm.UserEmail = emailAddress;
        }

        return Task.CompletedTask;
    }
}
public class NewCustomerFormState
{
    internal NewCustomerForm newCustomerForm { get; set; } = new();
    internal List<ChatMessageContent> conversation { get; set; } = [];
}

public class CreditScoreCheckStep : KernelProcessStep
{
    public static class Functions
    {
        public const string DetermineCreditScore = nameof(DetermineCreditScore);
    }

    private const int MinCreditScore = 600;

    [KernelFunction(Functions.DetermineCreditScore)]
    public async Task DetermineCreditScoreAsync(KernelProcessStepContext context, NewCustomerForm customerDetails, Kernel _kernel)
    {
        // 调用 API 验证客户详细信息的信用评分的占位符
        var creditScore = customerDetails.UserDateOfBirth == "02/03/1990" ? 700 : 500;

        if (creditScore >= MinCreditScore)
        {
            Console.WriteLine("[信用检查] 信用评分检查通过");
            await context.EmitEventAsync(new() { Id = AccountOpeningEvents.CreditScoreCheckApproved, Data = true });
            return;
        }
        Console.WriteLine("[信用检查] 信用评分检查失败");
        await context.EmitEventAsync(new()
        {
            Id = AccountOpeningEvents.CreditScoreCheckRejected,
            Data = $"我们遗憾地通知您，您的信用评分 {creditScore} 不足以申请 PRIME ABC 类型的账户",
            Visibility = KernelProcessEventVisibility.Public,
        });
    }
}
public class FraudDetectionStep : KernelProcessStep
{
    public static class Functions
    {
        public const string FraudDetectionCheck = nameof(FraudDetectionCheck);
    }

    [KernelFunction(Functions.FraudDetectionCheck)]
    public async Task FraudDetectionCheckAsync(KernelProcessStepContext context, bool previousCheckSucceeded, NewCustomerForm customerDetails, Kernel _kernel)
    {
        // 调用 API 验证用户详细信息的欺诈检测的占位符
        if (customerDetails.UserId == "123-456-7890")
        {
            Console.WriteLine("[欺诈检查] 欺诈检查失败");
            await context.EmitEventAsync(new()
            {
                Id = AccountOpeningEvents.FraudDetectionCheckFailed,
                Data = "我们遗憾地通知您，我们发现您提供的新账户 PRIME ABC 类型的信息存在一些不一致的详细信息。",
                Visibility = KernelProcessEventVisibility.Public,
            });
            return;
        }

        Console.WriteLine("[欺诈检查] 欺诈检查通过");
        await context.EmitEventAsync(new() { Id = AccountOpeningEvents.FraudDetectionCheckPassed, Data = true, Visibility = KernelProcessEventVisibility.Public });
    }
}
public class MailServiceStep : KernelProcessStep
{
    public static class Functions
    {
        public const string SendMailToUserWithDetails = nameof(SendMailToUserWithDetails);
    }

    [KernelFunction(Functions.SendMailToUserWithDetails)]
    public async Task SendMailServiceAsync(KernelProcessStepContext context, string message)
    {
        Console.WriteLine("======== 邮件服务 ======== ");
        Console.WriteLine(message);
        Console.WriteLine("============================== ");

        await context.EmitEventAsync(new() { Id = AccountOpeningEvents.MailServiceSent, Data = message });
    }
}
public class NewAccountStep : KernelProcessStep
{
    public static class Functions
    {
        public const string CreateNewAccount = nameof(CreateNewAccount);
    }

    [KernelFunction(Functions.CreateNewAccount)]
    public async Task CreateNewAccountAsync(KernelProcessStepContext context, bool previousCheckSucceeded, NewCustomerForm customerDetails, List<ChatMessageContent> interactionTranscript, Kernel _kernel)
    {
        // 调用 API 为用户创建新账户的占位符
        var accountId = new Guid();
        AccountDetails accountDetails = new()
        {
            UserDateOfBirth = customerDetails.UserDateOfBirth,
            UserFirstName = customerDetails.UserFirstName,
            UserLastName = customerDetails.UserLastName,
            UserId = customerDetails.UserId,
            UserPhoneNumber = customerDetails.UserPhoneNumber,
            UserState = customerDetails.UserState,
            UserEmail = customerDetails.UserEmail,
            AccountId = accountId,
            AccountType = AccountType.PrimeABC,
        };

        Console.WriteLine($"[账户创建] 新账户 {accountId} 已创建");

        await context.EmitEventAsync(new()
        {
            Id = AccountOpeningEvents.NewMarketingRecordInfoReady,
            Data = new MarketingNewEntryDetails
            {
                AccountId = accountId,
                Name = $"{customerDetails.UserFirstName} {customerDetails.UserLastName}",
                PhoneNumber = customerDetails.UserPhoneNumber,
                Email = customerDetails.UserEmail,
            }
        });

        await context.EmitEventAsync(new()
        {
            Id = AccountOpeningEvents.CRMRecordInfoReady,
            Data = new AccountUserInteractionDetails
            {
                AccountId = accountId,
                UserInteractionType = UserInteractionType.OpeningNewAccount,
                InteractionTranscript = interactionTranscript
            }
        });

        await context.EmitEventAsync(new()
        {
            Id = AccountOpeningEvents.NewAccountDetailsReady,
            Data = accountDetails,
        });
    }
}

public class NewMarketingEntryStep : KernelProcessStep
{
    public static class Functions
    {
        public const string CreateNewMarketingEntry = nameof(CreateNewMarketingEntry);
    }

    [KernelFunction(Functions.CreateNewMarketingEntry)]
    public async Task CreateNewMarketingEntryAsync(KernelProcessStepContext context, MarketingNewEntryDetails userDetails, Kernel _kernel)
    {
        Console.WriteLine($"[营销条目创建] 新账户 {userDetails.AccountId} 已创建");

        // 调用 API 为营销目的创建用户新条目的占位符
        await context.EmitEventAsync(new() { Id = AccountOpeningEvents.NewMarketingEntryCreated, Data = true });
    }
}
public class WelcomePacketStep : KernelProcessStep
{
    public static class Functions
    {
        public const string CreateWelcomePacket = nameof(CreateWelcomePacket);
    }

    [KernelFunction(Functions.CreateWelcomePacket)]
    public async Task CreateWelcomePacketAsync(KernelProcessStepContext context, bool marketingEntryCreated, bool crmRecordCreated, AccountDetails accountDetails, Kernel _kernel)
    {
        Console.WriteLine($"[欢迎包] 新账户 {accountDetails.AccountId} 已创建");

        var mailMessage = $"""
            亲爱的 {accountDetails.UserFirstName} {accountDetails.UserLastName}
            我们很高兴地通知您，您已成功创建了一个新的 PRIME ABC 账户！
            
            账户详细信息：
            账户号码：{accountDetails.AccountId}
            账户类型：{accountDetails.AccountType}
            
            请为安全起见保密此信息。
            
            这是我们档案中的联系信息：
            
            电子邮件：{accountDetails.UserEmail}
            电话：{accountDetails.UserPhoneNumber}
            
            感谢您开设账户！
            """;

        await context.EmitEventAsync(new()
        {
            Id = AccountOpeningEvents.WelcomePacketCreated,
            Data = mailMessage,
            Visibility = KernelProcessEventVisibility.Public,
        });
    }
}
#endregion


#region 实体类

public class NewCustomerForm
{
    [JsonPropertyName("userFirstName")]
    public string UserFirstName { get; set; } = string.Empty;

    [JsonPropertyName("userLastName")]
    public string UserLastName { get; set; } = string.Empty;

    [JsonPropertyName("userDateOfBirth")]
    public string UserDateOfBirth { get; set; } = string.Empty;

    [JsonPropertyName("userState")]
    public string UserState { get; set; } = string.Empty;

    [JsonPropertyName("userPhoneNumber")]
    public string UserPhoneNumber { get; set; } = string.Empty;

    [JsonPropertyName("userId")]
    public string UserId { get; set; } = string.Empty;

    [JsonPropertyName("userEmail")]
    public string UserEmail { get; set; } = string.Empty;

    public NewCustomerForm CopyWithDefaultValues(string defaultStringValue = "未回答")
    {
        NewCustomerForm copy = new();
        PropertyInfo[] properties = typeof(NewCustomerForm).GetProperties();

        foreach (PropertyInfo property in properties)
        {
            // 获取属性的值
            string? value = property.GetValue(this) as string;

            // 检查值是否为空字符串
            if (string.IsNullOrEmpty(value))
            {
                property.SetValue(copy, defaultStringValue);
            }
            else
            {
                property.SetValue(copy, value);
            }
        }

        return copy;
    }

    public bool IsFormCompleted()
    {
        return !string.IsNullOrEmpty(UserFirstName) &&
            !string.IsNullOrEmpty(UserLastName) &&
            !string.IsNullOrEmpty(UserId) &&
            !string.IsNullOrEmpty(UserDateOfBirth) &&
            !string.IsNullOrEmpty(UserState) &&
            !string.IsNullOrEmpty(UserEmail) &&
            !string.IsNullOrEmpty(UserPhoneNumber);
    }
}
public class AccountDetails : NewCustomerForm
{
    public Guid AccountId { get; set; }
    public AccountType AccountType { get; set; }
}

public enum AccountType
{
    PrimeABC,
    Other,
}

public static class AccountOpeningEvents
{
    // 开始流程
    public static readonly string StartProcess = nameof(StartProcess);

    // 新客户表单欢迎消息完成
    public static readonly string NewCustomerFormWelcomeMessageComplete = nameof(NewCustomerFormWelcomeMessageComplete);

    // 新客户表单完成
    public static readonly string NewCustomerFormCompleted = nameof(NewCustomerFormCompleted);

    // 新客户表单需要更多信息
    public static readonly string NewCustomerFormNeedsMoreDetails = nameof(NewCustomerFormNeedsMoreDetails);

    // 客户互动记录已准备好
    public static readonly string CustomerInteractionTranscriptReady = nameof(CustomerInteractionTranscriptReady);

    // 新账户验证检查通过
    public static readonly string NewAccountVerificationCheckPassed = nameof(NewAccountVerificationCheckPassed);

    // 信用评分检查通过
    public static readonly string CreditScoreCheckApproved = nameof(CreditScoreCheckApproved);

    // 信用评分检查被拒绝
    public static readonly string CreditScoreCheckRejected = nameof(CreditScoreCheckRejected);

    // 欺诈检测检查通过
    public static readonly string FraudDetectionCheckPassed = nameof(FraudDetectionCheckPassed);

    // 欺诈检测检查失败
    public static readonly string FraudDetectionCheckFailed = nameof(FraudDetectionCheckFailed);

    // 新账户详细信息已准备好
    public static readonly string NewAccountDetailsReady = nameof(NewAccountDetailsReady);

    // 新营销记录信息已准备好
    public static readonly string NewMarketingRecordInfoReady = nameof(NewMarketingRecordInfoReady);

    // 新营销记录条目已创建
    public static readonly string NewMarketingEntryCreated = nameof(NewMarketingEntryCreated);

    // 客户关系管理（CRM）记录信息已准备好
    public static readonly string CRMRecordInfoReady = nameof(CRMRecordInfoReady);

    // 客户关系管理（CRM）记录条目已创建
    public static readonly string CRMRecordInfoEntryCreated = nameof(CRMRecordInfoEntryCreated);

    // 欢迎包已创建
    public static readonly string WelcomePacketCreated = nameof(WelcomePacketCreated);

    // 邮件服务已发送
    public static readonly string MailServiceSent = nameof(MailServiceSent);
}

public record AccountUserInteractionDetails
{
    public Guid AccountId { get; set; }

    public List<ChatMessageContent> InteractionTranscript { get; set; } = [];

    public UserInteractionType UserInteractionType { get; set; }
}

public enum UserInteractionType
{
    Complaint,
    AccountInfoRequest,
    OpeningNewAccount
}
public record MarketingNewEntryDetails
{
    public Guid AccountId { get; set; }

    public string Name { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }
}

#endregion
