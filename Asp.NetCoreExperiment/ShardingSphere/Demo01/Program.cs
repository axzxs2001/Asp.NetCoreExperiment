using MySql.Data.MySqlClient;
using System.Data;
using Dapper;

var constrbuilder = new MySqlConnectionStringBuilder()
{
    UserID = "root",
    Password = "root",
    Server = "127.0.0.1",
    Port = 3307,
    Database = "sharding_db",
};


Console.WriteLine(constrbuilder.ConnectionString);

MySqlConnectorDemo(constrbuilder);
MySqlDemo(constrbuilder);

static void MySqlConnectorDemo(MySqlConnectionStringBuilder constrbuilder)
{
    using var con = new MySqlConnector.MySqlConnection(constrbuilder.ConnectionString);
    Console.WriteLine($"MySqlConnector:{ con.ConnectionString}");
    con.Open();
    var list = con.Query<dynamic>("select * from accounting").ToList();
    Console.WriteLine(list.Count);
}

static void MySqlDemo(MySqlConnectionStringBuilder constrbuilder)
{

    using var con = new MySqlConnection(constrbuilder.ConnectionString);
    Console.WriteLine($"MySql:{ con.ConnectionString}");
    con.Open();
    var list = con.Query<dynamic>("select * from accounting").ToList();
    Console.WriteLine(list.Count);

    //using var cmd = new MySqlConnector.MySqlCommand("select * from accounting", con);
    //using var dr = cmd.ExecuteReader();
    //var table = new DataTable();
    //table.Load(dr);
    //foreach (var row in table.Rows)
    //{
    //    Console.WriteLine(row);
    //}
}