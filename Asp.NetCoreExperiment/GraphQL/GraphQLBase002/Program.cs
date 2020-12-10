using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Execution;
using HotChocolate.Types;
using System;
using System.Collections.Generic;


namespace GraphQLBase002
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**********A***********");
            A.Run();
            Console.WriteLine("**********B***********");
            FirstVersion.Run();
            Console.WriteLine("**********C***********");
            SecondVersion.Run();
            Console.WriteLine("**********D***********");
            ThreeVersion.Run();
        }
    }

    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }
    }

    #region A
    public class A
    {
        public static void Run()
        {
            var schema = SchemaBuilder.New()
                .AddQueryType<QueryType>()
                .Create();
            var executor = schema.MakeExecutable();
            Console.WriteLine(executor.Execute("{ now }").ToJson());
        }
        public class Query
        {
            public string Now()
            {
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            }
        }
        public class QueryType : ObjectType<Query>
        {
            protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
            {
                descriptor.Field<Query>(t => t.Now()).Type<StringType>();
            }
        }
    }
    #endregion


    #region FirstVersion
    public class FirstVersion
    {
        public static void Run()
        {
            var schema = SchemaBuilder.New()
                .AddQueryType<QueryType>()
                .Create();
            var executor = schema.MakeExecutable();
            //回为返回是字符串，所以用定义的Resolver name来查询
            Console.WriteLine(executor.Execute("{ students }").ToJson());
        }



        public class Query
        {
            public IList<Student> GetStudents()
            {
                return new List<Student>() {
                    new Student {
                        Id = 100,
                        Name = "ABCD",
                        Age=20
                    },
                     new Student {
                        Id = 101,
                        Name = "EFGH",
                        Age=19
                    }
                };
            }
        }


        public class QueryType : ObjectType<Query>
        {
            protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
            {
                //定义了有students来请求GetStudents方法，返回的类型是StringType，所以在Resolver中会把实体转成Json
                descriptor.Field<Query>(t => t.GetStudents()).Name("students").Type<NonNullType<StringType>>().Resolver(ctx =>
               {
                   var result = ctx.Parent<Query>().GetStudents();
                   return Newtonsoft.Json.JsonConvert.SerializeObject(result);
               });
            }
        }
    }

    #endregion

    #region SecondVersion
    public class SecondVersion
    {
        public static void Run()
        {
            var schema = SchemaBuilder.New()
                .AddQueryType<QueryType>()
                .Create();
            var executor = schema.MakeExecutable();


            Console.WriteLine(executor.Execute("{ student {id name age} }").ToJson());
            Console.WriteLine(executor.Execute("{ students {id name age} }").ToJson());
        }
        public class Query
        {

            public Student GetStudent()
            {
                return new Student
                {
                    Id = 1,
                    Name = "AAAAA",
                    Age = 19

                };
            }

            public List<Student> GetStudents()
            {
                return new List<Student>{
                    new Student
                    {
                        Id = 100,
                        Name = "ABCD",
                        Age = 19
                    },
                    new Student
                    {
                        Id = 101,
                        Name = "EFGH",
                        Age = 20
                    }
                };
            }
        }

        public class StudentType : ObjectType<Student>
        {

            protected override void Configure(IObjectTypeDescriptor<Student> descriptor)
            {
            }
        }

        public class QueryType : ObjectType<Query>
        {
            protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
            {
                descriptor.Field(t => t.GetStudent()).Type<NonNullType<StudentType>>().Name("student");
                descriptor.Field(t => t.GetStudents()).Type<ListType<NonNullType<StudentType>>>().Name("students");
            }
        }
    }

    #endregion


    #region ThreeVersion
    public class ThreeVersion
    {
        public static void Run()
        {
            var schema = SchemaBuilder.New()
                .AddProjections()
                .AddQueryType<Query>()
                .Create();
            var executor = schema.MakeExecutable();


            Console.WriteLine(executor.Execute("{ student{id name age} }").ToJson());

            Console.WriteLine(executor.Execute("{ students{id name age} }").ToJson());
        }
        public class Query
        {
            [UseProjection]            
            public Student GetStudent()
            {
                return new Student
                {
                    Id = 1,
                    Name = "AAAAA",
                    Age = 19

                };
            }
            [UseProjection]
            public List<Student> GetStudents()
            {
                return new List<Student>{
                    new Student
                    {
                        Id = 100,
                        Name = "ABCD",
                        Age = 19
                    },
                    new Student
                    {
                        Id = 101,
                        Name = "EFGH",
                        Age = 20
                    }
                };
            }
        }
    }

    #endregion


}