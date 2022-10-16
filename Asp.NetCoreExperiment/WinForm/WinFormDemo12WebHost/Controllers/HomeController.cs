using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Diagnostics;
using WinFormDemo12WebHost.Models;

namespace WinFormDemo12WebHost.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Goods
        public IActionResult Goods()
        {
            return View();
        }
        static List<GoodsType> _goodsTypes = new List<GoodsType> {
            new GoodsType {ID=1,Name="A类型" },
            new GoodsType {ID=2,Name="B类型" },
        };
        static List<Goods> _goodses = new List<Goods>
        {

        };
        [HttpGet("/home/goodses")]
        public async Task<JsonResult?> QueryGoodsesAsync()
        {
            try
            {
                _logger.LogInformation("BackQuery Goods List");
                var goodsTypes = _goodsTypes;
                var goodses = _goodses;

                return new JsonResult(
                    new
                    {
                        Data = new
                        {
                            GoodsTypes = goodsTypes,
                            Goodses = goodses
                        }
                    });
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
        [HttpDelete("/home/goods")]
        public async Task<JsonResult?> DeleteGoodsAsync(int id)
        {
            try
            {
                _logger.LogInformation("delete goods");
                var result = _goodses.Remove(_goodses.SingleOrDefault(s => s.ID == id));
                return new JsonResult(
                    new
                    {
                        Result = result
                    });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { Result = false, Message = exc.Message });
            }
        }
        [HttpPut("/home/goods")]
        public async Task<JsonResult?> ModifyGoodsAsync(Goods goods)
        {
            try
            {
                _logger.LogInformation("modify goods");
                _goodses.Remove(_goodses.SingleOrDefault(s => s.ID == goods.ID));
                goods.ID = _goodses.Max(s => s.ID) + 1;
                _goodses.Add(goods);
                return new JsonResult(
                    new
                    {
                        Result = goods
                    });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { Result = false, Message = exc.Message });
            }
        }
        [HttpPost("/home/goods")]
        public async Task<JsonResult?> AddGoodstAsync(Goods goods)
        {
            try
            {
                _logger.LogInformation("add goods");
                goods.ID = _goodses.Count > 0 ? _goodses.Max(s => s.ID) + 1 : 1;
                _goodses.Add(goods);
                return new JsonResult(
                    new
                    {
                        Result = true,
                        Data = goods
                    });
            }
            catch (Exception exc)
            {
                _logger.LogCritical(exc, exc.Message);
                return new JsonResult(new { Result = false, Message = exc.Message });
            }
        }
        #endregion
    }
    public class GoodsType
    {
        public int ID { get; set; }
        public string? Name { get; set; }
    }
    public class Goods
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Describe { get; set; }
        public bool Validate { get; set; }

        public int GoodsTypeID { get; set; }

        public int SerialNumber { get; set; }
        public string GoodsType { get; set; }
        public decimal MaxPrice
        {
            get
            {
                return Price >= 70000 ? Price + 3000 : (Price > 0 ? Price + 2000 : 0);
            }
        }
        public decimal MinPrice
        {
            get
            {
                return Price >= 70000 ? Price - 3000 : (Price > 2000 ? Price - 2000 : 0);
            }
        }
    }
}