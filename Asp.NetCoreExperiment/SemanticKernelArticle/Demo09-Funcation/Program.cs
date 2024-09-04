using System.Text.Json;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using System.ComponentModel;
using System.Globalization;

var chatModelId = "gpt-4o";
var key = File.ReadAllText(@"C:\GPT\key.txt");


var kernel = Kernel.CreateBuilder()
   .AddOpenAIChatCompletion(chatModelId, key)
   .Build();


//string FunctionDefinition = "请给我5个专有名词关于: {{$input}}";
//var myFunction = kernel.CreateFunctionFromPrompt(FunctionDefinition);
//var result = await kernel.InvokeAsync(myFunction, new() { ["input"] = ".NET" });
//Console.WriteLine(result.GetValue<string>());
//Console.WriteLine(JsonSerializer.Serialize(result.Metadata?["Usage"]));

var plugin = new FunctionsChainingPlugin();
var myFunction = kernel.CreateFunctionFromMethod(typeof(FunctionsChainingPlugin).GetMethod("Function1Async"),target: plugin);
var result = await myFunction.InvokeAsync(kernel);
var customType = result.GetValue<MyCustomType>();

Console.WriteLine($"CustomType.Number: {customType!.Number}");
Console.WriteLine($"CustomType.Text: {customType.Text}");

//var result = await kernel.InvokePromptAsync("1 + 1 = ?");
//Console.WriteLine(result.GetValue<string>());
//Console.WriteLine(JsonSerializer.Serialize(result.Metadata?["Usage"]));


//var functions = kernel.ImportPluginFromType<FunctionsChainingPlugin>();
//var customType = await kernel.InvokeAsync<MyCustomType>(functions["Function1"]);
//Console.WriteLine($"CustomType.Number: {customType!.Number}");
//Console.WriteLine($"CustomType.Text: {customType.Text}");


Console.WriteLine();


class FunctionsChainingPlugin
{
    private const string PluginName = nameof(FunctionsChainingPlugin);

    [KernelFunction]
    public async Task<MyCustomType> Function1Async(Kernel kernel)
    {
        return new MyCustomType
        {
            Number = 1,
            Text = "Function1"
        };
    }
}

class MyCustomType
{
    public int Number { get; set; }

    public string? Text { get; set; }
}

/// <summary>
/// Implementation of <see cref="TypeConverter"/> for <see cref="MyCustomType"/>.
/// In this example, object instance is serialized with <see cref="JsonSerializer"/> from System.Text.Json,
/// but it's possible to convert object to string using any other serialization logic.
/// </summary>
class MyCustomTypeConverter : TypeConverter
{
    public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType) => true;

    /// <summary>
    /// This method is used to convert object from string to actual type. This will allow to pass object to
    /// method function which requires it.
    /// </summary>
    public override object? ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
    {
        return JsonSerializer.Deserialize<MyCustomType>((string)value);
    }

    /// <summary>
    /// This method is used to convert actual type to string representation, so it can be passed to AI
    /// for further processing.
    /// </summary>
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        return JsonSerializer.Serialize(value);
    }
}