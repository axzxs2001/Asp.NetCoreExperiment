using GreenDonut;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Execution;
using HotChocolate.Execution.Instrumentation;
using HotChocolate.Fetching;
using HotChocolate.Language;
using Microsoft.Extensions.DiagnosticAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            Console.WriteLine(executor.Execute("{person(id:1){id name tel} }").ToJson());

        }
        /// <summary>
        /// 查询类
        /// </summary>
        public class Query
        {
            [UseProjection]
            public Person GetPerson(int id)
            {
                return new Person { Id = id, Name = "ZhangSanFeng", Tel = "13453467114" };
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
