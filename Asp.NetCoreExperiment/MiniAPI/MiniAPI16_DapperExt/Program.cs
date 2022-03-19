using Dapper;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

//#region SingleDatabase  ע�������ַ���  ע��IDapperPlusDB  ע��IDbConnection
//builder.Services.AddScoped<IDapperPlusDB, DapperPlusDB>(service =>
//{
//    return new DapperPlusDB(new SqlConnection(builder.Configuration.GetConnectionString("SqlServer")));
//});
//#endregion

#region MultiDatabase  ע��������ݿ����Ӷ���
//builder.Services.AddScoped<IDapperPlusDB, DapperPlusDB>(service =>
//{
//    return new DapperPlusDB(new SqlConnection(builder.Configuration.GetConnectionString("SqlServer")));
//});
//builder.Services.AddScoped<IDapperPlusDB, DapperPlusDB>(service =>
//{
//    return new DapperPlusDB(new MySqlConnection(builder.Configuration.GetConnectionString("MySql")));
//});
#endregion


//#region MultiDatabase  ע������������ݿ����Ӷ���
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
/// ���ݿ�����
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
/// IDapperPlusDB���ݿ����� 
/// </summary>
public interface IDapperPlusDB : IDisposable
{
    /// <summary>
    /// ���Ӷ���
    /// </summary>
    /// <returns></returns>
    IDbConnection GetConnection();


    /// <summary>
    /// ���ݿ�����
    /// </summary>
    DataBaseType DataBaseType { get; }
    /// <summary>
    /// ���ݿ��־
    /// </summary>
    string? DataBaseMark { get; }

    /// <summary>
    /// ��ѯ����
    /// </summary>
    /// <typeparam name="T">ӳ��ʵ����</typeparam>
    /// <param name="sql">sql���</param>
    /// <param name="param">��������</param>
    /// <param name="transaction">����</param>
    /// <param name="buffered">�Ƿ񻺴���</param>
    /// <param name="commandTimeout">command��ʱʱ��(��)</param>
    /// <param name="commandType">command����</param>
    /// <returns></returns>
    IEnumerable<T> Query<T>(string sql, object? param = null, IDbTransaction? transaction = null, bool buffered = false, int? commandTimeout = null, CommandType? commandType = null);


    /// <summary>
    /// �첽��ѯ����
    /// </summary>
    /// <typeparam name="T">ӳ��ʵ����</typeparam>
    /// <param name="sql">sql���</param>
    /// <param name="param">��������</param>
    /// <param name="transaction">����</param>
    /// <param name="buffered">�Ƿ񻺴���</param>
    /// <param name="commandTimeout">command��ʱʱ��(��)</param>
    /// <param name="commandType">command����</param>
    /// <returns></returns>
    Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);


    /// <summary>
    /// ��ѯ���������첽����
    /// </summary>
    /// <typeparam name="T">ӳ��ʵ����</typeparam>
    /// <param name="sql">sql���</param>
    /// <param name="param">��������</param>
    /// <param name="transaction">����</param> 
    /// <param name="commandTimeout">command��ʱʱ��(��)</param>
    /// <param name="commandType">command����</param>
    Task<T> QuerySingleOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);

    /// <summary>
    /// ִ�з���
    /// </summary>
    /// <param name="sql">ӳ��ʵ����</param>
    /// <param name="param">��������</param>
    /// <param name="transaction">����</param>
    /// <param name="commandTimeout">command��ʱʱ��(��)</param>
    /// <param name="commandType">command����</param>
    /// <returns></returns>
    int Execute(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);

    /// <summary>
    /// �쳣ִ�з���
    /// </summary>
    /// <param name="sql">ӳ��ʵ����</param>
    /// <param name="param">��������</param>
    /// <param name="transaction">����</param>
    /// <param name="commandTimeout">command��ʱʱ��(��)</param>
    /// <param name="commandType">command����</param>
    /// <returns></returns>
    Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);


    /// <summary>
    /// ��ѯ��ֵ
    /// </summary>
    /// <typeparam name="T">ӳ��ʵ����</typeparam>
    /// <param name="sql">sql���</param>
    /// <param name="param">��������</param>
    /// <param name="transaction">����</param>
    /// <param name="commandTimeout">command��ʱʱ��(��)</param>
    /// <param name="commandType">command����</param>
    /// <returns></returns>
    T ExecuteScalar<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);

    /// <summary>
    /// �첽��ѯ��ֵ
    /// </summary>
    /// <typeparam name="T">ӳ��ʵ����</typeparam>
    /// <param name="sql">sql���</param>
    /// <param name="param">��������</param>
    /// <param name="transaction">����</param>
    /// <param name="commandTimeout">command��ʱʱ��(��)</param>
    /// <param name="commandType">command����</param>
    /// <returns></returns>
    Task<T> ExecuteScalarAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null);

}
/// <summary>
/// IDapperPlusDB���ݿ����� 
/// </summary>
public class DapperPlusDB : IDapperPlusDB
{
    /// <summary>
    /// ���Ӷ���
    /// </summary>
    IDbConnection _dbConnection;
    /// <summary>
    /// ����
    /// </summary>
    /// <param name="dbConnection">���Ӷ���</param>
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
    /// ����
    /// </summary>
    /// <param name="dbConnection">���Ӷ���</param>
    /// <param name="dataBaseMark">���ݿ��־</param>
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
    /// ���ݿ��־
    /// </summary>
    public string? DataBaseMark { get; }

    /// <summary>
    /// ���ݿ�����
    /// </summary>
    public DataBaseType DataBaseType { get; }



    /// <summary>
    /// ���Ӷ���
    /// </summary>
    /// <returns></returns>
    public IDbConnection GetConnection()
    {
        return _dbConnection;
    }
    /// <summary>
    /// ��ѯ����
    /// </summary>
    /// <typeparam name="T">ӳ��ʵ����</typeparam>
    /// <param name="sql">sql���</param>
    /// <param name="param">��������</param>
    /// <param name="transaction">����</param>
    /// <param name="buffered">�Ƿ񻺴���</param>
    /// <param name="commandTimeout">command��ʱʱ��(��)</param>
    /// <param name="commandType">command����</param>
    /// <returns></returns>
    public IEnumerable<T> Query<T>(string sql, object? param = null, IDbTransaction? transaction = null, bool buffered = false, int? commandTimeout = null, CommandType? commandType = null)
    {
        return _dbConnection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
    }
    /// <summary>
    /// ��ѯ�첽����
    /// </summary>
    /// <typeparam name="T">ӳ��ʵ����</typeparam>
    /// <param name="sql">sql���</param>
    /// <param name="param">��������</param>
    /// <param name="transaction">����</param> 
    /// <param name="commandTimeout">command��ʱʱ��(��)</param>
    /// <param name="commandType">command����</param>
    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await _dbConnection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }
    /// <summary>
    /// ��ѯ���������첽����
    /// </summary>
    /// <typeparam name="T">ӳ��ʵ����</typeparam>
    /// <param name="sql">sql���</param>
    /// <param name="param">��������</param>
    /// <param name="transaction">����</param> 
    /// <param name="commandTimeout">command��ʱʱ��(��)</param>
    /// <param name="commandType">command����</param>
    public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await _dbConnection.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType);
    }

    /// <summary>
    /// ִ�з���
    /// </summary>
    /// <param name="sql">ӳ��ʵ����</param>
    /// <param name="param">��������</param>
    /// <param name="transaction">����</param>
    /// <param name="commandTimeout">command��ʱʱ��(��)</param>
    /// <param name="commandType">command����</param>
    /// <returns></returns>
    public int Execute(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return _dbConnection.Execute(sql, param, transaction, commandTimeout, commandType);
    }

    /// <summary>
    /// �첽ִ�з���
    /// </summary>
    /// <param name="sql">ӳ��ʵ����</param>
    /// <param name="param">��������</param>
    /// <param name="transaction">����</param>
    /// <param name="commandTimeout">command��ʱʱ��(��)</param>
    /// <param name="commandType">command����</param>
    /// <returns></returns>
    public async Task<int> ExecuteAsync(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return await _dbConnection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
    }


    /// <summary>
    /// ��ѯ��ֵ
    /// </summary>
    /// <typeparam name="T">ӳ��ʵ����</typeparam>
    /// <param name="sql">sql���</param>
    /// <param name="param">��������</param>
    /// <param name="transaction">����</param>
    /// <param name="commandTimeout">command��ʱʱ��(��)</param>
    /// <param name="commandType">command����</param>
    /// <returns></returns>
    public T ExecuteScalar<T>(string sql, object? param = null, IDbTransaction? transaction = null, int? commandTimeout = null, CommandType? commandType = null)
    {
        return _dbConnection.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
    }

    /// <summary>
    /// �첽��ѯ��ֵ
    /// </summary>
    /// <typeparam name="T">ӳ��ʵ����</typeparam>
    /// <param name="sql">sql���</param>
    /// <param name="param">��������</param>
    /// <param name="transaction">����</param>
    /// <param name="commandTimeout">command��ʱʱ��(��)</param>
    /// <param name="commandType">command����</param>
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