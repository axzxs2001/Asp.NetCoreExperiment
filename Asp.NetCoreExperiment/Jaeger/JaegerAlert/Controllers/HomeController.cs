using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JaegerAlert.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<AlertList>> Get()
        {
            _logger.LogInformation("获取警报列表");
            return await GetServices();
        }
        /// <summary>
        /// 获取所有服务
        /// </summary>
        /// <returns></returns>
        async Task<IEnumerable<AlertList>> GetServices()
        {
            var service = await GetJaegerServices();
            var services = new List<AlertList>();
            foreach (var serviceName in service.Data)
            {
                if (serviceName == "jaeger-query")
                {
                    continue;
                }
                var alerts = new List<AlertItem>();
                var tracesModels = await GetJaegerTraces(serviceName);
                foreach (var traces in tracesModels.Data)
                {
                    foreach (var span in traces.Spans)
                    {
                        if (span.IsAlertMark)
                        {
                            var method = span.Tags.SingleOrDefault(s => s.Key == "http.method")?.Value;
                            var operation = span.Tags.SingleOrDefault(s => s.Key == "http.url")?.Value;
                            alerts.Add(new AlertItem { TraceID = traces.TraceID, Duration = span.Duration, Method = method, Operation = operation, StartTime = span.StartTime });
                        }
                    }
                }
                services.Add(new AlertList() { ServiceName = serviceName, Alerts = alerts });
            }
            return services;
        }

        /// <summary>
        /// 获取服务下的跟踪条目
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        async Task<TracesData> GetJaegerTraces(string serviceName)
        {
            using var client = _clientFactory.CreateClient("Jaeger");
            var request = new HttpRequestMessage(HttpMethod.Get, $"/api/traces?service={serviceName}");

            using var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var traces = Newtonsoft.Json.JsonConvert.DeserializeObject<TracesData>(jsonString);
                return traces;
            }
            else
            {
                return new TracesData();
            }
        }
        /// <summary>
        /// 获取服务
        /// </summary>
        /// <returns></returns>
        async Task<ServicesData> GetJaegerServices()
        {
            using var client = _clientFactory.CreateClient("Jaeger");
            var request = new HttpRequestMessage(HttpMethod.Get, "/api/services");
            using var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var service = Newtonsoft.Json.JsonConvert.DeserializeObject<ServicesData>(jsonString);
                return service;
            }
            else
            {
                return new ServicesData();
            }
        }
    }   
}