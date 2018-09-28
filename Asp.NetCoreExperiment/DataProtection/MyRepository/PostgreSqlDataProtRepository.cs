using Dapper;
using Microsoft.AspNetCore.DataProtection.Repositories;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace PGRepository
{
    public class PostgreSqlDataProtRepository : IXmlRepository
    {
        public IReadOnlyCollection<XElement> GetAllElements()
        {
            var list = new List<XElement>();
            var constring = "Server=192.168.252.41;Port=5432;UserId=postgres;Password=postgres2018;Database=TestDB;";
            using (var con = new Npgsql.NpgsqlConnection(constring))
            {
                var sql = "select value from  dataprotectiondb";
                var values = con.Query<string>(sql);
                foreach (var value in values)
                {
                    list.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<XElement>(value));
                }
            }

            var coll = new ReadOnlyCollection<XElement>(list);
            return coll;
        }

        public void StoreElement(XElement element, string friendlyName)
        {
            var constring = "Server=192.168.252.41;Port=5432;UserId=postgres;Password=postgres2018;Database=TestDB;";
            using (var con = new Npgsql.NpgsqlConnection(constring))
            {
                var deleteSql = "delete from dataprotectiondb where name=@name";
                con.Execute(deleteSql, new { name = friendlyName });

                var sql = "insert into dataprotectiondb(name,value) values(@name,@value)";
                con.Execute(sql, new { name = friendlyName, value = Newtonsoft.Json.JsonConvert.SerializeObject(element) });
            }
        }
    }
}
