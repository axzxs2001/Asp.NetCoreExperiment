using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddProblemDetails(opt =>
//{
//    opt.CustomizeProblemDetails = context =>
//    {
//        context.ProblemDetails.Instance = $"{context.HttpContext.Request.Protocol} {context.HttpContext.Request.Method} {context.HttpContext.Request.Path}";
//        context.ProblemDetails.Extensions.TryAdd("requestId", context.HttpContext.TraceIdentifier);

//        var activity = context.HttpContext.Features.Get<IHttpActivityFeature>()?.Activity;
//        context.ProblemDetails.Extensions.TryAdd("traceId", activity?.Id);

//        if (context.HttpContext.Connection != null && context.HttpContext.Connection.RemoteIpAddress != null)
//        {
//            context.ProblemDetails.Extensions.TryAdd("clientIP", context.HttpContext.Connection.RemoteIpAddress?.ToString());
//        }
//    };
//});
//builder.Services.AddExceptionHandler<ProblemExceptionHandler>();

var app = builder.Build();

app.MapGet("/test", () =>
{
    return Results.Problem(type: "Bad Request",
         title: "无效的编号",
         detail: "编号格式为：N00000，N+5位数字",
         statusCode: StatusCodes.Status400BadRequest);

});
//app.MapGet("/test", () =>
//{
//    throw new ProblemException(error: "无效的编号",
//         message: "编号格式为：N00000，N+5位数字");

//});
//app.UseExceptionHandler();
app.Run();

public class ProblemException : Exception
{
    public string Error { get; }
    public ProblemException(string error, string message) : base(message)
    {
        Error = error;
    }
}
public class ProblemExceptionHandler : IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;
    public ProblemExceptionHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ProblemException problemException)
        {
            return true;
        }
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = problemException.Error,
            Detail = problemException.Message,
            Type = "Bad Request"
        };
        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails
        });
    }
}