using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GRPCDemo01Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GRPCDemo01WebTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {


        private readonly ILogger<WeatherForecastController> _logger;
        private readonly Greeter.GreeterClient _client;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, Greeter.GreeterClient client)
        {
            _client = client;
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            var tokenResponse = await _client.LoginAsync(new UserRequest { Username = "gsw", Password = "111111" });
            var token = $"Bearer {tokenResponse.Token }";
            var headers = new Metadata { { "Authorization", token } };
            var request = new HelloRequest { Name = "桂素伟" };
            var call = await _client.SayHelloAsync(request, headers);
            return call.Message;
        }
    }
}
