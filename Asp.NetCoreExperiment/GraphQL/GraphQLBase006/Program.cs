using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Types;
using HotChocolate.Language.Utilities;
using System;
using System.Text;

namespace GraphQLBase006
{
    class Program
    {
        static void Main(string[] args)
        {        
            
            UnionDemo.Run();
        }
    }
    public class UnionDemo
    {

        public static void Run()
        {
            var schema = SchemaBuilder.New()                
                .AddQueryType<Query>()
                .AddType<Car>()
                .AddType<Cabbage>()
                .AddType<Earth>()
                .AddProjections()
                .Create();

            var executor = schema.MakeExecutable();
            Console.WriteLine(executor.Execute(@"
{
    formats
    {
        __typename,
        ... on Car{
            brand,
            price
        },
        ... on Cabbage{
            name,
            nutrition
        }
        ... on Earth{
            diameter        
        }
    } 
}").ToJson(withIndentations:false));

        
        }
    }


    public class Query
    {
        public IUnion[] GetFormats()
        {
            return new IUnion[]
            {
                    new Car{
                         Brand="Benz",
                         Price=1000000
                    },
                    new Cabbage{
                       Name="灰子白",
                       Nutrition="纤维"
                    },
                    new Earth{
                      Diameter=12742
                    }
            };
        }
    }

    [UnionType("Unio")]
    public interface IUnion
    {
    }
    public class Car : IUnion
    {
        public string Brand { get; set; }
        public decimal Price { get; set; }
    }

    public class Cabbage : IUnion
    {
        public string Name { get; set; }
        public string Nutrition { get; set; }
    }
    public class Earth : IUnion
    {
        public double Diameter { get; set; }
    }

}
