using System;
using Npgsql;
using Dapper;
using System.Text;
using System.Collections.Generic;

namespace DataBaseTool
{
    class Program
    {
        static void Main(string[] args)
        {
            var tables = GetTables();
            var funcations = GetFunctions();
            var content = System.Text.Json.JsonSerializer.Serialize(new { tables, funcations },new System.Text.Json.JsonSerializerOptions() { WriteIndented = true });
            Console.WriteLine(content);
        }
        static string connectionString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=StarPayApplication;";

        static IEnumerable<dynamic> GetTables()
        {
            using (var con = new NpgsqlConnection(connectionString))
            {
                var sql = $@"select table_name,column_name,column_default,is_nullable,data_type,character_maximum_length ,numeric_precision_radix from information_schema.columns where table_schema = 'public'  order by table_name,ordinal_position";
                return con.Query<dynamic>(sql);

            }
        }

        static IEnumerable<dynamic> GetFunctions()
        {
            using (var con = new NpgsqlConnection(connectionString))
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
                return con.Query<dynamic>(sql);

            }
        }
    }
}
