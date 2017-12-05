using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eureka_Steeltoe.Models;
using Steeltoe.Discovery.Eureka;

namespace Eureka_Steeltoe.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        /// <summary>
        /// 获了发现服务里的全部应用
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var _discoveryClient = new DiscoveryClient(new EurekaClientConfig
            {
                EurekaServerServiceUrls = "http://localhost:8080/eureka/V2/",
                ProxyHost = "http://localhost:8080/eureka/V2/",
                ProxyPort = 8080,

            });
            //得到服务中心所有服务和它的Url地址
            foreach (var item in _discoveryClient.Applications.GetRegisteredApplications())
            {
                yield return $"{item.Name}={item.Instances.FirstOrDefault().HomePageUrl}";
            }
        }
    }
}
