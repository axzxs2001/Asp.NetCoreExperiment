using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Execution;
using HotChocolate.Language;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using HotChocolate.Types.Relay;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GraphQLBase004
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectiveDemo.Run();
        }
    }
    public class DirectiveDemo
    {
        public static void Run()
        {
            var schema = SchemaBuilder.New()
                .AddProjections()
                .AddFiltering()
                .AddQueryType<Query>()
                .Create();
            var executor = schema.MakeExecutable();
            Console.WriteLine(executor.Execute("{ student{id name age} }").ToJson());

        }
        public class Query
        {
            [UseProjection]
            [SomeMiddleware]
            public Student GetStudent()
            {
                return new Student
                {
                    Id = 1,
                    Name = "abcde",
                    Age = 234
                };
            }

            [UseProjection]
            public List<Student> GetStudents()
            {
                return new List<Student>{
                    new Student
                    {
                        Id = 100,
                        Name = "aBcD",
                        Age=10
                    },
                    new Student
                    {
                        Id = 101,
                        Name = "EFGH",
                        Age=20
                    }
                };
            }
        }
        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
        }
        public class SomeMiddlewareAttribute : ObjectFieldDescriptorAttribute
        {

            public string ABC
            {
                get; set;
            }
            public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
            {
                descriptor.Use(next => context =>
                {
                    var obj = context.GetType().GetMethod("Parent").MakeGenericMethod(context.ObjectType.RuntimeType).Invoke(context, new object[0]);
                    var value = (member as MethodInfo).Invoke(obj, new object[0]);
                    context.Result = new Student
                    {
                        Id = 101,
                        Name = "EFGH",
                        Age = 20
                    };
                    return next.Invoke(context);
                });
            }
        }

    }
    public static class SomeSchemaBuilderExtensions
    {
        public static ISchemaBuilder AddABC(this ISchemaBuilder builder)
        {
            
            return builder;
        }
     
    }
}
