

using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Primitives;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<KestrelServerOptions>(options =>
{
    //默认是30M
    options.Limits.MaxRequestBodySize = int.MaxValue;
});


var app = builder.Build();

app.MapPost("/upload", async (IFormFile file) =>
{
    Console.WriteLine(file.FileName);
    using var stream = System.IO.File.OpenWrite(file.FileName);
    await file.CopyToAsync(stream);
});

app.MapPost("/uploads", async (IFormFileCollection files) =>
{
    foreach (var file in files)
    {
        Console.WriteLine(file.FileName);
        using var stream = System.IO.File.OpenWrite(file.FileName);
        await file.CopyToAsync(stream);
    }
});


app.MapPost("/uploadbig", async (Stream body, CancellationToken cancellationToken) =>
{
    if (body.CanRead)
    {
        var setLength = 10240;
        var readLength = 0;
        var step = 1;
        do
        {
            var arr = new byte[setLength];
            readLength = await body.ReadAsync(arr, 0, arr.Length);
            Console.WriteLine($"第{step++}次读取流的长度：{readLength}");
        } while (readLength >= setLength);
    }
    await Task.Delay(1);
});

#region Preview2


app.MapGet("/strs1", (StringValues strs) =>
{
    var backStr = new StringBuilder();
    foreach (var str in strs)
    {
        backStr.Append($"str={str}，");
    }
    return backStr.ToString().TrimEnd('，');

});

app.MapGet("/strs2", (string[] strs) =>
{
    var backStr = new StringBuilder();
    foreach (var str in strs)
    {
        backStr.Append($"str={str}，");
    }
    return backStr.ToString().TrimEnd('，'); 
});

app.MapGet("/points", (Point[] points) =>
{
    var backPoint = new StringBuilder();
    foreach (var point in points)
    {
        backPoint.AppendLine(point.ToString());
    }
    return backPoint.ToString();

});
#endregion
app.Run();

record Point
{
    public double X { get; set; }
    public double Y { get; set; }
    public static bool TryParse(string? pointStr, out Point? point)
    {
        if (pointStr is null)
        {
            point = null;
            return false;
        }
        point = new Point
        {
            X = double.Parse(pointStr.Split(',')[0]),
            Y = double.Parse(pointStr.Split(',')[1])
        };
        return true;
    }
}

