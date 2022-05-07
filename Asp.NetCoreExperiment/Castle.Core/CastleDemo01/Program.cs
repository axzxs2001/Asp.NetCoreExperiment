using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddInterceptedSingleton<ITestService, TestService, AddLogInterceptor>();

var app = builder.Build();

app.MapGet("/test", (ITestService test) =>
{
    app.Logger.LogInformation("/test开始");
    var result = test.Get(121);
    app.Logger.LogInformation("/test结束");
    return result;
});

app.Run();


public interface ITestService
{
    string Get(int id);
}
public class TestService : ITestService
{
    private readonly ILogger<TestService> _logger;
    public TestService(ILogger<TestService> logger)
    {
        _logger = logger;
    }
    public string Get(int id)
    {
        _logger.LogInformation("TestService.Get({id})", id);
        return "OK";
    }
}
public class AddLogInterceptor : IInterceptor
{
    private readonly ILogger<AddLogInterceptor> _logger;
    public AddLogInterceptor(ILogger<AddLogInterceptor> logger)
    {
        _logger = logger;
    }
    public void Intercept(IInvocation invocation)
    {
        //用invocation可以获取被调用对象和方法的信息
        _logger.LogInformation("开始调用{name}，参数：{args}", invocation.Method.Name, string.Join("，", invocation.Arguments));
        invocation.Proceed();
        _logger.LogInformation("结束调用{name},返回结果：{result}", invocation.Method.Name, invocation.ReturnValue);
    }
}
public static class InterceptedExpansion
{
    public static void AddInterceptedSingleton<TIService, TService, TInterceptor>(this IServiceCollection services)
        where TIService : class
        where TService : class, TIService
        where TInterceptor : class, IInterceptor
    {
        services.TryAddSingleton<IProxyGenerator, ProxyGenerator>();
        services.AddSingleton<TService>();
        services.TryAddTransient<TInterceptor>();
        services.AddSingleton(provider =>
        {
            var proxyGenerator = provider.GetRequiredService<IProxyGenerator>();
            var service = provider.GetRequiredService<TService>();
            var interceptor = provider.GetRequiredService<TInterceptor>();
            return proxyGenerator.CreateInterfaceProxyWithTarget<TIService>(service, interceptor);
        });
    }
}