using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel.Planning;
using Microsoft.SemanticKernel.Planning.Handlebars;
using System.ComponentModel;
#pragma warning disable SKEXP0061
#pragma warning disable SKEXP0060

//https://github.com/MicrosoftDocs/semantic-kernel-docs/blob/main/samples/dotnet/11-Planner/Program.cs


var chatModelId = "gpt-4";
var key = File.ReadAllText(@"C:\GPT\key.txt");
var builder = Kernel.CreateBuilder();
// Add a text or chat completion service using either:
// builder.Services.AddAzureOpenAIChatCompletion()
// builder.Services.AddAzureOpenAITextGeneration()
builder.Services.AddOpenAIChatCompletion(chatModelId, key);
// builder.Services.AddOpenAITextGeneration()
//builder.WithCompletionService();

builder.Plugins.AddFromType<MathSolver>();
var kernel = builder.Build();

// Create chat history
ChatHistory history = [];

// Get chat completion service
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

// Start the conversation
while (true)
{
    // Get user input
    Console.Write("User > ");
    history.AddUserMessage(Console.ReadLine()!);

    // Enable auto function calling
    OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
    {
        ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
    };

    // Get the response from the AI
    var result = chatCompletionService.GetStreamingChatMessageContentsAsync(
        history,
        executionSettings: openAIPromptExecutionSettings,
        kernel: kernel);

    // Stream the results
    string fullMessage = "";
    var first = true;
    await foreach (var content in result)
    {
        if (content.Role.HasValue && first)
        {
            Console.Write("Assistant > ");
            first = false;
        }
        Console.Write(content.Content);
        fullMessage += content.Content;
    }
    Console.WriteLine();

    // Add the message from the agent to the chat history
    history.AddAssistantMessage(fullMessage);
}








public class MathSolver
{
    private readonly ILogger _logger;

    public MathSolver(ILoggerFactory loggerFactory)
    {
        this._logger = loggerFactory.CreateLogger<MathSolver>();
    }

    [KernelFunction]
    [Description("Solves a math problem.")]
    [return: Description("The solution to the math problem.")]
    public async Task<string> SolveAsync(
        Kernel kernel,
        [Description("The math problem to solve; describe it in 2-3 sentences to ensure full context is provided")] string problem
    )
    {
        var kernelWithMath = kernel.Clone();

        // Remove the math solver plugin so that we don't get into an infinite loop
        kernelWithMath.Plugins.Remove(kernelWithMath.Plugins["MathSolver"]);

        // Add the math plugin so the LLM can solve the problem
        kernelWithMath.Plugins.AddFromType<MathPlugin>();

        var planner = new HandlebarsPlanner(new HandlebarsPlannerOptions() { AllowLoops = true });

        // Create a plan
        var plan = await planner.CreatePlanAsync(kernelWithMath, problem);
        this._logger.LogInformation($"Plan: {plan}");

        // Execute the plan
        var result = (await plan.InvokeAsync(kernelWithMath, [])).Trim();
        this._logger.LogInformation($"Results: {result}");

        return result;
    }
    public class MathPlugin
    {
        [KernelFunction, Description("Take the square root of a number")]
        public static double Sqrt(
            [Description("The number to take a square root of")] double number1
        )
        {
            return Math.Sqrt(number1);
        }

        [KernelFunction, Description("Add two numbers")]
        public static double Add(
            [Description("The first number to add")] double number1,
            [Description("The second number to add")] double number2
        )
        {
            return number1 + number2;
        }

        [KernelFunction, Description("Subtract two numbers")]
        public static double Subtract(
            [Description("The first number to subtract from")] double number1,
            [Description("The second number to subtract away")] double number2
        )
        {
            return number1 - number2;
        }

        [KernelFunction, Description("Multiply two numbers. When increasing by a percentage, don't forget to add 1 to the percentage.")]
        public static double Multiply(
            [Description("The first number to multiply")] double number1,
            [Description("The second number to multiply")] double number2
        )
        {
            return number1 * number2;
        }

        [KernelFunction, Description("Divide two numbers")]
        public static double Divide(
            [Description("The first number to divide from")] double number1,
            [Description("The second number to divide by")] double number2
        )
        {
            return number1 / number2;
        }

        [KernelFunction, Description("Raise a number to a power")]
        public static double Power(
            [Description("The number to raise")] double number1,
            [Description("The power to raise the number to")] double number2
        )
        {
            return Math.Pow(number1, number2);
        }

        [KernelFunction, Description("Take the log of a number")]
        public static double Log(
            [Description("The number to take the log of")] double number1,
            [Description("The base of the log")] double number2
        )
        {
            return Math.Log(number1, number2);
        }

        [KernelFunction, Description("Round a number to the target number of decimal places")]
        public static double Round(
            [Description("The number to round")] double number1,
            [Description("The number of decimal places to round to")] double number2
        )
        {
            return Math.Round(number1, (int)number2);
        }

        [KernelFunction, Description("Take the absolute value of a number")]
        public static double Abs(
            [Description("The number to take the absolute value of")] double number1
        )
        {
            return Math.Abs(number1);
        }

        [KernelFunction, Description("Take the floor of a number")]
        public static double Floor(
            [Description("The number to take the floor of")] double number1
        )
        {
            return Math.Floor(number1);
        }

        [KernelFunction, Description("Take the ceiling of a number")]
        public static double Ceiling(
            [Description("The number to take the ceiling of")] double number1
        )
        {
            return Math.Ceiling(number1);
        }

        [KernelFunction, Description("Take the sine of a number")]
        public static double Sin(
            [Description("The number to take the sine of")] double number1
        )
        {
            return Math.Sin(number1);
        }

        [KernelFunction, Description("Take the cosine of a number")]
        public static double Cos(
            [Description("The number to take the cosine of")] double number1
        )
        {
            return Math.Cos(number1);
        }

        [KernelFunction, Description("Take the tangent of a number")]
        public static double Tan(
            [Description("The number to take the tangent of")] double number1
        )
        {
            return Math.Tan(number1);
        }

        [KernelFunction, Description("Take the arcsine of a number")]
        public static double Asin(
            [Description("The number to take the arcsine of")] double number1
        )
        {
            return Math.Asin(number1);
        }

        [KernelFunction, Description("Take the arccosine of a number")]
        public static double Acos(
            [Description("The number to take the arccosine of")] double number1
        )
        {
            return Math.Acos(number1);
        }

        [KernelFunction, Description("Take the arctangent of a number")]
        public static double Atan(
            [Description("The number to take the arctangent of")] double number1
        )
        {
            return Math.Atan(number1);
        }
    }
}