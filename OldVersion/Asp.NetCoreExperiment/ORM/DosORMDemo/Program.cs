using Dos.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DosORMDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new DB();
            foreach(var order in db.GetOrders())
            {
                Console.WriteLine(order);
            }
        }
    }

    public class DB
    {
        public static readonly DbSession Context = new DbSession(DatabaseType.SqlServer, "server=.;database=orderdb;uid=sa;pwd=1;");
        public List<Orders> GetOrders()
        {
            return DB.Context.From<Orders>().ToList();
        }
    }

    public class Orders:Dos.ORM.Entity
    {
        public string ID { get; set; }
        public DateTime OrderTime { get; set; }
        public string OrderUserID { get; set; }

        public override string ToString()
        {
            return $"ID={ID},OrderTime={OrderTime},OrderUserID={OrderUserID}";
        }
    }
    public class Events : Dos.ORM.Entity
    {
        public int ID { get; set; }
        public string EventType { get; set; }

        public string OrderID { get; set; }
        public DateTime CreateTime { get; set; }

        public int ShipStatus { get; set; }
        public int StorageStatus { get; set; }
        public string EntityJson { get; set; }
        public override string ToString()
        {
            return $"ID={ID},EventType={EventType},OrderID={OrderID},CreateTime={CreateTime},ShipStatus={ShipStatus},StorageStatus={StorageStatus},EntityJson={EntityJson}";
        }
    }
}
