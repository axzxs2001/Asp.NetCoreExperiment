using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ArchitectureDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("选择操作： 1、添加  2、查询  3、枚举返回集合");
                switch (Console.ReadLine())
                {
                    case "1":
                        //模拟api json post，自动把Sex,UserType整数转成实体类中的枚举类型
                        var newPerson = System.Text.Json.JsonSerializer.Deserialize<Person>(@"{""Name"":""桂素伟"",""Sex"":1,""UserType"":15}");
                        AddPerson(newPerson);
                        break;
                    case "2":
                        foreach (var person in GetPersons())
                        {
                            Console.WriteLine("程序中看到的数据");
                            Console.WriteLine($"Name:{person.Name},Sex:{person.Sex},UserType:{person.UserType}");
                            Console.WriteLine("给用户返回Json");
                            //模拟从数据库查出数据转发成json到前端
                            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(person));
                        }
                        break;
                    case "3":
                        Console.WriteLine("Sex集合：");
                        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(GetEnumList<Sex>()));
                        Console.WriteLine("UserType集合：");
                        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(GetEnumList<UserType>()));
                        break;
                }
            }

        }
        static void AddPerson(Person person)
        {
            using (var con = new MySqlConnection("server=localhost;database=marsdb;uid=root;pwd=mars2020;"))
            {
                var sql = @"INSERT INTO `marsdb`.`persons`(`id`,`name`,`sex`,`usertype`) VALUES(@id,@name,@sex,@usertype);";
                con.Execute(sql, person);
            }
        }

        static List<Person> GetPersons()
        {
            using (var con = new MySqlConnection("server=localhost;database=marsdb;uid=root;pwd=mars2020;"))
            {
                var sql = @"select * from `marsdb`.`persons`";
                return con.Query<Person>(sql).ToList();
            }
        }

        static List<dynamic> GetEnumList<T>() where T : struct, Enum
        {
            var enums = Enum.GetValues<T>();
            var list = new List<dynamic>();
            foreach (var item in enums)
            {
                list.Add(new { Name = item.ToString(), Value = Convert.ToInt32(item) });
            }
            return list;
        }
    }
    /*mysql库
CREATE TABLE `persons` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `sex` int DEFAULT NULL,
  `usertype` int DEFAULT NULL,
  PRIMARY KEY (`id`)
)
     */
    /// <summary>
    /// 人员类型
    /// </summary>
    public class Person
    {
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public Sex Sex { get; set; }
        /// <summary>
        /// 用户类型
        /// </summary>
        public UserType UserType { get; set; }

    }

    /// <summary>
    /// 性别枚举
    /// </summary>
    public enum Sex
    {
        /// <summary>
        /// 其他
        /// </summary>
        None = 0,
        /// <summary>
        /// 男
        /// </summary>
        Male = 1,
        /// <summary>
        /// 女
        /// </summary>
        Female = 2,
    }
    /// <summary>
    /// 用户枚举，位枚举
    /// </summary>
    [Flags]
    public enum UserType
    {
        /// <summary>
        /// app用户
        /// </summary>
        App = 1,
        /// <summary>
        /// web用户
        /// </summary>
        Web = 2,
        /// <summary>
        /// 小程序用户
        /// </summary>
        MiniPro = 4,
        /// <summary>
        /// 公众号用户
        /// </summary>
        OfficialAccounts = 8
    }
}
