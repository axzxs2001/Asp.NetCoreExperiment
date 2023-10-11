using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseStaticFiles();

app.MapPost("/upload", ([FromForm] DocumentUpload document) =>
{
    return Results.Ok();
}).DisableAntiforgery();

app.Run();

public class DocumentUpload
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public IFormFileCollection? Documents { get; set; }
}