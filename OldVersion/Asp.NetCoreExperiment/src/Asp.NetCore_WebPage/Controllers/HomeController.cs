using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Asp.NetCore_WebPage.Model.Repository;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Asp.NetCore_WebPage.Model;
using Asp.NetCore_WebPage.Model.生成最大编号;

namespace Asp.NetCore_WebPage.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(ICreateSN createSn)
        {
            var maxyz = createSn.GetSN("YZ");
        }

        [HttpGet]
        public IActionResult Index()
        {

            return View();
        }
        [HttpGet("login")]
        public IActionResult Login()
        {
            // HandleJL<HomeController>(Request.Form,"jl_");
            return View();
        }
        /// <summary>
        /// 取界面参数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="form"></param>
        /// <param name="beforeStr"></param>
        /// <returns></returns>
        List<T> HandleJL<T>(IFormCollection form, string beforeStr) where T : class, new()
        {
            var list = new List<T>();
            foreach (var f in form)
            {
                if (f.Key.Contains(beforeStr))
                {
                    var id = Convert.ToInt32(f.Key.Split(new string[] { "_" }, StringSplitOptions.None)[1]);
                    var type = typeof(T);
                    var t = Activator.CreateInstance(typeof(T)) as T;
                    type.GetProperty("ID").SetValue(t, id);
                    type.GetProperty("Name").SetValue(t, f.Value);
                    list.Add(t);
                }
            }
            return list;
        }
        [HttpPost("login")]
        public IActionResult Login(string username, string password, string validatecode)
        {
            var validateCode = HttpContext.Session.GetString("LoginValidateCode");

            if (!string.IsNullOrEmpty(validateCode) && validateCode == validatecode)
            {
                return Redirect("/");
            }
            else
            {
                ViewData["error"] = "验证不正确";
                return View();
            }
        }
        [HttpGet("error")]
        public IActionResult Error()
        {
            return View();
        }
        /// <summary>
        /// 无权限
        /// </summary>
        /// <returns></returns>
        [HttpGet("nopermission")]
        public IActionResult NoPermission()
        {
            return View();
        }

        /// <summary>
        /// 图形验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("validatecode")]
        public IActionResult ValidateCode([FromServices]VierificationCodeServices _vierificationCodeServices)
        {
            string code = "";
            var ms = _vierificationCodeServices.Create(out code);
            HttpContext.Session.SetString("LoginValidateCode", code);
            Response.Body.Dispose();
            return File(ms.ToArray(), @"image/png");
        }
        /// <summary>
        /// 验证验证码是否正确
        /// </summary>
        /// <param name="yzm"></param>
        /// <returns></returns>
        [HttpGet("validateyzm")]
        public bool ValidateYZM(string yzm)
        {
            return HttpContext.Session.GetString("LoginValidateCode") == yzm;

        }

        [HttpGet("valid")]
        public IActionResult ValidatePage()
        {
            return View();
        }

        [HttpPost("valid")]
        public string ValidatePage(string mobile, string name)
        {
            return $"姓名：{name} 手机：{mobile}";
        }

        [HttpGet("showpdf")]
        public IActionResult ShowPDF()
        {
            return View();
        }
    }
}
