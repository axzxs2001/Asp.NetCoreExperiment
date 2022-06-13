using System.Diagnostics.Contracts;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IOrderServgice, OrderService>();


var app = builder.Build();


app.MapGet("/test/{name}", (IOrderServgice orderServgice,string name) =>
{
    var r = orderServgice.AddOrder(name);
    return r.Match(
         s =>
         {

             return TypedResults.Ok();
         },
         f =>
         {
             return TypedResults.StatusCode(500);
         });

});

app.Run();



public interface IOrderServgice
{
    Result<bool?> AddOrder(string name);
}
public class OrderService : IOrderServgice
{
    public Result<bool?> AddOrder(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {

            return new Result<bool?>(new Exception("abcde"));
        }
        else
        {
            return new Result<bool?>(true);
        }
    }
}

public class Result<T>
{  
    private readonly Exception? _exception;
    private readonly T _value;

    public Result(T value)
    {
        _value = value;
        _exception = null;
    }
    public Result(Exception exception)
    {
        _exception = exception;
        _value = default(T);
    }

    public static implicit operator Result<T>(T value) => new Result<T>(value);

    public static implicit operator Result<T>(Exception exception) => new Result<T>(exception);

    public IResult Match(Func<T, IResult> onSuccess, Func<Exception, IResult> onFailure)
    {
        if (_value != null)
        {
            return onSuccess(_value);
        }
        else
        {
            return onFailure(_exception);
        }
    }

}