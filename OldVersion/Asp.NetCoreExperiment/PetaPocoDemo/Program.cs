using PetaPoco;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetaPocoDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new PetaPoco.Database(connectionString: "server=.;database=orderdb;uid=sa;pwd=1;", providerName: "sqlservers");
            foreach (var order in db.Query<Orders>("select * from orders"))
            {
                Console.WriteLine(order);
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
