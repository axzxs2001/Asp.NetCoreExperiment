using StackExchange.Redis;
using System;

namespace RedisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379");
            IDatabase db = redis.GetDatabase();
            string value = "abcdefg";
            db.StringSet("mykey", value);
            string value1 = db.StringGet("mykey");
            Console.WriteLine(value1); // writes: "abcdefg"
            Console.ReadLine();
        }
    }
}