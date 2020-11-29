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
        }
    }

    #region A
    public class A
    {
        public static void Run()
        {
            var schema = SchemaBuilder.New()
                .AddQueryType<Query>()
                .Create();
            var executor = schema.MakeExecutable();
            Console.WriteLine(executor.Execute("{ hello }").ToJson());
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
                        Name = "桂素伟"
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
                // descriptor.Field(t => t.Name).Type<NonNullType<StringType>>();
                descriptor.Field<Query>(t => t.Hello()).Type<ListType<NonNullType<StringType>>>().Resolver(ctx =>
                {
                    var ttt = ctx.Service<Query>().Hello();


                    return "foo";
                }); ;
            }
        }
    }

    #endregion

}