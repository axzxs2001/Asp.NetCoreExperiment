using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIVersionDemo.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public Order QueryOrder()
        {
            _logger.LogInformation("查询产品");
            return new Order()
            {
                OrderID = 1,
                Products = new List<Product>()
                {
                    new Product() { ID = 1, Name = "A物品", Price = 100.20m }
                }
            };
        }


        [HttpGet("{id}")]
        [MapToApiVersion("2.0")]
        public Order2 QueryOrder2()
        {
            _logger.LogInformation("查询产品");
            return new Order2()
            {
                OrderID = 1,
                Products = new List<Product2>()
                {
                new Product2() { ID = 1, Name = "A物品", Price = 100.20m, Description = "产自山西" }
                }
            };
        }
    }

    public class Order
    {
        public int OrderID { get; set; }
        public List<Product> Products { get; set; }

    }

    public class Order2
    {
        public int OrderID { get; set; }
        public List<Product2> Products { get; set; }
    }
}
