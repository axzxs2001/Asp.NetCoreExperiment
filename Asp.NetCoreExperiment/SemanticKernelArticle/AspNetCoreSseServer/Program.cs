using ModelContextProtocol;
using AspNetCoreSseServer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMcpServer()    
    .WithStdioServerTransport()
    .WithToolsFromAssembly();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapMcpSse();

app.Run();
