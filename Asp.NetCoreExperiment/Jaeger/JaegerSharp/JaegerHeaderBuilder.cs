using OpenTracing.Propagation;
using OpenTracing.Util;
using System.Collections.Generic;

namespace JaegerSharp
{
    /// <summary>
    /// 获取Header
    /// </summary>
    public class JaegerHeaderBuilder
    {
        public static Dictionary<string, string> CreateInjectTracingSpanHeader()
        {
            var headers = new Dictionary<string, string>();
            var carrier = new TextMapInjectAdapter(headers);
            GlobalTracer.Instance.Inject(GlobalTracer.Instance.ActiveSpan.Context, BuiltinFormats.HttpHeaders, carrier);
            return headers;
        }
    }
}
