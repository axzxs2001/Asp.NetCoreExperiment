using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace OneOfDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/product/{id}")]
        public IActionResult Get(int id)
        {
            OneOf<Product, NotFound, SystemError> result = GetProject(id);
            return result.Match<IActionResult>(
                product =>
                {
                    _logger.LogInformation("查询成功");
                    return new JsonResult(product);
                },
                notfound =>
                {
                    _logger.LogInformation("没有查到");
                    return new NotFoundResult();
                },
                systemerror =>
                {
                    _logger.LogError("查询成败");
                    return new StatusCodeResult(500);
                });
        }
        /// <summary>
        /// 按ID查询产品，有三种返回类型，找到产品返回；按ID查询不到；查询过程发生错误
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OneOf<Product, NotFound, SystemError> GetProject(int id)
        {
            try
            {
                //这里实现真实查库
                var num = RandomNumberGenerator.GetInt32(1, 10);
                if (num % 3 == 0)
                {
                    return new NotFound();
                }
                else
                {
                    return new Product() { ID = id, Name = "手机" };
                }
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new SystemError();
            }
        }
    }

    public class NotFound
    {
    }
    public class SystemError
    {
    }
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
