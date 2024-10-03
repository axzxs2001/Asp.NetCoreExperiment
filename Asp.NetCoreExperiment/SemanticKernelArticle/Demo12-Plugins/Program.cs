#pragma warning disable SKEXP0001
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System;
using System.ComponentModel;
using System.Globalization;

var chatModelId = "gpt-4o";
var key = File.ReadAllText(@"C:\GPT\key.txt");

var builder = Kernel.CreateBuilder();
builder.AddOpenAIChatCompletion(chatModelId, key);
var kernel = builder.Build();

//method 01
//kernel.ImportPluginFromFunctions("dataPlugin",
//[
//    kernel.CreateFunctionFromMethod(GetJapaneseDate, "GetJapaneseDate", "按日本历法，获取当前日期")
//]);


//method 02
//kernel.ImportPluginFromType<CurrentDateTime>();

//method 03
//kernel.ImportPluginFromObject(new CurrentDateTime());

//var settings = new OpenAIPromptExecutionSettings()
//{
//    ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions
//};
//var result = await kernel.InvokePromptAsync("今天是几号？", new KernelArguments(settings));
//Console.WriteLine(result);



//method 04
var translateDirectory = Path.Combine(
    System.IO.Directory.GetCurrentDirectory(),
    "plugins",
    "TranslatePlugin");
kernel.ImportPluginFromPromptDirectory(translateDirectory);

//method 04
var result = await kernel.InvokeAsync("TranslatePlugin", "translate", new() {
            { "input", "今天是星期一" }
          });
Console.WriteLine(result);



//[KernelFunction]
//string GetJapaneseDate()
//{
//    var japaneseFormat = new CultureInfo("ja-JP", false).DateTimeFormat;
//    japaneseFormat.Calendar = new JapaneseCalendar();
//    return DateTime.Now.ToString("gg yy年MM月dd日", japaneseFormat);
//}


//public class CurrentDateTime
//{
//    [KernelFunction, Description("获取当前日本历法的日期")]
//    public string GetJapaneseDate()
//    {
//        var japaneseFormat = new CultureInfo("ja-JP", false).DateTimeFormat;
//        japaneseFormat.Calendar = new JapaneseCalendar();
//        return DateTime.Now.ToString("gg yy年MM月dd日", japaneseFormat);
//    }
//}