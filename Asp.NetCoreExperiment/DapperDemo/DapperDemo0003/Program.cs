using Npgsql;
using System;
using Dapper;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DapperDemo0003
{
    class Program
    {
        static void Main(string[] args)
        {

            TestQueryEnum();
            //Query();
            //TestProc();
            //InsertMethod();
            //Console.ReadLine();
        }
        static void TestQueryEnum()
        {
            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=postgres;";
            var sql = @"select * from  abc";
            using (var db = new NpgsqlConnection(connString))
            {
                var result = db.Query<ABC>(sql);
                foreach (var item in result)
                {
                    Console.WriteLine($"id:{item.ID}  name:{item.Name}  sex:{item.Sex}  sex1:{item.Sex1}");
                }
            }
        }

        static void TestInsertEnum()
        {
            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=postgres;";
            var sql = @"insert into abc(sex,name,sex1) values(@sex,@name,@sex1)";
            using (var db = new NpgsqlConnection(connString))
            {
                var result = db.Execute(sql, new ABC { Name = "张三", Sex = Sex.Female, Sex1 = Sex.Male });
                Console.WriteLine(result);
            }
        }

        class ABC
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public Sex Sex { get; set; }
            public Sex Sex1 { get; set; }
        }
        enum Sex
        {
            Male,
            Female
        }

        static void Query()
        {
            //SqlMapper.AddTypeMap(typeof(DateTime), System.Data.DbType.DateTimeOffset);

            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=postgres;";
            var sql = @"select * from t4 where time=@time";
            using (var db = new NpgsqlConnection(connString))
            {
                var time = DateTimeOffset.Parse("2020-01-21 23:59:59.999999").ToOffset(TimeSpan.FromHours(9));
                var result = db.Query(sql, new { time }).SingleOrDefault();
                Console.WriteLine(result.id);
            }

        }

        static void TestProc()
        {
            var connString = "Server=13.94.40.38;Port=5432;UserId=postgres;Password=postgres2018;Database=postgres;";
            var sql = @"call get_data()";
            using (var db = new NpgsqlConnection(connString))
            {
                var result = db.Execute(sql);
                Console.WriteLine($"insert into 结果：{result}");
            }
        }


        static void InsertMethod()
        {
            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=NetStarsUnionpayDB;";
            var sql = @"insert into permissions(PermissionName,Action,Predicate,Describe) values(@permissionname,@action,@predicate,@describe)";
            using (var db = new NpgsqlConnection(connString))
            {
                var list = new List<dynamic>() {
                    new { PermissionName = "主页", Action = "/", Predicate = "Get", Describe = "" },
                    new { PermissionName = "主页", Action = "/", Predicate = "Get", Describe = "" },
                    new { PermissionName = "主页", Action = "/", Predicate = "Get", Describe = "" },
                    new { PermissionName = "主页", Action = "/", Predicate = "Get", Describe = "" },
                    new { PermissionName = "主页", Action = "/", Predicate = "Get11111111111111111111111111111111111", Describe = "" }
                };

                var result = db.Execute(sql, list);
                Console.WriteLine($"insert into 结果：{result}");
            }
        }

        static void QueryMethod()
        {
            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=gsw790622;Database=NetStarsUnionpayDB;";
            using (var db = new NpgsqlConnection(connString))
            {
                var sql = @"SELECT 
RolePermissions.RoleID,
RolePermissions.PermissionID,
Roles.RoleName,
Roles.Describe as RoleDescribe,
Permissions.PermissionName,
Permissions.Action,
Permissions.Predicate,
Permissions.Describe as PermissionDescribe
from Roles join RolePermissions
on Roles.ID=RolePermissions.RoleID
join Permissions
on RolePermissions.PermissionID =Permissions.ID;";
                var list = db.Query<RolePermissionModel>(sql).ToList();
                foreach (var rolePermission in list)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(rolePermission));
                }
            }

        }
    }
    //static class DateTimeOffsetExt
    //{
    //    public static string ToLongString(this DateTimeOffset date)
    //    {
    //        return date.ToLongString();
    //    }
    //}

    public class RolePermissionModel
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public string RoleDescribe { get; set; }

        /// <summary>
        /// 权限ID
        /// </summary>
        public int PermissionID { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string PermissionName { get; set; }
        /// <summary>
        /// 可访问Action
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 谓词：Get,Post,Put,Delete
        /// </summary>
        public string Predicate { get; set; }

        /// <summary>
        /// 权限描述
        /// </summary>
        public string PermissionDescribe { get; set; }
    }
}
