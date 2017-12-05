using System;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using Newtonsoft.Json.Linq;
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
                //生成Json
                var sig = conn.Query<dynamic>("select * from ftable").FirstOrDefault();
                var s = Newtonsoft.Json.JsonConvert.SerializeObject(sig);
                Console.WriteLine(s);


                //字符串动态生成dapper参数
                var str = Newtonsoft.Json.JsonConvert.SerializeObject(sig.timespan);
                var jsonstr = "{\"id\":1,\"name\":\"aa11\",\"age\":11,\"timespan\":" + str + "}";
                dynamic o = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonstr);
                var pars = new DynamicParameters();
                foreach (var v in (o as JObject).Children())
                {
                    if (v.Type == JTokenType.Property)
                    {
                        if (((v as JProperty).Value as JValue).Path == "timespan")
                        {
                            var value = (v as JProperty).Value as JValue;
                           
                            pars.Add(((v as JProperty).Value as JValue).Path,(byte[])value);
                        }
                        else
                        {
                            pars.Add(((v as JProperty).Value as JValue).Path, ((v as JProperty).Value as JValue).Value);
                        }
                    }
                }
                var ss = conn.Execute("update ftable set age=@age where id=@id and timespan=@timespan", pars);

            }
        }
    }


}
