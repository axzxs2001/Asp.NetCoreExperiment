using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<Setting>()
    .BindConfiguration("Setting")
    //.ValidateOnStart<Setting>()
    //.ValidateDataAnnotations();
    .Validate(setting =>
    {
        return !string.IsNullOrWhiteSpace(setting.Name) && !string.IsNullOrWhiteSpace(setting.Value);
    }, "Setting配置有误");

var app = builder.Build();

app.Services.GetService<IOptionsMonitor<Setting>>()?.OnChange((setting, name) =>
{
    app.Logger.LogInformation(setting.Value);
});


app.MapGet("/opt1", (IOptions<Setting> option) =>
{
    app.Logger.LogInformation("opt1");
    return option.Value;
});
app.MapGet("/opt2", (IOptionsSnapshot<Setting> option) =>
{
    app.Logger.LogInformation("opt2");
    return option.Value;
});
app.MapGet("/opt3", (IOptionsMonitor<Setting> option) =>
{
    app.Logger.LogInformation("opt3");

    return option.CurrentValue;
});
app.Run();


class Setting
{
    [Required(ErrorMessage = "Name不能为空")]
    public string Name { get; set; }
    public string Value { get; set; }
}
