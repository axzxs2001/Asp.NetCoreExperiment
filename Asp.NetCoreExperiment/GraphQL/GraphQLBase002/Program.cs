using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Types;
using System;

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
                .AddQueryType<Query>()
                .Create();
            var executor = schema.MakeExecutable();
            Console.WriteLine(executor.Execute("{ hello }").ToJson());
        }
        public class Query
        {
            public Test Hello() => new Test { ID = 100, Name = "桂素伟" };
        }  }

        public class Test
        {
            public int ID { get; set; }

            public string Name { get; set; }
        }
    }
    #endregion

}