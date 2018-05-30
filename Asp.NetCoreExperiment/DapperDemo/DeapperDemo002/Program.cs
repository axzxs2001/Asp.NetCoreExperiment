using Npgsql;
using System;

namespace DeapperDemo002
{
    class Program
    {
        static void Main(string[] args)
        {
            var connString = "Server=127.0.0.1;Port=5432;UserId=postgres;Password=gsw790622;Database=NetStarsUnionpayDB;";
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Insert some data
                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = @"INSERT INTO public.""Roles""(""RoleName"") VALUES (@rolename)";
                    cmd.Parameters.AddWithValue("rolename", "aaa");
                    cmd.ExecuteNonQuery();
                }

                // Retrieve all rows
                //using (var cmd = new NpgsqlCommand("SELECT some_field FROM data", conn))
                //using (var reader = cmd.ExecuteReader())
                //    while (reader.Read())
                //        Console.WriteLine(reader.GetString(0));
            }
         


        }
    }
}
