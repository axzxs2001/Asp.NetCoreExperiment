using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Demo001_WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public async Task<string> Get(int id)
        //{
        //    using(var con=new SqlConnection("server=.;database=testdb;uid=sa;pwd=1;"))
        //    using (var cmd = new SqlCommand())
        //    {
        //        System.Threading.Thread.Sleep(5000);
        //        cmd.Connection = con;
        //        cmd.CommandText = "select count(1) from ttt";
        //        con.Open();
        //        var count = await cmd.ExecuteScalarAsync();
        //        return count.ToString();
        //    }
         
        //}
        [HttpGet("{id}")]
        public string Get(int id)
        {
            using (var con = new SqlConnection("server=.;database=testdb;uid=sa;pwd=1;"))
            using (var cmd = new SqlCommand())
            {
                System.Threading.Thread.Sleep(5000);
                cmd.Connection = con;
                cmd.CommandText = "select count(1) from ttt";
                con.Open();
                var count = cmd.ExecuteScalar();
                return count.ToString();
            }

        }
        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
