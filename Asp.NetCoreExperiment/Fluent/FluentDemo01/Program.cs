using System;
using System.Data;
using System.Data.SqlClient;
using Ductus.FluentDocker.Builders;
namespace FluentDemo01
{
    class Program
    {
        static void Main(string[] args)
        {
            var cont = new Builder()
                .UseContainer()
                .UseImage("abc/alpine-postgres")
                .ExposePort(5432)
                .WithEnvironment("")
                .Build()
                .Start();

            var con = FSqlConnection
                   .CreateConnection()
                   .ForServer("127.0.0.1")
                   .AndDataBase("mydb")
                   .UseUser("sa")
                   .UsePassword("pwd")
                   .Connect();


            Console.WriteLine(con.ConnectionString);
        }
    }

    class FSqlConnection : IServerSelectionStage, IDataBaseSelectionStage, IUserSelectionStage, IPasswordSelectionStage
    {
        private FSqlConnection() { }
        public static IServerSelectionStage CreateConnection()
        {
            return new FSqlConnection();
        }

        private string _server;
        public IDataBaseSelectionStage ForServer(string server)
        {
            _server = server;
            return this;
        }
        private string _database;
        public IUserSelectionStage AndDataBase(string database)
        {
            _database = database;
            return this;
        }
        private string _username;
        public IPasswordSelectionStage UseUser(string username)
        {
            _username = username;
            return this;
        }
        private string _password;
        public FSqlConnection UsePassword(string password)
        {
            _password = password;
            return this;
        }

        public IDbConnection Connect()
        {
            return new SqlConnection($"server={_server};database={_database};uid={_username};pwd={_password};");
        }
    }

    internal interface IServerSelectionStage
    {
        IDataBaseSelectionStage ForServer(string server);
    }
    internal interface IDataBaseSelectionStage
    {
        IUserSelectionStage AndDataBase(string server);
    }
    internal interface IUserSelectionStage
    {
        IPasswordSelectionStage UseUser(string username);
    }
    internal interface IPasswordSelectionStage
    {
        FSqlConnection UsePassword(string password);
    }
}
