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
            Console.WriteLine("=======================================");
            B.Run();
            Console.WriteLine("=======================================");
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
                .AddDirectiveType<UpperDirectiveType>()
                .AddDirectiveType<ReplaceDirectiveType>()
                .Create();
            var executor = schema.MakeExecutable();

            Console.WriteLine(executor.Execute("{ tests{id name @upper(name:\"this is test\")  age} }").ToJson());
            Console.WriteLine("---------------------------------");
            Console.WriteLine(executor.Execute("{ tests{id name @replace(old:\"E\",new:\"1\")  age} }").ToJson());
            Console.WriteLine("---------------------------------");
            Console.WriteLine(executor.Execute("{ tests{id name @upper(name:\"this is test\") @replace(old:\"e\",new:\"1\")  age} }").ToJson());
            Console.WriteLine("---------------------------------");
            Console.WriteLine(executor.Execute("{ tests{id name @replace(old:\"e\",new:\"1\")  @upper(name:\"this is test\") age} }").ToJson());
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
                    Name = "eFGH",
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
                            var test = context.Parent<Test>();
                            Console.WriteLine($"原始数据：ID={test.Id},Name={test.Name}");
                            context.Result = context.Parent<Test>().Name.ToUpper();
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
                            var test = context.Parent<Test>();
                            Console.WriteLine($"原始数据：ID={test.Id},Name={test.Name}");
                            context.Result = context.Parent<Test>().Name.Replace(dir["old"].ToString(), dir["new"].ToString());
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
