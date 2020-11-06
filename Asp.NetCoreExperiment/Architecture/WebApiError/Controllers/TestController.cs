using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiError.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private readonly ILogger<TestController> _logger;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var ran = new Random();
            switch (ran.Next(1, 4))
            {
                case 1:
                    int i = 0;
                    var j = 10 / i;
                    return Ok();
                case 2:
                    throw new RegisteredException("这是一个错误");
                default:
                    return Ok();
            }

        }
        //--1、UseExceptionHandler方式
        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("/adduser")]
        public IActionResult AddUser([FromBody] UserModel user)
        {
            return Ok(user);
        }

        //--1、UseExceptionHandler方式
        /// <summary>
        /// 错误处理页
        /// </summary>  
        /// <returns></returns>
        [HttpGet("/error")]
        public IActionResult Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            //如果是业务自定义异常，进行特殊处理
            if (context.Error is DaMeiException)
            {
                return Problem(detail: context.Error.StackTrace, title: $"{context.Error.Message}", type: "HIS");
            }
            else
            {
                return Problem(detail: context.Error.StackTrace, title: context.Error.Message);
            }
        }
    }

}
