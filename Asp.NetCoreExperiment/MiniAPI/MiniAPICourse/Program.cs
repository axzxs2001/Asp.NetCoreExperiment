using Microsoft.AspNetCore.Mvc;
using MiniAPICourse;
using MiniAPICourse.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder();

builder.Services.AddRouting(options =>
{
    options.ConstraintMap["Uint"] = typeof(Uint);
});

var app = builder.Build();

#region 第一个参数
//简单路由
app.MapGet("/", () => "Hello .NET Mini API...?");
//正则路由
app.MapGet("/area/{postcode:regex(^[0-9]{{3}}-[0-9]{{4}}$)}", (string postcode) => $"邮编：{postcode}");

//参数路由
app.MapGet("/question/{id:Uint}", (uint id) => $"查询ID为{id}试题");
#endregion



#region 第二个参数


app.MapGet("/answer", ([FromQuery] int id) => $"[FromQuery]-请求的AnswerID为{id}");

app.MapGet("/answers", ([FromHeader] string key) => $"[FromHeader]-secretkey为{key}");

app.MapGet("/question/{questiontype}", ([FromRoute] string questionType) => $"[FromRoute]-questiontype={questionType}");

app.MapPost("/answer", ([FromBody] Answer answer) => $"[FromBody]-answer：{System.Text.Json.JsonSerializer.Serialize(answer)}");

app.MapPost("/questiontype", ([FromForm] string questionTypeName) => $"[FromForm]-questionTypeName：{questionTypeName}");

//可选参数：
//全部试卷 http://localhost:5157/user/1/exampapers
//某个试卷 http://localhost:5157/user/1/exampapers?exampaperid=1
app.MapGet("/user/{id}/exampapers", (int id, int? examPaperID) => $"查询用户ID为{id}的{(examPaperID.HasValue ? $"试卷ID为{examPaperID.Value}的试卷" : "全部试卷")}");


//自定义参数绑定 area=[(35.721875, 139.786564),(35.723903, 139.803464),(35.705806, 139.806078),(35.705118, 139.779927)]
app.MapGet("/area1/hotel", (Area1 area) => $"TryParse Area1:{System.Text.Json.JsonSerializer.Serialize(area)}");
app.MapGet("/area2/hotel", (Area2 area) => $"BindAsync Area2:{System.Text.Json.JsonSerializer.Serialize(area)}");
#endregion

app.Run();


public class Area1
{
    public Coordinates[]? Coordinateses { get; set; }
    public static bool TryParse(string? value, IFormatProvider? provider, out Area1? area)
    {
        var CoordinatesGroupStrings = value?.Split(new string[] { "[(", ")]", "),(" },
                StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (CoordinatesGroupStrings != null)
        {
            var coordinatesList = new List<Coordinates>();
            foreach (var coordinateGroupString in CoordinatesGroupStrings)
            {
                var coordinateStrings = coordinateGroupString.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                var latitudeResult = double.TryParse(coordinateStrings[0], out double latitude);
                var longitudeResult = double.TryParse(coordinateStrings[1], out double longitude);
                if (latitudeResult && longitudeResult)
                {
                    coordinatesList.Add(new Coordinates(latitude, longitude));
                }
            }
            area = new Area1 { Coordinateses = coordinatesList.ToArray() };
            return true;
        }
        area = null;
        return false;
    }
}
public record Coordinates(double Latitude, double Longitude);

public class Area2
{
    public Coordinates[]? Coordinateses { get; set; }

    public static ValueTask<Area2?> BindAsync(HttpContext context, ParameterInfo parameter)
    {
        var CoordinatesGroupStrings = context.Request.Query["area"].ToString().Split(new string[] { "[(", ")]", "),(" },
               StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        if (CoordinatesGroupStrings != null)
        {
            var coordinatesList = new List<Coordinates>();
            foreach (var coordinateGroupString in CoordinatesGroupStrings)
            {
                var coordinateStrings = coordinateGroupString.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                var latitudeResult = double.TryParse(coordinateStrings[0], out double latitude);
                var longitudeResult = double.TryParse(coordinateStrings[1], out double longitude);
                if (latitudeResult && longitudeResult)
                {
                    coordinatesList.Add(new Coordinates(latitude, longitude));
                }
            }
            return ValueTask.FromResult<Area2?>(new Area2 { Coordinateses = coordinatesList.ToArray() });

        }
        return ValueTask.FromResult<Area2?>(null);
    }
}
