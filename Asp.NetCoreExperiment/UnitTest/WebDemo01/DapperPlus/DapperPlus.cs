using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace WebDemo01.Services
{
    /// <summary>
    /// DappePlusr类
    /// </summary>
    public class DapperPlus : IDapperPlus
    {
        protected IDbConnection _connection;
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public DapperPlus()
        {
        }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="connection">连接</param>
        /// <param name="configuration">配置</param>
        public DapperPlus(IDbConnection connection, IConfiguration configuration)
        {
            _connection = connection;
            _connection.ConnectionString = configuration.GetConnectionString("DefaultConnectionString");
        }
        /// <summary>
        /// 连接
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                return _connection;
            }
        }
        /// <summary>
        /// 获取数据库类型
        /// </summary>
        public DataBaseType DataBaseType
        {
            get
            {
                if (_connection != null)
                {
                    switch (_connection.GetType().Name)
                    {
                        case "MySqlConnection":
                            return DataBaseType.MySql;
                        case "MsSqlConnection":
                            return DataBaseType.MsSql;
                        case "NpgsqlConnection":
                            return DataBaseType.MsSql;
                    }
                }
                throw new Exception("connection is null");
            }
        }
        #region Execute

        /// <summary>
        /// Execute parameterized SQL.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The number of rows affected.</returns>
        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.Execute(sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Execute parameterized SQL.
        /// </summary>
        /// <param name="command">The command to execute on this connection.</param>
        /// <returns>The number of rows affected.</returns>
        public int Execute(CommandDefinition command)
        {
            return _connection.Execute(command);
        }

        /// <summary>
        /// Execute a command asynchronously using Task.
        /// </summary>
        /// <param name="command">The command to execute on this connection.</param>
        /// <returns>The number of rows affected.</returns>
        public async Task<int> ExecuteAsync(CommandDefinition command)
        {
            return await _connection.ExecuteAsync(command);
        }

        /// <summary>
        /// Execute a command asynchronously using Task.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The number of rows affected.</returns>
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Execute parameterized SQL and return an System.Data.IDataReader.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="commandBehavior">The System.Data.CommandBehavior flags for this reader.</param>
        /// <returns>
        ///  An System.Data.IDataReader that can be used to iterate over the results of the SQL query.    
        ///   This is typically used when the results of a query are not processed by Dapper,for example, used to fill a System.Data.DataTable or DataSet.
        /// </returns>
        public IDataReader ExecuteReader(CommandDefinition command, CommandBehavior commandBehavior)
        {
            return _connection.ExecuteReader(command, commandBehavior);
        }

        /// <summary>
        /// Execute parameterized SQL and return an System.Data.IDataReader.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>
        ///  An System.Data.IDataReader that can be used to iterate over the results of the SQL query.    
        ///   This is typically used when the results of a query are not processed by Dapper,for example, used to fill a System.Data.DataTable or DataSet.
        /// </returns>
        public IDataReader ExecuteReader(CommandDefinition command)
        {
            return _connection.ExecuteReader(command);
        }

        /// <summary>
        /// Execute parameterized SQL and return an System.Data.IDataReader.
        /// </summary>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="transaction">The transaction to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>
        ///  An System.Data.IDataReader that can be used to iterate over the results of the SQL query.     
        ///  This is typically used when the results of a query are not processed by Dapper,for example, used to fill a System.Data.DataTable or DataSet.
        /// </returns>        
        public IDataReader ExecuteReader(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.ExecuteReader(sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Execute parameterized SQL and return an System.Data.IDataReader.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <param name="commandBehavior">The System.Data.CommandBehavior flags for this reader.</param>
        /// <returns>
        /// An System.Data.IDataReader that can be used to iterate over the results of the SQL query.
        /// This is typically used when the results of a query are not processed by Dapper,for example, used to fill a System.Data.DataTable or DataSet.
        /// </returns>
        public async Task<IDataReader> ExecuteReaderAsync(CommandDefinition command, CommandBehavior commandBehavior)
        {
            return await _connection.ExecuteReaderAsync(command, commandBehavior);
        }

        /// <summary>
        /// Execute parameterized SQL and return an System.Data.IDataReader.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>
        /// An System.Data.IDataReader that can be used to iterate over the results of the SQL query.
        /// This is typically used when the results of a query are not processed by Dapper,for example, used to fill a System.Data.DataTable or DataSet.
        /// </returns>
        public async Task<IDataReader> ExecuteReaderAsync(CommandDefinition command)
        {
            return await _connection.ExecuteReaderAsync(command);
        }

        /// <summary>
        /// Execute parameterized SQL and return an System.Data.IDataReader.
        /// </summary>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="transaction">The transaction to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>
        /// An System.Data.IDataReader that can be used to iterate over the results of the SQL query.
        /// This is typically used when the results of a query are not processed by Dapper,for example, used to fill a System.Data.DataTable or DataSet.
        /// </returns>
        public async Task<IDataReader> ExecuteReaderAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.ExecuteReaderAsync(sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="transaction">The transaction to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell selected as System.Object.</returns>
        public object ExecuteScalar(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.ExecuteScalar(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="transaction">The transaction to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as T.</returns>
        public T ExecuteScalar<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
        }

        //     The first cell selected as T.

        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <returns>The first cell selected as T.</returns>
        public T ExecuteScalar<T>(CommandDefinition command)
        {
            return _connection.ExecuteScalar<T>(command);
        }


        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns> The first cell selected as System.Object.</returns>
        public object ExecuteScalar(CommandDefinition command)
        {
            return _connection.ExecuteScalar(command);
        }


        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <param name="command">The command to execute.</param>
        /// <returns>The first cell selected as System.Object.</returns>
        public async Task<object> ExecuteScalarAsync(CommandDefinition command)
        {
            return await _connection.ExecuteScalarAsync(command);
        }

        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="transaction">The transaction to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as T.</returns>
        public async Task<T> ExecuteScalarAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="transaction">The transaction to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as System.Object.</returns>
        public async Task<object> ExecuteScalarAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.ExecuteScalarAsync(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute parameterized SQL that selects a single value.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <returns>The first cell selected as T.</returns>
        public async Task<T> ExecuteScalarAsync<T>(CommandDefinition command)
        {
            return await _connection.ExecuteScalarAsync<T>(command);
        }
        #endregion

        #region Query 
        /// <summary>
        /// Executes a single-row query, returning the data typed as type.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="buffered">Whether to buffer results in memory.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>A sequence of data of the supplied type; if a basic type(int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is created per row, and a direct column-name===member-name mapping is assumed(case insensitive).
        /// 异常:T:System.ArgumentNullException:type is null.
        /// </returns>
        public IEnumerable<object> Query(Type type, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.Query(type, sql, param, transaction, buffered, commandTimeout, commandType);
        }


        /// <summary>
        /// Executes a query, returning the data typed as T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="buffered">Whether to buffer results in memory.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        ///  A sequence of data of the supplied type; if a basic type(int, string, etc) is queried then the data from the first column is assumed, otherwise an instance is created per row, and a direct column-name===member-name mapping is assumed (case insensitive).    
        /// </returns>
        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }


        /// <summary>
        /// Perform a multi-mapping query with an arbitrary number of input types. This returns a single type, combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="types">Array of types in the recordset.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of TReturn.</returns>
        public IEnumerable<TReturn> Query<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.Query<TReturn>(sql, types, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <summary>
        /// Perform a multi-mapping query with 7 input types. If you need more types -> use Query with Type[] parameter. This returns a single type, combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TSixth">The sixth type in the recordset.</typeparam>
        /// <typeparam name="TSeventh">The seventh type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of TReturn.</returns>
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <summary>
        /// Perform a multi-mapping query with 6 input types. This returns a single type,combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TSixth">The sixth type in the recordset.</typeparam>       
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of TReturn.</returns>
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }


        /// <summary>
        /// Perform a multi-mapping query with 2 input types. This returns a single type,combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>       
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of TReturn.</returns>
        public IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.Query<TFirst, TSecond, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <summary>
        /// Perform a multi-mapping query with 5 input types. This returns a single type,combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of TReturn.</returns>
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <summary>
        /// Perform a multi-mapping query with 4 input types. This returns a single type,combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of TReturn.</returns> 
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.Query<TFirst, TSecond, TThird, TFourth, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <summary>
        /// Return a sequence of dynamic objects with properties matching the columns.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>Note: each row can be accessed via "dynamic", or by casting to an IDictionary<string,object></returns>
        public IEnumerable<dynamic> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.Query(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        /// <summary>
        /// Perform a multi-mapping query with 3 input types. This returns a single type,combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of TReturn.</returns> 
        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.Query<TFirst, TSecond, TThird, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <summary>
        /// Executes a query, returning the data typed as T.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns>A sequence of data of T; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is created per row, and a direct column-name===member-name mapping is assumed (case insensitive).</returns>
        public IEnumerable<T> Query<T>(CommandDefinition command)
        {
            return _connection.Query<T>(command);
        }

        /// <summary>
        /// Perform an asynchronous multi-mapping query with 2 input types. This returns a single type, combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(CommandDefinition command, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id")
        {
            return await _connection.QueryAsync<TFirst, TSecond, TReturn>(command, map, splitOn);
        }

        /// <summary>
        /// Perform an asynchronous multi-mapping query with 2 input types. This returns a single type, combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of TReturn.</returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryAsync<TFirst, TSecond, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a query asynchronously using Task.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>Note: each row can be accessed via "dynamic", or by casting to an IDictionary<string,object></returns>
        public async Task<IEnumerable<dynamic>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryAsync<dynamic>(sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Execute a query asynchronously using Task.
        /// </summary>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns>Note: each row can be accessed via "dynamic", or by casting to an IDictionary<string,object></returns>
        public async Task<IEnumerable<dynamic>> QueryAsync(CommandDefinition command)
        {
            return await _connection.QueryAsync(command);
        }



        /// <summary>
        /// Perform an asynchronous multi-mapping query with 3 input types. This returns a single type, combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of TReturn.</returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryAsync<TFirst, TSecond, TThird, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }




        /// <summary>
        /// Perform an asynchronous multi-mapping query with 3 input types. This returns a single type, combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(CommandDefinition command, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn = "Id")
        {
            return await _connection.QueryAsync<TFirst, TSecond, TThird, TReturn>(command, map, splitOn);
        }




        /// <summary>
        /// Perform an asynchronous multi-mapping query with 4 input types. This returns a single type, combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of TReturn.</returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }


        /// <summary>
        /// Perform an asynchronous multi-mapping query with 4 input types. This returns a single type, combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, string splitOn = "Id")
        {
            return await _connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(command, map, splitOn);
        }



        /// <summary>
        /// Execute a query asynchronously using Task.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns></returns>
        public async Task<IEnumerable<object>> QueryAsync(Type type, CommandDefinition command)
        {
            return await _connection.QueryAsync(type, command);
        }


        /// <summary>
        /// Execute a query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>A sequence of data of T; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is created per row,and a direct column-name===member-name mapping is assumed (case insensitive).</returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Perform an asynchronous multi-mapping query with 5 input types. This returns a single type, combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of TReturn.</returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }



        /// <summary>
        /// Perform an asynchronous multi-mapping query with 6 input types. This returns a single type, combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TSixth">The sixth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of TReturn.</returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }


        /// <summary>
        /// Perform an asynchronous multi-mapping query with 6 input types. This returns a single type, combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TSixth">The sixth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, string splitOn = "Id")
        {
            return await _connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(command, map, splitOn);
        }


        /// <summary>
        /// Perform an asynchronous multi-mapping query with 7 input types. This returns a single type, combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TSixth">The sixth type in the recordset.</typeparam>
        /// <typeparam name="TSeventh">The seventh type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of TReturn.</returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(sql, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }


        /// <summary>
        /// Perform an asynchronous multi-mapping query with 7 input types. This returns a single type, combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TSixth">The sixth type in the recordset.</typeparam>
        /// <typeparam name="TSeventh">The seventh type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, string splitOn = "Id")
        {
            return await _connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(command, map, splitOn);
        }

        /// <summary>
        /// Perform an asynchronous multi-mapping query with an arbitrary number of input types. This returns a single type, combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="types">Array of types in the recordset.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="buffered">Whether to buffer the results in memory.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>An enumerable of TReturn.</returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<TReturn>(string sql, Type[] types, Func<object[], TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryAsync<TReturn>(sql, types, map, param, transaction, buffered, splitOn, commandTimeout, commandType);
        }


        /// <summary>
        /// Execute a query asynchronously using Task.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>T:System.ArgumentNullException:type is null.</returns>
        public async Task<IEnumerable<object>> QueryAsync(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryAsync(type, sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Perform an asynchronous multi-mapping query with 5 input types. This returns a single type, combined from the raw types via map.
        /// </summary>
        /// <typeparam name="TFirst">The first type in the recordset.</typeparam>
        /// <typeparam name="TSecond">The second type in the recordset.</typeparam>
        /// <typeparam name="TThird">The third type in the recordset.</typeparam>
        /// <typeparam name="TFourth">The fourth type in the recordset.</typeparam>
        /// <typeparam name="TFifth">The fifth type in the recordset.</typeparam>
        /// <typeparam name="TReturn">The combined type to return.</typeparam>
        /// <param name="command">The command to execute.</param>
        /// <param name="map">The function to map row types to the return type.</param>
        /// <param name="splitOn">The field we should split and read the second object from (default: "Id").</param>
        /// <returns>An enumerable of TReturn.</returns>
        public async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn = "Id")
        {
            return await _connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(command, map, splitOn);
        }

        /// <summary>
        /// Execute a query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns>A sequence of data of T; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is created per row,and a direct column-name===member-name mapping is assumed (case insensitive).</returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(CommandDefinition command)
        {
            return await _connection.QueryAsync<T>(command);
        }

        /// <summary>
        /// Executes a query, returning the data typed as T.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns>A single instance or null of the supplied type; if a basic type (int, string,etc) is queried then the data from the first column in assumed, otherwise an instance is created per row, and a direct column-name===member-name mapping is assumed (case insensitive).</returns>
        public T QueryFirst<T>(CommandDefinition command)
        {
            return _connection.QueryFirst<T>(command);
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as type.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>        A sequence of data of the supplied type; if a basic type(int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is created per row, and a direct column-name===member-name mapping is assumed (case insensitive).        
        /// 异常:
        /// T:System.ArgumentNullException:type is null.
        /// </returns>
        public object QueryFirst(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryFirst(type, sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Executes a single-row query, returning the data typed as T.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is created per row, and a direct column-name===member-name mapping is assumed (case insensitive).</returns>
        public T QueryFirst<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryFirst<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Return a dynamic object with properties matching the columns.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object></returns>
        public dynamic QueryFirst(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryFirst(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns>Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object></returns>
        public async Task<dynamic> QueryFirstAsync(CommandDefinition command)
        {
            return await _connection.QueryFirstAsync(command);
        }


        /// <summary>
        ///  Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns></returns>
        public async Task<T> QueryFirstAsync<T>(CommandDefinition command)
        {
            return await _connection.QueryFirstAsync<T>(command);
        }


        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns></returns>
        public async Task<dynamic> QueryFirstAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryFirstAsync(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns></returns>
        public async Task<object> QueryFirstAsync(Type type, CommandDefinition command)
        {
            return await _connection.QueryFirstAsync(type, command);
        }


        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns></returns>
        public async Task<T> QueryFirstAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryFirstAsync(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// T:System.ArgumentNullException:
        //  type is null.
        /// </returns>
        public async Task<object> QueryFirstAsync(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryFirstAsync(type, sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Executes a single-row query, returning the data typed as T.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is created per row, and a direct column-name===member-name mapping is assumed (case insensitive).</returns>
        public T QueryFirstOrDefault<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Executes a query, returning the data typed as T.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns>A single or null instance of the supplied type; if a basic type (int, string,etc) is queried then the data from the first column in assumed, otherwise an instance is created per row, and a direct column-name===member-name mapping is assumed (case insensitive).</returns>
        public T QueryFirstOrDefault<T>(CommandDefinition command)
        {
            return _connection.QueryFirstOrDefault<T>(command);
        }

        /// <summary>
        /// Return a dynamic object with properties matching the columns.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object></returns>
        public dynamic QueryFirstOrDefault(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryFirstOrDefault(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as type.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// 异常:
        /// T:System.ArgumentNullException:type is null.
        /// </returns>
        public object QueryFirstOrDefault(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryFirstOrDefault(type, sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns>Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object></returns>
        public async Task<dynamic> QueryFirstOrDefaultAsync(CommandDefinition command)
        {
            return await _connection.QueryFirstOrDefaultAsync(command);
        }

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async Task<dynamic> QueryFirstOrDefaultAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryFirstOrDefaultAsync(sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns></returns>
        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        ///  T:System.ArgumentNullException:
        ///  type is null.
        /// </returns>
        public async Task<object> QueryFirstOrDefaultAsync(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryFirstOrDefaultAsync(type, sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        ///  Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns></returns>
        public async Task<T> QueryFirstOrDefaultAsync<T>(CommandDefinition command)
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(command);
        }


        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns></returns>
        public async Task<object> QueryFirstOrDefaultAsync(Type type, CommandDefinition command)
        {
            return await _connection.QueryFirstOrDefaultAsync(type, command);
        }

        /// <summary>
        /// Execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns></returns>
        public GridReader QueryMultiple(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QueryMultiple(sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="command">The command to execute for this query.</param>
        /// <returns></returns>
        public GridReader QueryMultiple(CommandDefinition command)
        {
            return _connection.QueryMultiple(command);
        }


        /// <summary>
        /// Execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="command">The command to execute for this query.</param>
        /// <returns></returns>
        public async Task<GridReader> QueryMultipleAsync(CommandDefinition command)
        {
            return await _connection.QueryMultipleAsync(command);
        }


        /// <summary>
        /// Execute a command that returns multiple result sets, and access each in turn.
        /// </summary>
        /// <param name="sql">The SQL to execute for this query.</param>
        /// <param name="param">The parameters to use for this query.</param>
        /// <param name="transaction">The transaction to use for this query.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns></returns>
        public async Task<GridReader> QueryMultipleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QueryMultipleAsync(sql, param, transaction, commandTimeout, commandType);
        }



        /// <summary>
        /// Executes a single-row query, returning the data typed as type.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        ///  A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// 异常:
        /// T:System.ArgumentNullException:type is null.
        /// </returns>
        public object QuerySingle(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QuerySingle(type, sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        ///  Executes a query, returning the data typed as T.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns>A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is created per row, and a direct column-name===member-name mapping is assumed (case insensitive).</returns>
        public T QuerySingle<T>(CommandDefinition command)
        {
            return _connection.QuerySingle<T>(command);
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as T.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is created per row, and a direct column-name===member-name mapping is assumed (case insensitive).</returns>
        public T QuerySingle<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QuerySingle<T>(sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Return a dynamic object with properties matching the columns.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object></returns>
        public dynamic QuerySingle(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QuerySingle(sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns></returns>
        public async Task<dynamic> QuerySingleAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QuerySingleAsync(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// T:System.ArgumentNullException:type is null.
        /// </returns>
        public async Task<object> QuerySingleAsync(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QuerySingleAsync(type, sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns></returns>
        public async Task<T> QuerySingleAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QuerySingleAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns></returns>
        public async Task<object> QuerySingleAsync(Type type, CommandDefinition command)
        {
            return await _connection.QuerySingleAsync(command);
        }

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns></returns>
        public async Task<T> QuerySingleAsync<T>(CommandDefinition command)
        {
            return await _connection.QuerySingleAsync<T>(command);
        }

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns>Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object></returns>
        public async Task<dynamic> QuerySingleAsync(CommandDefinition command)
        {
            return await _connection.QuerySingleAsync(command);
        }


        /// <summary>
        /// Return a dynamic object with properties matching the columns.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object></returns>
        public dynamic QuerySingleOrDefault(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QuerySingleOrDefault(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Executes a query, returning the data typed as T.
        /// </summary>
        /// <typeparam name="T">The type of results to return.</typeparam>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns>A single instance of the  supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is created per row, and a direct column-name===member-name mapping is assumed (case insensitive).</returns>
        public T QuerySingleOrDefault<T>(CommandDefinition command)
        {
            return _connection.QuerySingleOrDefault<T>(command);
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as T.
        /// </summary>
        /// <typeparam name="T">The type of result to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>A sequence  of data  of the  supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is created per row, and a direct column-name===member-name mapping is assumed (case insensitive).</returns>
        public T QuerySingleOrDefault<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QuerySingleOrDefault<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Executes a single-row query, returning the data typed as type.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first column in assumed, otherwise an instance is created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// T:System.ArgumentNullException:
        /// type is null.
        /// </returns>
        public object QuerySingleOrDefault(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return _connection.QuerySingleOrDefault(type, sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns>
        /// T:System.ArgumentNullException: type is null.
        /// </returns>
        public async Task<object> QuerySingleOrDefaultAsync(Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QuerySingleOrDefaultAsync(type, sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="type">The type to return.</param>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns></returns>
        public async Task<object> QuerySingleOrDefaultAsync(Type type, CommandDefinition command)
        {
            return await _connection.QuerySingleOrDefaultAsync(type, command);
        }


        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns></returns>
        public async Task<dynamic> QuerySingleOrDefaultAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QuerySingleOrDefaultAsync(sql, param, transaction, commandTimeout, commandType);
        }


        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="sql">The SQL to execute for the query.</param>
        /// <param name="param">The parameters to pass, if any.</param>
        /// <param name="transaction">The transaction to use, if any.</param>
        /// <param name="commandTimeout">The command timeout (in seconds).</param>
        /// <param name="commandType">The type of command to execute.</param>
        /// <returns></returns>
        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await _connection.QuerySingleOrDefaultAsync(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns>Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object></returns>
        public async Task<dynamic> QuerySingleOrDefaultAsync(CommandDefinition command)
        {
            return await _connection.QuerySingleOrDefaultAsync(command);
        }

        /// <summary>
        /// Execute a single-row query asynchronously using Task.
        /// </summary>
        /// <typeparam name="T">The type to return.</typeparam>
        /// <param name="command">The command used to query on this connection.</param>
        /// <returns></returns>
        public async Task<T> QuerySingleOrDefaultAsync<T>(CommandDefinition command)
        {
            return await _connection.QuerySingleOrDefaultAsync<T>(command);
        }
        #endregion
    }
}
