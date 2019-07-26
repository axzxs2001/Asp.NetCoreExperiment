using Npgsql;
using System;

namespace DeapperDemo002
{
    class Program
    {
        static void Main(string[] args)
        {
            //var file = System.IO.Directory.GetCurrentDirectory() + "/sql.txt";

            //var content = System.IO.File.ReadAllText(file).ToLower();

            //return;
            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=postgre2018;Database=TestDB;";
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Insert some data
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO Roles(RoleName) VALUES (@rolename)";
                    cmd.Parameters.AddWithValue("rolename", "aaa");
                    cmd.ExecuteNonQuery();
                }

                using (var cmd = new NpgsqlCommand("SELECT id FROM Roles", conn))
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        Console.WriteLine(reader.GetString(0));
            }



        }
    }
}
