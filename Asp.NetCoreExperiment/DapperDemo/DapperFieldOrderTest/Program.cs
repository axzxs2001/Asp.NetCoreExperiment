using System;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Collections.Generic;

namespace DapperFieldOrderTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var con = new SqlConnection("server=.;database=testdb;uid=sa;pwd=sa;"))
            {
                var sql = "select name,age,page,id from test1";
                var ents = con.Query<dynamic>(sql);
                Console.WriteLine($"一共数据：{ents.Count()}");
                //字段顺序与查询顺序一致
                foreach (var ent in ents)
                {
                    foreach (var col in ent)
                    {
                        Console.Write(col.Key + ":" + col.Value + ",");
                    }
                    Console.WriteLine();
                }
                //精确类型
                foreach (IEnumerable<KeyValuePair<string, dynamic>> ent in ents)
                {                    
                    foreach (KeyValuePair<string, dynamic> col in ent)
                    {
                        Console.Write(col.Key + ":" + col.Value + ",");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
