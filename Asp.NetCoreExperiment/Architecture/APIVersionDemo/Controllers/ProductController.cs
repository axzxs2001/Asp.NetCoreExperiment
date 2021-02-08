using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIVersionDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public Product QueryProduct([FromRoute] int id)
        {
            _logger.LogInformation("查询产品");
            return new Product() { ID = id, Name = "A物品", Price = 100.20m };
        }


        [HttpGet("{id}")]
        [MapToApiVersion("2.0")]
        public Product QueryProductv2([FromRoute] int id)
        {
            _logger.LogInformation("查询产品");
            return new Product2() { ID = id, Name = "A物品", Price = 100.20m, Description = "产自山西" };

        }

        private void F(string s)
        {

        }
    }

    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Product2 : Product
    {
        public string Description { get; set; }
    }
}
