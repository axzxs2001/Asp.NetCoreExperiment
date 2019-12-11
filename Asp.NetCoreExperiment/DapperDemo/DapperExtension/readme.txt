
1、单数据库使用方式
	//Startup注入
	public void ConfigureServices(IServiceCollection services)
	{
		#region 单数据库  注入连接字符串  注入IDapperPlusDB  注入IDbConnection
		var connection = string.Format(Configuration.GetConnectionString("Sqlite"), System.IO.Directory.GetCurrentDirectory());
		services.AddSingleton(connection);
		services.AddScoped<IDapperPlusDB, DapperPlusDB>();
		services.AddScoped<IDbConnection, SqliteConnection>();
		#endregion

		services.AddControllers()
			.AddNewtonsoftJson();
	}

	//service构造中调用
	class SingleDatabaseService
	{
		public SingleDatabaseService(IDapperPlusDB dapperPlusDB)
		{
			_sqliteDB = dapperPlusDB;
		}
	}
2、多数据注入和调用
   //Startup注入
   public void ConfigureServices(IServiceCollection services)
   {
	   #region 多数据库  注入各个数据库链接对象
	   services.AddScoped<IDapperPlusDB, DapperPlusDB>(service =>
	   {
		   return new DapperPlusDB(new SqliteConnection(string.Format(Configuration.GetConnectionString("Sqlite"), System.IO.Directory.GetCurrentDirectory())));
	   });
	   services.AddScoped<IDapperPlusDB, DapperPlusDB>(service =>
	   {
		   return new DapperPlusDB(new NpgsqlConnection(Configuration.GetConnectionString("Postgre")));
	   });
	   #endregion

		#region MultiDatabase  注入相对类型数据库链接对象
	   //services.AddScoped<IDapperPlusDB, DapperPlusDB>(service =>
	   //{
	   //    return new DapperPlusDB(dbConnection: new NpgsqlConnection(Configuration.GetConnectionString("Postgre")), dataBaseMark: "pg");
	   //});
	   //services.AddScoped<IDapperPlusDB, DapperPlusDB>(service =>
	   //{
	   //    return new DapperPlusDB(dbConnection: new NpgsqlConnection(Configuration.GetConnectionString("Postgre_test")), dataBaseMark: "pgtest");
	   //});
	   #endregion
	   services.AddControllers()
		   .AddNewtonsoftJson();
   }


	//service构造中调用
	class MultiDatabaseService
	{
		readonly IDapperPlusDB _sqliteDB;
		readonly IDapperPlusDB _postgreDB;
		public MultiDatabaseService(IEnumerable<IDapperPlusDB> dapperPlusDBs)
		{
			foreach (var dapperPlusDB in dapperPlusDBs)
			{
				switch (dapperPlusDB.DataBaseType)
				{
					case DataBaseType.Sqlite:
						_sqliteDB = dapperPlusDB;
						break;
					case DataBaseType.Postgre:
						_postgreDB = dapperPlusDB;
						break;
				}
			}
		}
	}

