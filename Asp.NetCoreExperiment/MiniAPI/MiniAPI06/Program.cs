using Microsoft.AspNetCore.DataProtection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataProtection();
var app = builder.Build();


app.MapGet("/test", (IDataProtectionProvider provider,ILogger<Program> logger) =>
{
    var protector = provider.CreateProtector("a.b.c");
    var protectString = protector.Protect("gsw");
    logger.LogInformation(protectString);
    logger.LogInformation(protector.Unprotect(protectString));
    return "ok";
});

app.Run();
