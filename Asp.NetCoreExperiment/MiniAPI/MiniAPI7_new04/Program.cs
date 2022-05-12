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

    //�����Ȩ
    var schemeName = "Bearer";
    c.AddSecurityDefinition(schemeName, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "�����벻����Bearer��Token",
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



//��
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
    operation.Description = "����һ�����ܵĹ��ܣ�����ʵ�����";
    operation.Summary = "���Data";
    operation.Parameters.Clear();
    operation.RequestBody = new OpenApiRequestBody
    {
        Description = "��ӵ�����ʵ��",
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

//ɾ
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
    operation.Description = "����һ�����ܵĹ��ܣ�����ʵ��ɾ��";
    operation.Summary = "�����ɾ��";
    operation.Parameters[0].Description = "���";
    operation.Parameters[0].AllowEmptyValue = true;
    return operation;
});

//��
app.MapPut("/test", (Data data) =>
{
})
.WithTags("all test")
.WithOpenApi(operation =>
{
    operation.Description = "����һ�����ܵĹ��ܣ�����ʵ���޸�";
    operation.Summary = "�޸�Data";
    operation.Parameters.Clear();
    return operation;
});

//��
app.MapGet("/test/{id}", Results<Ok<Data>, NotFound> (HttpRequest request, int? id) =>
{
    if (id.HasValue)
    {
        return TypedResults.Ok(new Data() { ID = id.Value, Name = "����", Token = request.Headers["Authorization"] });
    }
    else
    {
        return TypedResults.NotFound();
    }
})
.WithTags("all test")
.WithOpenApi(operation =>
 {
     operation.Description = "����һ�����ܵĹ��ܣ�����ʵ�ֲ�ѯ";
     operation.Summary = "����Ų�ѯ";
     operation.Parameters[0].Description = "���";
     operation.Parameters[0].AllowEmptyValue = true;
     return operation;
 });



app.Run();


/// <summary>
/// �ύ����
/// </summary>
class Data
{
    /// <summary>
    /// ��� 
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// ����
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Token
    /// </summary>
    public string? Token { get; set; }
}