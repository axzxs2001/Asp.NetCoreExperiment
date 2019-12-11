using Dapper;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace DapperExtension
{
    /// <summary>
    /// IDapperPlusDB数据库类型 
    /// </summary>
    public interface IDapperPlusDB : IDisposable
    {
        /// <summary>
        /// 连接对象
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();

        /// <summary>
        /// 数据库标志
        /// </summary>
        string DataBaseMark { get; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        DataBaseType DataBaseType { get; }


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
        IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = false, int? commandTimeout = null, CommandType? commandType = null);


        /// <summary>
        /// 异步查询方法
        /// </summary>
        /// <typeparam name="T">映射实体类</typeparam>
        /// <param name="sql">sql语句</param>
        /// <param name="param">参数对象</param>
        /// <param name="transaction">事务</param>
        /// <param name="buffered">是否缓存结果</param>
        /// <param name="commandTimeout">command超时时间(秒)</param>
        /// <param name="commandType">command类型</param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);




        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="sql">映射实体类</param>
        /// <param name="param">参数对象</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">command超时时间(秒)</param>
        /// <param name="commandType">command类型</param>
        /// <returns></returns>
        int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// 异常执行方法
        /// </summary>
        /// <param name="sql">映射实体类</param>
        /// <param name="param">参数对象</param>
        /// <param name="transaction">事务</param>
        /// <param name="commandTimeout">command超时时间(秒)</param>
        /// <param name="commandType">command类型</param>
        /// <returns></returns>
        Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);


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
        T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

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
        Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);

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
        GridReader QueryMultiple(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);


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
        Task<GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
    }
}
