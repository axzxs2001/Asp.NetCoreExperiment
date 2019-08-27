using System;

namespace GeneratePgSql
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
             * 1、安装包
            Install-package Microsoft.EntityFrameworkCore.Tools 
            Install-package Microsoft.EntityFrameworkCore.Design 
            Install-package Npgsql.EntityFrameworkCore.PostgreSQL
            *2、生成实体类
            Scaffold-DbContext -Connection "Server=127.0.0.1;Port=5432;UserId=postgres;Password=mypassword;Database=TestDB1;" Npgsql.EntityFrameworkCore.PostgreSQL -OutputDir "Models"
            *
            */
            Console.WriteLine("Hello World!");
        }
    }
}
