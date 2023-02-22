using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddOptions<APIConfig>()
    .Bind(builder.Configuration.GetSection(nameof(APIConfig)))
    .Validate(c =>
    {
        return c.TimeOut > 0 && c.TimeOut <= 1000 && c.Max >= -1.5 && c.Max <= 1.5 && Enum.TryParse<Category>(c.Category, true, out Category o);

    },"APIConfig≈‰÷√”–ŒÛ£°")
    //.Validate<APIConfig>((c1, c2) => {
        
    //    return true;
    //})
    //.ValidateDataAnnotations()
    .ValidateOnStart();

var app = builder.Build();



app.MapGet("/apiconfig", (IOptions<APIConfig> options) =>
{
    return options.Value;
});

app.Run();

public class APIConfig
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
    Large,
    Small,
    Middle
}
