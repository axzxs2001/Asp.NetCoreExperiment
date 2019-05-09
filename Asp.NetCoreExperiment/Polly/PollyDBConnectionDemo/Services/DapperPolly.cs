using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using System.Data;
using Polly;
using Microsoft.Extensions.Logging;

namespace PollyDBConnectionDemo.Services
{
    public class DapperPolly
    {
        public int GetCount()
        {
            using (var con = new NpgsqlConnection("Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=pp;Pooling=true;MinPoolSize=1;MaxPoolSize=100;CommandTimeout=0;"))
            {
                var list = con.Query<dynamic>("select * from a");
                return list.Count();
            }
        }
    }


    public class ReliableDapper
    {
        /// <summary>
        /// 连接对象
        /// </summary>
        IDbConnection _dbConnection;
        ILogger<ReliableDapper> _logger;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="dbConnection">连接对象</param>
        /// <param name="connectionString">连接字符串</param>
        public ReliableDapper(IDbConnection dbConnection, string connectionString, ILogger<ReliableDapper> logger)
        {
            _dbConnection = dbConnection;
            _dbConnection.ConnectionString = connectionString;
            _logger = logger;
        }
        /// <summary>
        /// 连接对象
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            return _dbConnection;
        }
        /// <summary>
        /// 查询方法
        /// </summary>
        /// <typeparam name="T">映射实体类</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数对象</param>
        /// <param name="transaction">事务</param>
        /// <param name="buffered">是否缓存结果</param>
        /// <param name="commandTimeout">command超时时间(秒)</param>
        /// <param name="commandType">command类型</param>
        /// <returns></returns>
        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = false, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<T> result = null;
            PollyInvock(() =>
            {
                result = _dbConnection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
            });
            return result;
        }
        void PollyInvock(Action action)
        {
            var policy = Policy
                .Handle<NpgsqlException>()
                .Or<Exception>()
                .Retry(3, (excetpion, index, context) =>
                {
                    _logger.LogError($"===第{index}次，异常消息：{excetpion.Message}");
                });
            policy.Execute(() =>
            {
                action();
            });
        }

        private async Task PollyInvockAsync(Func<Task> func)
        {
            var policy = Policy
                .Handle<NpgsqlException>()
                .Or<Exception>()
                .RetryAsync(3, (excetpion, index, context) =>
                { });
            await policy.ExecuteAsync(() =>
            {
                return func();
            });
            //return Task.CompletedTask;
        }
        /// <summary>
        /// 查询异步方法
        /// </summary>
        /// <typeparam name="T">映射实体类</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数对象</param>
        /// <param name="transaction">事务</param> 
        /// <param name="commandTimeout">command超时时间(秒)</param>
        /// <param name="commandType">command类型</param>
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<T> result = null;
            await PollyInvockAsync(async () =>
             {
                 result = await _dbConnection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
             });
            return result;
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="sql">映射实体类</param>
        /// <param name="param">参数对象</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">command超时时间(秒)</param>
        /// <param name="commandType">command类型</param>
        /// <returns></returns>
        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            int result = 0;
            PollyInvock(() =>
            {
                result = _dbConnection.Execute(sql, param, transaction, commandTimeout, commandType);
            });
            return result;
        }

        /// <summary>
        /// 异步执行方法
        /// </summary>
        /// <param name="sql">映射实体类</param>
        /// <param name="param">参数对象</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">command超时时间(秒)</param>
        /// <param name="commandType">command类型</param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            int result = 0;
            await PollyInvockAsync(async () =>
            {
                result = await _dbConnection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
            });
            return result;
        }


        /// <summary>
        /// 查询单值
        /// </summary>
        /// <typeparam name="T">映射实体类</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数对象</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">command超时时间(秒)</param>
        /// <param name="commandType">command类型</param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            T result = default(T);
            PollyInvock(() =>
            {
                result = _dbConnection.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
            });
            return result;
        }

        /// <summary>
        /// 异步查询单值
        /// </summary>
        /// <typeparam name="T">映射实体类</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数对象</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">command超时时间(秒)</param>
        /// <param name="commandType">command类型</param>
        /// <returns></returns>
        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            T result = default(T);
            await PollyInvockAsync(async () =>
            {
                result = await _dbConnection.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
            });
            return result;
        }
    }
}
