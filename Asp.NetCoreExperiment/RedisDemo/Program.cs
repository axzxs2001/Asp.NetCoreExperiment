using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace RedisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
            IDatabase db = redis.GetDatabase();
            //string value = "abcdefg";
            //db.StringSet("mykey", value);
            //string value1 = db.StringGet("mykey");
            //Console.WriteLine(value1); // writes: "abcdefg"

            var list = new List<Item>();
            list.Add(new Item { ID = 1, Name = "name1" });
            list.Add(new Item { ID = 2, Name = "name2" });
            db.StringSet("list",Newtonsoft.Json.JsonConvert.SerializeObject(list));



            Console.ReadLine();
        }
    }
    class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}