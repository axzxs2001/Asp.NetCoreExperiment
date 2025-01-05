
#pragma warning disable SKEXP0050
#pragma warning disable SKEXP0001

using Microsoft.SemanticKernel.Data;
using Microsoft.SemanticKernel.Plugins.Web.Bing;

var textSearch = new BingTextSearch(apiKey: File.ReadAllText(@"C:/gpt/bingkey.txt"));

var query = "What is the Semantic Kernel?";

// Search and return results
KernelSearchResults<string> searchResults = await textSearch.SearchAsync(query, new() { Top = 4 });
var sn = 1;
await foreach (string result in searchResults.Results)
{
    Console.WriteLine($"{sn++}、{result}");
}