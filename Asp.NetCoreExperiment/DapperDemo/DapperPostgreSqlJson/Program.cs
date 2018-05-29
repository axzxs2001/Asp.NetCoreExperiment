using Dapper;
using System;
using System.Data;

namespace DapperPostgreSqlJson
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlMapper.AddTypeHandler(new MyTypeHandler<Data>());
            var constring = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres;Database=TestDB;";
            using (var con = new Npgsql.NpgsqlConnection(constring))
            {
                var results = con.Execute("insert into test(id,data) values(@ID,@Data::json)", new Test { ID = 10, Data = new Data { IDs = 2, Age = 33, Name = "张三", Sex = true } });
                var result = con.Query<Test>("select * from test");
            }
        }
    }
    public class Test
    {
        public int ID { get; set; }
        public Data Data { get; set; }
    }
  
    public class Data 
    {
        public int IDs { get; set; }
        public string Name { get; set; }
        public bool Sex { get; set; }
        public int Age { get; set; }
    }

    public class MyTypeHandler<T> : SqlMapper.TypeHandler<T> 
    {
        public override T Parse(object value)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(value.ToString());
        }
        public override void SetValue(IDbDataParameter parameter, T value)
        {
            parameter.Value = Newtonsoft.Json.JsonConvert.SerializeObject(value);
        }
    }
}
