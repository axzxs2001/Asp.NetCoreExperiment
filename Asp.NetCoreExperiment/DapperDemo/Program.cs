using System;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using Newtonsoft.Json;
namespace DapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            UpdateTimeStamp();

        }
        /// <summary>
        /// 时间戳更新
        /// </summary>
        static void UpdateTimeStamp()
        {
            using (var conn = new SqlConnection("server=.;database=test;uid=sa;pwd=1"))
            {
                var sig = conn.Query<dynamic>("select * from ftable").FirstOrDefault();

                var s = Newtonsoft.Json.JsonConvert.SerializeObject(sig, new JsonSerializerSettings { });
                Console.WriteLine(s);
                var o = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(s);

                var ss = conn.Execute("update ftable set age=@age where timespan=@timespan", new { age = 49, timespan = (byte[])o.timespan });

            }
        }
    }


}
