using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Dapper;

namespace BulkNpgSqlDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var tablename = "";
            var sqlconnection ="连接字符串";
            using (var sqlcon = new SqlConnection(sqlconnection))
            {
                var watch = new Stopwatch();
                watch.Start();
                var pagesize = 1000;
                var pageindex = 1;
                while (true)
                {
                    var s = "select   ROW_NUMBER() OVER (ORDER BY 主键) as rowno,* from "+ tablename;
                    var sql = $@"SELECT TOP {pagesize} * FROM ({s})querytable WHERE rowno > {(pageindex - 1) * pagesize} ";
                    var list = sqlcon.Query<dynamic>(sql, new { orgshortname = "nss" }).ToList();
                    if (list.Count == 0)
                    {
                        break;
                    }
                
                    var insertSql = new GenerPgSql().GenerySql(list, tablename);
                    var pgconnection = "Server=180.12.175.72;Port=5432;UserId=postgres;Password=123456;Database=postgres;Pooling=true;MinPoolSize=1;MaxPoolSize=100;CommandTimeout=300;";
                    using (var pgcon = new Npgsql.NpgsqlConnection(pgconnection))
                    {
                        pgcon.Execute(insertSql,list);
                    }
                    Console.WriteLine(pageindex);
                    pageindex++;
                }
                Console.WriteLine("制造数据总共花费{0}ms.", watch.Elapsed.TotalMilliseconds);

            }


        }
        class GenerPgSql
        {
            public string GenerySql(List<dynamic> dataList, string tableName)
            {
                var pgconnection = "Server=180.12.175.72;Port=5432;UserId=postgres;Password=postgres2018;Database=postgres;Pooling=true;MinPoolSize=1;MaxPoolSize=100;CommandTimeout=300;";
                List<dynamic> fieldItmes = null;
                using (var pgcon = new Npgsql.NpgsqlConnection(pgconnection))
                {
                    var sql = @"SELECT  a.attname AS field,t.typname AS type,a.attlen AS length,a.atttypmod AS lengthvar,a.attnotnull AS notnull
FROM pg_class c,pg_attribute a,pg_type t
WHERE c.relname =@tablename and a.attnum > 0 and a.attrelid = c.oid and a.atttypid = t.oid order by a.attname";
                    fieldItmes = pgcon.Query<dynamic>(sql: sql, param: new { tablename = tableName.ToLower() }).AsList();
                }
                var sqlBuilder = new StringBuilder(@"INSERT INTO public.wxbsbankaccount(");

                sqlBuilder.Append(string.Join(',', fieldItmes.Select(s => s.field)));
                sqlBuilder.Append(") values");
                foreach (IEnumerable<KeyValuePair<string, dynamic>> data in dataList)
                {
                    var line = new StringBuilder("(");
                    foreach (var field in fieldItmes)
                    {
                        var fieldData = data.SingleOrDefault(s => s.Key.ToLower() == field.field);
                        if (fieldData.Key != null)
                        {
                            string fieldValue = Convert.ToString(fieldData.Value);
                            if (fieldValue==null)
                            {
                                fieldValue = "null,";
                            }
                            else
                            {
                                fieldValue = fieldValue.Replace("'", "''");
                                fieldValue = $"'{fieldValue}',";
                            }
                            line.Append(fieldValue);
                        }

                    }
                    sqlBuilder.Append(line.ToString().TrimEnd(',') + "),");
                }
                return sqlBuilder.ToString().TrimEnd(',');
            }
        }
    }
}
