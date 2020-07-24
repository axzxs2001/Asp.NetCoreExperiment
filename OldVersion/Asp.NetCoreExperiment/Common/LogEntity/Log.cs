using System;
using System.Reflection;

namespace LogEntity
{
    public class Log
    {
        /// <summary>
        /// log生成的时间，ISO 8601格式
        /// </summary>
        public DateTimeOffset DateTime { get { return DateTimeOffset.Now; } }
        /// <summary>
        ///  log级别，有效的级别是：INFO，WARN，ERROR, FATAL
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ServiceName { get { return Assembly.GetEntryAssembly().GetName().Name; } }
        /// <summary>
        /// 主机名
        /// </summary>
        public string HostName { get { return Environment.MachineName; } }
        /// <summary>
        /// 线程id
        /// </summary>
        public int ThreadId { get { return System.Threading.Thread.CurrentThread.ManagedThreadId; } }

        /// <summary>
        /// 日志消息
        /// </summary>
        public string Message { get; set; }
    }

    public struct LogLevel
    {
        public static string INFO = "INFO";
        public static string WARN = "WARN";
        public static string ERROR = "ERROR";
        public static string FATAL = "FATAL";


    }
}
