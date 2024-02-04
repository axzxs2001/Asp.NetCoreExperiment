using Coravel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder();
builder.Services.AddScheduler();
var app = builder.Build();
app.Services.UseScheduler(scheduler =>
{
    scheduler.Schedule(
        async () =>
        {
            await Task.Delay(3000);
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.fffffff}");

        })
        .EverySeconds(2);
});
app.Run();