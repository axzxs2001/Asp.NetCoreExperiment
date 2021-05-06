//using System;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace WebDemo01.Services
{
    public interface IReadDapper
    {

    }
    public class ReadDapper : IReadDapper
    {
        private readonly IDbConnection _connection;
        public ReadDapper(IDbConnection connection, IConfiguration configuration)
        {          
            foreach (var item in configuration.GetSection("ConnectionStrings").Get<Dictionary<string, string>>())
            {
                if (item.Key.ToLower().Contains("read"))
                {
                    _connection = connection;
                    _connection.ConnectionString = item.Value;
                }
            }
        }

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
        //        //
        //        // 摘要:
        //        //     Executes a query, returning the data typed as T.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   buffered:
        //        //     Whether to buffer results in memory.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type of results to return.
        //        //
        //        // 返回结果:
        //        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //        //     queried then the data from the first column is assumed, otherwise an instance
        //        //     is created per row, and a direct column-name===member-name mapping is assumed
        //        //     (case insensitive).
        //        public IEnumerable<T> Query<T>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        //        {
        //            return _connection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        //        }
        //        //
        //        // 摘要:
        //        //     Perform a multi-mapping query with an arbitrary number of input types. This returns
        //        //     a single type, combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for this query.
        //        //
        //        //   types:
        //        //     Array of types in the recordset.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        //   param:
        //        //     The parameters to use for this query.
        //        //
        //        //   transaction:
        //        //     The transaction to use for this query.
        //        //
        //        //   buffered:
        //        //     Whether to buffer the results in memory.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   commandTimeout:
        //        //     Number of seconds before command execution timeout.
        //        //
        //        //   commandType:
        //        //     Is it a stored proc or a batch?
        //        //
        //        // 类型参数:
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static IEnumerable<TReturn> Query<TReturn>(this IDbConnection cnn, string sql, Type[] types, Func<object[], TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Perform a multi-mapping query with 7 input types. If you need more types -> use
        //        //     Query with Type[] parameter. This returns a single type, combined from the raw
        //        //     types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for this query.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        //   param:
        //        //     The parameters to use for this query.
        //        //
        //        //   transaction:
        //        //     The transaction to use for this query.
        //        //
        //        //   buffered:
        //        //     Whether to buffer the results in memory.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   commandTimeout:
        //        //     Number of seconds before command execution timeout.
        //        //
        //        //   commandType:
        //        //     Is it a stored proc or a batch?
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TThird:
        //        //     The third type in the recordset.
        //        //
        //        //   TFourth:
        //        //     The fourth type in the recordset.
        //        //
        //        //   TFifth:
        //        //     The fifth type in the recordset.
        //        //
        //        //   TSixth:
        //        //     The sixth type in the recordset.
        //        //
        //        //   TSeventh:
        //        //     The seventh type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Perform a multi-mapping query with 6 input types. This returns a single type,
        //        //     combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for this query.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        //   param:
        //        //     The parameters to use for this query.
        //        //
        //        //   transaction:
        //        //     The transaction to use for this query.
        //        //
        //        //   buffered:
        //        //     Whether to buffer the results in memory.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   commandTimeout:
        //        //     Number of seconds before command execution timeout.
        //        //
        //        //   commandType:
        //        //     Is it a stored proc or a batch?
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TThird:
        //        //     The third type in the recordset.
        //        //
        //        //   TFourth:
        //        //     The fourth type in the recordset.
        //        //
        //        //   TFifth:
        //        //     The fifth type in the recordset.
        //        //
        //        //   TSixth:
        //        //     The sixth type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Perform a multi-mapping query with 2 input types. This returns a single type,
        //        //     combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for this query.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        //   param:
        //        //     The parameters to use for this query.
        //        //
        //        //   transaction:
        //        //     The transaction to use for this query.
        //        //
        //        //   buffered:
        //        //     Whether to buffer the results in memory.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   commandTimeout:
        //        //     Number of seconds before command execution timeout.
        //        //
        //        //   commandType:
        //        //     Is it a stored proc or a batch?
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static IEnumerable<TReturn> Query<TFirst, TSecond, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Perform a multi-mapping query with 5 input types. This returns a single type,
        //        //     combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for this query.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        //   param:
        //        //     The parameters to use for this query.
        //        //
        //        //   transaction:
        //        //     The transaction to use for this query.
        //        //
        //        //   buffered:
        //        //     Whether to buffer the results in memory.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   commandTimeout:
        //        //     Number of seconds before command execution timeout.
        //        //
        //        //   commandType:
        //        //     Is it a stored proc or a batch?
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TThird:
        //        //     The third type in the recordset.
        //        //
        //        //   TFourth:
        //        //     The fourth type in the recordset.
        //        //
        //        //   TFifth:
        //        //     The fifth type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Perform a multi-mapping query with 4 input types. This returns a single type,
        //        //     combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for this query.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        //   param:
        //        //     The parameters to use for this query.
        //        //
        //        //   transaction:
        //        //     The transaction to use for this query.
        //        //
        //        //   buffered:
        //        //     Whether to buffer the results in memory.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   commandTimeout:
        //        //     Number of seconds before command execution timeout.
        //        //
        //        //   commandType:
        //        //     Is it a stored proc or a batch?
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TThird:
        //        //     The third type in the recordset.
        //        //
        //        //   TFourth:
        //        //     The fourth type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Return a sequence of dynamic objects with properties matching the columns.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   buffered:
        //        //     Whether to buffer the results in memory.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 言论：
        //        //     Note: each row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        //        public static IEnumerable<dynamic> Query(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Perform a multi-mapping query with 3 input types. This returns a single type,
        //        //     combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for this query.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        //   param:
        //        //     The parameters to use for this query.
        //        //
        //        //   transaction:
        //        //     The transaction to use for this query.
        //        //
        //        //   buffered:
        //        //     Whether to buffer the results in memory.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   commandTimeout:
        //        //     Number of seconds before command execution timeout.
        //        //
        //        //   commandType:
        //        //     Is it a stored proc or a batch?
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TThird:
        //        //     The third type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Executes a query, returning the data typed as T.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type of results to return.
        //        //
        //        // 返回结果:
        //        //     A sequence of data of T; if a basic type (int, string, etc) is queried then the
        //        //     data from the first column in assumed, otherwise an instance is created per row,
        //        //     and a direct column-name===member-name mapping is assumed (case insensitive).
        //        public static IEnumerable<T> Query<T>(this IDbConnection cnn, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Perform an asynchronous multi-mapping query with 2 input types. This returns
        //        //     a single type, combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   command:
        //        //     The command to execute.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TReturn> map, string splitOn = "Id");
        //        //
        //        // 摘要:
        //        //     Perform an asynchronous multi-mapping query with 2 input types. This returns
        //        //     a single type, combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for this query.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        //   param:
        //        //     The parameters to use for this query.
        //        //
        //        //   transaction:
        //        //     The transaction to use for this query.
        //        //
        //        //   buffered:
        //        //     Whether to buffer the results in memory.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   commandTimeout:
        //        //     Number of seconds before command execution timeout.
        //        //
        //        //   commandType:
        //        //     Is it a stored proc or a batch?
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 言论：
        //        //     Note: each row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        //        public static Task<IEnumerable<dynamic>> QueryAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        //
        //        // 言论：
        //        //     Note: each row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        //        public static Task<IEnumerable<dynamic>> QueryAsync(this IDbConnection cnn, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Perform an asynchronous multi-mapping query with 3 input types. This returns
        //        //     a single type, combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for this query.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        //   param:
        //        //     The parameters to use for this query.
        //        //
        //        //   transaction:
        //        //     The transaction to use for this query.
        //        //
        //        //   buffered:
        //        //     Whether to buffer the results in memory.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   commandTimeout:
        //        //     Number of seconds before command execution timeout.
        //        //
        //        //   commandType:
        //        //     Is it a stored proc or a batch?
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TThird:
        //        //     The third type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Perform an asynchronous multi-mapping query with 3 input types. This returns
        //        //     a single type, combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   command:
        //        //     The command to execute.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TThird:
        //        //     The third type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TReturn> map, string splitOn = "Id");
        //        //
        //        // 摘要:
        //        //     Perform an asynchronous multi-mapping query with 4 input types. This returns
        //        //     a single type, combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for this query.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        //   param:
        //        //     The parameters to use for this query.
        //        //
        //        //   transaction:
        //        //     The transaction to use for this query.
        //        //
        //        //   buffered:
        //        //     Whether to buffer the results in memory.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   commandTimeout:
        //        //     Number of seconds before command execution timeout.
        //        //
        //        //   commandType:
        //        //     Is it a stored proc or a batch?
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TThird:
        //        //     The third type in the recordset.
        //        //
        //        //   TFourth:
        //        //     The fourth type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Perform an asynchronous multi-mapping query with 4 input types. This returns
        //        //     a single type, combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   command:
        //        //     The command to execute.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TThird:
        //        //     The third type in the recordset.
        //        //
        //        //   TFourth:
        //        //     The fourth type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, string splitOn = "Id");
        //        //
        //        // 摘要:
        //        //     Execute a query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   type:
        //        //     The type to return.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        public static Task<IEnumerable<object>> QueryAsync(this IDbConnection cnn, Type type, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Execute a query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type of results to return.
        //        //
        //        // 返回结果:
        //        //     A sequence of data of T; if a basic type (int, string, etc) is queried then the
        //        //     data from the first column in assumed, otherwise an instance is created per row,
        //        //     and a direct column-name===member-name mapping is assumed (case insensitive).
        //        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Perform an asynchronous multi-mapping query with 5 input types. This returns
        //        //     a single type, combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for this query.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        //   param:
        //        //     The parameters to use for this query.
        //        //
        //        //   transaction:
        //        //     The transaction to use for this query.
        //        //
        //        //   buffered:
        //        //     Whether to buffer the results in memory.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   commandTimeout:
        //        //     Number of seconds before command execution timeout.
        //        //
        //        //   commandType:
        //        //     Is it a stored proc or a batch?
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TThird:
        //        //     The third type in the recordset.
        //        //
        //        //   TFourth:
        //        //     The fourth type in the recordset.
        //        //
        //        //   TFifth:
        //        //     The fifth type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Perform an asynchronous multi-mapping query with 6 input types. This returns
        //        //     a single type, combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for this query.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        //   param:
        //        //     The parameters to use for this query.
        //        //
        //        //   transaction:
        //        //     The transaction to use for this query.
        //        //
        //        //   buffered:
        //        //     Whether to buffer the results in memory.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   commandTimeout:
        //        //     Number of seconds before command execution timeout.
        //        //
        //        //   commandType:
        //        //     Is it a stored proc or a batch?
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TThird:
        //        //     The third type in the recordset.
        //        //
        //        //   TFourth:
        //        //     The fourth type in the recordset.
        //        //
        //        //   TFifth:
        //        //     The fifth type in the recordset.
        //        //
        //        //   TSixth:
        //        //     The sixth type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Perform an asynchronous multi-mapping query with 6 input types. This returns
        //        //     a single type, combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   command:
        //        //     The command to execute.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TThird:
        //        //     The third type in the recordset.
        //        //
        //        //   TFourth:
        //        //     The fourth type in the recordset.
        //        //
        //        //   TFifth:
        //        //     The fifth type in the recordset.
        //        //
        //        //   TSixth:
        //        //     The sixth type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn> map, string splitOn = "Id");
        //        //
        //        // 摘要:
        //        //     Perform an asynchronous multi-mapping query with 7 input types. This returns
        //        //     a single type, combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for this query.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        //   param:
        //        //     The parameters to use for this query.
        //        //
        //        //   transaction:
        //        //     The transaction to use for this query.
        //        //
        //        //   buffered:
        //        //     Whether to buffer the results in memory.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   commandTimeout:
        //        //     Number of seconds before command execution timeout.
        //        //
        //        //   commandType:
        //        //     Is it a stored proc or a batch?
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TThird:
        //        //     The third type in the recordset.
        //        //
        //        //   TFourth:
        //        //     The fourth type in the recordset.
        //        //
        //        //   TFifth:
        //        //     The fifth type in the recordset.
        //        //
        //        //   TSixth:
        //        //     The sixth type in the recordset.
        //        //
        //        //   TSeventh:
        //        //     The seventh type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Perform an asynchronous multi-mapping query with 7 input types. This returns
        //        //     a single type, combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   command:
        //        //     The command to execute.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TThird:
        //        //     The third type in the recordset.
        //        //
        //        //   TFourth:
        //        //     The fourth type in the recordset.
        //        //
        //        //   TFifth:
        //        //     The fifth type in the recordset.
        //        //
        //        //   TSixth:
        //        //     The sixth type in the recordset.
        //        //
        //        //   TSeventh:
        //        //     The seventh type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn> map, string splitOn = "Id");
        //        //
        //        // 摘要:
        //        //     Perform an asynchronous multi-mapping query with an arbitrary number of input
        //        //     types. This returns a single type, combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for this query.
        //        //
        //        //   types:
        //        //     Array of types in the recordset.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        //   param:
        //        //     The parameters to use for this query.
        //        //
        //        //   transaction:
        //        //     The transaction to use for this query.
        //        //
        //        //   buffered:
        //        //     Whether to buffer the results in memory.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   commandTimeout:
        //        //     Number of seconds before command execution timeout.
        //        //
        //        //   commandType:
        //        //     Is it a stored proc or a batch?
        //        //
        //        // 类型参数:
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static Task<IEnumerable<TReturn>> QueryAsync<TReturn>(this IDbConnection cnn, string sql, Type[] types, Func<object[], TReturn> map, object param = null, IDbTransaction transaction = null, bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   type:
        //        //     The type to return.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 异常:
        //        //   T:System.ArgumentNullException:
        //        //     type is null.
        //        public static Task<IEnumerable<object>> QueryAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Perform an asynchronous multi-mapping query with 5 input types. This returns
        //        //     a single type, combined from the raw types via map.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   splitOn:
        //        //     The field we should split and read the second object from (default: "Id").
        //        //
        //        //   command:
        //        //     The command to execute.
        //        //
        //        //   map:
        //        //     The function to map row types to the return type.
        //        //
        //        // 类型参数:
        //        //   TFirst:
        //        //     The first type in the recordset.
        //        //
        //        //   TSecond:
        //        //     The second type in the recordset.
        //        //
        //        //   TThird:
        //        //     The third type in the recordset.
        //        //
        //        //   TFourth:
        //        //     The fourth type in the recordset.
        //        //
        //        //   TFifth:
        //        //     The fifth type in the recordset.
        //        //
        //        //   TReturn:
        //        //     The combined type to return.
        //        //
        //        // 返回结果:
        //        //     An enumerable of TReturn.
        //        public static Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(this IDbConnection cnn, CommandDefinition command, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, string splitOn = "Id");
        //        //
        //        // 摘要:
        //        //     Execute a query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type to return.
        //        //
        //        // 返回结果:
        //        //     A sequence of data of T; if a basic type (int, string, etc) is queried then the
        //        //     data from the first column in assumed, otherwise an instance is created per row,
        //        //     and a direct column-name===member-name mapping is assumed (case insensitive).
        //        public static Task<IEnumerable<T>> QueryAsync<T>(this IDbConnection cnn, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Executes a query, returning the data typed as T.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type of results to return.
        //        //
        //        // 返回结果:
        //        //     A single instance or null of the supplied type; if a basic type (int, string,
        //        //     etc) is queried then the data from the first column in assumed, otherwise an
        //        //     instance is created per row, and a direct column-name===member-name mapping is
        //        //     assumed (case insensitive).
        //        public static T QueryFirst<T>(this IDbConnection cnn, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Executes a single-row query, returning the data typed as type.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   type:
        //        //     The type to return.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 返回结果:
        //        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //        //     queried then the data from the first column in assumed, otherwise an instance
        //        //     is created per row, and a direct column-name===member-name mapping is assumed
        //        //     (case insensitive).
        //        //
        //        // 异常:
        //        //   T:System.ArgumentNullException:
        //        //     type is null.
        //        public static object QueryFirst(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Executes a single-row query, returning the data typed as T.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type of result to return.
        //        //
        //        // 返回结果:
        //        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //        //     queried then the data from the first column in assumed, otherwise an instance
        //        //     is created per row, and a direct column-name===member-name mapping is assumed
        //        //     (case insensitive).
        //        public static T QueryFirst<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Return a dynamic object with properties matching the columns.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 言论：
        //        //     Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        //        public static dynamic QueryFirst(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        //
        //        // 言论：
        //        //     Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        //        public static Task<dynamic> QueryFirstAsync(this IDbConnection cnn, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type to return.
        //        public static Task<T> QueryFirstAsync<T>(this IDbConnection cnn, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        public static Task<dynamic> QueryFirstAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   type:
        //        //     The type to return.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        public static Task<object> QueryFirstAsync(this IDbConnection cnn, Type type, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type of result to return.
        //        public static Task<T> QueryFirstAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   type:
        //        //     The type to return.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 异常:
        //        //   T:System.ArgumentNullException:
        //        //     type is null.
        //        public static Task<object> QueryFirstAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Executes a single-row query, returning the data typed as T.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type of result to return.
        //        //
        //        // 返回结果:
        //        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //        //     queried then the data from the first column in assumed, otherwise an instance
        //        //     is created per row, and a direct column-name===member-name mapping is assumed
        //        //     (case insensitive).
        //        public static T QueryFirstOrDefault<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Executes a query, returning the data typed as T.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type of results to return.
        //        //
        //        // 返回结果:
        //        //     A single or null instance of the supplied type; if a basic type (int, string,
        //        //     etc) is queried then the data from the first column in assumed, otherwise an
        //        //     instance is created per row, and a direct column-name===member-name mapping is
        //        //     assumed (case insensitive).
        //        public static T QueryFirstOrDefault<T>(this IDbConnection cnn, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Return a dynamic object with properties matching the columns.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 言论：
        //        //     Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        //        public static dynamic QueryFirstOrDefault(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Executes a single-row query, returning the data typed as type.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   type:
        //        //     The type to return.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 返回结果:
        //        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //        //     queried then the data from the first column in assumed, otherwise an instance
        //        //     is created per row, and a direct column-name===member-name mapping is assumed
        //        //     (case insensitive).
        //        //
        //        // 异常:
        //        //   T:System.ArgumentNullException:
        //        //     type is null.
        //        public static object QueryFirstOrDefault(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        //
        //        // 言论：
        //        //     Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        //        public static Task<dynamic> QueryFirstOrDefaultAsync(this IDbConnection cnn, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        public static Task<dynamic> QueryFirstOrDefaultAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type of result to return.
        //        public static Task<T> QueryFirstOrDefaultAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   type:
        //        //     The type to return.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 异常:
        //        //   T:System.ArgumentNullException:
        //        //     type is null.
        //        public static Task<object> QueryFirstOrDefaultAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type to return.
        //        public static Task<T> QueryFirstOrDefaultAsync<T>(this IDbConnection cnn, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   type:
        //        //     The type to return.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        public static Task<object> QueryFirstOrDefaultAsync(this IDbConnection cnn, Type type, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Execute a command that returns multiple result sets, and access each in turn.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for this query.
        //        //
        //        //   param:
        //        //     The parameters to use for this query.
        //        //
        //        //   transaction:
        //        //     The transaction to use for this query.
        //        //
        //        //   commandTimeout:
        //        //     Number of seconds before command execution timeout.
        //        //
        //        //   commandType:
        //        //     Is it a stored proc or a batch?
        //        public static GridReader QueryMultiple(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a command that returns multiple result sets, and access each in turn.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command to execute for this query.
        //        public static GridReader QueryMultiple(this IDbConnection cnn, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Execute a command that returns multiple result sets, and access each in turn.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command to execute for this query.
        //        [AsyncStateMachine(typeof(< QueryMultipleAsync > d__57))]
        //        public static Task<GridReader> QueryMultipleAsync(this IDbConnection cnn, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Execute a command that returns multiple result sets, and access each in turn.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for this query.
        //        //
        //        //   param:
        //        //     The parameters to use for this query.
        //        //
        //        //   transaction:
        //        //     The transaction to use for this query.
        //        //
        //        //   commandTimeout:
        //        //     Number of seconds before command execution timeout.
        //        //
        //        //   commandType:
        //        //     Is it a stored proc or a batch?
        //        public static Task<GridReader> QueryMultipleAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Executes a single-row query, returning the data typed as type.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   type:
        //        //     The type to return.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 返回结果:
        //        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //        //     queried then the data from the first column in assumed, otherwise an instance
        //        //     is created per row, and a direct column-name===member-name mapping is assumed
        //        //     (case insensitive).
        //        //
        //        // 异常:
        //        //   T:System.ArgumentNullException:
        //        //     type is null.
        //        public static object QuerySingle(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Executes a query, returning the data typed as T.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type of results to return.
        //        //
        //        // 返回结果:
        //        //     A single instance of the supplied type; if a basic type (int, string, etc) is
        //        //     queried then the data from the first column in assumed, otherwise an instance
        //        //     is created per row, and a direct column-name===member-name mapping is assumed
        //        //     (case insensitive).
        //        public static T QuerySingle<T>(this IDbConnection cnn, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Executes a single-row query, returning the data typed as T.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type of result to return.
        //        //
        //        // 返回结果:
        //        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //        //     queried then the data from the first column in assumed, otherwise an instance
        //        //     is created per row, and a direct column-name===member-name mapping is assumed
        //        //     (case insensitive).
        //        public static T QuerySingle<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Return a dynamic object with properties matching the columns.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 言论：
        //        //     Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        //        public static dynamic QuerySingle(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        public static Task<dynamic> QuerySingleAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   type:
        //        //     The type to return.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 异常:
        //        //   T:System.ArgumentNullException:
        //        //     type is null.
        //        public static Task<object> QuerySingleAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type of result to return.
        //        public static Task<T> QuerySingleAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   type:
        //        //     The type to return.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        public static Task<object> QuerySingleAsync(this IDbConnection cnn, Type type, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type to return.
        //        public static Task<T> QuerySingleAsync<T>(this IDbConnection cnn, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        //
        //        // 言论：
        //        //     Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        //        public static Task<dynamic> QuerySingleAsync(this IDbConnection cnn, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Return a dynamic object with properties matching the columns.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 言论：
        //        //     Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        //        public static dynamic QuerySingleOrDefault(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Executes a query, returning the data typed as T.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type of results to return.
        //        //
        //        // 返回结果:
        //        //     A single instance of the supplied type; if a basic type (int, string, etc) is
        //        //     queried then the data from the first column in assumed, otherwise an instance
        //        //     is created per row, and a direct column-name===member-name mapping is assumed
        //        //     (case insensitive).
        //        public static T QuerySingleOrDefault<T>(this IDbConnection cnn, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Executes a single-row query, returning the data typed as T.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type of result to return.
        //        //
        //        // 返回结果:
        //        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //        //     queried then the data from the first column in assumed, otherwise an instance
        //        //     is created per row, and a direct column-name===member-name mapping is assumed
        //        //     (case insensitive).
        //        public static T QuerySingleOrDefault<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Executes a single-row query, returning the data typed as type.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   type:
        //        //     The type to return.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 返回结果:
        //        //     A sequence of data of the supplied type; if a basic type (int, string, etc) is
        //        //     queried then the data from the first column in assumed, otherwise an instance
        //        //     is created per row, and a direct column-name===member-name mapping is assumed
        //        //     (case insensitive).
        //        //
        //        // 异常:
        //        //   T:System.ArgumentNullException:
        //        //     type is null.
        //        public static object QuerySingleOrDefault(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   type:
        //        //     The type to return.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 异常:
        //        //   T:System.ArgumentNullException:
        //        //     type is null.
        //        public static Task<object> QuerySingleOrDefaultAsync(this IDbConnection cnn, Type type, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   type:
        //        //     The type to return.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        public static Task<object> QuerySingleOrDefaultAsync(this IDbConnection cnn, Type type, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        public static Task<dynamic> QuerySingleOrDefaultAsync(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   sql:
        //        //     The SQL to execute for the query.
        //        //
        //        //   param:
        //        //     The parameters to pass, if any.
        //        //
        //        //   transaction:
        //        //     The transaction to use, if any.
        //        //
        //        //   commandTimeout:
        //        //     The command timeout (in seconds).
        //        //
        //        //   commandType:
        //        //     The type of command to execute.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type to return.
        //        public static Task<T> QuerySingleOrDefaultAsync<T>(this IDbConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        //
        //        // 言论：
        //        //     Note: the row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        //        public static Task<dynamic> QuerySingleOrDefaultAsync(this IDbConnection cnn, CommandDefinition command);
        //        //
        //        // 摘要:
        //        //     Execute a single-row query asynchronously using Task.
        //        //
        //        // 参数:
        //        //   cnn:
        //        //     The connection to query on.
        //        //
        //        //   command:
        //        //     The command used to query on this connection.
        //        //
        //        // 类型参数:
        //        //   T:
        //        //     The type to return.
        //        public static Task<T> QuerySingleOrDefaultAsync<T>(this IDbConnection cnn, CommandDefinition command);


    }
}
