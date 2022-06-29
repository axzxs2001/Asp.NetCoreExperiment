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
using System.Drawing;


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
dbtoCommand.AddOption(dbTypeOption);
#endregion

#region annotation
//创建子命令选项 annotation 别名 ann
var annotationOption = new Option<string>(name: "--annotation", description: "注解，生成go语言时，在每个数据项后面，如:`db:\"{$name}\" json:\"{$name}\"`");
dbTypeOption.AddAlias("-ann");
dbtoCommand.AddOption(annotationOption);
#endregion


//设置命令dbto执行的动作，这是带上language参数
dbtoCommand.SetHandler(async (string language, string connectionString, string dbType, string annotation) =>
{
	var dataBase = await GetDataBaseInfomationAsync(connectionString, dbType);
	switch (language.ToLower())
	{
		case "c#":
		case "csharp":
			await DBToCSharpAsync(dataBase.DBName, dbType, dataBase.Tables);
			break;
		case "go":
			await DBToGoAsync(dataBase.DBName, dbType, dataBase.Tables, annotation);
			break;
		case "java":
			break;
		default:
			break;

	}
}, languageOption, connectionStringOption, dbTypeOption, annotationOption);

//添加命令dbto到 根命令中
rootCommand.Add(dbtoCommand);
await rootCommand.InvokeAsync(args);


#region 生成的语言c#,go
static async Task DBToCSharpAsync(string dbName, string dbType, List<(string TableName, List<dynamic> Columns)> tables)
{

	var dbPath = $"{Environment.CurrentDirectory}\\{dbName}";
	if (!Directory.Exists(dbPath))
	{
		Directory.CreateDirectory(dbPath);
	}
	foreach (var table in tables)
	{
		var csBuilder = new StringBuilder();
		csBuilder.AppendLine($"using System;");
		csBuilder.AppendLine();
		csBuilder.AppendLine($"namespace {dbName};");
		csBuilder.AppendLine($"public class {table.TableName}");
		csBuilder.AppendLine("{");
		foreach (var column in table.Columns)
		{
			var typeName = TypeMap.Types["csharp"][dbType][column.typename];
			var name = column.name;
			if (!string.IsNullOrWhiteSpace(column.comment))
			{
				csBuilder.AppendLine(@$"
   /// <summary>
   /// {column.comment}
   /// </summary>");
			}
			csBuilder.AppendLine($"   public {(typeName == "string" && column.isnull == "YES" ? "string?" : typeName)} {name}{{get;set;}}");
		}
		csBuilder.AppendLine("}");
		await File.WriteAllTextAsync($"{dbPath}\\{table.TableName}.cs", csBuilder.ToString(), Encoding.UTF8);
	}
}

static async Task DBToGoAsync(string dbName, string dbType, List<(string TableName, List<dynamic> Columns)> tables, string annotation)
{

	var dbPath = $"{Environment.CurrentDirectory}\\{dbName}";
	if (!Directory.Exists(dbPath))
	{
		Directory.CreateDirectory(dbPath);
	}
	foreach (var table in tables)
	{
	  

		var nameMaxLen = 0;
		var typeMaxLen = 0;
		var existTime = false;
		foreach (var column in table.Columns)
		{
			if (column.name.Length > nameMaxLen)
			{
				nameMaxLen = column.name.Length;
			}
			if (column.typename.Length > typeMaxLen)
			{
				typeMaxLen = column.name.Length;
			}
			if ("time.Time" == TypeMap.Types["go"][dbType][column.typename])
			{
				existTime = true;
			}
		}
		var csBuilder = new StringBuilder();
		csBuilder.AppendLine($"package {dbName}");

		csBuilder.AppendLine();
		csBuilder.AppendLine($"import (");
		csBuilder.AppendLine($"\t\ttime");
		csBuilder.AppendLine($")");
		csBuilder.AppendLine();
		csBuilder.AppendLine($"type {table.TableName} struct{{");

		foreach (var column in table.Columns)
		{
			var typeName = TypeMap.Types["go"][dbType][column.typename];
			var name = column.name;

			csBuilder.Append($"\t\t{name.PadRight(nameMaxLen)} {typeName.PadRight(typeMaxLen)} ");
			if (!string.IsNullOrWhiteSpace(annotation))
			{
				csBuilder.Append(annotation.Replace("{$name}", name).Replace("{$type}", typeName));
			}
			if (!string.IsNullOrWhiteSpace(column.comment))
			{
				csBuilder.Append(@$"//{column.comment}");
			}
			csBuilder.AppendLine();
		}
		csBuilder.AppendLine("}");
		await File.WriteAllTextAsync($"{dbPath}\\{table.TableName}.go", csBuilder.ToString(), Encoding.UTF8);
	}
}

#endregion

#region 获取mssql,mysql,pgsql的库，表信息
static async Task<(string DBName, List<(string TableName, List<dynamic> Columns)> Tables)> GetDataBaseInfomationAsync(string connectionString, string dbType)
{
	switch (dbType.ToLower())
	{
		case "mssql":
			return await GetMSSQLInfomationAsync(connectionString);
		case "mysql":
			return await GetMySQLInfomationAsync(connectionString);
		case "pgsql":
			return await GetPgSQLInfomationAsync(connectionString);
		default:
			throw new Exception($"\"{dbType}\" is error");
	}

}

static async Task<(string DBName, List<(string TableName, List<dynamic> Columns)> Tables)> GetMSSQLInfomationAsync(string connectionString)
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
	var tablesInformations = new List<(string TableName, List<dynamic> Columns)>();
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
		tablesInformations.Add((tableName, tablefieldses));
	}

	return (con.Database, tablesInformations);
}

static async Task<(string DBName, List<(string TableName, List<dynamic> Columns)> Tables)> GetMySQLInfomationAsync(string connectionString)
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

	var tablesInformations = new List<(string TableName, List<dynamic> Columns)>();
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
		tablesInformations.Add((tableName, tablefieldses));
	}
	return (con.Database, tablesInformations);
}

static async Task<(string DBName, List<(string TableName, List<dynamic> Columns)> Tables)> GetPgSQLInfomationAsync(string connectionString)
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

	var tablesInformations = new List<(string TableName, List<dynamic> Columns)>();
	foreach (var tableName in tableNames)
	{
		using var fileCmd = new NpgsqlCommand(@"SELECT	
	A.attname AS name,
	T.typname AS typename,
	col_description (A.attrelid, A.attnum) AS comment,
	case when A.attnotnull=true then 'YES' else 'NO' end AS isnull	
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
			tablefieldses.Add(new { name = fileReader.GetString(0), typename = fileReader.GetString(1), comment = fileReader.IsDBNull(2) ? null : fileReader.GetString(2), isnull = fileReader.GetString(3) });
		}
		fileReader.Close();
		await fileReader.DisposeAsync();
		tablesInformations.Add((tableName, tablefieldses));
	}
	return (con.Database, tablesInformations);
}
#endregion


#region 语言和数据库映射关系




static class TypeMap
{
	static readonly Dictionary<string, Dictionary<string, Dictionary<string, string>>> typeMap;
	static TypeMap()
	{
		var csharpDBDic = new Dictionary<string, Dictionary<string, string>>()
		{
			{"mssql", MSSQLToCSharp },
			{"mysql", MySQLToCSharp},
			{"pgsql", PgSQLToCSharp},
		};
		var goDBDic = new Dictionary<string, Dictionary<string, string>>()
		{
			{"mssql", MSSQLToGo },
			{"mysql", MySQLToGo},
			{"pgsql", PgSQLToGo},
		};
		typeMap = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>()
		{
			{"csharp",csharpDBDic },
			{"go",goDBDic}
		};
	}

	public static Dictionary<string, Dictionary<string, Dictionary<string, string>>> Types
	{
		get
		{
			return typeMap;
		}
	}
	static Dictionary<string, string> MSSQLToGo => new Dictionary<string, string>
	{
		{"bigint","int64"},
		{"binary","[]byte"},
		{"bit","bool"},
		{"char","string"},
		{"date","DateTime"},
		{"datetime","time.Time"},
		{"datetime2","time.Time"},
		{"datetimeoffset","time.Time" },
		{"decimal","float64" },
		{"float","float32" },
		{"image","[]byte" },
		{"int","int32" },
		{"money","float64" },
		{"nchar","string" },
		{"ntext","string" },
		{"numeric","float64" },
		{"nvarchar","string" },
		{"real","float32" },
		{"rowversion","[]byte" },
		{"smalldatetime","time.Time" },
		{"smallint","int16" },
		{"smallmoney","float64" },
		{"sql_variant","interface{}"},
		{"text","string" },
		{"time","time.Duration" },
		{"timestamp","[]byte" },
		{"tinyint","byte" },
		{"uniqueidentifier","string" },
		{"varbinary","[]byte" },
		{"varchar","string" },
	};
	static Dictionary<string, string> MySQLToGo => new Dictionary<string, string>
	{

		{"bool","bool"},
		{"boolean","bool" },
		{"bit","bool" },
		{"tinyint","int8" },
		{"tinyint unsigned","uint8" },
		{"smallint","int16" },
		{"smallint unsigned","uint16" },
		{"year","int16" },
		{"int","int32" },
		{"int unsigned","uint32" },
		{"integer","int32" },
		{"integer unsigned","uint32" },
		{"mediumint","int32" },
		{"mediumint unsigned","int32" },
		{"bigint","int64" },
		{"bigint unsigned","uint64" },
		{"float","float32" },
		{"double","float64" },
		{"real","float64" },
		{"decimal","float64" },
		{"numeric","float64" },
		{"dec","float64" },
		{"fixed","float64" },
		{"float unsigned","float64" },
		{"double unsigned","float64" },
		{"serial","float64" },
		{"date","time.Time" },
		{"timestamp","time.Time" },
		{"datetime","time.Time" },
		{"time","time.Time" },
		{"datetimeoffset","time.Time" },
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
		{"binary","[]byte" },
		{"varbinary","[]byte" },
		{"tinyblob","[]byte" },
		{"blob","[]byte" },
		{"mediumblob","[]byte" },
		{"longblob","[]byte" },
		{"char byte","[]byte" },
		{"json","string" },
	};
	static Dictionary<string, string> PgSQLToGo => new Dictionary<string, string>
	{
		{"bool","bool" },
		{"boolean","bool" },
		{"smallint","int16" },
		{"int2","int16" },
		{"integer","int32" },
		{"int4","int32" },
		{"bigint","int64" },
		{"int8","int64" },
		{"real","float32" },
		{"double precision","float64" },
		{"money","float64" },
		{"numeric","float64" },
		{"decimal","float64" },
		{"float","float64" },
		{"date","time.Time" },
		{"timestamp","time.Time" },
		{"timestamp with time zone","time.Time" },
		{"timestamp without time zone","time.Time" },
		{"time","time.Time" },
		{"time with time zone","time.Time" },
		{"time without time zone","time.Time" },
		{"interval","time.Time" },
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
		{"bytea","[]byte" },
		{"uuid","string" },
	};
	static Dictionary<string, string> MSSQLToCSharp => new Dictionary<string, string>
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
	static Dictionary<string, string> MySQLToCSharp => new Dictionary<string, string>
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
		{"json","string" },
	};
	static Dictionary<string, string> PgSQLToCSharp => new Dictionary<string, string>
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

#endregion