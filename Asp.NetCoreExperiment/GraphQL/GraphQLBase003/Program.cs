using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Execution;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GraphQLBase003
{
    class Program
    {
        static void Main(string[] args)
        {
            // A.Run();
            // Console.WriteLine("=======================================");
            // B.Run();
            // Console.WriteLine("=======================================");
            DirectiveDemo.Run();
        }
    }

    #region A
    public class A
    {
        public static void Run()
        {
            var schema = SchemaBuilder.New()
                .AddProjections()
                .AddQueryType<Query>()
                //.AddDirectiveType<MyDirective>()
                .Create();
            var executor = schema.MakeExecutable();


            Console.WriteLine(executor.Execute("{ hello @skip(if: false)  }").ToJson());

        }
        public class Query
        {

            public string Hello() => "world";

        }

    }


    #endregion

    #region B
    public class B
    {
        public static void Run()
        {
            var schema = SchemaBuilder.New()
                .AddProjections()
                .AddQueryType<Query>()
                .Create();
            var executor = schema.MakeExecutable();


            Console.WriteLine(executor.Execute("{ test {id @skip(if: true) name  } }").ToJson());
            Console.WriteLine(executor.Execute("{ tests{id name @include(if :false)  age} }").ToJson());
        }
        public class Query
        {
            [UseProjection]
            public Test Test()
            {
                return new Test
                {
                    Id = 1,
                    Name = "AAAAA"
                };
            }

            [UseProjection]
            public List<Test> Tests()
            {
                return new List<Test>{ new Test
                {
                    Id = 100,
                    Name = "ABCD",
                    Age=10
                },
                new Test
                {
                    Id = 101,
                    Name = "EFGH",
                    Age=20
                }
                };

            }
        }
        public class Test
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int Age { get; set; }
        }


    }
    #endregion

    #region C
    public class DirectiveDemo
    {
        public static void Run()
        {
            var schema = SchemaBuilder.New()
                .AddProjections()
                .AddQueryType<Query>()
                .AddDirectiveType<UpperDirectiveType>()
                .AddDirectiveType<ReplaceDirectiveType>()
                .Create();
            var executor = schema.MakeExecutable();
            Console.WriteLine("原name=abcde ");
            Console.WriteLine("--------------转大写-------------------");
            Console.WriteLine(executor.Execute("{ student{id name @upper(name:\"this is test\")  age} }").ToJson());
            Console.WriteLine("--------------a替换成1 -------------------");
            Console.WriteLine(executor.Execute("{ student{id name @replace(old:\"a\",new:\"1\")  age} }").ToJson());
            Console.WriteLine("--------------然后全部转大写-.a替换成1 -------------------");
            Console.WriteLine(executor.Execute("{ student{id name @upper(name:\"this is test\") @replace(old:\"a\",new:\"1\")  age} }").ToJson());
            Console.WriteLine("--------------a替换成1.然后全部转大写-------------------");
            Console.WriteLine(executor.Execute("{ student{id name @replace(old:\"a\",new:\"1\")  @upper(name:\"this is test\") age} }").ToJson());
        }
        public class Query
        {
            [UseProjection]
            public Student GetStudent()
            {
                return new Student
                {
                    Id = 1,
                    Name = "abcde",
                    Age = 234
                };
            }
            [UseProjection]
            public List<Student> GetStudents()
            {
                return new List<Student>{
                    new Student
                    {
                        Id = 100,
                        Name = "aBcD",
                        Age=10
                    },
                    new Student
                    {
                        Id = 101,
                        Name = "EFGH",
                        Age=20
                    }
                };
            }
        }
        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }

        public class UpperDirectiveType : DirectiveType<UpperDirective>
        {
            protected override void Configure(IDirectiveTypeDescriptor<UpperDirective> descriptor)
            {
                descriptor.Name("upper");
                descriptor.Location(DirectiveLocation.Field);
                descriptor.Use(next => context =>
                {
                    foreach (var directive in context.FieldSelection.Directives)
                    {
                        if (directive.Name.Value == "upper")
                        {
                            if (context.Field.Member.MemberType == System.Reflection.MemberTypes.Property)
                            {
                                var pro = context.Field.Member as PropertyInfo;
                                var obj = context.GetType().GetMethod("Parent").MakeGenericMethod(context.ObjectType.RuntimeType).Invoke(context, new object[0]);
                                var value = pro.GetValue(obj);
                                pro.SetValue(obj, value.ToString().ToUpper());                            
                            }
                        }
                    }
                    return next.Invoke(context);
                });
            }
        }

        public class UpperDirective
        {
            public string Name
            {
                get;
                set;
            }
        }

        public class ReplaceDirectiveType : DirectiveType<ReplaceDirective>
        {

            protected override void Configure(IDirectiveTypeDescriptor<ReplaceDirective> descriptor)
            {
                descriptor.Name("replace");
                descriptor.Location(DirectiveLocation.Field);
                descriptor.Use(next => context =>
                {
                    foreach (var directive in context.FieldSelection.Directives)
                    {
                        if (directive.Name.Value == "replace")
                        {
                            var dir = new Dictionary<string, object>();
                            foreach (var item in directive.Arguments)
                            {
                                dir.Add(item.Name.Value?.ToLower(), item.Value.Value);
                            }
                            if (context.Field.Member.MemberType == System.Reflection.MemberTypes.Property)
                            {                                
                                var s = context.Parent<Student>();
                                var pro = context.Field.Member as PropertyInfo;
                                var obj = context.GetType().GetMethod("Parent").MakeGenericMethod(context.ObjectType.RuntimeType).Invoke(context, new object[0]);
                                var value = pro.GetValue(obj);
                                pro.SetValue(obj, value.ToString().Replace(dir["old"].ToString(), dir["new"].ToString()));                                
                            }
                        }
                    }
                    return next.Invoke(context);
                });


            }
        }

        public class ReplaceDirective
        {
            public string Old
            {
                get;
                set;
            }
            public string New
            {
                get;
                set;
            }
        }

    }
    #endregion
}
