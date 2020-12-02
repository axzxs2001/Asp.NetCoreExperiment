using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphQLBase002
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
                // .AddObjectType<TestType>()
                .Create();
            var executor = schema.MakeExecutable();

            Console.WriteLine(executor.Execute("{ hello }").ToJson());
        }
        public class Query
        {
            public Test Hello
            {
                get
                {
                    return new Test
                    {
                        ID = 100,
                        Name = "ABCD"
                    };
                }
            }
        }

        public class Test
        {
            public int ID { get; set; }

            public string Name { get; set; }
        }
        public class TestType : ObjectType<Test>
        {
            protected override void Configure(IObjectTypeDescriptor<Test> descriptor)
            {
                //  descriptor.Field<Test>(t => t.ID).Type<IntType>();
                //  descriptor.Field<Test>(t => t.Name).Type<StringType>();
            }
        }

        public class QueryType : ObjectType<Query>
        {
            protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
            {
                descriptor.Field<Query>(t => t.Hello).Type<NonNullType<TestType>>();
            }
        }
    }

    #endregion

}