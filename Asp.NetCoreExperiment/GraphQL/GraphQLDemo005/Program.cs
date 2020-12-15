using GreenDonut;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Execution;
using HotChocolate.Fetching;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLDemo005
{

    class Program
    {
        static void Main(string[] args)
        {
            DataLoaderDemo.Run();
        }
    }
    public class DataLoaderDemo
    {

        public static void Run()
        {
            var schema = SchemaBuilder.New()
                .AddProjections()
                .AddQueryType<Query>()
             
                .Create();
            var executor = schema.MakeExecutable();
            Console.WriteLine(executor.ExecuteAsync("{ person(id:1){id name tel} }").Result.ToJson());

        }
        /// <summary>
        /// 查询类
        /// </summary>
        public class Query
        {
            [UseProjection]
            public Task<Person> GetPerson(int id, [DataLoader] PersonDataLoader personLoader)
            {            
                return personLoader.LoadAsync(id);
            }
        }

        public class PersonDataLoader : DataLoaderBase<int, Person>
        {
            public PersonDataLoader() : base(new BatchScheduler(), new DataLoaderOptions<int>())
            {
            }
            protected override ValueTask<IReadOnlyList<Result<Person>>> FetchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
            {
                var list = new List<Result<Person>>();
                for (int i = 0; i < 200; i++)
                {
                    var person1 = new Person { Id = i, Tel = "12323232321", Name = "张三"+i };
                    var result1 = Result<Person>.Resolve(person1);
                    list.Add(result1);
                }
                var result = new ValueTask<IReadOnlyList<Result<Person>>>(list);
                return result;
            }

        }
        /// <summary>
        /// 用户
        /// </summary>
        public class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Tel { get; set; }
        }
    }

}
