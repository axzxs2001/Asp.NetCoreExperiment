using System.ComponentModel;

//long i = 10;
//Console.WriteLine(i.GetType().Name);



//var i1 = System.Numerics.BigInteger.Parse("10000000000000000000000000000000");

//TypeConverter dateOnlyConverter = TypeDescriptor.GetConverter(typeof(DateOnly));

//DateOnly? date = dateOnlyConverter.ConvertFromString("1940-10-09") as DateOnly?;

//MySql.Data.MySqlClient.MySqlConnectionStringBuilder c = new MySql.Data.MySqlClient.MySqlConnectionStringBuilder();


//var ddd = DateTimeOffset.Now;
//using var con = new MySql.Data.MySqlClient.MySqlConnection("server=localhost;database=world;uid=root;pwd=mars2020;");
//con.Open();
//using var cmd = new MySql.Data.MySqlClient.MySqlCommand("insert into new_table(id,tt) value(3,@time)",con);
//cmd.Parameters.Add("@time", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value =
//DateTimeOffset.Now.DateTime;

//cmd.ExecuteNonQuery();

Console.WriteLine(DateTimeOffset.Now.DateTime);
Console.WriteLine(DateTimeOffset.Now.LocalDateTime);
Console.WriteLine(DateTimeOffset.Now.UtcDateTime);
Console.WriteLine(DateTimeOffset.Now);

Console.WriteLine(DateTime.Now);
Console.WriteLine(DateTime.UtcNow);
