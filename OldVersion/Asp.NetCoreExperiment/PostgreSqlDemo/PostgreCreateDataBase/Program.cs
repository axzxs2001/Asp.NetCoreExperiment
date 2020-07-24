using Dapper;
using Npgsql;
using System;

namespace PostgreCreateDataBase
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbName = "abc";
            using (var conn = new NpgsqlConnection("Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;"))
            {
                var createDBSql = $"drop database {dbName};create database {dbName}";
                conn.Execute(createDBSql);

            }
            using (var conn = new NpgsqlConnection($"Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;database={dbName};"))
            {
                var createTableSql = @"CREATE TABLE t_users(id serial PRIMARY KEY,name character varying(64),age integer);";
                conn.Execute(createTableSql);

                var insertTableSql = "insert into t_users(name,age) values(@name,@age)";
                conn.Execute(insertTableSql, new { name = "张三", age = 12 });

                var selectSql = "select * from t_users";
                var list = conn.Query<dynamic>(selectSql);
                foreach (var item in list)
                {
                    Console.WriteLine(item.id + "       " + item.name + "       " + item.age);
                }
            
            }
        }
    }
}
