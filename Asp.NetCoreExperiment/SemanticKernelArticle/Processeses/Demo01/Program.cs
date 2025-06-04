
using Microsoft.SemanticKernel;

#pragma warning disable

Kernel kernel = Kernel.CreateBuilder()
    .Build();


var process = new ProcessBuilder("ChatBot");
var startStep = process.AddStepFromType<StartStep>();
var doSomeWorkStep = process.AddStepFromType<DoSomeWorkStep>();
var doMoreWorkStep = process.AddStepFromType<DoMoreWorkStep>();
var lastStep = process.AddStepFromType<LastStep>();

// Define the process flow
process
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
var kernelProcess = process.Build();


// Start the process with an initial external event
await using var runningProcess = await kernelProcess.StartAsync(
    kernel,
        new KernelProcessEvent()
        {
            Id = ProcessEvents.StartProcess,
            Data = null
        });


public static class ProcessEvents
{
    public const string StartProcess = nameof(StartProcess);
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