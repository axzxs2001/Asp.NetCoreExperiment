using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Dapper;
using System.Data.SqlClient;
using Lucene.Net.Store;
using System.IO;
using Lucene.Net.Index;

namespace PostgresqlDemo01.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        CXDic _cx;
        public ValuesController(CXDic cx)
        {
            _cx = cx;
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            //        using (var con = new SqlConnection("server=.;database=testdb;uid=sa;pwd=1;"))
            //        {
            //            var list = con.Query<dynamic>("SELECT * FROM t_bx_feeitem");



            //            var connString = "Host=127.0.0.1;Username=postgres;Password=gsw790622;Database=testdb01;";

            //            using (var conn = new NpgsqlConnection(connString))
            //            {
            //                var result = conn.Execute(@"INSERT INTO public.t_bx_feeitem(
            //fmefeeitemid,  fname, fnumber, fsize, fpy)
            //VALUES(@fmefeeitemid,@fname, @fnumber, @fsize,@fpy) ", list);
            //                var json1 = Newtonsoft.Json.JsonConvert.SerializeObject(list, Newtonsoft.Json.Formatting.Indented);
            //            }
            //        }
            return new string[] { "value1", "value2" };
        }

        [HttpGet("/query1")]
        public IActionResult Query1(string name)
        {
            var connString = "Host=127.0.0.1;Username=postgres;Password=gsw790622;Database=testdb01;";
            using (var conn = new NpgsqlConnection(connString))
            {
                var result = conn.Query(@"select  fname,fnumber,fsize,fpy from public.t_bx_feeitem where
  fname like '%" + name + "%' or fpy like  '%" + name + "%' LIMIT 100 ");
                return new JsonResult(result);
            }
        }

        [HttpGet("/query2")]
        public IActionResult Query2(string name)
        {
            using (var conn = new SqlConnection("server=.;database=testdb;uid=sa;pwd=1;"))
            {
                var result = conn.Query(@"select top 100  fname,fnumber,fsize,fpy from t_bx_feeitem where  fname like '%" + name + "%' or fpy like  '%" + name + "%'");
                return new JsonResult(result);
            }
        }
        [HttpGet("/query3")]
        public IActionResult Query3(string name)
        {
            using (var conn = new SqlConnection("server=.;database=testdb;uid=sa;pwd=1;"))
            {
                var result = conn.Query($@"select top 100  fname,fnumber,fsize,fpy from t_bx_feeitem where CONTAINS(*, '{name}')");
                return new JsonResult(result);
            }
        }
        [HttpGet("/query0")]
        public IActionResult Query0(string name)
        {
            return new JsonResult("");
        }


        [HttpGet("/query4")]
        public IActionResult Query4(string name)
        {
            return new JsonResult(_cx.Read(name));
        }

        [HttpGet("/getkey")]

        public IActionResult GetKey(string lx)
        {
            using (var con = new SqlConnection("server=.;database=testdb;uid=sa;pwd=1;"))
            {
                var cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "getlsh";
                cmd.CommandType = CommandType.StoredProcedure;             
                cmd.Parameters.Add(new SqlParameter() { ParameterName = "@lx", Value = lx });
                var par = new SqlParameter();
                par.ParameterName = "@lsh";
                par.Direction = System.Data.ParameterDirection.Output;
                par.SqlDbType = System.Data.SqlDbType.VarChar;
                par.Size = 30;
                cmd.Parameters.Add(par);
                con.Open();
                cmd.ExecuteReader();
                var lsh = par.Value.ToString();
                return new JsonResult(lsh);
            }

        }

    }


}
