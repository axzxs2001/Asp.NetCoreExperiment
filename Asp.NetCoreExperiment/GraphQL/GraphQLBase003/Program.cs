using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Execution;
using HotChocolate.Types;
using System;
using System.Collections.Generic;

namespace GraphQLBase003
{
    class Program
    {
        static void Main(string[] args)
        {
            A.Run();
            B.Run();
            C.Run();
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
    public class C
    {
        public static void Run()
        {
            var schema = SchemaBuilder.New()
                .AddProjections()
                .AddQueryType<Query>()
                .AddDirectiveType<MyDirectiveType>()
                .Create();
            var executor = schema.MakeExecutable();


            Console.WriteLine(executor.Execute("{ tests{id name @upper(name:\"this is test\")  age} }").ToJson());        
            Console.WriteLine(executor.Execute("{ tests{id name   age} }").ToJson());
        }
        public class Query
        {
            [UseProjection]
            public Test Test()
            {
                return new Test
                {
                    Id = 1,
                    Name = "AAAAA",
                    Age = 234
                };
            }

            [UseProjection]
            public List<Test> Tests()
            {
                return new List<Test>{ new Test
                {
                    Id = 100,
                    Name = "aBcD",
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

        public class MyDirectiveType : DirectiveType<MyDirective>
        {

            protected override void Configure(IDirectiveTypeDescriptor<MyDirective> descriptor)
            {
                descriptor.Name("upper");
                descriptor.Location(DirectiveLocation.Field);
                descriptor.Use(next => context =>
                {
                    Console.WriteLine(context.FieldSelection.Directives[0].Arguments[0].Name.Value);
                    Console.WriteLine(context.FieldSelection.Directives[0].Arguments[0].Value.Value);
                    context.Result = context.Parent<Test>().Name.ToUpper();
                    return next.Invoke(context);
                });

              
            }
        }

        public class MyDirective
        {
            public string Name
            {
                get;
                set;
            } = "无";
        }

    }
    #endregion
}
