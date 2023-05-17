using CustomLoogerDemo;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Logging.ClearProviders();
builder.Services.AddControllers();
builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, MyProvider>());
builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IMyLogger, MyLogger>());
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public interface IMyLogger : ILogger
{
    //void Log<TState>(string category, LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter);
    void Log(string category, LogLevel logLevel, string message);
}

public class MyProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        return new MyLogger();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
}

public class MyLogger : IMyLogger
{

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return NoopDisposable.Instance;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }
        if (formatter == null)
        {
            throw new ArgumentNullException(nameof(formatter));
        }
        string message = formatter(state, exception);
        if (string.IsNullOrEmpty(message))
        {
            return;
        }
        Console.WriteLine(message);

    }
    public void Log(string category, LogLevel logLevel, string message)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        if (string.IsNullOrEmpty(message))
        {
            return;
        }
        Console.WriteLine(category + " -- " + "----" + message);

    }
    private class NoopDisposable : IDisposable
    {
        public static NoopDisposable Instance = new NoopDisposable();

        public void Dispose()
        {
        }
    }
}