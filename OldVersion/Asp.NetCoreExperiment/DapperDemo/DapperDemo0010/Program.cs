using Dapper;
using Npgsql;
using System;
using System.Collections;
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
                var a = db.Query<AA>(sql).Take(4);
                var b = db.Query<AA>(sql);

                //  Console.WriteLine(a.First() == b.First());
                var com = new MyEqualityComparer();
                Console.WriteLine("-------------------starPayDetails中对帐失败的明细---------------------");
                var exceptStarPayDetails = a.Except(a.Intersect(b, com), com);
                foreach (var item in exceptStarPayDetails)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine("----------------------walletDetails中对帐失败的明细--------------------");
                var exceptWalletDetails = b.Except(b.Intersect(a, com), com);
                foreach (var item in exceptWalletDetails)
                {
                    Console.WriteLine(item);
                }
                //var abcs = new List<dynamic>();
                //foreach (var item in list)
                //{
                //    abcs.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(item));
                //}
            }
        }
    }
    class MyEqualityComparer : IEqualityComparer<AA>
    {
        public bool Equals(AA item1, AA item2)
        {
            if (item1 == null && item2 == null)
                return true;
            else if ((item1 != null && item2 == null) ||
                    (item1 == null && item2 != null))
                return false;

            return item1.ID.Equals(item2.ID) &&
                   item1.Content.Equals(item2.Content);
        }

        public int GetHashCode(AA item)
        {
            return new { item.ID, item.Content }.GetHashCode();
        }
    }
    public class AA
    {
        public int ID { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return $"{ID}-{Content}";
        }

        //public override bool Equals(object obj)
        //{
        //    var a = (AA)obj;
        //    return (a.ID == ID) && (a.Content == Content);
        //}

        //public static bool operator ==(AA a, AA b)
        //{
        //    return (a.ID == b.ID) && (a.Content == b.Content);
        //}
        //public static bool operator !=(AA a, AA b)
        //{
        //    return (a.ID != b.ID) || (a.Content != b.Content);
        //}

    }

}
