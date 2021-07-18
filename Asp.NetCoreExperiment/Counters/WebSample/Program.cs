using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var l = new SimpleEventListener();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

public class SimpleEventListener : EventListener
{
    public SimpleEventListener()
    {
    }

    protected override void OnEventSourceCreated(EventSource source)
    {
        //source.Name.Equals("System.Runtime") ||
        if (source.Name.Equals("Microsoft.AspNetCore.Hosting"))
        {
            EnableEvents(source, EventLevel.Verbose, EventKeywords.All, new Dictionary<string, string>()
            {
                ["EventCounterIntervalSec"] = "1"
            });
        }
    }

    protected override void OnEventWritten(EventWrittenEventArgs eventData)
    {
        if (!eventData.EventName.Equals("EventCounters"))
        {
            return;
        }
 
        for (int i = 0; i < eventData.Payload.Count; ++i)
        {
            if (eventData.Payload[i] is IDictionary<string, object> eventPayload)
            {
                var (counterName, counterValue) = GetRelevantMetric(eventPayload);
                Console.WriteLine($"{counterName} : {counterValue}");
            }
        }
    }

    private static (string counterName, string counterValue) GetRelevantMetric(
        IDictionary<string, object> eventPayload)
    {
        var counterName = "";
        var counterValue = "";

        if (eventPayload.TryGetValue("DisplayName", out object displayValue))
        {
            counterName = displayValue.ToString();
        }
        if (eventPayload.TryGetValue("Mean", out object value) ||
            eventPayload.TryGetValue("Increment", out value))
        {
            counterValue = value.ToString();
        }

        return (counterName, counterValue);
    }
}