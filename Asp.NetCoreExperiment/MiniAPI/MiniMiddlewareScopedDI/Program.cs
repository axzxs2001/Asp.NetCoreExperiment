var builder = WebApplication.CreateBuilder(args);
//�ڶ���
builder.Services.AddTransient<ITestService, TestService>();
//��һ��
//builder.Services.AddScoped<ITestService, TestService>();
var app = builder.Build();

app.UseMiddleware<TestMiddleware>();

app.MapGet("/djy", () =>
{
    Console.WriteLine("���ͣ�");
    return "OK";
});

app.Run();


public interface ITestService
{
    void Print();
}
public class TestService : ITestService
{
    public TestService()
    {
        Console.WriteLine($"Time:{DateTime.Now},ToDo:TestService.ctor");
    }
    public void Print()
    {
        Console.WriteLine($"Time:{DateTime.Now},ToDo:TestService.Print");
    }
}
//��һ��
//public class TestMiddleware
//{
//    private readonly RequestDelegate _next;

//    public TestMiddleware(RequestDelegate next)
//    {
//        _next = next;
//    }

//    public async Task InvokeAsync(HttpContext context, ITestService testService)
//    {
//        testService.Print();
//        await _next(context);
//    }
//}

//�ڶ���
public class TestMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ITestService _testService;
    public TestMiddleware(RequestDelegate next, ITestService testService)
    {
        Console.WriteLine($"Time:{DateTime.Now},ToDo:TestMiddleware.ctor");
        _next = next;
        _testService = testService;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        _testService.Print();
        await _next(context);
    }
}