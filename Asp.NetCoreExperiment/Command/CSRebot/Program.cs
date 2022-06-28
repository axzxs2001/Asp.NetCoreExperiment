using Microsoft.Data.SqlClient;
using System.CommandLine;
using System.CommandLine.Binding;
using System.Data;
using System.Text;
using System;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using System.Reflection.Metadata;
using File = System.IO.File;
using MySql.Data.MySqlClient;
using Npgsql;
using NpgsqlTypes;


//dotnet run dbto -l C# -constr 'server=localhost;database=abcd;uid=sa;pwd=sa;encrypt=true;trustservercertificate=true' -t mssql
//dotnet run dbto -l C# -constr 'server=localhost;database=abcd;uid=root;pwd=mars2020;' -t mysql
//dotnet run dbto -l C# -constr 'Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgres2018;Database=stealthdb;'-t pgsql
//完成后，选中项目并打包，然后进入项目所有的文件夹执行工具安装命令
//dotnet tool install -g --add-source ./nupkg CSRebot
//创建根命令
var rootCommand = new RootCommand("这是一款C#开发辅助工具，CSRebot");
rootCommand.SetHandler(() =>
{
    Console.WriteLine("老桂欢迎您使用CSRebot!");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("\r\n   ___________ ____       __          __ \r\n  / ____/ ___// __ \\___  / /_  ____  / /_\r\n / /    \\__ \\/ /_/ / _ \\/ __ \\/ __ \\/ __/\r\n/ /___ ___/ / _, _/  __/ /_/ / /_/ / /_  \r\n\\____//____/_/ |_|\\___/_.___/\\____/\\__/  \r\n                                         \r\n");
    Console.ResetColor();
    Console.WriteLine("help command：csrebot -h");
    Console.WriteLine();
});
//创建子命令 show
var dbtoCommand = new Command("dbto", "从数据库生成");

#region 选项 language
//创建子命令选项 language 别名 lan
var languageOption = new Option<string>(name: "--language", description: "从数据库生成实体类的语言")
{
    IsRequired = true,
}.FromAmong("c#", "C#", "csharp", "CSharp", "CSHARP", "go", "GO", "java", "JAVA");
languageOption.AddAlias("-lan");
languageOption.AddAlias("-l");
//添加language选项到dbto命令中
dbtoCommand.AddOption(languageOption);
#endregion

#region connestionString
//创建子命令选项 connestionString 别名 constr
var connectionStringOption = new Option<string>(name: "--connectionstring", description: "数据库连接字符串")
{
    IsRequired = true,
};
connectionStringOption.AddAlias("-constr");

//添加language选项到dbto命令中
dbtoCommand.AddOption(connectionStringOption);
#endregion

#region dbtype
//创建子命令选项 connestionString 别名 constr
var dbTypeOption = new Option<string>(name: "--dbtype", description: "数据库类型")
{
    IsRequired = true,
}.FromAmong("mssql", "mysql", "pgsql"); ;
dbTypeOption.AddAlias("-dbt");
dbTypeOption.AddAlias("-t");
//添加language选项到dbto命令中
dbtoCommand.AddOption(dbTypeOption);
#endregion

//设置命令dbto执行的动作，这是带上language参数
dbtoCommand.SetHandler(async (string language, string connectionstring, string dbType) =>
{
    switch (language.ToLower())
    {
        case "c#":
        case "csharp":
            await DBToCSharpAsync(connectionstring, dbType);
            break;
        case "go":
            break;
        case "java":
            break;
        default:
            break;

    }
}, languageOption, connectionStringOption, dbTypeOption);

//添加命令dbto到 根命令中
rootCommand.Add(dbtoCommand);
await rootCommand.InvokeAsync(args);


static async Task DBToCSharpAsync(string connectionString, string dbType)
{
    switch (dbType.ToLower())
    {
        case "mssql":
            await MSSQLToCSharpAsync(connectionString);
            break;
        case "mysql":
            await MySQLToCSharpAsync(connectionString);
            break;
        case "pgsql":
            await PgSQLToCSharpAsync(connectionString);
            break;
        default:
            break;
    }
}

static async Task MSSQLToCSharpAsync(string connectionString)
{
    using var con = new SqlConnection(connectionString);
    using var cmd = new SqlCommand("select name from sysobjects where xtype='U'", con);
    await con.OpenAsync();
    using var reader = await cmd.ExecuteReaderAsync();
    var tableNames = new List<string>();
    while (reader.Read())
    {
        tableNames.Add(reader.GetString(0));
    }
    await reader.CloseAsync();

    var dbPath = $"{Environment.CurrentDirectory}\\{con.Database}";
    if (!Directory.Exists(dbPath))
    {
        Directory.CreateDirectory(dbPath);
    }
    foreach (var tableName in tableNames)
    {
        using var fileCmd = new SqlCommand(@"
select c.name,col.DATA_TYPE as typename,com.value as comment,col.IS_NULLABLE as isnull
from sys.tables t 
join sys.columns c 
on t.object_id=c.object_id
join INFORMATION_SCHEMA.COLUMNS col 
on col.column_name=c.name
left join sys.extended_properties com
on com.major_id=c.object_id and com.minor_id=c.column_id
where t.name=@tablename and col.TABLE_NAME=@tablename 

", con);
        fileCmd.Parameters.Add("tablename", SqlDbType.VarChar).Value = tableName;
        using var fileReader = await fileCmd.ExecuteReaderAsync();
        var tablefieldses = new List<dynamic>();
        while (fileReader.Read())
        {
            tablefieldses.Add(new { name = fileReader.GetString(0), typename = fileReader.GetString(1), comment = fileReader.IsDBNull(2) ? null : fileReader.GetString(2), isnull = fileReader.GetString(3) });
        }
        fileReader.Close();
        await fileReader.DisposeAsync();

        var csBuilder = new StringBuilder();
        csBuilder.AppendLine($"public class {tableName}");
        csBuilder.AppendLine("{");
        foreach (var tablefields in tablefieldses)
        {
            var typeName = TypeMap.MSSQLToCSharp[tablefields.typename];
            var name = tablefields.name;
            if (!string.IsNullOrWhiteSpace(tablefields.comment))
            {
                csBuilder.AppendLine(@$"
   /// <summary>
   /// {tablefields.comment}
   /// </summary>");
            }
            csBuilder.AppendLine($"   public {(typeName == "string" && tablefields.isnull == "YES" ? "string?" : typeName)} {name}{{get;set;}}");
        }
        csBuilder.AppendLine("}");
        File.WriteAllText($"{dbPath}\\{tableName}.cs", csBuilder.ToString(), Encoding.UTF8);
    }
}


static async Task MySQLToCSharpAsync(string connectionString)
{
    using var con = new MySqlConnection(connectionString);
    using var cmd = new MySqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA=@databasename;", con);
    cmd.Parameters.Add("databasename", MySqlDbType.VarChar).Value = con.Database;
    await con.OpenAsync();
    using var reader = await cmd.ExecuteReaderAsync();
    var tableNames = new List<string>();
    while (reader.Read())
    {
        tableNames.Add(reader.GetString(0));
    }
    await reader.CloseAsync();

    var dbPath = $"{Environment.CurrentDirectory}\\{con.Database}";
    if (!Directory.Exists(dbPath))
    {
        Directory.CreateDirectory(dbPath);
    }
    foreach (var tableName in tableNames)
    {
        using var fileCmd = new MySqlCommand("select column_name as name,data_type as typename,column_comment as comment,is_nullable as isnull from information_schema.columns where table_name=@tablename", con);
        fileCmd.Parameters.Add("tablename", MySqlDbType.VarChar).Value = tableName;
        using var fileReader = await fileCmd.ExecuteReaderAsync();
        var tablefieldses = new List<dynamic>();
        while (fileReader.Read())
        {
            tablefieldses.Add(new { name = fileReader.GetString(0), typename = fileReader.GetString(1), comment = fileReader.IsDBNull(2) ? null : fileReader.GetString(2), isnull = fileReader.GetString(3) });
        }
        fileReader.Close();
        await fileReader.DisposeAsync();

        var csBuilder = new StringBuilder();
        csBuilder.AppendLine($"public class {tableName}");
        csBuilder.AppendLine("{");
        foreach (var tablefields in tablefieldses)
        {
            var typeName = TypeMap.MySQLToCSharp[tablefields.typename];
            var name = tablefields.name;
            if (!string.IsNullOrWhiteSpace(tablefields.comment))
            {
                csBuilder.AppendLine(@$"
   /// <summary>
   /// {tablefields.comment}
   /// </summary>");
            }
            csBuilder.AppendLine($"   public {(typeName == "string" && tablefields.isnull == "YES" ? "string?" : typeName)} {name}{{get;set;}}");
        }
        csBuilder.AppendLine("}");
        File.WriteAllText($"{dbPath}\\{tableName}.cs", csBuilder.ToString(), Encoding.UTF8);
    }
}

static async Task PgSQLToCSharpAsync(string connectionString)
{
    using var con = new NpgsqlConnection(connectionString);
    using var cmd = new NpgsqlCommand("select tablename from pg_tables where schemaname='public'", con);

    await con.OpenAsync();
    using var reader = await cmd.ExecuteReaderAsync();
    var tableNames = new List<string>();
    while (reader.Read())
    {
        tableNames.Add(reader.GetString(0));
    }
    await reader.CloseAsync();

    var dbPath = $"{Environment.CurrentDirectory}\\{con.Database}";
    if (!Directory.Exists(dbPath))
    {
        Directory.CreateDirectory(dbPath);
    }
    foreach (var tableName in tableNames)
    {
        using var fileCmd = new NpgsqlCommand(@"SELECT	
	A.attname AS name,
    T.typname AS typename,
	col_description (A.attrelid, A.attnum) AS comment,
	A.attnotnull AS isnull	
FROM
	pg_class AS C,
	pg_attribute AS A,
	pg_type AS T
WHERE
	C.relname = @tablename
AND A.attrelid = C.oid
AND A.attnum > 0
AND A.atttypid=T.oid;", con);
        fileCmd.Parameters.Add("tablename", NpgsqlDbType.Varchar).Value = tableName;
        using var fileReader = await fileCmd.ExecuteReaderAsync();
        var tablefieldses = new List<dynamic>();
        while (fileReader.Read())
        {
            tablefieldses.Add(new { name = fileReader.GetString(0), typename = fileReader.GetString(1), comment = fileReader.IsDBNull(2) ? null : fileReader.GetString(2), isnull = fileReader.GetBoolean(3) });
        }
        fileReader.Close();
        await fileReader.DisposeAsync();

        var csBuilder = new StringBuilder();
        csBuilder.AppendLine($"public class {tableName}");
        csBuilder.AppendLine("{");
        foreach (var tablefields in tablefieldses)
        {
            var typeName = TypeMap.PgSQLToCSharp[tablefields.typename];
            var name = tablefields.name;
            if (!string.IsNullOrWhiteSpace(tablefields.comment))
            {
                csBuilder.AppendLine(@$"
   /// <summary>
   /// {tablefields.comment}
   /// </summary>");
            }
            csBuilder.AppendLine($"   public {(typeName == "string" && tablefields.isnull ? "string?" : typeName)} {name}{{get;set;}}");
        }
        csBuilder.AppendLine("}");
        File.WriteAllText($"{dbPath}\\{tableName}.cs", csBuilder.ToString(), Encoding.UTF8);
    }
}

static class TypeMap
{
    internal static Dictionary<string, string> MSSQLToCSharp => new Dictionary<string, string>
    {
        {"bigint","long"},
        {"binary","byte[]"},
        {"bit","bool"},
        {"char","string"},
        {"date","DateTime"},
        {"datetime","DateTime"},
        {"datetime2","DateTime"},
        {"datetimeoffset","DateTimeOffset" },
        {"decimal","decimal" },
        {"float","double" },
        {"image","byte[]" },
        {"int","int" },
        {"money","decimal" },
        {"nchar","string" },
        {"ntext","string" },
        {"numeric","decimal" },
        {"nvarchar","string" },
        {"real","float" },
        {"rowversion","byte[]" },
        {"smalldatetime","DateTime" },
        {"smallint","short" },
        {"smallmoney","decimal" },
        {"sql_variant","object"},
        {"text","string" },
        {"time","TimeSpan" },
        {"timestamp","byte[]" },
        {"tinyint","byte" },
        {"uniqueidentifier","Guid" },
        {"varbinary","byte[]" },
        {"varchar","string" },
    };

    internal static Dictionary<string, string> MySQLToCSharp => new Dictionary<string, string>
    {

        {"bool","bool"},
        {"boolean","bool" },
        {"bit","bool" },
        {"tinyint","sbyte" },
        {"tinyint unsigned","byte" },
        {"smallint","short" },
        {"smallint unsigned","ushort" },
        {"year","short" },
        {"int","int" },
        {"int unsigned","uint" },
        {"integer","int" },
        {"integer unsigned","uint" },
        {"mediumint","int" },
        {"mediumint unsigned","int" },
        {"bigint","long" },
        {"bigint unsigned","ulong" },
        {"float","float" },
        {"double","double" },
        {"real","double" },
        {"decimal","decimal" },
        {"numeric","decimal" },
        {"dec","decimal" },
        {"fixed","decimal" },
        {"float unsigned","decimal" },
        {"double unsigned","decimal" },
        {"serial","decimal" },
        {"date","DateTime" },
        {"timestamp","DateTime" },
        {"datetime","DateTime" },
        {"time","DateTime" },
        {"datetimeoffset","DateTimeOffset" },
        {"char","string" },
        {"varchar","string" },
        {"tinytext","string" },
        {"text","string" },
        {"mediumtext","string" },
        {"longtext","string" },
        {"set","string" },
        {"enum","string" },
        {"nchar","string" },
        {"national char","string" },
        {"nvarchar","string" },
        {"national varchar","string" },
        {"character varying","string" },
        {"binary","byte[]" },
        {"varbinary","byte[]" },
        {"tinyblob","byte[]" },
        {"blob","byte[]" },
        {"mediumblob","byte[]" },
        {"longblob","byte[]" },
        {"char byte","byte[]" },
        {"json","text" },
    };
    internal static Dictionary<string, string> PgSQLToCSharp => new Dictionary<string, string>
    {
        {"bool","bool" },
        {"boolean","bool" },
        {"smallint","short" },
        {"int2","short" },
        {"integer","int" },
        {"int4","int" },
        {"bigint","long" },
        {"int8","long" },
        {"real","float" },
        {"double precision","double" },
        {"money","double" },
        {"numeric","decimal" },
        {"decimal","decimal" },
        {"float","decimal" },
        {"date","DateTime" },
        {"timestamp","DateTime" },
        {"timestamp with time zone","DateTime" },
        {"timestamp without time zone","DateTime" },
        {"time","DateTime" },
        {"time with time zone","DateTimeOffset" },
        {"time without time zone","DateTime" },
        {"interval","DateTime" },      
        {"char","string" },
        {"varchar","string" },
        {"text","string" },
        {"citext","string" },
        {"enum","string" },
        {"bit","string" },
        {"varbit","string" },
        {"cidr","string" },
        {"inet","string" },
        {"macaddr","string" },
        {"point","string" },
        {"line","string" },
        {"lseg","string" },
        {"box","string" },
        {"path","string" },
        {"polygon","string" },
        {"circle","string" },
        {"xml*","string" },
        {"character","string" },
        {"character varying","string" },
        {"bit varying","string" },
        {"json","string" },
        {"jsonb","string" },
        {"bytea","byte[]" },
        {"uuid","Guid" },
    };
}

