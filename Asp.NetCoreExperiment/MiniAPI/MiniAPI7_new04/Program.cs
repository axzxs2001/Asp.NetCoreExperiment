using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{

    c.SwaggerDoc("v1",
       new OpenApiInfo
       {
           Title = "MiniAPI7_new04-V1",
           Version = "v1"
       }
    );
    //var filePath = Path.Combine(System.AppContext.BaseDirectory, "MiniAPI7_new04.xml");
    //c.IncludeXmlComments(filePath);

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



//增
app.MapPost("/test", Results<Ok<Data>, NotFound> (Data data) =>
{
    if (data != null)
    {
        data.ID = 101;
        return TypedResults.Ok(data);
    }
    else
    {
        return TypedResults.NotFound();
    }
})
.WithTags("all test")
.WithOpenApi(operation =>
{   
    operation.Description = "这是一个神密的功能，用来实现添加";
    operation.Summary = "添加Data";
    operation.Parameters.Clear();
    operation.RequestBody = new OpenApiRequestBody
    {
        Description = "添加的数据实体",
        Required = true,
        Content = new Dictionary<string, OpenApiMediaType>
        {
            {
                "application/json", new OpenApiMediaType
                {
                    Schema = new OpenApiSchema
                    {
                        Type = "object",
                        Properties = new Dictionary<string, OpenApiSchema>
                        {
                            {"ID",new OpenApiSchema{ Type="integer" } },
                            {"Name",new OpenApiSchema{ Type="string" } },
                            {"Token",new OpenApiSchema{ Type="string" } }
                        },
                    },
                }
            }
        },

    };   
    return operation;
});

//删
app.MapDelete("/test/{id}", Results<Ok, NotFound> (int? id) =>
{
    if (id.HasValue)
    {
        return TypedResults.Ok();
    }
    else
    {
        return TypedResults.NotFound();
    }
})
.WithTags("all test")
.WithOpenApi(operation =>
{
    operation.Description = "这是一个神密的功能，用来实现删除";
    operation.Summary = "按编号删除";
    operation.Parameters[0].Description = "编号";
    operation.Parameters[0].AllowEmptyValue = true;
    return operation;
});

//改
app.MapPut("/test", (Data data) =>
{
})
.WithTags("all test")
.WithOpenApi(operation =>
{
    operation.Description = "这是一个神密的功能，用来实现修改";
    operation.Summary = "修改Data";
    operation.Parameters.Clear();
    return operation;
});

//查
app.MapGet("/test/{id}", Results<Ok<Data>, NotFound> (HttpRequest request, int? id) =>
{
    if (id.HasValue)
    {
        return TypedResults.Ok(new Data() { ID = id.Value, Name = "测试", Token = request.Headers["Authorization"] });
    }
    else
    {
        return TypedResults.NotFound();
    }
})
.WithTags("all test")
.WithOpenApi(operation =>
 {
     operation.Description = "这是一个神密的功能，用来实现查询";
     operation.Summary = "按编号查询";
     operation.Parameters[0].Description = "编号";
     operation.Parameters[0].AllowEmptyValue = true;
     return operation;
 });



app.Run();


/// <summary>
/// 提交数据
/// </summary>
class Data
{
    /// <summary>
    /// 编号 
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Token
    /// </summary>
    public string? Token { get; set; }
}