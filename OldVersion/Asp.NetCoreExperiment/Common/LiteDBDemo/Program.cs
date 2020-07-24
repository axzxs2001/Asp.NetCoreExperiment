using LiteDB;
using System;
using System.IO;
using System.Linq;

namespace LiteDBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new LiteDatabase($"Filename={Directory.GetCurrentDirectory()}/MyData.db;Password=1234;"))
            {
                // Get a collection (or create, if doesn't exist)
                var col = db.GetCollection<Customer>("customers");


                var list = col.FindAll().ToList();
                // Create your new customer instance
                for (var i = 0; i < 10; i++)
                {
                    var customer = new Customer
                    {
                        ABC = new { abc=1,cba="ddd"},
                       // Id = ObjectId.NewObjectId(),
                        Name = "John Doe" + i,
                        Phones = new string[] { "8000-0000", "9000-0000" },
                        IsActive = true,
                        Child = new Child { Id = 1, Name = "子1" },
                        Childs = new Child[] { new Child { Id = 1, Name = "子2" }, new Child { Id = 2, Name = "子3" } }
                    };

                    // Insert new customer document (Id will be auto-incremented)
                    col.Insert(customer);
                }
                var cus = col.FindOne(x => x.Name == "John Doe5");
                col.Delete(cus.ID);

                // Update a document inside a collection
                //customer.Name = "Jane Doe";

                //col.Update(customer);

                //// Index document using document Name property
                //col.EnsureIndex(x => x.Name);

                //// Use LINQ to query documents (filter, sort, transform)
                //var results = col.Query()
                //    .Where(x => x.Name.StartsWith("J"))
                //    .OrderBy(x => x.Name)
                //    .Select(x => new { x.Name, NameUpper = x.Name.ToUpper() })
                //    .Limit(10)
                //    .ToList();

                //// Let's create an index in phone numbers (using expression). It's a multikey index
                //col.EnsureIndex(x => x.Phones);

                //// and now we can query phones
                //var r = col.FindOne(x => x.Phones.Contains("8888-5555"));
            }
        }
    }

    public class Customer
    {
        public dynamic ABC { get; set; }
        public ObjectId ID { get; set; }
        public string Name { get; set; }
        public string[] Phones { get; set; }
        public bool IsActive { get; set; }

        public Child Child { get; set; }

        public Child[] Childs { get; set; }
    }

    public class Child
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
