using Microsoft.Data.SqlClient;
using System.CommandLine;
using System.CommandLine.Binding;
using System.Data;
using System.Text;
using System;


//dotnet run dbto -l C# -constr 'server=localhost;database=mqpay;uid=sa;pwd=sa;encrypt=true;trustservercertificate=true' -t mssql

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
}.FromAmong("c#", "C#", "csharp", "CSharp", "go", "GO", "java", "JAVA");
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
        using var fileCmd = new SqlCommand("SELECT syscolumns.name,systypes.name as typename FROM syscolumns, systypes WHERE syscolumns.xusertype = systypes.xusertype AND syscolumns.id = object_id(@tablename)", con);
        fileCmd.Parameters.Add("tablename", SqlDbType.VarChar).Value = tableName;
        using var fileReader = await fileCmd.ExecuteReaderAsync();
        var tablefieldses = new List<dynamic>();
        while (fileReader.Read())
        {
            tablefieldses.Add(new { name = fileReader.GetString(0), typename = fileReader.GetString(1) });
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
            csBuilder.AppendLine($"   public {typeName} {name}{{get;set;}}");
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
        {"","" }
    };
    internal static Dictionary<string, string> PgSQLToCSharp => new Dictionary<string, string>
    {
        {"","" }
    };
}