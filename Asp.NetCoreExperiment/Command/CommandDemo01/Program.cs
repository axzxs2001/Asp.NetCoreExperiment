using System.CommandLine;
using System.CommandLine.Completions;
using System.CommandLine.Parsing;


//await Command03Async(args);
//await Command02Async(args);

//await Command01Async(args);

await Command04Async(args);
//await Command05Async(args);
//await Command06Async(args);

//dotnet run show green red
static async Task Command06Async(string[] args)
{
    //创建根命令
    var rootCommand = new RootCommand("这是一个命令行工具：旦猫");
    rootCommand.SetHandler(() =>
    {
        Console.WriteLine("欢迎使用《旦猫》");
    });
    //创建子命令 show
    var showCommand = new Command("show", "显示一些信息");

    //创建参数 color
    var showArgument = new Argument<string[]>(name: "color", description: "设置输出信息的色彩")
    {
        Arity = ArgumentArity.OneOrMore,
    };
    //添加参数到show命令中
    showCommand.AddArgument(showArgument);
    //设置命令show执行的动作，这是带上times参数，类型为整弄
    showCommand.SetHandler((string[] colors) =>
    {
        foreach (var color in colors)
        {
            var enumColors = Enum.GetNames(typeof(ConsoleColor));
            foreach (var enumColor in enumColors)
            {
                if (enumColor.ToLower() == color.ToLower())
                {
                    Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), enumColor);
                    Console.WriteLine($"这是《旦猫》的show命令");
                    Console.ResetColor();
                    break;
                }
            }
        }
    }, showArgument);
    //添加命令show到 根命令中
    rootCommand.Add(showCommand);
    await rootCommand.InvokeAsync(args);
}


static async Task Command05Async(string[] args)
{
    //创建根命令
    var rootCommand = new RootCommand("这是一个命令行工具：旦猫");
    rootCommand.SetHandler(() =>
    {
        Console.WriteLine("欢迎使用《旦猫》");
    });
    //创建子命令 show
    var showCommand = new Command("show", "显示一些信息");

    //创建参数 color
    var showArgument = new Argument<ConsoleColor>(name: "color", description: "设置输出信息的色彩", parse: ParseColor<ConsoleColor>)
    {
        Arity = ArgumentArity.ExactlyOne,
    };
    ConsoleColor ParseColor<ConsoleColor>(ArgumentResult result)
    {
        var color = result.Tokens[0].Value;
        return (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color.ToString(), true);
    }
    //添加参数到show命令中
    showCommand.AddArgument(showArgument);
    //设置命令show执行的动作，这是带上times参数，类型为整弄
    showCommand.SetHandler((ConsoleColor color) =>
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"这是《旦猫》的show命令");
        Console.ResetColor();
    }, showArgument);
    //添加命令show到 根命令中
    rootCommand.Add(showCommand);
    await rootCommand.InvokeAsync(args);
}


static async Task Command04Async(string[] args)
{
    //创建根命令
    var rootCommand = new RootCommand("这是一个命令行工具：旦猫");
    rootCommand.SetHandler(() =>
    {
        Console.WriteLine("欢迎使用《旦猫》");
    });
    //创建子命令 show
    var showCommand = new Command("show", "显示一些信息");

    //创建参数 color
    var showArgument = new Argument<string>(name: "color", description: "设置输出信息的色彩")
    {
        Arity = ArgumentArity.ExactlyOne,
    };
    //添加参数到show命令中
    showCommand.AddArgument(showArgument);
    //设置命令show执行的动作，这是带上times参数，类型为整弄
    showCommand.SetHandler((string color) =>
    {
        Console.ForegroundColor = (ConsoleColor)Enum.Parse(typeof(ConsoleColor), color,true);
        Console.WriteLine($"这是《旦猫》的show命令");
        Console.ResetColor();      
    }, showArgument);
    //添加命令show到 根命令中
    rootCommand.Add(showCommand);
    await rootCommand.InvokeAsync(args);
}


//static async Task Command03Async(string[] args)
//{
//    await new DateCommand().InvokeAsync(args);
//}

//static async Task Command02Async(string[] args)
//{
//    //创建根命令
//    var rootCommand = new RootCommand("这是一个命令行工具：旦猫");
//    rootCommand.SetHandler(() =>
//    {
//        Console.WriteLine("欢迎使用《旦猫》");
//    });
//    //创建子命令 show
//    var showCommand = new Command("show", "显示一些信息");
//    //创建子命令选项 times 别名 t
//    var timesOption = new Option<int>(name: "--times", description: "显示的次数", getDefaultValue: () => 1)
//    {
//        IsHidden = true,
//    };
//    timesOption.AddAlias("-t");
//    //添加选项到show命令中
//    showCommand.AddOption(timesOption);
//    //设置命令show执行的动作，这是带上times参数，类型为整弄
//    showCommand.SetHandler((int times) =>
//    {
//        for (var i = 1; i <= times; i++)
//        {
//            Console.WriteLine($"这是《旦猫》的show命令,显示次数={i}");
//        }
//    }, timesOption);
//    //添加命令show到 根命令中
//    rootCommand.Add(showCommand);
//    await rootCommand.InvokeAsync(args);
//}


//static async Task Command01Async(string[] args)
//{
//    var rootCommand = new RootCommand("这是一个命令行工具：旦猫");
//    rootCommand.SetHandler(() =>
//    {
//        Console.WriteLine("欢迎使用《旦猫》");
//    });

//    var sub1Command = new Command("show", "显示一些信息");
//    rootCommand.Add(sub1Command);
//    sub1Command.SetHandler(() =>
//    {
//        Console.WriteLine("这是《旦猫》的show命令");
//    });

//    await rootCommand.InvokeAsync(args);
//}


class DateCommand : Command
{
    private Argument<string> subjectArgument =
        new("subject", "The subject of the appointment.");
    private Option<DateTime> dateOption =
        new("--date", "The day of week to schedule. Should be within one week.");

    public DateCommand() : base("schedule", "Makes an appointment for sometime in the next week.")
    {
        this.AddArgument(subjectArgument);
        this.AddOption(dateOption);

        dateOption.AddCompletions((ctx) =>
        {
            var today = System.DateTime.Today;
            var dates = new List<CompletionItem>();
            foreach (var i in Enumerable.Range(1, 7))
            {
                var date = today.AddDays(i);
                dates.Add(new CompletionItem(
                    label: date.ToShortDateString(),
                    sortText: $"{i:2}"));
            }
            return dates;
        });

        this.SetHandler((string subject, DateTime date) =>
        {
            Console.WriteLine($"Scheduled \"{subject}\" for {date}");
        }, subjectArgument, dateOption);
    }
}