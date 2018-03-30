using System;
//using PetaPoco;
namespace PetaPocoDemo
{
    class Program
    {
        static void Main(string[] args)
        {
         //   var db = new PetaPoco.Database("connectionStringName");
         //   var db = DatabaseConfiguration.Build()
         //.UsingConnectionString("Host=127.0.0.1;Username=petapoco;Password=petapoco;Database=petapoco;Port=5001")
         //.UsingProvider<PostgreSQLDatabaseProvider>()
         //.UsingDefaultMapper<ConventionMapper>(m =>
         //{
         //    m.InflectTableName = (inflector, s) => inflector.Pluralise(inflector.Underscore(s));
         //    m.InflectColumnName = (inflector, s) => inflector.Underscore(s);
         //})
         //.Create();
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
