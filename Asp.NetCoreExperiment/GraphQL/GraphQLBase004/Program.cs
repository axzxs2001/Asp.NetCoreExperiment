using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Execution;
using HotChocolate.Types;
using HotChocolate.Types.Descriptors;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace GraphQLBase004
{
    class Program
    {
        static void Main(string[] args)
        {
            DescriptorAttributeDemo.Run();
        }
    }
    public class DescriptorAttributeDemo
    {
        public static void Run()
        {
            var schema = SchemaBuilder.New()
                .AddProjections()
                .AddQueryType<Query>()
                .Create();
            var executor = schema.MakeExecutable();
            Console.WriteLine(executor.Execute("{ student{id userName password tel} }").ToJson());
            Console.WriteLine("===============");
            Console.WriteLine(executor.Execute("{ students{id userName password tel} }").ToJson());
        }
        /// <summary>
        /// 查询类
        /// </summary>
        public class Query
        {
            [UseProjection]
            [UseDesensitization(SensitiveFields = new string[] { "password", "tel" })]
            public User GetStudent()
            {
                return new User
                {
                    Id = 1,
                    UserName = "gsw",
                    Tel = "13453467114",
                    Password = "111111"
                };
            }
            [UseProjection]
            [UseDesensitization(SensitiveFields = new string[] { "password", "tel" })]
            public List<User> GetStudents()
            {
                return new List<User>(){
                    new User
                    {
                        Id = 1,
                        UserName = "gsw",
                        Tel = "13453467114",
                        Password = "111111"
                    },
                    new User
                    {
                        Id = 1,
                        UserName = "gsw",
                        Tel = "13453467114",
                        Password = "111111"
                    }
                };
            }
        }
        /// <summary>
        /// 用户
        /// </summary>
        public class User
        {
            public int Id { get; set; }
            public string UserName { get; set; }
            public string Tel { get; set; }
            public string Password { get; set; }
        }
        /// <summary>
        /// 脱敏特性类
        /// </summary>
        public class UseDesensitizationAttribute : ObjectFieldDescriptorAttribute
        {
            public string[] SensitiveFields
            {
                get; set;
            }
            public override void OnConfigure(IDescriptorContext context, IObjectFieldDescriptor descriptor, MemberInfo member)
            {
                descriptor.Use(next => context =>
                {
                    var obj = context.GetType().GetMethod("Parent").MakeGenericMethod(context.ObjectType.RuntimeType).Invoke(context, new object[0]);
                    var resultObj = (member as MethodInfo).Invoke(obj, new object[0]);
                    foreach (var proName in SensitiveFields)
                    {
                        var resulttType = resultObj.GetType();
                        //处理泛型集合
                        if (resulttType.IsGenericType)
                        {
                            foreach (var resultItem in (resultObj as IList))
                            {

                                SetValue(proName, resultItem.GetType(), resultItem);
                            }
                        }
                        else
                        {
                            SetValue(proName, resulttType, resultObj);
                        }
                        void SetValue(string proName, Type type, object resultObj)
                        {
                            var pro = type.GetProperty(proName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
                            if (pro != null && pro.PropertyType.IsAssignableFrom(typeof(string)))
                            {
                                var len = pro.GetValue(resultObj).ToString()?.Length;
                                pro.SetValue(resultObj, "".PadLeft(len.Value, '*'));
                            }
                        }
                    }
                    context.Result = resultObj;
                    return next.Invoke(context);
                });
            }
        }
    }
}
