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
            var tablenames = File.ReadAllText(@"C:\MyFile\abc\tablenames.txt").Split(',');
            var pgconnection = File.ReadAllText(@"C:\MyFile\abc\pgconnection.txt");
            var queryPgSqls = File.ReadAllLines(@"C:\MyFile\abc\tablesql.txt");

            foreach (var tablename in tablenames)
            {
                Console.WriteLine($"开始搬运{tablename}数据");
                using (var con = new Npgsql.NpgsqlConnection(pgconnection))
                {
                    var deleteSql = con.ExecuteScalar<string>(queryPgSqls[0], new { keyname = tablename });
                    var selectql = con.ExecuteScalar<string>(queryPgSqls[1], new { keyname = tablename });
                    var watch = new Stopwatch();
                    watch.Start();
                    var date = DateTime.Now.AddDays(-1);
                    new GenerPgSql().MigrationData(tablename, deleteSql, selectql, date);
                    Console.WriteLine($"搬运{tablename}数据总共花费{watch.Elapsed.TotalMilliseconds}ms.");
                }
            }
        }
        class GenerPgSql
        {
            public bool MigrationData(string tablename, string deleteSql, string selectSql, DateTime date)
            {
                var par = File.ReadAllText(@"C:\MyFile\abc\par.txt");
                var pgconnection = File.ReadAllText(@"C:\MyFile\abc\pgconnection.txt");
                var pagesize = 1000;
                var pageindex = 1;
                using (var pgcon = new Npgsql.NpgsqlConnection(pgconnection))
                {
                    pgcon.Execute(deleteSql,new { orgshortname = par, date });
                }
                while (true)
                {
                    var list = GetList(tablename, selectSql, date, pagesize, pageindex);
                    if (list.Count == 0)
                    {
                        break;
                    }
                    var insertSql = new GenerPgSql().GenerySql(pgconnection, list, tablename);
                    using (var pgcon = new Npgsql.NpgsqlConnection(pgconnection))
                    {
                        pgcon.Execute(insertSql);
                    }
                    pageindex++;
                }
                return true;
            }

            public List<dynamic> GetList(string tablename, string selectSql, DateTime date, int pagesize, int pageindex)
            {
                var par = File.ReadAllText(@"C:\MyFile\abc\par.txt");
                var sqlconnection = File.ReadAllText(@"C:\MyFile\abc\sqlconnection.txt");
                using (var sqlcon = new SqlConnection(sqlconnection))
                {               
                    var sql = $@"SELECT TOP {pagesize} * FROM ({selectSql})querytable WHERE rowno > {(pageindex - 1) * pagesize} ";
                    var list = sqlcon.Query<dynamic>(sql, new { orgshortname = par, date }).ToList();
                    return list;
                }
            }

            string GenerySql(string pgconnection, List<dynamic> dataList, string tableName)
            {
                List<dynamic> fieldItmes = null;
                using (var pgcon = new Npgsql.NpgsqlConnection(pgconnection))
                {
                    var sql = @"SELECT  a.attname AS field,t.typname AS type,a.attlen AS length,a.atttypmod AS lengthvar,a.attnotnull AS notnull
FROM pg_class c,pg_attribute a,pg_type t
WHERE c.relname =@tablename and a.attnum > 0 and a.attrelid = c.oid and a.atttypid = t.oid order by a.attname";
                    fieldItmes = pgcon.Query<dynamic>(sql: sql, param: new { tablename = tableName.ToLower() }).AsList();
                }
                var sqlBuilder = new StringBuilder($"INSERT INTO {tableName}(");
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
                            if (fieldValue == null)
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
