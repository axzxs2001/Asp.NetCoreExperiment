using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCDemo.Models;

namespace MVCDemo.Controllers
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

        [HttpGet("addblog")]
        public IActionResult AddBlog()
        {
            return View();
        }
        [HttpPost("addblog")]
        public IActionResult AddBlog(BlogViewModel blog)
        {
            return View();
        }
        [HttpGet("editblog")]
        public IActionResult EditBlog(string id)
        {
            return View(new BlogViewModel {  Title = "第一篇", Author = "桂素伟", ViewTimes = 10, Content = "内容内容内容内容内容内容内容" });
        }
        [HttpPost("editblog")]
        public IActionResult EditBlog(BlogViewModel blog)
        {
            return View();
        }
        [HttpGet("deleteblog")]
        public IActionResult DeleteBlog(string id)
        {
            return View(new BlogViewModel { Title = "第一篇", Author = "桂素伟", ViewTimes = 10, Content = "内容内容内容内容内容内容内容" });
        }
        [HttpPost("deleteblog")]
        public IActionResult DeleteBlog(BlogViewModel blog)
        {
            return View();
        }
        [HttpGet("detailblog")]
        public IActionResult DetailBlog(string id)
        {
            return View(new BlogViewModel { Title = "第一篇", Author = "桂素伟", ViewTimes = 10, Content = "内容内容内容内容内容内容内容" });
        }
        [HttpGet("blogs")]
        public IActionResult Blogs()
        {
            var blogs = new List<BlogViewModel>()
            {
               new BlogViewModel{ Title="第一篇", Author="桂素伟", ViewTimes=10, Content="内容内容内容内容内容内容内容"},
               new BlogViewModel{ Title="第二篇", Author="桂素伟", ViewTimes=10, Content="内容内容内容内容内容内容内容"},
               new BlogViewModel{ Title="第三篇", Author="桂素伟", ViewTimes=10, Content="内容内容内容内容内容内容内容"},
               new BlogViewModel{ Title="第四篇", Author="桂素伟", ViewTimes=10, Content="内容内容内容内容内容内容内容"},

            };
            return View(blogs);
        }
    }
}
