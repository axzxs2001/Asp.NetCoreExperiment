using Microsoft.AspNetCore.Routing.Patterns;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();


app.MapGet("/test",  [AIData]() =>
{
 
});
app.MapPost("/test", [AIData(Name = "test", Describe = "²âÊÔ", Rule = new string[] { "required" })] () =>
{

});


app.Map("/test", () => {
    return "aaddd";
});

app.Run();


[AttributeUsage(AttributeTargets.Method| AttributeTargets.Class)]
public class AIDataAttribute:Attribute
{
    public string Name { get; set; }

    public string Describe { get; set; }

    public string[] Rule { get; set; }



}


