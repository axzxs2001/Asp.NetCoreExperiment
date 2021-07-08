
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;


namespace Demo01
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var l = new SimpleEventListener();
            l.EventWritten += L_EventWritten;
            l.EventSourceCreated += L_EventSourceCreated;
    
            Console.WriteLine("Hello World!");

            Console.ReadLine();
        }

        private static void L_EventSourceCreated(object sender, EventSourceCreatedEventArgs e)
        {
            Console.WriteLine($"{e.EventSource.Name} ********  ");
        }

        private static void L_EventWritten(object sender, EventWrittenEventArgs e)
        {
            Console.WriteLine($"{e.EventSource.Name} ********  {e.Message} ");
        }
    }

    public class SimpleEventListener : EventListener
    {
        public SimpleEventListener()
        {
        }

        protected override void OnEventSourceCreated(EventSource source)
        {
            if (!source.Name.Equals("System.Runtime"))
            {
                return;
            }

            EnableEvents(source, EventLevel.Verbose, EventKeywords.All, new Dictionary<string, string>()
            {
                ["EventCounterIntervalSec"] = "1"
            });
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
}
