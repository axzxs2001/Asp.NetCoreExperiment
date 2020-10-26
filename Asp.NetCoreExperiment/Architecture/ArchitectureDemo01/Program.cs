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
                        var person = new Person
                        {                           
                            Name = "桂素伟",
                            Sex = Sex.Male,
                            UserType = UserType.App | UserType.Web
                        };
                        Console.WriteLine("录入信息是：");
                        Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(person));
                        AddPerson(person);
                        break;
                    case "2":
                        foreach (var per in GetPersons())
                        {
                            Console.WriteLine($"Name:{per.Name},Sex:{per.Sex},UserType:{per.UserType}");
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
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 性别，用枚举语义明确 
        /// </summary>
        public Sex Sex { get; set; }

        public UserType UserType { get; set; }

    }

    /// <summary>
    /// 一成不变的枚举类型
    /// </summary>
    public enum Sex
    {
        None = 0,
        Male = 1,
        Female = 2,
    }
    /// <summary>
    /// 位数组
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
        /// 不程序用户
        /// </summary>
        MiniPro = 4,
        /// <summary>
        /// 公众号用户
        /// </summary>
        OfficialAccounts = 8
    }
}
