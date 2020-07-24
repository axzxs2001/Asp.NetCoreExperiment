using Chloe.Entity;
using Chloe.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChloeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new MsSqlContext("server=.;database=orderdb;uid=sa;pwd=1;");
            var orders = context.Query<Orders>().ToList();
            foreach (var order in orders)
            {
                Console.WriteLine(order);
            }
        }
    }
    [Table("Orders")]
    public class Orders 
    {
        [Column(IsPrimaryKey = true)]
        public string ID { get; set; }
        public DateTime OrderTime { get; set; }
        public string OrderUserID { get; set; }

        public override string ToString()
        {
            return $"ID={ID},OrderTime={OrderTime},OrderUserID={OrderUserID}";
        }
    }
}
