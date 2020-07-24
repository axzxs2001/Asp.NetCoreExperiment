using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SnapshotDemo.Models;

namespace SnapshotDemo.Controllers
{
    public class HomeController : Controller
    {
        IConfiguration _con;
        public HomeController(IConfiguration con)
        {
            _con = con;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            //读取配置
            ViewData["Message"] = "Your application description page." + _con.GetSection("appsetting").GetSection("par1").Value;

            return View();
        }

        public IActionResult Contact()
        {

            var kv = new ConsulSharp.KV.KVGovern();
            var result = kv.ReadKey(new ConsulSharp.KV.ReadKeyParmeter { DC = "dc1", Key = "keyname" }).GetAwaiter().GetResult();
            var decodeValue = result[0].DecodeValue;
            ViewData["Message"] = "Your contact page.";
            //写入配置的值
            _con.GetSection("appsetting").GetSection("par1").Value = DateTime.Now.ToString("yyyyMMddHHmmssfff")+ decodeValue;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
