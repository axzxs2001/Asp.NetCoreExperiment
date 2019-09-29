using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Demo001.Models;
using Npgsql;
using System.Text;

namespace Demo001.Controllers
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
        /// <summary>
        /// 2、SQl注放
        /// </summary>
        /// <returns></returns>
        public IActionResult SqlInjection(string p)
        {
            using (var con = new NpgsqlConnection("连接字符串"))
            {
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = con;

                    #region 2.1
                    //2.1 不正确的
                    //cmd.CommandText = "select * from table1 where id=" + p;
                    //2.1 正确的
                    cmd.CommandText = "select * from table1 where id=@p";
                    cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "@p", Value = p });
                    #endregion
                    #region 2.2                  
                    //2.2 拼接
                    var sql2 = new StringBuilder("select * from table1");
                    sql2.Append(" where id=2");
                    cmd.CommandText = sql2.ToString();
                    cmd.Parameters.Add(new NpgsqlParameter { ParameterName = "@p", Value = p });
                    #endregion


                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
