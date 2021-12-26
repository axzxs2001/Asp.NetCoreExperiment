using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using MiniAPICourse;
using MiniAPICourse.Models;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder();



//builder.Services.AddDbContext<ExamContext>(options =>
//  options.UseSqlServer(builder.Configuration.GetConnectionString("ExamDatabase")));

//builder.Services.PostConfigure<AppInfo>(options =>
//{
//    options.Name = "新名称";
//});


builder.Services.Configure<RedisSetting>(builder.Configuration.GetSection("RedisSetting"));
builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions
                            <RedisSetting>, RedisSettingValidation>());


//builder.Services.AddOptions<RedisSetting>()
//          .Bind(builder.Configuration.GetSection("RedisSetting"))
//          .ValidateDataAnnotations()
//          .Validate(config =>
//          {
//              if (config.Port < 1000)
//              {
//                  return false;
//              }

//              return true;
//          }, "端口不能少于1000"); 


//builder.Services.Configure<RedisSetting>(RedisSetting.Main,
//                                   builder.Configuration.GetSection("RedisSettings:Main"));
//builder.Services.Configure<RedisSetting>(RedisSetting.Prepare,
//                                    builder.Configuration.GetSection("RedisSettings:Prepare"));


var app = builder.Build();

app.MapGet("/redissetting", (IOptions<RedisSetting> options) =>
{
    try
    {
        return options.Value.ToString();
    }
    catch (OptionsValidationException exc)
    {
        return exc.Message;
    }
});

app.MapGet("/snapshotredissetting", (IOptionsSnapshot<RedisSetting> options) =>
{
    return options.Value;
});
app.MapGet("/monitorstart", (IOptionsMonitor<RedisSetting> options) =>
{
    options.OnChange(redisSetting =>
   {
       app.Logger.LogInformation(options.CurrentValue.ToString());
   });
    return options.CurrentValue;
});

app.Run();

public record RedisSetting
{
    public string? Host { get; set; }
    public int Port { get; set; }
    public string? Password { get; set; }
    [RegularExpression(@"^\d+ms$", ErrorMessage = "格式不正确，必须是ms")]
    public string? ConnectionTimeOut { get; set; }
}

//public record RedisSetting
//{
//    public const string Main = "Main";
//    public const string Prepare = "Prepare";
//    public string? Host { get; set; }
//    public int Port { get; set; }
//    public string? Password { get; set; }
//    public string? ConnectionTimeOut { get; set; }
//}


public class RedisSettingValidation : IValidateOptions<RedisSetting>
{
    public RedisSetting _config { get; init; }
    public RedisSettingValidation(IConfiguration config)
    {
        _config = config.GetSection("RedisSetting")
            .Get<RedisSetting>();
    }
    public ValidateOptionsResult Validate(string name, RedisSetting options)
    {
        string? vor=null;
        var rx = new Regex(@"^((25[0-5]|2[0-4]\d|[01]?\d\d?)\.){3}(25[0-5]|2[0-4]\d|[01]?\d\d?)$");
        if (options != null&&options.Host!=null)
        {
            var match = rx.Match(options.Host);
            if (string.IsNullOrEmpty(match.Value))
            {
                vor = $"{options.Host} 格式不正确";
            }

            if (vor != null)
            {
                return ValidateOptionsResult.Fail(vor);
            }
        }
        return ValidateOptionsResult.Success;
    }
}