using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Services
    .AddOptions<MyConfig>()
    .Bind(config.GetSection(nameof(MyConfig)))
    .Validate(c =>
    {
        return c.TimeOut > 0 && c.TimeOut <= 2000 && c.Max >= -1.5 && c.Max <= 1.5 && Enum.TryParse<Category>(c.Category, true, out Category o);

    })
    //.ValidateDataAnnotations()
    .ValidateOnStart();

var app = builder.Build();



app.MapGet("/test", (IOptions<MyConfig> options) =>
{
    return options.Value;
});

app.Run();

public class MyConfig
{
    [Range(0,1000)]
    public int TimeOut { get; set; }
    [EnumDataType(typeof(Category))]
    public string? Category { get; set; }
    [Range(-1.5, 1.5)]
    public double Max { get; set; }
}

public enum Category
{
    None,
    Big,
    Small,
    Middle
}
