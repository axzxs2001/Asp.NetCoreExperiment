using SqlSugar;
using System;
using System.Collections.Generic;

namespace SqlSugarDemo
{
    //https://www.codeisbug.com/doc/
    //https://github.com/sunkaixuan/SqlSugar
    class Program
    {
        static void Main(string[] args)
        {
            //添加

            var db = new DbContext();
            db.Db.Insertable(new Orders { ID = "abcd123", OrderTime = DateTime.Now, OrderUserID = "user001" }).ExecuteCommand();
            //查询
            var bus = new Business();
            foreach(var order in bus.GetOrders())
            {
                Console.WriteLine(order);
            }
            Console.WriteLine("=====================================");
            foreach (var events in bus.GetEvetns())
            {
                Console.WriteLine(events);
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
    public class Events
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
    public class DbContext
    {
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = "server=.;database=orderdb;uid=sa;pwd=1;",
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            });
        }
        public SqlSugarClient Db;
        public SimpleClient<Orders> OrderDb { get { return new SimpleClient<Orders>(Db); } }
        public SimpleClient<Events> EventsDb { get { return new SimpleClient<Events>(Db); } }
    }
    public class Business : DbContext
    {
        //use DbContext
        public void GetAll()
        {

            //use Db get student list
            var Orders = Db.Queryable<Orders>().ToList();

            //use  StudentDb get student list
            var events = EventsDb.GetList();
            //StudentDb.GetById
            //StudentDb.Delete
            //StudentDb.Update
            //StudentDb.Insert
            //StudentDb.GetPageList
            //....
            //SchoolDb.GetById
            //....

        }
        public List<Orders> GetOrders()
        {
            return Db.Queryable<Orders>().ToList();
        }
        public List<Events> GetEvetns()
        {
            return EventsDb.GetList();
        }
    }
}
