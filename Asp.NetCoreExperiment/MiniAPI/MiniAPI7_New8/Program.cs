var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddSingleton<IProblemDetailsWriter, CustomWriter>();
//builder.Services.AddProblemDetails();


builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = (context) =>
    {
        context.ProblemDetails.Title = "后台发生错误了";
        context.ProblemDetails.Extensions.Add("my-extension", new { Property = "value" });
    };
});

var app = builder.Build();





app.MapGet("/test", () =>
{
    throw new Exception("错误");
    //return TypedResults.StatusCode(500);

   
});

app.Run();

public class CustomWriter : IProblemDetailsWriter
{

    public bool CanWrite(ProblemDetailsContext context)
        => context.HttpContext.Response.StatusCode == 500;


    ValueTask IProblemDetailsWriter.WriteAsync(ProblemDetailsContext context)
    {
        context.ProblemDetails.Title = "这是一个后台的错误";
        context.HttpContext.Response.WriteAsJsonAsync(context.ProblemDetails);

        return ValueTask.CompletedTask;
    }
}