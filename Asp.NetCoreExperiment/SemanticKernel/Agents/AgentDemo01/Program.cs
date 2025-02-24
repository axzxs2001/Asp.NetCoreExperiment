using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Agents;
using Microsoft.SemanticKernel.Agents.OpenAI;
using Microsoft.SemanticKernel.ChatCompletion;
using OpenAI;
using System.ComponentModel;
#pragma warning disable 


var key = File.ReadAllText("c://gpt/key.txt");

await UseAutoFunctionInvocationFilterWithAgentInvocationAsync();

async Task UseAutoFunctionInvocationFilterWithAgentInvocationAsync()
{
    // Define the agent
    ChatCompletionAgent agent =
        new()
        {
            Instructions = "回答关于菜单的问题",
            Kernel = CreateKernelWithFilter(),
            Arguments = new KernelArguments(new PromptExecutionSettings() { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() }),
        };

    KernelPlugin plugin = KernelPluginFactory.CreateFromType<MenuPlugin>();
    agent.Kernel.Plugins.Add(plugin);

    /// Create the chat history to capture the agent interaction.
    ChatHistory chat = [];

    // Respond to user input, invoking functions where appropriate.
    await InvokeAgentAsync("你好");
    await InvokeAgentAsync("有什么特色沙拉");
    await InvokeAgentAsync("有什么特色饮品?");
    await InvokeAgentAsync("有什么特色汤品");
    await InvokeAgentAsync("谢谢");


    // Local function to invoke agent and display the conversation messages.
    async Task InvokeAgentAsync(string input)
    {
        Console.WriteLine(input);
        ChatMessageContent message = new(AuthorRole.User, input);
        chat.Add(message);  

        await foreach (ChatMessageContent response in agent.InvokeAsync(chat))
        {
            // Do not add a message implicitly added to the history.
            if (!response.Items.Any(i => i is FunctionCallContent || i is FunctionResultContent))
            {
                chat.Add(response);
            }

            Console.Write(response.Content);
        }
        Console.WriteLine();
    }
}


Kernel CreateKernelWithFilter()
{
    IKernelBuilder builder = Kernel.CreateBuilder();

    builder.AddOpenAIChatCompletion(
                 "gpt-4o",
                 key);

    //builder.Services.AddSingleton<IAutoFunctionInvocationFilter>(new AutoInvocationFilter());

    return builder.Build();
}

sealed class MenuPlugin
{
    [KernelFunction, Description("提供菜单上的特价菜品列表。")]
    public string GetSpecials()
    {
        return
            """
                特色汤品：蛤蜊浓汤
                特色沙拉：科布沙拉
                特色饮品：印度奶茶
                """;
    }

    [KernelFunction, Description("提供菜单上单品的价格.")]
    public string GetItemPrice(
        [Description("菜单菜品的名称")]
        string menuItem)
    {
        return "$9.99";
    }
}

 sealed class AutoInvocationFilter(bool terminate = true) : IAutoFunctionInvocationFilter
{
    public async Task OnAutoFunctionInvocationAsync(AutoFunctionInvocationContext context, Func<AutoFunctionInvocationContext, Task> next)
    {
        // Execution the function
        await next(context);

        // Signal termination if the function is from the MenuPlugin
        if (context.Function.PluginName == nameof(MenuPlugin))
        {
            context.Terminate = terminate;
        }
    }
}