using iBoxDB.LocalServer;
using System;
using System.IO;

namespace IBoxDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new DB($"{Directory.GetCurrentDirectory()}");
            db.GetConfig().EnsureTable<Record>("Table", "Id");
            AutoBox auto = db.Open();
            for (var i = 0; i < 10; i++)
            {
                auto.Insert("Table", new Record { Id = 1L+i, Name = "Andy" });
            }
            var list = auto.Select<Record>("from Table");
            var o1 = auto.Get<Record>("Table",1L);
            o1.Name = "Kelly";
            auto.Update("Table", o1);
            auto.Delete("Table", 1L);
            var list1 = auto.Select<Record>("from Table");
        }
    }

    internal class Record
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
}
