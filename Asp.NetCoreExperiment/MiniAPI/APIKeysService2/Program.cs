using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddKeyedSingleton<IConfigRepository, ISMSConfigRepository>("smsRep");
//builder.Services.AddKeyedSingleton<INotifyService, SMSService>("smsSev");

//builder.Services.AddKeyedSingleton<IConfigRepository, IEMailConfigRepository>("emailRep");
//builder.Services.AddKeyedSingleton<INotifyService, EMailService>("emailSev");


//builder.Services.AddKeyedScoped<IConfigRepository, ISMSConfigRepository>("smsRep");
//builder.Services.AddKeyedScoped<INotifyService, SMSService>("smsSev");

//builder.Services.AddKeyedScoped<IConfigRepository, IEMailConfigRepository>("emailRep");
//builder.Services.AddKeyedScoped<INotifyService, EMailService>("emailSev");


builder.Services.AddKeyedTransient<IConfigRepository, ISMSConfigRepository>("smsRep");
builder.Services.AddKeyedTransient<INotifyService, SMSService>("smsSev");

builder.Services.AddKeyedTransient<IConfigRepository, IEMailConfigRepository>("emailRep");
builder.Services.AddKeyedTransient<INotifyService, EMailService>("emailSev");

var app = builder.Build();

app.MapGet("/email", ([FromKeyedServices("emailSev")] INotifyService notifyService, string message) => new
{
    Result = notifyService.Notify(message),
    Messgae = message,
    Type = notifyService.GetType().Name
});
app.MapGet("/sms", ([FromKeyedServices("smsSev")] INotifyService notifyService, string message) => new
{
    Result = notifyService.Notify(message),
    Messgae = message,
    Type = notifyService.GetType().Name
});

app.Run();

public interface INotifyService
{
    bool Notify(string message);
}
public class SMSService : INotifyService
{
    private readonly Dictionary<string, dynamic> _configs;
    private readonly ILogger<EMailService> _logger;
    public SMSService([FromKeyedServices("smsRep")] IConfigRepository configRepository, ILogger<EMailService> logger)
    {
        _logger = logger;
        _configs = configRepository.GetConfig();
    }
    public bool Notify(string message)
    {
        _logger.LogInformation($"{_configs["name"]},SMSService,这里根据配置文件完成短信的通知发送,Message:{message}");
        return true;
    }
}
public class EMailService : INotifyService
{
    private readonly Dictionary<string, dynamic> _configs;
    private readonly ILogger<EMailService> _logger;
    public EMailService([FromKeyedServices("emailRep")] IConfigRepository configRepository, ILogger<EMailService> logger)
    {
        _logger = logger;
        _configs = configRepository.GetConfig();
    }
    public bool Notify(string message)
    {
        _logger.LogInformation($"{_configs["name"]},EMailService,这里根据配置文件完成邮件的通知发送,Message:{message}");
        return true;
    }
}
public interface IConfigRepository
{
    Dictionary<string, dynamic> GetConfig();
}
public class IEMailConfigRepository : IConfigRepository
{
    public Dictionary<string, dynamic> GetConfig()
    {
        //从数据库中获取配置信息
        return new Dictionary<string, dynamic>() { { "name", "email配置" } };
    }
}
public class ISMSConfigRepository : IConfigRepository
{
    public Dictionary<string, dynamic> GetConfig()
    {
        //从数据库中获取配置信息
        return new Dictionary<string, dynamic>() { { "name", "sms配置" } }; ;
    }
}