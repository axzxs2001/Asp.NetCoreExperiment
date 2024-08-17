var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddTransient<IDemo, Demo>();
builder.Services.AddScoped<IDemo, Demo>();
//builder.Services.AddSingleton<IDemo, Demo>();
var app = builder.Build();


app.MapGet("/demo", (IDemo demo) =>
{
    Console.WriteLine(demo.Time);

});

app.Run();


interface IDemo
{
    DateTime Time { get; set; }
}
class Demo : IDemo
{

    public DateTime Time { get; set; } = DateTime.Now;
}