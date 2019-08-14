using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;

namespace DapperDemo0010
{
    class Program
    {
        static void Main(string[] args)
        {
            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=TestDB;";
            var sql = @"select content from jsontb";
            using (var db = new NpgsqlConnection(connString))
            {
                var list = db.Query<string>(sql);
                var abcs = new List<dynamic>();
                foreach (var item in list)
                {
                    abcs.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(item));
                }
            }
        }
    }

    class ABC
    {
        public int ID { get; set; }
        public int Age { get; set; }

        public string Name { get; set; }
    }
}
