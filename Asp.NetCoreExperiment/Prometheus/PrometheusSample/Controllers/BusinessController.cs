using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PrometheusSample.Models;
using PrometheusSample.Services;
using System;
using System.Threading.Tasks;

namespace PrometheusSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinessController : ControllerBase
    {

        private readonly ILogger<BusinessController> _logger;
        private readonly IOrderService _orderService;

        public BusinessController(ILogger<BusinessController> logger, IOrderService orderService)
        {
            _orderService = orderService;
            _logger = logger;

        }



        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="username">用户名</param>
        /// <returns></returns>
        [HttpPost("/register")]
        public async Task<IActionResult> RegisterUser([FromBody] User user)
        {
            try
            {
                _logger.LogInformation("用户注册");
                var result = await _orderService.Register(user.UserName);
                if (result)
                {
                    return new JsonResult(new { Result = true });
                }
                else
                {
                    return new JsonResult(new { Result = false });
                }
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { Result = false, Message = exc.Message });
            }
        }

        [HttpGet("/order")]
        public IActionResult Order(string orderno)
        {
            try
            {
                _logger.LogInformation("下单");
                //返回订单金额
                var random = new Random();
                return new JsonResult(new { Result = true, data = random.Next(1, 8000) });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new
                {
                    Result = false,
                    Message = exc.Message
                });
            }
        }
        [HttpGet("/pay")]
        public IActionResult Pay()
        {
            try
            {
                _logger.LogInformation("支付");
                return new JsonResult(new { Result = true });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { Result = false, Message = exc.Message });
            }
        }
        [HttpGet("/ship")]
        public IActionResult Ship()
        {
            try
            {
                _logger.LogInformation("发货");
                return new JsonResult(new { Result = true });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { Result = false, Message = exc.Message });
            }
        }
    }
}
