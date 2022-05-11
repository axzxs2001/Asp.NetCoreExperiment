using Microsoft.AspNetCore.OpenApi;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}


//app.MapGet("/test", (int i) =>
//{
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi(operation =>
//{
//    operation.Summary = "Retrieve a Todo given its ID";
//    operation.Parameters[0].AllowEmptyValue = true;
//    return operation;
//});

//app.Run();

using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    //c.SwaggerDoc("v1",
    //   new OpenApiInfo
    //   {
    //       Title = "MiniAPI7_new04-V1",
    //       Version = "v1"
    //   }
    //);
    ////var filePath = Path.Combine(System.AppContext.BaseDirectory, "MiniAPI7_new04.xml");
    ////c.IncludeXmlComments(filePath);

    ////添加授权
    //var schemeName = "Bearer";
    //c.AddSecurityDefinition(schemeName, new OpenApiSecurityScheme
    //{
    //    In = ParameterLocation.Header,
    //    Description = "请输入不带有Bearer的Token",
    //    Name = "Authorization",
    //    Type = SecuritySchemeType.Http,
    //    Scheme = schemeName.ToLowerInvariant(),
    //    BearerFormat = "JWT"
    //});
    //c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    //                {
    //                    new OpenApiSecurityScheme
    //                    {
    //                        Reference = new OpenApiReference
    //                        {
    //                            Type = ReferenceType.SecurityScheme,
    //                            Id = schemeName
    //                        }
    //                    },
    //                    new string[0]
    //                }
    //            });

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // c.EnablePersistAuthorization();
    });
}
/// <summary>
/// 删除Test
/// </summary>
/// <returns></returns>
app.MapPut("/test", (Data data) =>
{
})
.WithName("puttest")
.WithTags("all test");

app.MapDelete("/test/{id}", TestHandle.DeleteTest)
.WithName("deletetest")
.WithTags("all test");


app.MapGet("/test/{id}", (HttpRequest request, int id) =>
{
    Console.WriteLine(request.Headers["Authorization"]);
    return new Data();
})
.WithTags("all test")
.Produces<Data>(StatusCodes.Status200OK)
.WithOpenApi(operation =>
 {
     operation.Description = "这是一个神密的功能";
     operation.Summary = "按编号查询";
     //operation.Tags = new List<OpenApiTag> { new OpenApiTag { Description = "all test" } };
    
     operation.Parameters[0].Name = "id";
     operation.Parameters[0].Description = "编号";
     operation.Parameters[0].AllowEmptyValue = true;
     return operation;
 });

app.MapPost("/test", (Data data) =>
{
})
.WithName("posttest")
.WithTags("all test");


app.Run();

class TestHandle
{
    /// <summary>
    /// 删除Test333
    /// </summary>
    /// <param name="id">Data的主键</param>
    /// <returns></returns>
    public static bool DeleteTest(int id)
    {
        return true;
    }
}
/// <summary>
/// 提交数据
/// </summary>
class Data
{
    /// <summary>
    /// 编号 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
}