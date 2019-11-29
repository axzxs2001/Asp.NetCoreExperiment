using Dapper;
using Npgsql;
using System;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace DeapperDemo002
{
    class FF : IFormatProvider
    {
        public object GetFormat(Type formatType)
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            //var time1 = DateTime.Parse("2019-09-06 18:35:38.506006");
            //var time2 = DateTime.Parse("2019-09-06 18:35:38.190378");
            //Console.WriteLine((time1 - time2).TotalMilliseconds);

            // var obj = Convert.ChangeType(DateTime.Parse("2019-06-01 23:25:25"), typeof(DateTimeOffset), new FF());
            // var currentCulture = Thread.CurrentThread.CurrentCulture;
            Test8();
        }
        static void Test8()
        {
            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=1222;Database=testdb;";
            using (var conn = new NpgsqlConnection(connString))
            {
                var sql = @"select * from t where name =any( @names)";
                var result = conn.Query<dynamic>(sql, new { names = new string[] { "abc" } });

            }
        }
        static void Test7()
        {
            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=abc;";
            using (var conn = new NpgsqlConnection(connString))
            {
                var sql = @" select * from shop where shop_id =any(@p)";
                var list = new List<dynamic> {
                    new{shop_id="0014924a-118b-440e-b01c-a40653af649e" },
                    new{shop_id="00175409-8216-42ac-bdad-46826d2d7b44" }
                };

                var result = conn.Query<dynamic>(sql, new { p = list.Select(s => (string)s.shop_id).ToArray() });

            }
        }

        static void Test6()
        {
            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=abc;";
            using (var conn = new NpgsqlConnection(connString))
            {
                var sql = @" select now(),now() + interval '-1 microseconds'";
                var result = conn.Query<dynamic>(sql);

            }
        }

        static void Test5()
        {
            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=abc;";
            using (var conn = new NpgsqlConnection(connString))
            {
                var sql = @"select
     cast( create_time as character varying)  as CreateTime
	,create_time
from enterprise";
                var cmd = new NpgsqlCommand(sql, conn);
                var table = new DataTable();
                conn.Open();
                var reader = cmd.ExecuteReader();
                table.Load(reader);


                var list = conn.Query<dynamic>(sql);

            }

        }
        static void Test4()
        {
            /*  get_seq函数
             CREATE OR REPLACE FUNCTION get_seq (seq_name TEXT)
RETURNS SETOF bigint as
$$
BEGIN
RETURN QUERY  EXECUTE  'select last_value from  ' || seq_name;
END;
$$ LANGUAGE plpgsql;
             
             */
            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=abc;";
            using (var conn = new NpgsqlConnection(connString))
            {
                //insert into tablename(主键，字段1，字段2) values(@主键，@字段1，@字段2) on conflict(主键) do update set 字段1=@字段1，字段2=@字段2
                var sql = @"INSERT INTO test3 (
	id
	,a
	,b
	,c
	,d
	)
values (
	case 
		when @id >= (
				select get_seq(pg_get_serial_sequence('test3', 'id'))
				) and @id not in(select id from test3)
			then (
					select nextval(pg_get_serial_sequence('test3', 'id'))
					)
		else @id
		end
	,@a
	,@b
	,@c
	,@d
	) on conflict(id) do

update
set a = @a
	,b = @b
	,c = @c
	,d = @d
where test3.d < @d";
                var result = conn.Execute(sql, new List<dynamic>{
                    new { id = 30, a = "aaaa", b = 123, c = 12.3d, d = DateTimeOffset.Parse("2019-09-13 01:57:26.803664+00") },
                    new { id = 31, a = "aaaa", b = 123, c = 12.3d, d = DateTimeOffset.Parse("2019-09-13 01:54:26.803664+00") }
                });
                Console.WriteLine(result);
            }
        }

        static void Test3()
        {
            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=abc;";
            using (var conn = new NpgsqlConnection(connString))
            {
                var sql = @"select b,a,d,c from test3";
                var list = conn.Query<dynamic>(sql);
                var itemString = new StringBuilder();
                foreach (IDictionary<string, object> item in list)
                {
                    itemString.AppendJoin(',', item?.Keys);
                    itemString.AppendLine();
                    break;
                }
                foreach (IDictionary<string, object> item in list)
                {
                    itemString.AppendJoin(',', item?.Values);
                    itemString.AppendLine();
                }
                Console.WriteLine(itemString.ToString());
            }
        }

        static void Test2()
        {
            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=abc;";
            using (var conn = new NpgsqlConnection(connString))
            {

                #region 错误做法
                //var sql = @"select * from ""T_Test"" where  date(createtime)=date(@date)";
                //var date = DateTimeOffset.Parse("2019-09-03 09:27:00 +09:00");
                //var list = conn.Query<dynamic>(sql, new { date });
                #endregion

                #region 正确做法
                var sql = @"select * from ""T_Test"" where  createtime>=@beginTime and createtime<=@endtime";
                var beginTime = DateTimeOffset.Parse("2019-09-03 00:00:00");
                var endTime = DateTimeOffset.Parse("2019-09-03 23:59:59.9999994");
                var list = conn.Query<dynamic>(sql, new { beginTime, endTime });
                #endregion
                foreach (var item in list)
                {
                    Console.WriteLine($"ID:{item.id}  Name:{item.name}  Time:{item.createtime} ");
                }
            }
        }
        static void Test1()
        {
            //var file = System.IO.Directory.GetCurrentDirectory() + "/sql.txt";

            //var content = System.IO.File.ReadAllText(file).ToLower();

            //return;
            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgre2018;Database=TestDB;";
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Insert some data
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO Roles(RoleName) VALUES (@rolename)";
                    cmd.Parameters.AddWithValue("rolename", "aaa");
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new NpgsqlCommand("SELECT id FROM Roles", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        Console.WriteLine(reader.GetString(0));
            }
        }


    }
}
