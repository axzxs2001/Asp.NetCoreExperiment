using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DapperDemo0010
{
    class Program
    {
        static void Main(string[] args)
        {
            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=TestDB;";
            var sql = @"select id,content from jsontb";
            using (var db = new NpgsqlConnection(connString))
            {
                var a = db.Query<AA>(sql).First();
                var b = db.Query<AA>(sql).First();
                var c = a.Equals(b);
                Console.WriteLine(c);
                Console.WriteLine(a==b);
                //var abcs = new List<dynamic>();
                //foreach (var item in list)
                //{
                //    abcs.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(item));
                //}
            }
        }
    }
    public class AA
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public bool Equals(AA aa)
        {
            return (ID == aa.ID) && (Content == aa.Content);
        }

        public  static bool operator == (AA a,AA b)
        {
            return (a.ID ==b.ID) && (a.Content ==b.Content);
        }
        public static bool operator !=(AA a, AA b)
        {
            return (a.ID != b.ID) || (a.Content != b.Content);
        }

    }
    class ABC
    {
        public int ID { get; set; }
        public int Age { get; set; }

        public string Name { get; set; }
    }
}
