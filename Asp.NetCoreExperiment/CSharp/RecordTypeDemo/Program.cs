using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RecordTypeDemo
{


    class Program
    {
        static void Main(string[] args)
        {
            //接口类型
            IShow show = new Entity1() { ID = 1, Name = "桂素伟" };
            Console.WriteLine(show);
            
            
            //抽象类型
            Entity entity = new Entity1() { ID = 1, Name = "桂素伟" };
            Console.WriteLine(entity);
            //注1:虽然 类型名{属性名1=属性值,属性名2=属性值,……} 相等，但比较结果是不等的
            Console.WriteLine($"(show == entity结果：{show == entity}");

            //实体类型
            var entity1 = new Entity1() { ID = 1, Name = "桂素伟" };
            Console.WriteLine(entity1);
            Console.WriteLine($"(entity1 == entity结果：{entity1 == entity}");


            AddEntity(entity);

            ReflectionTest(entity1);
        }

        /// <summary>
        /// 反射中使用record，和类相山
        /// </summary>
        /// <param name="entity"></param>
        static void ReflectionTest(Entity entity)
        {
            var type = Assembly.GetExecutingAssembly().GetType("RecordTypeDemo.Entity");
            Console.WriteLine(type.IsClass);
        }
        /// <summary>
        /// 对dapper适配
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        static bool AddEntity(Entity entity)
        {
            using (var con = new NpgsqlConnection("Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=postgres;"))
            {
                var list = con.Query<Entity1>("select * from entitys").ToList();
                if (!list.Contains(entity))
                {
                    con.Execute("insert into entitys(id,name) values(@id,@name)", entity);
                }
                return true;
            }
            /*表结构
CREATE TABLE public.entitys
(
    id integer NOT NULL DEFAULT nextval('entitys_id_seq'::regclass),
    name character varying(256) COLLATE pg_catalog."default",
    CONSTRAINT entitys_pkey PRIMARY KEY (id)
)
*/
        }
    }

    #region 面向对象特征和类一样
    /// <summary>
    /// 接口
    /// </summary>
    public interface IShow
    {
        void Show();
    }
    /// <summary>
    /// 抽象记录
    /// </summary>
    public abstract record Entity : IShow
    {
        public abstract int ID { get; set; }

        public abstract string Name { get; set; }

        public void Show()
        {
            Console.WriteLine($"{this.GetType().Name}:");
            Console.WriteLine($"{this.ToString()}");
        }
    }
    /// <summary>
    /// 记录
    /// </summary>
    public record Entity1 : Entity
    {
        public override int ID
        {
            get;
            set;
        }
        public override string Name
        {
            get;
            set;
        }
    }
    #endregion
}
