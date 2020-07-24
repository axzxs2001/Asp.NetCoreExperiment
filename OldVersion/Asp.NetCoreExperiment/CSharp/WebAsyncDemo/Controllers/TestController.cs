using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Dapper;

namespace WebAsyncDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {

        private readonly ILogger<TestController> _logger;
        private readonly string _connectionString;
        public TestController(ILogger<TestController> logger, IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
            _logger = logger;
        }

        [HttpGet("/test1")]
        public async Task<string> Test1()
        {
            await Query(() =>
            {
                Console.WriteLine("Test1完成");
            });
            return "ok";
        }
        [HttpGet]
        public string Get()
        {
            _ = Query(() =>
            {
                Console.WriteLine("Get完成");
            });
            return "ok";
        }

        public async Task<object> Query(Action action)
        {
            using (var con = new Npgsql.NpgsqlConnection(_connectionString))
            {
                var result = await con.ExecuteScalarAsync("select pg_sleep(10);");
                Console.WriteLine(result);
                Console.WriteLine("完成");
                action();
                return result;
            }
        }
    }
}
