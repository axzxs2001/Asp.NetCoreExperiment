using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using Npgsql;
namespace IEnumerableToList
{
    class Program
    {
        static void Main(string[] args)
        {
            var constring = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=StarPayECDB;Pooling=true;MinPoolSize=1;MaxPoolSize=100;CommandTimeout=300";
            using (var con = new NpgsqlConnection(constring))
            {
                var records = con.Query<Record>("select id,name,create_time as createtime from spec_publishs");
                var list = records.ToList();
                var newlist = list.FirstOrDefault();
               // Changes(list);
                Change(newlist);
                Print(records);
                Console.ReadKey();
            }
        }
        static void Changes(IEnumerable<Record> records)
        {
            foreach (var record in records)
            {
                record.CreateTime = DateTime.Now;
            }
        }
        static void Change(Record record)
        {
            record.CreateTime = DateTime.Now;
        }
        static void Print(IEnumerable<Record> records)
        {
            foreach (var record in records)
            {
                Console.WriteLine(record.CreateTime);
            }
        }
    }

    class Record
    {
        public int ID { get; set; }

        public DateTime CreateTime { get; set; }

        public string Name { get; set; }
    }
}
