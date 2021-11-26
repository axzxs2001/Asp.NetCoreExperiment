using MySql.Data.MySqlClient;

var constrbuilder = new MySqlConnectionStringBuilder()
{
    UserID = "root",
    Password = "root",
    Server = "192.168.252.41",
    Port = 3307,
    Database = "sharding_db",
    ConnectionProtocol = MySqlConnectionProtocol.Tcp,
};

Console.WriteLine(constrbuilder.ConnectionString);

using var con = new MySqlConnection(constrbuilder.ConnectionString);
con.Open();
using var cmd = new MySqlCommand("select * from accounting", con);
using var dr = cmd.ExecuteReader();
while (dr.Read())
{
    Console.WriteLine(dr.GetValue(0));
}
