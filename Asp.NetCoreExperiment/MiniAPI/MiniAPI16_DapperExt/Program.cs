using Dapper;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

//#region SingleDatabase  注入连接字符串  注入IDapperPlusDB  注入IDbConnection
//builder.Services.AddScoped<IDapperPlusDB, DapperPlusDB>(service =>
//{
//    return new DapperPlusDB(new SqlConnection(builder.Configuration.GetConnectionString("SqlServer")));
//});
//#endregion

#region MultiDatabase  注入各个数据库链接对象
//builder.Services.AddScoped<IDapperPlusDB, DapperPlusDB>(service =>
//{
//    return new DapperPlusDB(new SqlConnection(builder.Configuration.GetConnectionString("SqlServer")));
//});
//builder.Services.AddScoped<IDapperPlusDB, DapperPlusDB>(service =>
//{
//    return new DapperPlusDB(new MySqlConnection(builder.Configuration.GetConnectionString("MySql")));
//});
#endregion


//#region MultiDatabase  注入相对类型数据库链接对象
builder.Services.AddScoped<IDapperPlusDB, DapperPlusDB>(service =>
{
    return new DapperPlusDB(dbConnection: new MySqlConnection(builder.Configuration.GetConnectionString("MySqlRead")), dataBaseMark: "read");
});
builder.Services.AddScoped<IDapperPlusDB, DapperPlusDB>(service =>
{
    return new DapperPlusDB(dbConnection: new MySqlConnection(builder.Configuration.GetConnectionString("MySqlWrite")), dataBaseMark: "write");
});
//#endregion

var app = builder.Build();


//app.MapGet("/answers/{QuestionID}", async (IDapperPlusDB db, int QuestionID) =>
//{
//    return await db.QueryAsync<AnswerModel>("select * from answers where QuestionID=@QuestionID", new { QuestionID });
//});

//app.MapGet("/data/{id}", async (IEnumerable<IDapperPlusDB> dbs, int id) =>
//{
//    IDapperPlusDB? mssqlDB = null, mysqldb = null;
//    foreach (var db in dbs)
//    {
//        switch (db.DataBaseType)
//        {
//            case DataBaseType.SqlServer:
//                mssqlDB = db;
//                break;
//            case DataBaseType.MySql:
//                mysqldb = db;
//                break;
//        }
//    }
//    if (mssqlDB != null && mysqldb != null)
//    {
//        return new
//        {
//            MSSqlData = await mssqlDB.QuerySingleOrDefaultAsync<AnswerModel>("select * from answers where id=@id;", new { id }),
//            MySqlData = await mysqldb.QuerySingleOrDefaultAsync<CityModel>("select * from city where id=@id;", new { id })
//        };
//    }
//    return new
//    {
//        MSSqlData = new AnswerModel { },
//        MySqlData = new CityModel { }
//    };
//});

app.MapGet("/data/{id}", async (IEnumerable<IDapperPlusDB> dbs, int id) =>
{
    IDapperPlusDB? readDB = null, writedb = null;
    foreach (var db in dbs)
    {
        switch (db.DataBaseMark)
        {
            case "read":
                readDB = db;
                break;
            case "write":
                writedb = db;
                break;
        }
    }
    if (readDB != null && writedb != null)
    {
        return new
        {
            MSSqlData = await readDB.QuerySingleOrDefaultAsync<CityModel>("select * from city where id=@id;", new { id }),
            MySqlData = await writedb.QuerySingleOrDefaultAsync<CityModel>("select * from city where id=@id;", new { id })
        };
    }
    return new
    {
        MSSqlData = new CityModel { },
        MySqlData = new CityModel { }
    };
});
app.Run();



/// <summary>
/// 数据库类型
/// </summary>
public enum DataBaseType
{
    None,
    Sqlite,
    Postgre,
    SqlServer,
    Oracle,
    MySql
}

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
    /// 数据库类型
    /// </summary>
    DataBaseType DataBaseType { get; }
    /// <summary>
    /// 数据库标志
    /// </summary>
    string? DataBaseMark { get; }

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
    IEnumerable<T> Query<T>(string sql, object? param = null, IDbTransaction? transaction = null, bool buffered = false, int? commandTimeout = null, CommandType? commandType = null);


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
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);


    /// <summary>
    /// 查询单个对象异步方法
    /// </summary>
    /// <typeparam name="T">映射实体类</typeparam>
    /// <param name="sql">sql语句</param>
    /// <param name="param">参数对象</param>
    /// <param name="transaction">事务</param> 
    /// <param name="commandTimeout">command超时时间(秒)</param>
    /// <param name="commandType">command类型</param>
    Task<T> QuerySingleOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);

    /// <summary>
    /// 执行方法
    /// </summary>
    /// <param name="sql">映射实体类</param>
    /// <param name="param">参数对象</param>
    /// <param name="transaction">事务</param>
    /// <param name="commandTimeout">command超时时间(秒)</param>
    /// <param name="commandType">command类型</param>
    /// <returns></returns>
    int Execute(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);

    /// <summary>
    /// 异常执行方法
    /// </summary>
    /// <param name="sql">映射实体类</param>
    /// <param name="param">参数对象</param>
    /// <param name="transaction">事务</param>
    /// <param name="commandTimeout">command超时时间(秒)</param>
    /// <param name="commandType">command类型</param>
    /// <returns></returns>
    Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);


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
    T ExecuteScalar<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);

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
    Task<T> ExecuteScalarAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);

}
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
    public DapperPlusDB(IDbConnection dbConnection)
    {

        switch (dbConnection.GetType().Name)
        {
            case "SqliteConnection":
                DataBaseType = DataBaseType.Sqlite;
                break;
            case "NpgsqlConnection":
                DataBaseType = DataBaseType.Postgre;
                break;
            case "SqlConnection":
                DataBaseType = DataBaseType.SqlServer;
                break;
            case "OracleConnection":
                DataBaseType = DataBaseType.Oracle;
                break;
            case "MySqlConnection":
                DataBaseType = DataBaseType.MySql;
                break;
        }
        _dbConnection = dbConnection;
    }

    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="dbConnection">连接对象</param>
    /// <param name="dataBaseMark">数据库标志</param>
    public DapperPlusDB(IDbConnection dbConnection, string dataBaseMark)
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
        _dbConnection = dbConnection;
    }
    /// <summary>
    /// 数据库标志
    /// </summary>
    public string? DataBaseMark { get; }

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
    public IEnumerable<T> Query<T>(string sql, object? param = null, IDbTransaction? transaction = null, bool buffered = false, int? commandTimeout = null, CommandType? commandType = null)
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
    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await _dbConnection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }
    /// <summary>
    /// 查询单个对象异步方法
    /// </summary>
    /// <typeparam name="T">映射实体类</typeparam>
    /// <param name="sql">sql语句</param>
    /// <param name="param">参数对象</param>
    /// <param name="transaction">事务</param> 
    /// <param name="commandTimeout">command超时时间(秒)</param>
    /// <param name="commandType">command类型</param>
    public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await _dbConnection.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
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
    public int Execute(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
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
    public async Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await _dbConnection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
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
    public T ExecuteScalar<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
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
    public async Task<T> ExecuteScalarAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await _dbConnection.ExecuteScalarAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }


    public void Dispose()
    {
        if (_dbConnection != null)
        {
            _dbConnection.Dispose();
        }
    }
}