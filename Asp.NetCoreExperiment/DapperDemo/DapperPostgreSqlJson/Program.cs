using Dapper;
using System;
using System.Data;

namespace DapperPostgreSqlJson
{
    class Program
    {
        static void Main(string[] args)
        {
            //添加处理对象
            SqlMapper.AddTypeHandler(new MyTypeHandler<Data>());
            var constring = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres;Database=TestDB;";
            using (var con = new Npgsql.NpgsqlConnection(constring))
            {
                //注意json字段参数后要跟  ::json
                var results = con.Execute("insert into test(id,data) values(@ID,@Data::json)", new Test { ID = 10, Data = new Data { IDs = 2, Age = 33, Name = "张三", Sex = true } });
                var result = con.Query<Test>("select * from test");
            }
        }
    }
    /// <summary>
    /// 父对象
    /// </summary>
    public class Test
    {
        public int ID { get; set; }
        public Data Data { get; set; }
    }
    /// <summary>
    /// 子对象
    /// </summary>
    public class Data 
    {
        public int IDs { get; set; }
        public string Name { get; set; }
        public bool Sex { get; set; }
        public int Age { get; set; }
    }
    /// <summary>
    /// 对象到json，json到对象转换的Handler
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
