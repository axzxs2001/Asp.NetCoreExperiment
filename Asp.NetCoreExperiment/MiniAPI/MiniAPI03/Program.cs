using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

var app = builder.Build();

var supportedCultures = new[] { "zh-CN", "ja-JP", "en-US" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);
localizationOptions.ApplyCurrentCultureToResponseHeaders = true;
app.UseRequestLocalization(localizationOptions);


app.MapGet("/demo", (IStringLocalizer<SharedResource> sharedLocalizer) =>
{
    return sharedLocalizer["ok"].Value;
});

app.Run();

public class SharedResource
{
}