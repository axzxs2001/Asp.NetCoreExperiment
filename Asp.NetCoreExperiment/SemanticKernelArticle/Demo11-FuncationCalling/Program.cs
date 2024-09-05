using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
Kernel CreateKernel()
{
    var builder = Kernel.CreateBuilder();
    var chatModelId = "gpt-4o";
    var key = File.ReadAllText(@"C:\GPT\key.txt");
    // i.e. gpt-3.5-turbo-1106 or gpt-4-1106-preview
    builder.AddOpenAIChatCompletion("gpt-3.5-turbo-1106", key);
    builder.Services.AddLogging(c =>
    {
        c.SetMinimumLevel(LogLevel.Debug);
    });
    var kernel = builder.Build();

    kernel.ImportPluginFromFunctions("HelperFunctions",
    [
        kernel.CreateFunctionFromMethod(() => DateTime.UtcNow.ToString("R"), "GetCurrentUtcTime", "Retrieves the current time in UTC."),
            kernel.CreateFunctionFromMethod((string cityName) =>
                cityName switch
                {
                    "Boston" => "61 and rainy",
                    "London" => "55 and cloudy",
                    "Miami" => "80 and sunny",
                    "Paris" => "60 and rainy",
                    "Tokyo" => "50 and sunny",
                    "Sydney" => "75 and sunny",
                    "Tel Aviv" => "80 and sunny",
                    _ => "31 and snowing",
                }, "GetWeatherForCity", "Gets the current weather for the specified city"),
        ]);

    return kernel;
}
