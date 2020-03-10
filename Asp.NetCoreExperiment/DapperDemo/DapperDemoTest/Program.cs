using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using abc;
using Dapper;
using Npgsql;
namespace DapperDemoTest
{
    class Program
    {
        static string _connectionString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=postgres";
        static string GetDescriptionFromAttribute(MemberInfo member)
        {
            if (member == null) return null;

            var attrib = (DescriptionAttribute)Attribute.GetCustomAttribute(member, typeof(DescriptionAttribute), false);
            return attrib == null ? null : attrib.Description;
        }
        static void Main(string[] args)
        {
            //只对查询有用，对insert无用
            ColumnMapper.SetMapper();        
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            try
            {
                Console.WriteLine("1、添加    2、查询");
                switch (Console.ReadLine())
                {
                    case "1":
                        Add();
                        break;
                    case "2":
                        Query();
                        break;
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
                File.AppendAllText(Directory.GetCurrentDirectory() + "/log.txt", exc.Message);
            }
        }

        static void Query()
        {
            using (var con = new NpgsqlConnection(_connectionString))
            {
                var persons = con.Query<Person>(@"select * from public.t6");
                foreach (var person in persons)
                {
                    Console.WriteLine(person.LSHH);
                }
            }
        }

        static void Add()
        {
            var person = new Person() { Name = "aaa", Age = 1, Weight = 1.3, LSHH = "123" };
            using (var con = new NpgsqlConnection(_connectionString))
            {
                
                con.Execute(@"INSERT INTO public.t6(
	name, age, weight, lsh)
	VALUES (@name, @age, @weight, @lsh);", person);
            }
        }
    }
    public class Person
    {
        //非公有变量，查询可以获取，添加时不起作用
        //public string lsh;
        //internal string LSHS { get { return lsh; } set { lsh = value; } }
        [abc.Column(Name = "lsh")]
        public string LSHH { get; set; }

        public int ID { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public double Weight { get; set; }
    }
    public class ColumnMapper
    {
        public static void SetMapper()
        {
            //数据库字段名和c#属性名不一致，手动添加映射关系
            SqlMapper.SetTypeMap(typeof(Person), new ColumnAttributeTypeMapper<Person>());


            //每个需要用到[colmun(Name="")]特性的model，都要在这里添加映射

        }
    }




}
namespace abc
{
    public class ColumnAttributeTypeMapper<T> : FallbackTypeMapper
    {
        public ColumnAttributeTypeMapper()
            : base(new SqlMapper.ITypeMap[]
                {
                    new CustomPropertyTypeMap(
                       typeof(T),
                       (type, columnName) =>
                           type.GetProperties().FirstOrDefault(prop =>
                               prop.GetCustomAttributes(false)
                                   .OfType<ColumnAttribute>()
                                   .Any(attr => attr.Name == columnName)
                               )
                       ),
                    new DefaultTypeMap(typeof(T))
                })
        {
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ColumnAttribute : Attribute
    {
        public string Name { get; set; }
    }

    public class FallbackTypeMapper : SqlMapper.ITypeMap
    {
        private readonly IEnumerable<SqlMapper.ITypeMap> _mappers;

        public FallbackTypeMapper(IEnumerable<SqlMapper.ITypeMap> mappers)
        {
            _mappers = mappers;
        }
        public ConstructorInfo FindConstructor(string[] names, Type[] types)
        {
            foreach (var mapper in _mappers)
            {
                try
                {
                    ConstructorInfo result = mapper.FindConstructor(names, types);
                    if (result != null)
                    {
                        return result;
                    }
                }
                catch (NotImplementedException)
                {
                }
            }
            return null;
        }

        public SqlMapper.IMemberMap GetConstructorParameter(ConstructorInfo constructor, string columnName)
        {
            foreach (var mapper in _mappers)
            {
                try
                {
                    var result = mapper.GetConstructorParameter(constructor, columnName);
                    if (result != null)
                    {
                        return result;
                    }
                }
                catch (NotImplementedException)
                {
                }
            }
            return null;
        }

        public SqlMapper.IMemberMap GetMember(string columnName)
        {
            foreach (var mapper in _mappers)
            {
                try
                {
                    var result = mapper.GetMember(columnName);
                    if (result != null)
                    {
                        return result;
                    }
                }
                catch (NotImplementedException)
                {
                }
            }
            return null;
        }

        public ConstructorInfo FindExplicitConstructor()
        {
            return _mappers
                .Select(mapper => mapper.FindExplicitConstructor())
                .FirstOrDefault(result => result != null);
        }
    }
}
