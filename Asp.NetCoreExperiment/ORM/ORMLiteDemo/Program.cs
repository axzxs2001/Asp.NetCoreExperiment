using ServiceStack.OrmLite;
using System;

namespace ORMLiteDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var dbFactory = new OrmLiteConnectionFactory(
      "server=.;database=orderdb;uid=sa;pwd=1;",
      SqlServerDialect.Provider);
            using (var db = dbFactory.Open())
            {
                foreach (var order in db.Select<Orders>())
                {
                    Console.WriteLine(order);
                }
            }
        }
    }
    public class Orders
    {
        public string ID { get; set; }
        public DateTime OrderTime { get; set; }
        public string OrderUserID { get; set; }

        public override string ToString()
        {
            return $"ID={ID},OrderTime={OrderTime},OrderUserID={OrderUserID}";
        }
    }
}
