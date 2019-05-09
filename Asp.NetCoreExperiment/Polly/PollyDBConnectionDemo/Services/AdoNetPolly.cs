using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace PollyDBConnectionDemo.Services
{
    public class AdoNetPolly
    {
        public int GetCount()
        {
            #region 一
            //int i = 39;
            //PollyInvock(i =>
            //{
            //    using (var con = new SqlConnection($"server=192.168.252.{i};database=starpay;uid=sa;pwd=sa;Connect Timeout=5;"))
            //    {
            //        con.Open();
            //    }
            //});
            #endregion

            #region 二
            try
            {
                using (var con = new SqlConnection($"server=192.168.252.39;database=starpay;uid=sa;pwd=sa;Connect Timeout=5;"))
                {
                    con.PollyOpen();
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine($"Excepiton:{exc.Message}");
            }
            #endregion
            return 1;
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
    static class TTT
    {
        public static void PollyOpen(this SqlConnection connection)
        {
            //定义捕捉到的异常类型后重试
            var policy = Policy
                .Handle<SqlException>()
                //.Or<Exception>()
                //重试三次
                .Retry(3, (excetpion, index, context) =>
                {
                    Console.WriteLine($"================{excetpion.Message}");
                    Console.WriteLine($"================{index}");
                });
            //开始执行
            policy.Execute(() =>
           {
               connection.Open();
               Console.WriteLine("=============开始");

           });
        }
    }
}
