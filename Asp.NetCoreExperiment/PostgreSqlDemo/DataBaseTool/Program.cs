using System;
using Npgsql;
using Dapper;
using System.Text;

namespace DataBaseTool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        static string GetTables()
        {
            using (var con = new NpgsqlConnection(""))
            {
                var sql = $@"select table_name,column_name,column_default,is_nullable,data_type,character_maximum_length ,numeric_precision_radix from information_schema.columns where table_schema = 'public'  order by table_name,ordinal_position";
                var list = con.Query<dynamic>(sql);
                return System.Text.Json.JsonSerializer.Serialize(list);
            }
        }


        static string GetFunction()
        {
            using (var con = new NpgsqlConnection(""))
            {
                var sql = $@"SELECT
  pg_proc.proname,
  pg_type.typname,
  pg_proc.pronargs,
  pg_proc.proargnames
FROM
  pg_proc
    JOIN pg_type
   ON (pg_proc.prorettype = pg_type.oid)
WHERE pronamespace = (SELECT pg_namespace.oid FROM pg_namespace WHERE nspname = 'public') order by  pg_proc.proname";
                var list = con.Query<dynamic>(sql);
                return System.Text.Json.JsonSerializer.Serialize(list);
            }
        }
    }
}
