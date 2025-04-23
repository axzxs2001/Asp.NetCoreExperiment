#pragma warning disable SKEXP0080

using Microsoft.SemanticKernel;

var step00 = new Step00_Processes();
await step00.UseSimplestProcessAsync();


public class Step00_Processes()
{
    public static class ProcessEvents
    {
        public const string StartProcess = nameof(StartProcess);
    }

    public async Task UseSimplestProcessAsync()
    {
        // Create a simple kernel 
        var kernel = Kernel.CreateBuilder()
            //.AddOpenAIChatCompletion(
            //    modelId: "gpt-4o",
            //    apiKey: File.ReadAllText("c:/gpt/key.txt"))
            .Build();

        var processBuilder = new ProcessBuilder(nameof(Step00_Processes));

        // Create a process that will interact with the chat completion service
        //var process = new ProcessBuilder("ChatBot");
        var startStep = processBuilder.AddStepFromType<StartStep>();
        var doSomeWorkStep = processBuilder.AddStepFromType<DoSomeWorkStep>();
        var doMoreWorkStep = processBuilder.AddStepFromType<DoMoreWorkStep>();
        var lastStep = processBuilder.AddStepFromType<LastStep>();

        // Define the process flow
        processBuilder
            .OnInputEvent(ProcessEvents.StartProcess)
            .SendEventTo(new ProcessFunctionTargetBuilder(startStep));

        startStep
            .OnFunctionResult()
            .SendEventTo(new ProcessFunctionTargetBuilder(doSomeWorkStep));

        doSomeWorkStep
            .OnFunctionResult()
            .SendEventTo(new ProcessFunctionTargetBuilder(doMoreWorkStep));

        doMoreWorkStep
            .OnFunctionResult()
            .SendEventTo(new ProcessFunctionTargetBuilder(lastStep));

        lastStep
            .OnFunctionResult()
            .StopProcess();

        // Build the process to get a handle that can be started
        var kernelProcess = processBuilder.Build();

        // Start the process with an initial external event
        using var runningProcess = await kernelProcess.StartAsync(
            kernel,
                new KernelProcessEvent()
                {
                    Id = ProcessEvents.StartProcess,
                    Data = null
                });
       var result= await runningProcess.GetStateAsync();
        foreach(var step in result.Steps)
        {
            Console.WriteLine(step);
        }
        Console.WriteLine(result.State);

    }


    public sealed class StartStep : KernelProcessStep
    {
        [KernelFunction]
        public async ValueTask ExecuteAsync(KernelProcessStepContext context)
        {
            Console.WriteLine("Step 1 - Start\n");
        }
    }
    public sealed class LastStep : KernelProcessStep
    {
        [KernelFunction]
        public async ValueTask ExecuteAsync(KernelProcessStepContext context)
        {
            Console.WriteLine("Step 4 - This is the Final Step...\n");
        }
    }
    public sealed class DoSomeWorkStep : KernelProcessStep
    {
        [KernelFunction]
        public async ValueTask ExecuteAsync(KernelProcessStepContext context)
        {
            Console.WriteLine("Step 2 - Doing Some Work...\n");
        }
    }
    public sealed class DoMoreWorkStep : KernelProcessStep
    {
        [KernelFunction]
        public async ValueTask ExecuteAsync(KernelProcessStepContext context)
        {
            Console.WriteLine("Step 3 - Doing Yet More Work...\n");
        }
    }
}