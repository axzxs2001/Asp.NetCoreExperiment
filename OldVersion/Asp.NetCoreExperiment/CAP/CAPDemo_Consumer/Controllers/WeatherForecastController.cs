using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CAPDemo_Consumer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ISubscriberService _subscriberService;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ISubscriberService subscriberService)
        {
            _subscriberService = subscriberService;
            _logger = logger;
        }

        [HttpGet]
        public bool Get()
        {
            return true;
           
        }
    }
    public interface ISubscriberService
    {
        public void CheckReceivedMessage(string json);
    }

    public class SubscriberService : ISubscriberService, ICapSubscribe
    {
        [CapSubscribe("xxx.services.show.time")]
        public void CheckReceivedMessage(string json)
        {
            Console.WriteLine($"消费：{json}");
        }
    }
}
