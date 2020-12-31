using System.Collections.Generic;

namespace JaegerAlert
{
    /// <summary>
    /// 服务报警
    /// </summary>
    public class AlertList
    {
        public string ServiceName { get; set; }
        public List<AlertItem> Alerts { get; set; }
    }
    /// <summary>
    /// 报警条目
    /// </summary>
    public class AlertItem
    {
        public string TraceID { get; set; }
        public long StartTime { get; set; }

        public long Duration { get; set; }

        public string Method { get; set; }

        public string Operation { get; set; }
    }

    /// <summary>
    /// 服务数据
    /// </summary>
    public class ServicesData
    {
        public string[] Data { get; set; }
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
    }

    /// <summary>
    /// 跟踪数据
    /// </summary>
    public class TracesData
    {
        public TracesItem[] Data { get; set; }
        public int Total { get; set; }
        public int Limit { get; set; }
        public int Offset { get; set; }
    }
    /// <summary>
    /// 跟踪条目
    /// </summary>
    public class TracesItem
    {
        public string TraceID { get; set; }

        public Span[] Spans { get; set; }
    }
    /// <summary>
    /// Span
    /// </summary>
    public class Span
    {
        public string TraceID { get; set; }
        public string SpanID { get; set; }
        public bool IsAlertMark => TraceID == SpanID;
        public int Flags { get; set; }
        public string OperationName { get; set; }
        public long StartTime { get; set; }

        public long Duration { get; set; }
        public Tag[] Tags { get; set; }
    }
    /// <summary>
    /// Tag
    /// </summary>
    public class Tag
    {
        public string Key { get; set; }
        public string Type { get; set; }

        public string Value { get; set; }
    }
}