using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB.Mapping;
using System;
using System.Collections.Generic;

namespace Linq2DBDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            DataConnection.DefaultSettings = new MySettings();  

            using (var db = new DbOrder())
            {
                foreach (var order in db.Orders.ToListAsync().Result)
                {
                    Console.WriteLine(order);
                }

            }
        }
    }

    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal => false;
    }

    public class MySettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders
        {
            get { yield break; }
        }

        public string DefaultConfiguration => "SqlServer";
        public string DefaultDataProvider => "SqlServer";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "Orders",
                        ProviderName = "SqlServer",
                        ConnectionString = @"server=.;database=orderdb;uid=sa;pwd=1;"
                    };
            }
        }
    }
    public class DbOrder : LinqToDB.Data.DataConnection
    {
        public DbOrder() : base("Orders") { }

        public ITable<Orders> Orders { get { return GetTable<Orders>(); } }

    }

    [Table(Name = "Orders")]
    public class Orders
    {
        [PrimaryKey, Identity]
        public string ID { get; set; }
        [Column(Name = "OrderTime")]
        public DateTime OrderTime { get; set; }
        [Column(Name = "OrderUserID")]
        public string OrderUserID { get; set; }

        public override string ToString()
        {
            return $"ID={ID},OrderTime={OrderTime},OrderUserID={OrderUserID}";
        }
    }
}
