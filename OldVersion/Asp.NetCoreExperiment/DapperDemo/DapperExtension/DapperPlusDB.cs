using Dapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Polly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DapperExtension
{
    /// <summary>
    /// IDapperPlusDB数据库类型 
    /// </summary>
    public class DapperPlusDB : IDapperPlusDB
    {
        /// <summary>
        /// 连接对象
        /// </summary>
        IDbConnection _dbConnection;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="dbConnection">连接对象</param>
        /// <param name="connectionString">连接字符串</param>
        public DapperPlusDB(IDbConnection dbConnection, string connectionString = null)
        {
            switch (dbConnection.GetType().Name)
            {
                case "SqliteConnection":
                    DataBaseType = DataBaseType.Sqlite;
                    break;
                case "NpgsqlConnection":
                    DataBaseType = DataBaseType.Postgre;
                    break;
                case "SqlClientConnection":
                    DataBaseType = DataBaseType.SqlServer;
                    break;
                case "OracleConnection":
                    DataBaseType = DataBaseType.Oracle;
                    break;
                case "MySqlConnection":
                    DataBaseType = DataBaseType.MySql;
                    break;
            }
            if (!string.IsNullOrEmpty(connectionString))
            {
                dbConnection.ConnectionString = connectionString;
            }
            _dbConnection = dbConnection;
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="dbConnection">连接对象</param>
        /// <param name="dataBaseMark">数据库标志</param>
        /// <param name="connectionString">连接字符串</param>
        public DapperPlusDB(IDbConnection dbConnection, string dataBaseMark, string connectionString = null)
        {
            DataBaseMark = dataBaseMark;
            switch (dbConnection.GetType().Name)
            {
                case "SqliteConnection":
                    DataBaseType = DataBaseType.Sqlite;
                    break;
                case "NpgsqlConnection":
                    DataBaseType = DataBaseType.Postgre;
                    break;
                case "SqlClientConnection":
                    DataBaseType = DataBaseType.SqlServer;
                    break;
                case "OracleConnection":
                    DataBaseType = DataBaseType.Oracle;
                    break;
                case "MySqlConnection":
                    DataBaseType = DataBaseType.MySql;
                    break;
            }
            if (!string.IsNullOrEmpty(connectionString))
            {
                dbConnection.ConnectionString = connectionString;
            }
            _dbConnection = dbConnection;
        }
        /// <summary>
        /// 数据库标志
        /// </summary>
        public string DataBaseMark { get; }
        /// <summary>
        /// 数据库类型
        /// </summary>
        public DataBaseType DataBaseType { get; }



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
            var result = 0;
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
            var result = 0;
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
            var result = default(T);
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
            var result = default(T);
            await PollyInvockAsync(async () =>
            {
                result = await _dbConnection.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
            });
            return result;
        }

        /// <summary>
        /// 查询多返回结果方法
        /// </summary>
        /// <typeparam name="T">映射实体类</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数对象</param>
        /// <param name="transaction">事务</param>    
        /// <param name="commandTimeout">command超时时间(秒)</param>
        /// <param name="commandType">command类型</param>
        /// <returns></returns>
        public GridReader QueryMultiple(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _dbConnection.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// 异常查询多返回结果方法
        /// </summary>
        /// <typeparam name="T">映射实体类</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数对象</param>
        /// <param name="transaction">事务</param>    
        /// <param name="commandTimeout">command超时时间(秒)</param>
        /// <param name="commandType">command类型</param>
        /// <returns></returns>
        public async Task<GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _dbConnection.QueryMultipleAsync(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// 重试调用
        /// </summary>
        /// <param name="action"></param>
        private void PollyInvock(Action action)
        {
            var policy = Policy
                .Handle<Exception>()
                .Retry(3, (excetpion, index, context) =>
                {
                    Console.WriteLine(excetpion.Message);
                });
            policy.Execute(() =>
            {
                action();
            });
        }
        /// <summary>
        /// 异常重试调用
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        private async Task PollyInvockAsync(Func<Task> func)
        {
            var policy = Policy
                .Handle<Exception>()
                .RetryAsync(3, (excetpion, index, context) =>
                {
                    Console.WriteLine(excetpion.Message);
                });
            await policy.ExecuteAsync(() =>
            {
                return func();
            });           
        }

        public void Dispose()
        {
            if (_dbConnection != null)
            {
                _dbConnection.Dispose();
            }
        }
    }
}
