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
        //��֤�����������Ƿ���ȣ��������Ƿ���ȷ


        //�־û��޸�����

        //�㲥�¼�
        await _dispatcher.Broadcast(changePassword);
        return true;
    }
}
public class MessageNotify : IListener<UserChangePassword>
{
    public async Task HandleAsync(UserChangePassword changePassword)
    {
        Console.WriteLine($"{changePassword.Name}���ã�����{DateTime.Now.ToString("yyyy��MM��dd��HHʱmm��")}�������޸����룡");
        await Task.CompletedTask;
    }
}
public class EmailNotify : IListener<UserChangePassword>
{
    public async Task HandleAsync(UserChangePassword changePassword)
    {
        Console.WriteLine($"{changePassword.Name}����:\r\n����һ��֪ͨ���޸�������ʼ���");
        await Task.CompletedTask;
    }
}