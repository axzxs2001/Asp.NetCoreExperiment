using App.Metrics.Health;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace PrometheusDemo01
{
    public class UserServiceHealthCheck : HealthCheck
    {
        
        public UserServiceHealthCheck(string name = "MyHealthCheck") : base(name)
        {            
        }
        protected async override ValueTask<HealthCheckResult> CheckAsync(CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(Startup.ManageUserUrl))
            {
                using (var http = new HttpClient())
                {
                    http.BaseAddress = new Uri(Startup.ManageUserUrl);
                    var response = await http.GetAsync("/health");
                    if (response.IsSuccessStatusCode)
                    {
                        return HealthCheckResult.Healthy("Ok");
                    }
                }
            }
            return HealthCheckResult.Healthy(" Failure");
        }
    }
}
