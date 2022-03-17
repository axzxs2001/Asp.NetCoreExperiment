using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    c.SwaggerDoc("v1",
       new OpenApiInfo
       {
           Title = "MiniAPI08-V1",
           Version = "v1"
       }
    );
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "MiniAPI08.xml");
    c.IncludeXmlComments(filePath);

    //添加授权
    var schemeName = "Bearer";
    c.AddSecurityDefinition(schemeName, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "请输入不带有Bearer的Token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = schemeName.ToLowerInvariant(),
        BearerFormat = "JWT"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = schemeName
                            }
                        },
                        new string[0]
                    }
                });

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.EnablePersistAuthorization();
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


app.MapGet("/test/{id}",[EndpointDescription("Sends a Hello request to the backend")] (HttpRequest request,int id) =>
{
    Console.WriteLine(request.Headers["Authorization"]);
})
.WithName("名称")
.WithTags("all test").WithDescription("描述").WithDisplayName("显示名").WithSummary("汇总")//.WithGroupName("组名")
.Produces<Data>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status404NotFound);


app.MapPost("/test",(Data data) =>
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