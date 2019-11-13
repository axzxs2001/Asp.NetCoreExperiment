using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LogEntity
{
    public class StarPayLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var log = new Log { Level = logLevel.ToString(), Message = state.ToString() };
            var logstr = JsonSerializer.Serialize(log);
            Console.WriteLine(logstr);
        }
    }


    public class StarPayLogger<T> : ILogger<T>
    {

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var log = new Log { Level = logLevel.ToString(), Message = state.ToString() };
            var logstr = JsonSerializer.Serialize(log);


            var v = state as IReadOnlyList<KeyValuePair<string, object>>;
            var key = v[0].Key;
            var value = v[0].Value;

            var message = formatter((TState)v, exception);

        }
    }

    public class StarPayProvider : ILoggerProvider
    {

        private readonly ConcurrentDictionary<string, StarPayLogger> _loggers = new ConcurrentDictionary<string, StarPayLogger>();



        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new StarPayLogger());
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }

}
