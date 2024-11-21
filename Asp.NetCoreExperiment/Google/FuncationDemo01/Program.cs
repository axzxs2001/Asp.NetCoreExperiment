using Google.Cloud.Functions.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FuncationDemo01;

public class Function : IHttpFunction
{
    private readonly ILogger _logger;
    public Function(ILogger<Function> logger) =>
      _logger = logger;
    public async Task HandleAsync(HttpContext context)
    {
        _logger.LogInformation("Function received request");
        HttpRequest request = context.Request;  
        string name = ((string)request.Query["name"]) ?? "world";
        using TextReader reader = new StreamReader(request.Body);
        string text = await reader.ReadToEndAsync();       
        await context.Response.WriteAsync($"Hello {name}!text={text}");
    }

}
