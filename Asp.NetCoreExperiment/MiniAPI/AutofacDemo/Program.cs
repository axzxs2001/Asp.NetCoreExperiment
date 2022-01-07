using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);
//¸ü»»ÈÝÆ÷
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(containerBuilder =>
{
    containerBuilder.RegisterType<TransientService>().As<ITransientService>();
    containerBuilder.RegisterType<TestProperty>().As<ITestProperty>().PropertiesAutowired();
}));


var app = builder.Build();

app.MapGet("/transient", (ITransientService transientService) => transientService.Call());
app.MapGet("/testproperty", (ITestProperty testProperty) =>
{
    return testProperty.TestPro.Call();
});
app.Run();


public interface ITransientService
{
    string Call();
}
public class TransientService : ITransientService
{
    public DateTime Time { get; init; } = DateTime.Now;
    public string Call()
    {
        return $"TransientService {Time.ToString("yyyy-MM-dd HH:mm:ss.fffffff")} test¡­¡­";
    }
}

public interface ITestProperty
{
    ITransientService? TestPro { get; set; }
}
public class TestProperty : ITestProperty
{
    public ITransientService? TestPro { get; set; }
}