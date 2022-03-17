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
/// <summary>
/// ɾ��Test
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
.WithName("����")
.WithTags("all test").WithDescription("����").WithDisplayName("��ʾ��").WithSummary("����")//.WithGroupName("����")
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
    /// ɾ��Test333
    /// </summary>
    /// <param name="id">Data������</param>
    /// <returns></returns>
    public static bool DeleteTest(int id)
    {
        return true;
    }
}
/// <summary>
/// �ύ����
/// </summary>
class Data
{
    /// <summary>
    /// ��� 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// ����
    /// </summary>
    public string Name { get; set; }
}