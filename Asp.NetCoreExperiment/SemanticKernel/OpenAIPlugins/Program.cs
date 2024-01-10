using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.OpenApi;

var chatModelId = "gpt-4";
var key = File.ReadAllText(@"C:\GPT\key.txt");


Kernel kernel = new();
#pragma warning disable SKEXP0042 
var plugin = await kernel.ImportPluginFromOpenAIAsync("Klarna", new Uri("https://www.klarna.com/.well-known/ai-plugin.json"));

var arguments = new KernelArguments();
arguments["q"] = "Laptop";      // Category or product that needs to be searched for.
arguments["size"] = "3";        // Number of products to return
arguments["budget"] = "200";    // Maximum price of the matching product in local currency
arguments["countryCode"] = "US";// ISO 3166 country code with 2 characters based on the user location.
arguments["max_price"] = "1000";                           // Currently, only US, GB, DE, SE and DK are supported.

var functionResult = await kernel.InvokeAsync(plugin["productsUsingGET"], arguments);

var result = functionResult.GetValue<RestApiOperationResponse>();

Console.WriteLine("Function execution result: {0}", result?.Content?.ToString());

Console.ReadLine();