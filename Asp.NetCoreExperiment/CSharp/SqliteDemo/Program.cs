

using Microsoft.Data.Sqlite;
using System.Buffers;
using System.Data;

var sql = "select * from drugGoods where (goodsCode like @value1||'%' or goodsName like '%'|| @value1||'%' or logogram like '%'||@value1||'%' ) and goodsCode is not null";
var data = GetData(sql, new Dictionary<string, dynamic> { { "value1", ""} });


static DataTable GetData(string sql, Dictionary<string, dynamic> dic = null)
{
    var constring = @"Data Source=C:\\myfiles\sxyb_db.sqlite";
    var pars = new List<SqliteParameter>();
    if (dic != null)
    {
        foreach (var item in dic)
        {
            pars.Add(new SqliteParameter($"@{item.Key}", $"{item.Value}"));
        }
    }

    using var con = new SqliteConnection(constring);
    using var cmd = new SqliteCommand(sql, con);
    cmd.Parameters.AddRange(pars);
    con.Open();
    using var reader = cmd.ExecuteReader();
    var dt = new DataTable();
    dt.Load(reader);
    con.Close();
    return dt;
}