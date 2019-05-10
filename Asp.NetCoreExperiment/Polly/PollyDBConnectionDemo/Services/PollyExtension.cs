using Npgsql;
using Polly;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PollyDBConnectionDemo
{
    public static class PollyExtension
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

        public async static Task PollyOpenAsync(this NpgsqlConnection connection)
        {
            //定义捕捉到的异常类型后重试
            var policy = Policy
                .Handle<NpgsqlException>()
                .Or<Exception>()
                //重试三次
                .RetryAsync(3, (excetpion, index, context) =>
                {
                    Console.WriteLine($"================{excetpion.Message}");
                    Console.WriteLine($"================{index}");
                });
            //开始执行
            await policy.ExecuteAsync(async () =>
            {
                await connection.OpenAsync();
                Console.WriteLine("=============开始");

            });
        }
    }
}
