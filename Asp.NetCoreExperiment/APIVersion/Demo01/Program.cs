using System;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();

builder.Services.AddApiVersioning();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
var test = app.NewVersionedApi();
///test?api-version=1.0
test.MapGet("/test", () => "ok" ).HasApiVersion(1.0);



app.Run();

