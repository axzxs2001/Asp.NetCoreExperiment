using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Types;
using System;

namespace GraphQLBase001
{
    class Program
    {
        static void Main(string[] args)
        {
            var schemaString = @"
        type Query {
            hello: String
        }";
            Console.WriteLine("Schema-First");
            SchemaFirst.Run(schemaString);
            Console.WriteLine("Schema-First");
            CodeFirst.Run(schemaString);
            Console.WriteLine("PurCode-First");
            PureCodeFirst.Run();
            C.Run(schemaString);
            D.Run(schemaString);
            E.Run();
        }
    }
    #region SchemaFirst
    public class SchemaFirst
    {
        public static void Run(string schemaString)
        {
            var schema = SchemaBuilder
                .New()
                .AddDocumentFromString(schemaString)
                .AddResolver("Query", "hello", () => "world")
                .Create();
            var executor = schema.MakeExecutable();
            Console.WriteLine(executor.Execute("{ hello }").ToJson());
        }

    }
    #endregion
    #region CodeFirst
    public class CodeFirst
    {
        public static void Run(string schemaString)
        {
            var schema = SchemaBuilder
                .New()
                .AddDocumentFromString(schemaString)
                .BindComplexType<Query>()
                .Create();
            var executor = schema.MakeExecutable();
            Console.WriteLine(executor.Execute("{ hello }").ToJson());
        }
        public class Query
        {
            /// <summary>
            /// 目测这里只对Hello或GetHello免疫
            /// </summary>
            /// <returns></returns>
            public string Hello() => "world";
        }
    }
    #endregion
    #region PureCodeFirst
    public class PureCodeFirst
    {
        public static void Run()
        {
            var schema = SchemaBuilder
                .New()        
                .AddQueryType<Query>()
                .Create();
            var executor = schema.MakeExecutable();
            Console.WriteLine(executor.Execute("{ hello }").ToJson());
        }
        public class Query
        {
            /// <summary>
            /// 目测这里只对Hello或GetHello免疫
            /// </summary>
            /// <returns></returns>
            public string Hello() => "world";
        }
    }
    #endregion
    #region C
    public class C
    {
        public static void Run(string schemaString)
        {
            var schema = SchemaBuilder
                .New()
                .AddDocumentFromString(schemaString)
                .BindComplexType<Query>(c => c.Field(t => t.GetGreetings()).Name("hello"))
                .Create();
            var executor = schema.MakeExecutable();
            Console.WriteLine(executor.Execute("{ hello }").ToJson());
        }
        public class Query
        {
            public string GetGreetings() => "world";
        }
    }
    #endregion
    #region D
    public class D
    {
        public static void Run(string schemaString)
        {
            var schema = SchemaBuilder
                .New()
                .AddDocumentFromString(schemaString)
                .BindComplexType<Query>(c => c.Field(t => t.GetGreetings()))
                .BindResolver<QueryResolvers>(c => c.To<Query>())
                .Create();
            var executor = schema.MakeExecutable();
            Console.WriteLine(executor.Execute("{ hello }").ToJson());
        }
        public class Query
        {
            public string GetGreetings() => "world";
        }
        public class QueryResolvers
        {
            public string GetHello([Parent] Query query) => query.GetGreetings();
        }
    }
    #endregion
    #region E
    public class E
    {
        public static void Run()
        {
            var schema = SchemaBuilder
                .New()
                .AddQueryType<QueryType>()
                .Create();
            var executor = schema.MakeExecutable();
            Console.WriteLine(executor.Execute("{ hello }").ToJson());
            Console.WriteLine(executor.Execute("{ foo }").ToJson());
        }
        public class QueryType : ObjectType<Query>
        {
            protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
            {
                descriptor.Field("foo").Resolver("bar");
            }
        }
        public class Query
        {
            public string Hello() => "World";
        }

    }
    #endregion

}

