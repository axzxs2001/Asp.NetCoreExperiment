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
            var person = new Person
            {
                //ID = 1,
                Name = "桂素伟",
                Sex = Sex.Male
            };
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(person));

            AddPerson(person);

            foreach (var per in GetPersons())
            {
                Console.WriteLine($"Name:{per.Name},Sex:{per.Sex}");
            }

            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(GetSexs()));

        }
        static void AddPerson(Person person)
        {
            using (var con = new MySqlConnection("server=localhost;database=marsdb;uid=root;pwd=mars2020;"))
            {
                var sql = @"INSERT INTO `marsdb`.`persons`(`id`,`name`,`sex`) VALUES(@id,@name,@sex);";
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

        static List<dynamic> GetSexs()
        {
            var sexs = Enum.GetValues<Sex>();
            var list = new List<dynamic>();
            foreach (var sex in sexs)
            {
                list.Add(new { Name = sex.ToString(), Value = (int)sex });
            }
            return list;
        }
    }
    /*mysql库
   CREATE TABLE `persons` (
  `id` int NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `sex` int DEFAULT NULL,
  PRIMARY KEY (`id`)
   ) 
     */
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 语义明确
        /// 
        /// </summary>
        public Sex Sex { get; set; }

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
}
