using System.CommandLine;


await Command01Async(args);

static async Task Command01Async(string[] args)
{
    var rootCommand = new RootCommand("这是一个命令行工具：旦猫");
    rootCommand.SetHandler(() =>
    {
        Console.WriteLine("欢迎使用《旦猫》");
    });

    var sub1Command = new Command("show", "显示一些信息");
    rootCommand.Add(sub1Command);
    sub1Command.SetHandler(() =>
    {
        Console.WriteLine("这是《旦猫》的show命令");
    });

    await rootCommand.InvokeAsync(args);
}