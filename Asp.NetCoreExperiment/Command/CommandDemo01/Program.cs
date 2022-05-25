using System.CommandLine;
using System.CommandLine.Completions;


await Command03Async(args);
//await Command02Async(args);

//await Command01Async(args);

static async Task Command03Async(string[] args)
{
    await new DateCommand().InvokeAsync(args);
}

static async Task Command02Async(string[] args)
{
    var rootCommand = new RootCommand("这是一个命令行工具：旦猫");
    rootCommand.SetHandler(() =>
    {
        Console.WriteLine("欢迎使用《旦猫》");
    });


    //var digOption = new Option<int>
    //(name: "--dig",
    //description: "An option whose argument is parsed as an int.",
    //getDefaultValue: () => 42)
    //{ IsRequired = true };
    //digOption.AddAlias("-d");

    var digOption = new Option<int>(name: "--dig", description: "An option whose argument is parsed as an int.")
    {
        IsHidden = true
    };
    digOption.AddAlias("-d");

    var sub1Command = new Command("show", "显示一些信息");
    sub1Command.AddOption(digOption);

    rootCommand.Add(sub1Command);
    sub1Command.SetHandler((int d) =>
    {
        Console.WriteLine($"这是《旦猫》的show命令,-1={d}");
    }, digOption);

    await rootCommand.InvokeAsync(args);
}


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

        dateOption.AddCompletions((ctx) => {
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

        this.SetHandler((string subject, DateTime date) => {
            Console.WriteLine($"Scheduled \"{subject}\" for {date}");
        }, subjectArgument, dateOption);
    }
}