using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;
using Npgsql;
namespace PollyDBConnectionDemo.Services
{
    public class AdoNetPolly
    {
        public async Task<int> GetCount()
        {
            #region 直接Polly方法实现
            //int i = 39;
            //PollyInvock(i =>
            //{
            //    using (var con = new SqlConnection($"server=192.168.252.{i};database=starpay;uid=sa;pwd=sa;Connect Timeout=5;"))
            //    {
            //        con.Open();
            //    }
            //});
            #endregion

            #region 扩展方法实现
            try
            {
                using (var con = new NpgsqlConnection($"Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=pp;Pooling=true;MinPoolSize=1;MaxPoolSize=100;CommandTimeout=0;"))
                {
                   //con.PollyOpen();
                   await con.PollyOpenAsync();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine($"============Excepiton:{exc.Message}");
            }
            #endregion
            return 5;
        }

        void PollyInvock(Action<int> action)
        {
            try
            {
                var i = 39;
                //定义捕捉到的异常类型后重试
                var policy = Policy
                    .Handle<SqlException>()
                    .Or<Exception>()
                    //重试三次
                    .Retry(3, (excetpion, index, context) =>
                    {
                        Console.WriteLine($"================{excetpion.Message}");
                        Console.WriteLine($"================{index}");
                    });

                //开始执行
                policy.Execute(() =>
                {
                    action(i++);
                    Console.WriteLine("=============开始");

                });
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Excepiton:{exc.Message}");
            }
        }
    }



   
}
