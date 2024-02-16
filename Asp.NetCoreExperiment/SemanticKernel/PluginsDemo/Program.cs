
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.ComponentModel;
var chatModelId = "gpt-4-0125-preview";
var key = File.ReadAllText(@"C:\GPT\key.txt");
var builder = Kernel.CreateBuilder();
builder.Services.AddOpenAIChatCompletion(chatModelId, key);
builder.Plugins.AddFromType<LightPlugin>();
var kernel = builder.Build();

ChatHistory history = [];

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
var openAIPromptExecutionSettings = new OpenAIPromptExecutionSettings()
{
    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
};


while (true)
{
    Console.Write("用户 > ");
    history.AddUserMessage(Console.ReadLine()!);
    var result = await chatCompletionService.GetChatMessageContentAsync(
        history,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);
    Console.WriteLine("助理 > " + result);
    history.AddMessage(result.Role, result.Content!);
}


public class LightPlugin
{
    public bool IsOn { get; set; } = false;

    [KernelFunction]
    [Description("获取灯的状态。")]
    public string GetState() => this.IsOn ? "开" : "关";

    [KernelFunction]
    [Description("改变灯的状态。'")]
    public string ChangeState(bool newState)
    {
        this.IsOn = newState;
        var state = this.GetState();

        // Print the state to the console
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine($"[Light is now {state}]");
        Console.ResetColor();

        return state;
    }

    [KernelFunction]
    [Description("用灯显示出参数提供的文字。")]
    public void ShowCharacters(
        [Description("显示的文字")] string works,
        [Description("文字的颜色")] ConsoleColor color = ConsoleColor.Green)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(works);
        Console.ResetColor();
    }

    //[KernelFunction]
    //[Description("返回一个UTC时间")]
    //public string GetCurrentUtcTime() => DateTime.UtcNow.ToString("R");
}