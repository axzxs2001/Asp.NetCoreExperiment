
#pragma warning disable 


using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Data;
using Microsoft.SemanticKernel.Plugins.Web;
using Microsoft.SemanticKernel.Plugins.Web.Bing;


Kernel kernel = new();

// Load native plugins
var bing = kernel.ImportPluginFromType<SearchUrlPlugin>("search");

// Run
var ask = "What's the tallest building in Europe?";
var result = await kernel.InvokeAsync(bing["BingSearchUrl"], new() { ["query"] = ask });

Console.WriteLine(ask + "\n");
Console.WriteLine(result.GetValue<string>());


var textSearch = new BingTextSearch(apiKey: File.ReadAllText(@"C:/gpt/bingkey.txt"));
var query = "What is the Semantic Kernel?";


// Search and return results
KernelSearchResults<string> searchResults = await textSearch.SearchAsync(query, new() { Top = 4 });
var sn = 1;
await foreach (string item in searchResults.Results)
{
    Console.WriteLine($"{sn++}、{item}");
}