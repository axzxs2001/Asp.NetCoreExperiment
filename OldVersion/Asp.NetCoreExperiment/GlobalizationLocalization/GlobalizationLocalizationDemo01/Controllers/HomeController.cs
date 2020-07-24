using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GlobalizationLocalizationDemo01.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

namespace GlobalizationLocalizationDemo01.Controllers
{
    public class HomeController : Controller
    {
        readonly IStringLocalizer<HomeController> _localizer;
        readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public HomeController(IStringLocalizer<HomeController> localizer,IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _localizer = localizer;
            _sharedLocalizer = sharedLocalizer;

        }
        public IActionResult Index()
        {

            ViewData["ok"] = _sharedLocalizer["ok"].Value;
            ViewData["no"] = _sharedLocalizer["no"].Value;
            ViewData["cancel"] = _sharedLocalizer["cancel"].Value;
            var content = _localizer["controller_test"].Value;
            return View(model: content);
        }

        [HttpPost]
        public IActionResult Index(string culture, string returnUrl = "/")
        {
            Response.Cookies.Append(
                           CookieRequestCultureProvider.DefaultCookieName,
                           CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                           new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return LocalRedirect(returnUrl);
        }


        public IActionResult About()
        {
            ViewData["Message"] = _localizer["about"].Value;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = _localizer["contact"].Value;

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
