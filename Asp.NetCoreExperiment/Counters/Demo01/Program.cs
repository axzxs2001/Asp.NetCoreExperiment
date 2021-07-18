
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading;

namespace Demo01
{
    class Program
    {
        static void Main()
        {
            //InProgramDemo.Run();    
            //EventCounterDemo.Run();
            //PollingCounterDemo.Run();
            //IncrementingEventCounterDemo.Run();
            IncrementingPollingCounterDemo.Run();
            Console.Read();
        }
    }
    #region EventCounterDemo
    public class EventCounterDemo
    {
        public static void Run()
        {
            var listener = new WorkingEventListener();
            listener.WriteEvent += Listener_WriteEvent;
            new Thread(Working).Start();
        }
        //以控制台方式展示采集到的指标
        private static void Listener_WriteEvent(string key, string value)
        {
            Console.WriteLine($"{key}：{value}");
        }

        /// <summary>
        /// 模拟写业务指标，每秒写两次，i递增
        /// </summary>
        static void Working()
        {
            int i = 0;
            while (true)
            {
                var count = i;
                Console.WriteLine(count);
                WorkingEventSource.Instance.Working(count);
                System.Threading.Thread.Sleep(500);
                i += 1;
            }
        }



        /// <summary>
        /// Working业务事件源
        /// </summary>
        [EventSource(Name = "WorkingEventSource")]
        public sealed class WorkingEventSource : EventSource
        {
            public static readonly WorkingEventSource Instance = new WorkingEventSource();
            private EventCounter _workingCounter;
            /// <summary>
            /// working-time 是dotnet-counters  --counters的参数
            /// </summary>
            private WorkingEventSource()
            {
                _workingCounter = new EventCounter("working-time", this)
                {
                    DisplayName = "Working Time",
                    DisplayUnits = "ms"
                };
            }
            /// <summary>
            /// Working发送业务指标
            /// </summary>
            /// <param name="elapsedMilliseconds"></param>
            public void Working(long elapsedMilliseconds)
            {
                _workingCounter?.WriteMetric(elapsedMilliseconds);
            }

            protected override void Dispose(bool disposing)
            {
                _workingCounter?.Dispose();
                _workingCounter = null;
                base.Dispose(disposing);
            }
        }


        /// <summary>
        /// 指标输出委托
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public delegate void WriteContent(string key, string value);
        /// <summary>
        /// 指标监听器
        /// </summary>
        public class WorkingEventListener : EventListener
        {
            protected readonly string[] _countersName = new string[] { "WorkingEventSource" };
            public event WriteContent WriteEvent;
            protected override void OnEventSourceCreated(EventSource source)
            {
                if (_countersName.Contains(source.Name))
                {
                    EnableEvents(source, EventLevel.Verbose, EventKeywords.All, new Dictionary<string, string>()
                    {
                        ["EventCounterIntervalSec"] = "1"
                    });
                }
            }
            protected override void OnEventWritten(EventWrittenEventArgs eventData)
            {
                if (eventData.EventName.Equals("EventCounters"))
                {
                    for (int i = 0; i < eventData.Payload.Count; ++i)
                    {
                        if (eventData.Payload[i] is IDictionary<string, object> eventPayload)
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
                            WriteEvent(counterName, counterValue);
                        }
                    }
                }
            }
        }
    }
    #endregion

    #region PollingCounterDemo
    public class PollingCounterDemo
    {
        public static void Run()
        {
            var listener = new WorkingEventListener();
            listener.WriteEvent += Listener_WriteEvent;
            new Thread(Working).Start();
        }
        //以控制台方式展示采集到的指标
        private static void Listener_WriteEvent(string key, string value)
        {
            Console.WriteLine($"{key}：{value}");
        }

        /// <summary>
        /// 模拟写业务指标，每秒写两次，i递增
        /// </summary>
        static void Working()
        {
            int i = 0;
            while (true)
            {
                var count = i;
                Console.WriteLine(count);
                WorkingEventSource.Instance.Working(count);
                System.Threading.Thread.Sleep(500);
                i += 1;
            }
        }

        /// <summary>
        /// Working业务事件源
        /// </summary>
        [EventSource(Name = "WorkingEventSource")]
        public sealed class WorkingEventSource : EventSource
        {
            public static readonly WorkingEventSource Instance = new WorkingEventSource();
            private PollingCounter _workingCounter;

            //间隔内总时间
            private double _elapsedMilliseconds = 0;
            //间隔内次数
            private int _times = 0;
            /// <summary>
            /// working-time 是dotnet-counters  --counters的参数
            /// </summary>
            private WorkingEventSource() =>
                _workingCounter = new PollingCounter("working-time", this, MetricProvider)
                {
                    DisplayName = "Working Time",
                    DisplayUnits = "ms"
                };
            /// <summary>
            /// 自定义函数，来计算指标，本例模拟EventCounter的计算方式
            /// </summary>
            /// <returns></returns>
            double MetricProvider()
            {
                var value = _elapsedMilliseconds / _times;
                _elapsedMilliseconds = 0;
                _times = 0;
                return value;
            }
            /// <summary>
            /// Working发送业务指标
            /// </summary>
            /// <param name="elapsedMilliseconds"></param>
            public void Working(long elapsedMilliseconds)
            {
                _times++;
                _elapsedMilliseconds += elapsedMilliseconds;
            }

            protected override void Dispose(bool disposing)
            {
                _workingCounter?.Dispose();
                _workingCounter = null;
                base.Dispose(disposing);
            }
        }
        /// <summary>
        /// 指标输出委托
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public delegate void WriteContent(string key, string value);
        /// <summary>
        /// 指标监听器
        /// </summary>
        public class WorkingEventListener : EventListener
        {
            protected readonly string[] _countersName = new string[] { "WorkingEventSource" };
            public event WriteContent WriteEvent;
            protected override void OnEventSourceCreated(EventSource source)
            {
                if (_countersName.Contains(source.Name))
                {
                    EnableEvents(source, EventLevel.Verbose, EventKeywords.All, new Dictionary<string, string>()
                    {
                        ["EventCounterIntervalSec"] = "1"
                    });
                }
            }
            protected override void OnEventWritten(EventWrittenEventArgs eventData)
            {
                if (eventData.EventName.Equals("EventCounters"))
                {
                    for (int i = 0; i < eventData.Payload.Count; ++i)
                    {
                        if (eventData.Payload[i] is IDictionary<string, object> eventPayload)
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
                            WriteEvent(counterName, counterValue);
                        }
                    }
                }
            }
        }
    }
    #endregion


    #region IncrementingEventCounterDemo
    public class IncrementingEventCounterDemo
    {
        public static void Run()
        {
            var listener = new CustomEventListener();
            listener.WriteEvent += Listener_WriteEvent;
            new Thread(Working).Start();

        }
        /// <summary>
        ///  以控制台方式展示采集到的指标
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private static void Listener_WriteEvent(string key, string value)
        {
            Console.WriteLine($"{key}：{value}");
        }

        /// <summary>
        /// 模拟写业务指标，每秒写两次，i递增
        /// </summary>
        static void Working()
        {
            int i = 0;
            while (true)
            {
                var count = i;
                Console.WriteLine(count);
                WorkingEventSource.Instance.Working(count);
                System.Threading.Thread.Sleep(500);
                i += 1;
            }
        }


        [EventSource(Name = "WorkingEventSource")]
        public sealed class WorkingEventSource : EventSource
        {
            public static readonly WorkingEventSource Instance = new WorkingEventSource();
            private IncrementingEventCounter _requestCounter;
            /// <summary>
            /// working-time 是dotnet-counters  --counters的参数
            /// </summary>
            private WorkingEventSource() =>
                _requestCounter = new IncrementingEventCounter("working-time", this)
                {
                    DisplayName = "Working Time",
                    DisplayUnits = "ms"
                };
            /// <summary>
            /// Working发送业务指标
            /// </summary>
            /// <param name="elapsedMilliseconds"></param>
            public void Working(long elapsedMilliseconds)
            {
                _requestCounter.Increment(elapsedMilliseconds);
            }

            protected override void Dispose(bool disposing)
            {
                _requestCounter?.Dispose();
                _requestCounter = null;
                base.Dispose(disposing);
            }
        }
        /// <summary>
        /// 指标输出委托
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public delegate void WriteContent(string key, string value);
        /// <summary>
        /// 指标监听器
        /// </summary>
        public class CustomEventListener : EventListener
        {
            protected readonly string[] _countersName = new string[] { "WorkingEventSource" };

            public event WriteContent WriteEvent;
            protected override void OnEventSourceCreated(EventSource source)
            {
                if (_countersName.Contains(source.Name))
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
                        WriteEvent(counterName, counterValue);
                    }
                }
            }
        }
    }
    #endregion

    #region IncrementingPollingCounterDemo
    public class IncrementingPollingCounterDemo
    {
        public static void Run()
        {
            var listener = new CustomEventListener();
            listener.WriteEvent += Listener_WriteEvent;
            new Thread(Working).Start();

        }
        /// <summary>
        ///  以控制台方式展示采集到的指标
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private static void Listener_WriteEvent(string key, string value)
        {
            Console.WriteLine($"{key}：{value}");
        }
        /// <summary>
        /// 模拟写业务指标，每秒写两次，i递增
        /// </summary>
        static void Working()
        {
            int i = 0;
            while (true)
            {
                var count = i;
                Console.WriteLine(count);
                WorkingEventSource.Instance.Working(count);
                System.Threading.Thread.Sleep(500);
                i += 1;
            }
        }

        /// <summary>
        /// 自定义事件源
        /// </summary>
        [EventSource(Name = "WorkingEventSource")]
        public sealed class WorkingEventSource : EventSource
        {
            public static readonly WorkingEventSource Instance = new WorkingEventSource();

            private IncrementingPollingCounter _requestCounter;

            private WorkingEventSource() =>
                _requestCounter = new IncrementingPollingCounter("working-time", this, MetricProvider)
                {
                    DisplayName = "Working Time",
                    DisplayUnits = "ms"
                };
            private double _elapsedMilliseconds = 0;
            /// <summary>
            /// 自定义函数，来计算指标，本例模拟EventCounter的计算方式
            /// </summary>
            /// <returns></returns>
            double MetricProvider()
            {
                return _elapsedMilliseconds;
            }
            /// <summary>
            /// Working发送业务指标
            /// </summary>
            /// <param name="elapsedMilliseconds"></param>
            public void Working(long elapsedMilliseconds)
            {
                _elapsedMilliseconds += elapsedMilliseconds;
            }

            protected override void Dispose(bool disposing)
            {
                _requestCounter?.Dispose();
                _requestCounter = null;
                base.Dispose(disposing);
            }
        }
        /// <summary>
        /// 指标输出委托
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public delegate void WriteContent(string key, string value);
        /// <summary>
        /// 指标监听器
        /// </summary>
        public class CustomEventListener : EventListener
        {
            protected readonly string[] _countersName = new string[] { "WorkingEventSource" };

            public event WriteContent WriteEvent;
            protected override void OnEventSourceCreated(EventSource source)
            {
                if (_countersName.Contains(source.Name))
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
                        WriteEvent(counterName, counterValue);
                    }
                }
            }
        }
    }
    #endregion




    #region InProgramDemo
    public class InProgramDemo
    {
        public static void Run()
        {
            Console.WriteLine("监控开始");
            var listener = new MyEventListener();
            listener.WriteEvent += Listener_WriteEvent;
            Console.ReadLine();
        }
        /// <summary>
        ///  以控制台方式展示采集到的指标
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private static void Listener_WriteEvent(string key, string value)
        {
            Console.WriteLine($"{key}：{value}");
        }

        public delegate void WriteContent(string key, string value);
        public class MyEventListener : EventListener
        {
            protected readonly string[] _countersName = new string[]
            {
            "System.Runtime"
            };
            public event WriteContent WriteEvent;
            protected override void OnEventSourceCreated(EventSource source)
            {
                if (_countersName.Contains(source.Name))
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
                        WriteEvent(counterName, counterValue);
                    }
                }
            }
        }
    }
    #endregion
}
