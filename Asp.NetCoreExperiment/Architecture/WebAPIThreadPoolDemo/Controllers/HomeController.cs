using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPIThreadPoolDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/sync")]
        public string Sync()
        {
            var sw = new Stopwatch();
            sw.Start(); 
            TakeCPU().Wait();
            sw.Stop();
            this._logger.LogInformation($"用时:{sw.ElapsedMilliseconds }ms, 线程数:{ThreadPool.ThreadCount}");
            return "sync";
        }

        [HttpGet("/async")]
        public async Task<string> Async()
        {
            var sw = new Stopwatch();
            sw.Start();
            await TakeCPU(); 
            sw.Stop();
            this._logger.LogInformation($"用时:{sw.ElapsedMilliseconds }ms, 线程数:{ThreadPool.ThreadCount}");
            return "async";
        }

        public async Task TakeCPU()
        {
            var str = "Free. Cross-platform. Open source.A developer platform for building web apps.Free. Cross-platform. Open source.A developer platform for building web apps.Free. Cross-platform. Open source.A developer platform for building web apps.Free. Cross-platform. Open source.A developer platform for building web apps.Free. Cross-platform. Open source.A developer platform for building web apps.Free. Cross-platform. Open source.A developer platform for building web apps.Free. Cross-platform. Open source.A developer platform for building web apps.Free. Cross-platform. Open source.A developer platform for building web apps.";
            for (var i = 0; i < 100000; i++)
            {
                str = await MD5Hash(str);
            }
        }
        public async Task<string> MD5Hash(string input)
        {
            var hash = new StringBuilder();
            var md5provider = new MD5CryptoServiceProvider();
            var bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return await Task.FromResult<string>(hash.ToString());
        }

        [HttpGet("/syncdata")]
        public string SyncData()
        {
            var sw = new Stopwatch();
            sw.Start();
            using var con = new SqlConnection("server=.;database=AdventureWorks2016;uid=sa;pwd=sa;");

            var sql = @$"select* from sales.SalesOrderDetail d join sales.SalesOrderHeader h on h.SalesOrderID= d.SalesOrderID
-- join Production.Product p on d.ProductID= p.ProductID join Sales.Customer c on h.CustomerID= c.CustomerID 
where 1=1 or 'a'!='{DateTime.Now}'";
            var list = con.Query<dynamic>(sql).ToList();

            sw.Stop();
            this._logger.LogInformation($"用时:{sw.ElapsedMilliseconds }ms, 线程数:{ThreadPool.ThreadCount}");
            return "sync" + list.Count;
        }
        [HttpGet("/asyncdata")]
        public async Task<string> AsyncData()
        {
            var sw = new Stopwatch();
            sw.Start();
            using var con = new SqlConnection("server=.;database=AdventureWorks2016;uid=sa;pwd=sa;");
            var sql = @$"select* from sales.SalesOrderDetail d join sales.SalesOrderHeader h on h.SalesOrderID= d.SalesOrderID 
-- join Production.Product p on d.ProductID= p.ProductID join Sales.Customer c on h.CustomerID= c.CustomerID
where 1=1 or 'a'!='{DateTime.Now}'";
            var list = await con.QueryAsync<dynamic>(sql);
            sw.Stop();
            this._logger.LogInformation($"用时:{sw.ElapsedMilliseconds }ms, 线程数:{ThreadPool.ThreadCount}");
            return "async" + list.Count();
        }

    }
}
