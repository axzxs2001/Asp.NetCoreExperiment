using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MiniAPICourse;
using MiniAPICourse.Models;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder();


builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    config.AddIniFile("myconfig.ini",
                       optional: true,
                       reloadOnChange: true);
});
//ini取值
Console.WriteLine(builder.Configuration.GetSection("iniconfig:key1").Value);

//取值
Console.WriteLine(builder.Configuration.GetSection("Setting:Url").Value);

//绑定
var setting = new Setting1();
builder.Configuration.GetSection("Setting").Bind(setting);
Console.WriteLine(setting);

//record不可以用在Snapshot中
builder.Services.Configure<Setting>(builder.Configuration.GetSection("Setting"));


builder.Services.AddDbContext<ExamContext>(options =>
      options.UseSqlServer(builder.Configuration.GetConnectionString("ExamDatabase")));

var app = builder.Build();

app.MapGet("/config", (IConfiguration config) =>
{
    return config.GetSection("Setting:Method").Value;
});

app.MapGet("/snapshot", (IOptionsSnapshot<Setting> options) =>
{
    return options.Value;
});
app.MapGet("/monitorstart", (IOptionsMonitor<Setting> options) =>
{
    options.OnChange(seting =>
    {
        Console.WriteLine(seting.Url);
    });
    return options.CurrentValue;
});

app.Run();


public record Setting
{
    public string? Url { get; set; }
    public string? TimeOut { get; set; }
    public string? Method { set; get; }
}

record Setting1(string? Url = null, string? TimeOut = null, string? Method = null);
