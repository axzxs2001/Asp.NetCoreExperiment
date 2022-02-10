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
app.MapGet("/test/{sec}", (IDataProtectionProvider provider, ILogger<Program> logger,string  sec) =>
{
    var protector = provider.CreateProtector("a.b.c");    
    logger.LogInformation(protector.Unprotect(sec));
    return "ok";
});

app.Run();
