using System.Text.Json;
using System.Text;
using System.Threading;
using System.Collections.Generic;

await foreach (var item in GetDataAsync())
{
    Console.WriteLine(item);
}

async IAsyncEnumerable<string> GetDataAsync()
{
    var requestDto = """
    {
      "model": "phi3.5",
      "prompt": ".net是什么，请给出解释？", 
      "stream": true
    }
    """;
    var content = new StringContent(requestDto, Encoding.UTF8, "application/json");
    var client = new HttpClient();
    var response = await client.PostAsync("http://localhost:11434/api/generate", content);
    var responseStream = await response.Content.ReadAsStreamAsync();
    var streamReader = new StreamReader(responseStream);
    while (!streamReader.EndOfStream)
    {
        //var arr = new byte[1024];
        //await responseStream.ReadAsync(arr, CancellationToken.None);
        //yield return Encoding.UTF8.GetString(arr);
        var line = await streamReader.ReadLineAsync();
        if (!string.IsNullOrEmpty(line))
        {
            yield return line;
        }
    }
}
