using Dapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DapperSqlReConnection
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
        public DapperPlusDB(IDbConnection dbConnection, string connectionString)
        {
            _dbConnection = dbConnection;
            _dbConnection.ConnectionString = connectionString;
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
            return _dbConnection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
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
            return await _dbConnection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
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
            return _dbConnection.Execute(sql, param, transaction, commandTimeout, commandType);
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
            return await _dbConnection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// 执行动态json参数的增删改语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="jsonString">json参数</param>
        /// <param name="timeStampField">时间戳字段</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">command超时时间(秒)</param>
        /// <param name="commandType">command类型</param>
        /// <returns></returns>
        public int Execute(string sql, string jsonString, string timeStampField = "timestamp", IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var pars = JsonToEntity(jsonString, timeStampField);
            return _dbConnection.Execute(sql, pars, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// 异常执行动态json参数的增删改语句
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="jsonString">json参数</param>
        /// <param name="timeStampField">时间戳字段</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">command超时时间(秒)</param>
        /// <param name="commandType">command类型</param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(string sql, string jsonString, string timeStampField = "timestamp", IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var pars = JsonToEntity(jsonString, timeStampField);
            return await _dbConnection.ExecuteAsync(sql, pars, transaction, commandTimeout, commandType);
        }
        /// <summary>
        /// json转DynamicParameters
        /// </summary>
        /// <param name="jsonString">json字符串</param>
        /// <param name="timeStampField">时间戳字段</param>
        /// <returns></returns>
        private DynamicParameters JsonToEntity(string jsonString, string timeStampField)
        {
            var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(jsonString);
            var pars = new DynamicParameters();
            foreach (var jToken in (jsonObj as JObject).Children())
            {
                //只处理json属性
                if (jToken.Type == JTokenType.Property)
                {
                    //处理时间戳字段的转换
                    if (((jToken as JProperty).Value as JValue).Path == timeStampField)
                    {
                        pars.Add((jToken as JProperty).Value.Path, (byte[])(jToken as JProperty).Value);
                    }
                    else
                    {
                        var jsonPro = ((jToken as JProperty).Value as JValue);
                        pars.Add(jsonPro.Path, jsonPro.Value);
                    }
                }
            }
            return pars;
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
            return _dbConnection.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
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
            return await _dbConnection.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }
    }
}
