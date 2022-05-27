using Microsoft.AspNetCore.Mvc.Filters;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<MyFilter1>();
builder.Services.AddScoped<MyFilter2>();
var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();




public class MyFilter1 : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("MyFilter1 Action前执行");
        await next();
        Console.WriteLine("MyFilter1 Action后执行");
    }
}

public class MyFilter2 : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Console.WriteLine("MyFilter2 Action前执行");
        await next();
        Console.WriteLine("MyFilter2 Action后执行");
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class GSWFilterAttribute<TFilter> : Attribute, IFilterFactory, IOrderedFilter where TFilter : IAsyncActionFilter
{
    public bool IsReusable { get; set; }

    public int Order { get; set; }

    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        if (serviceProvider != null)
        {
            var filter = (IFilterMetadata)serviceProvider.GetRequiredService(typeof(TFilter));
            if (filter is IFilterFactory filterFactory)
            {
                filter = filterFactory.CreateInstance(serviceProvider);
            }
            return filter;
        }
        else
        {
            throw new ArgumentNullException(nameof(serviceProvider));
        }
    }
}