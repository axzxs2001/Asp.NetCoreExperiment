using Microsoft.AspNetCore.Http;
using PrometheusSample.Models;
using System.IO;
using System.Threading.Tasks;

namespace PrometheusSample.Middlewares
{
    /// <summary>
    /// 请求记录中间件
    /// </summary>
    public class BusinessMetricsMiddleware
    {
        private readonly RequestDelegate _next;
        public BusinessMetricsMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, MetricsHub metricsHub)
        {
            var originalBody = context.Response.Body;
            try
            {
                using (var memStream = new MemoryStream())
                {
                    //从管理返回的Response中取出返回数据，根据返回值进行监控指标计数
                    context.Response.Body = memStream;
                    await _next(context);

                    memStream.Position = 0;
                    string responseBody = new StreamReader(memStream).ReadToEnd();
                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                    if (metricsHub.GetCounter(context.Request.Path) != null || metricsHub.GetGauge(context.Request.Path) != null)
                    {
                        //这里约定所有action返回值是一个APIResult类型
                        var result = System.Text.Json.JsonSerializer.Deserialize<APIResult>(responseBody, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (result != null && result.Result)
                        {
                            //获取到Counter
                            var counter = metricsHub.GetCounter(context.Request.Path);
                            if (counter != null)
                            {
                                //计数
                                counter.Inc();
                            }

                            var gauges = metricsHub.GetGauge(context.Request.Path);
                            if (gauges != null)
                            {
                                //存在增加指标+就Inc
                                if (gauges.ContainsKey("+"))
                                {
                                    gauges["+"].Inc();
                                } 
                                //存在减少指标-就Dec
                                if (gauges.ContainsKey("-"))
                                {
                                    gauges["-"].Dec();
                                }
                            }

                            var summary = metricsHub.GetSummary(context.Request.Path);
                            if (summary != null)
                            {
                                var parseResult = int.TryParse(result.Data.ToString(), out int i);
                                if (parseResult)
                                {
                                    summary.Observe(i);
                                }
                            }


                            var histogram = metricsHub.GetHistogram(context.Request.Path);
                            if (histogram != null)
                            {
                                var parseResult = int.TryParse(result.Data.ToString(), out int i);
                                if (parseResult)
                                {
                                    histogram.Observe(i);
                                }
                            }
                        }
                    }

                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }

        }
    }
}
