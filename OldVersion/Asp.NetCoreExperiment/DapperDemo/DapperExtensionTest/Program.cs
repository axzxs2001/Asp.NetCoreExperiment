using DapperExtension;
using System;
using System.Linq;

namespace DapperExtensionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var connString = "Server=localhost;Port=5432;UserId=postgres;Password=postgres2018;Database=postgres;";
            var con = new Npgsql.NpgsqlConnection(connString);
            using (var db = new DapperPlusDB(con))
            {               
                var conn = db.GetConnection();
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    //查询异常不触发，只有在获取数据时才触发
                    var list = db.Query<dynamic>("select * from testtable1");
                    Console.WriteLine(list);
                    //增删改是当下触发
                    var result = db.Execute("insert into testtable1 values('aaa',111)");
                    Console.WriteLine(result);
                    tran.Commit();
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                    tran.Rollback();
                }
            }
            Console.ReadLine();
        }
    }
}
