using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebSqlConnectionTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
      
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/test")]
        public string Test()
        {
            try
            {
                using (var conn = new SqlConnection("Data Source=.;Initial Catalog=StarPay;Persist Security Info=True;User ID=sa;Password=sa;Max Pool Size=100"))
                {
                    conn.Open();
                    var sql = "select 1;--WAITFOR DELAY '00:00:03';";
                    var cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    return "SUCCESS";
                }
            }
            catch (Exception exc)
            {               
                return exc.Message;
            }
        }
    }
}
