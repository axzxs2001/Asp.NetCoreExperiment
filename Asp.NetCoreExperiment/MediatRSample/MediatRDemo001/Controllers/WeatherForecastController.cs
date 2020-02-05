using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MediatRDemo001.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
   
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMediator _mediator;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        public void Get()
        {
            _mediator.Publish(new Ping());
        }
    }
}
