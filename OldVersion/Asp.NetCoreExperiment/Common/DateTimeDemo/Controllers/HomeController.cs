using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DateTimeDemo.Models;

namespace DateTimeDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("/items")]
        public IActionResult GetImtes()
        {

            var serSettings = new Newtonsoft.Json.JsonSerializerSettings()
            {
                //DateParseHandling= Newtonsoft.Json.DateParseHandling.DateTimeOffset,
                //DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc,
                //DateFormatString = "yyyy-MM-dd HH:mm:ss"

            };

            return Json(new List<Item> {
                new Item { ID=1, Name="中国", CreateTime=DateTimeOffset.UtcNow } ,
                new Item { ID =2, Name = "日本", CreateTime = DateTimeOffset.UtcNow}
            }, serSettings);
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
    }

    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public DateTimeOffset CreateTime { get; set; }
    }
}
