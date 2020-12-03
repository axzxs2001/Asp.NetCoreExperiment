using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Execution;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GraphQLBase002
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("**********A***********");
            A.Run();
            Console.WriteLine("**********B***********");
            B.Run();
            Console.WriteLine("**********C***********");
            C.Run();
            Console.WriteLine("**********D***********");
            D.Run();
        }
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


    #region B
    public class B
    {
        public static void Run()
        {
            var schema = SchemaBuilder.New()
                .AddQueryType<QueryType>()
                .Create();
            var executor = schema.MakeExecutable();
            Console.WriteLine(executor.Execute("{ hello }").ToJson());
        }
        public class Query
        {
            public IList<Test> Hello()
            {
                return new List<Test>() {
                    new Test {
                        ID = 100,
                        Name = "ABCD"
                    },
                     new Test {
                        ID = 101,
                        Name = "EFGH"
                    }
                };
            }
        }


        public class Test
        {
            public int ID { get; set; }

            public string Name { get; set; }
        }


        public class QueryType : ObjectType<Query>
        {
            protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
            {
                descriptor.Field<Query>(t => t.Hello()).Type<NonNullType<StringType>>().Resolver(ctx =>
               {
                   var result = ctx.Parent<Query>().Hello();
                   var json = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                   Console.WriteLine(json);
                   return json;
               });
            }
        }
    }

    #endregion

    #region C
    public class C
    {
        public static void Run()
        {
            var schema = SchemaBuilder.New()
                .AddQueryType<QueryType>()
                .Create();
            var executor = schema.MakeExecutable();


            Console.WriteLine(executor.Execute("{ OneTest{id name} }").ToJson());


            Console.WriteLine(executor.Execute("{ tests{id name} }").ToJson());
        }
        public class Query
        {

            public Test GetTest()
            {
                return new Test
                {
                    Id = 1,
                    Name = "AAAAA"
                };
            }

            public List<Test> Tests()
            {
                return new List<Test>{ new Test
                {
                    Id = 100,
                    Name = "ABCD"
                },
                new Test
                {
                    Id = 101,
                    Name = "EFGH"
                }
                };

            }
        }
        public class Test
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
        public class TestType : ObjectType<Test>
        {
            public Test test;
            protected override void Configure(IObjectTypeDescriptor<Test> descriptor)
            {
            }
        }

        public class QueryType : ObjectType<Query>
        {
            protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
            {
                descriptor.Field(t => t.GetTest()).Type<NonNullType<TestType>>().Name("OneTest");
                descriptor.Field(t => t.Tests()).Type<ListType<NonNullType<TestType>>>();
            }
        }
    }

    #endregion


    #region D
    public class D
    {
        public static void Run()
        {
            var schema = SchemaBuilder.New()
                .AddProjections()
                .AddQueryType<Query>()
                .Create();
            var executor = schema.MakeExecutable();


            Console.WriteLine(executor.Execute("{ test{id name} }").ToJson());


            Console.WriteLine(executor.Execute("{ tests{id name} }").ToJson());
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
            [ABC]
            [UseProjection]
            public List<Test> Tests()
            {
                return new List<Test>{ new Test
                {
                    Id = 100,
                    Name = "ABCD"
                },
                new Test
                {
                    Id = 101,
                    Name = "EFGH"
                }
                };

            }
        }
        public class Test
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

    
        public class ABCAttribute : ObjectFieldDescriptorAttribute
        {

            public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
            {
                Console.WriteLine("custom attribute ABC");
            }
        }
    }

    #endregion


}