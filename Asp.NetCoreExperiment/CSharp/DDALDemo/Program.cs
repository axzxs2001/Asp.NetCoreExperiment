using Dapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;

namespace DDALDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var shards = GetShards();
            var results = new ConcurrentBag<Demo>();
            Parallel.ForEach(shards, shard =>
            {
                using (var con = new SqlConnection(shard.ConnectionString))
                {
                    var list = con.Query<Demo>("select id,name from test01");
                    foreach (var item in list)
                    {
                        results.Add(item);
                    }
                }
            });
            Console.ReadLine();
        }
        private static IEnumerable<ShardInformation> GetShards()
        {
            return new[]
            {
                new ShardInformation
                {
                    Id = 1,
                    ConnectionString = "server=.;database=testdb;uid=sa;pwd=sa;"
                },
                new ShardInformation
                {
                    Id = 2,
                    ConnectionString = "server=.;database=testdb;uid=sa;pwd=sa;"
                }
            };
        }
    }

    class ShardInformation
    {
        public int Id { get; set; }
        public string ConnectionString { get; set; }
    }
    class Demo
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}