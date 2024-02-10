using Coravel;
using Coravel.Events.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEvents();
builder.Services.AddTransient<UserService>();
builder.Services.AddTransient<MessageNotify>();
builder.Services.AddTransient<EmailNotify>();
var app = builder.Build();

var registration = app.Services.ConfigureEvents();
registration
    .Register<UserChangePassword>()
    .Subscribe<MessageNotify>()
    .Subscribe<EmailNotify>();


app.MapPost("/changepassword", async ([FromServices] UserService userService, [FromBody] UserChangePassword changePassword) =>
{
    return await userService.ChangePassword(changePassword);
});

app.Run();

public class UserChangePassword: IEvent
{
    public string Name { get; set; }
    public string UserName { get; set; }
    public string NewPassword1 { get; set; }
    public string NewPassword2 { get; set; }
    public string OldPassword { get; set; }
}
public class UserService 
{
    readonly IDispatcher _dispatcher;
    public UserService(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public async Task<bool> ChangePassword(UserChangePassword changePassword)
    {
        //验证两个新密码是否相等，旧密码是否正确


        //持久化修改密码

        //广播事件
        await _dispatcher.Broadcast(changePassword);
        return true;
    }
}
public class MessageNotify : IListener<UserChangePassword>
{
    public async Task HandleAsync(UserChangePassword changePassword)
    {
        Console.WriteLine($"{changePassword.Name}您好，你于{DateTime.Now.ToString("yyyy年MM月dd日HH时mm分")}进行了修改密码！");
        await Task.CompletedTask;
    }
}
public class EmailNotify : IListener<UserChangePassword>
{
    public async Task HandleAsync(UserChangePassword changePassword)
    {
        Console.WriteLine($"{changePassword.Name}您好:\r\n这是一封通知你修改密码的邮件！");
        await Task.CompletedTask;
    }
}