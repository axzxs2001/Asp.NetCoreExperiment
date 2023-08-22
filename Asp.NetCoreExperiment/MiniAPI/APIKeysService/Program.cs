
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Collections;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.TryAddKeyedScoped<IConfigRepository, ISMSConfigRepository>("smsRep");
builder.Services.TryAddKeyedScoped<INotifyService, SMSService>("smsSev");
builder.Services.AddScoped<SMS>();

builder.Services.TryAddKeyedSingleton<IConfigRepository, IEMailConfigRepository>("emailRep");
builder.Services.TryAddKeyedSingleton<INotifyService, EMailService>("emailSev");

var app = builder.Build();

app.MapGet("/sms", (SMS sms, string message) =>
{
    return new { Result = sms.Notify(message), Messgae = message, Type = sms.GetType().Name };
});
app.MapGet("/email", (string message) =>
{
    var email = app.Services.GetRequiredKeyedService<INotifyService>("emailSev");
    return new { Result = email.Notify(message), Messgae = message, Type = email.GetType().Name };
});

//app.MapGet("/sms", ([FromServices] KeyedServiceConsumer keyedService, string message) =>
//{
//    return true;
//    //return new { Result = keyedService["smsSev"].Notify(message), Messgae = message, Type = keyedService["smsSev"].GetType().Name };
//});
//app.MapGet("/sms", ([FromServices] IEnumerable<INotifyService> notifyServices, string message) =>
//    {
//        var notifyService = notifyServices.FirstOrDefault(x => x.GetType().Name == "SMSService");
//        return new { Result = notifyService!.Notify(message), Messgae = message, Type = notifyService.GetType().Name };
//    });
//app.MapGet("/email", ([FromServices][FromKeyedServices("emailSev")] INotifyService notifyService, string message) => new { Result = notifyService.Notify(message), Messgae = message, Type = notifyService.GetType().Name });

app.Run();

public class SMS([FromKeyedServices("smsSev")] INotifyService notifyService)
{
    public bool Notify(string message)
    {
        return notifyService.Notify(message);
    }
}
//public class EMail([FromKeyedServices("emailSev")] INotifyService notifyService)
//{
//    public bool Notify(string message)
//    {
//        return notifyService.Notify(message);
//    }
//}
//public class EMail(IServiceProvider keyedServiceProvider)
//{
//    public bool Notify(string message)
//    {
//        return keyedServiceProvider.GetRequiredKeyedService<INotifyService>("emailSev").Notify(message);
//    }
//}

//public class KeyedServiceConsumer(IKeyedServiceProvider keyedServiceProvider)
//{
//    //public INotifyService this[string key] => keyedServiceProvider.GetRequiredKeyedService<INotifyService>(key);
//}

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