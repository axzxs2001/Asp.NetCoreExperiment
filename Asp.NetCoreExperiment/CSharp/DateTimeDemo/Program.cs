using System;
using System.Globalization;
using Dapper;
using MySql.Data.MySqlClient;
using Npgsql;

namespace DateTimeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            DateOnly date = DateOnly.FromDateTime(DateTime.Now);
            Console.WriteLine(date);
            Console.WriteLine(date.ToLongDateString());
            Console.WriteLine(date.ToShortDateString());
            Console.WriteLine(DateOnly.FromDayNumber(735000));

            TimeOnly time = TimeOnly.FromDateTime(DateTime.Now);
            Console.WriteLine(time);
            Console.WriteLine(time.ToLongTimeString());
            Console.WriteLine(time.ToShortTimeString());



            //           using var con = new NpgsqlConnection("Server=.;Port=5432;UserId=postgres;Password=postgres2018;Database=postgres;");
            //           con.Execute(@"INSERT INTO public.timetest(
            // time_test, time_str, time_test_no, date_test, date_str, date_test_no, datetime_test, datetime_str, datetime_test_no)
            //VALUES (@time_test, @time_str, @time_test_no, @date_test, @date_str, @date_test_no, @datetime_test, @datetime_str, @datetime_test_no);", new { time_test = TimeOnly.FromDateTime(DateTime.Now), time_str = TimeOnly.FromDateTime(DateTime.Now), time_test_no = TimeOnly.FromDateTime(DateTime.Now), date_test = TimeOnly.FromDateTime(DateTime.Now), date_str = TimeOnly.FromDateTime(DateTime.Now), date_test_no = TimeOnly.FromDateTime(DateTime.Now), datetime_test = DateTime.Now, datetime_str = DateTime.Now, datetime_test_no = DateTime.Now });

            using var con = new MySqlConnection("Server=127.0.0.1;Database=shortdomain;Uid=root;Pwd=mars2020;");
            con.Execute(@"INSERT INTO timetest(
             time_test, time_str,  date_test, date_str,  datetime_test, datetime_str)
            VALUES (@time_test, @time_str,  @date_test, @date_str,  @datetime_test, @datetime_str );", new { time_test = (DateTime.Now), time_str = (DateTime.Now), date_test = (DateTime.Now), date_str = (DateTime.Now), datetime_test = DateTime.Now, datetime_str = DateTime.Now });


//            using var con = new MySqlConnection("Server=127.0.0.1;Database=shortdomain;Uid=root;Pwd=mars2020;");
//            var sql = @"INSERT INTO timetest(
//	 time_test,-- time_str,  
//date_test,-- date_str,  
//datetime_test-- datetime_str)
//	VALUES (@time_test, -- @time_str, 
//@date_test, -- @date_str, 
//@datetime_test -- @datetime_str 
//);";
//            using var cmd = new MySqlCommand(sql, con);
//            con.Open();
//            cmd.Parameters.Add(new MySqlParameter("@time_test", MySqlDbType.Time) { Value = TimeOnly.FromDateTime(DateTime.Now) });
//           // cmd.Parameters.Add(new MySqlParameter("@time_str", MySqlDbType.Time) { Value = TimeOnly.FromDateTime(DateTime.Now) });
//            cmd.Parameters.Add(new MySqlParameter("@date_test", MySqlDbType.Date) { Value = DateOnly.FromDateTime(DateTime.Now) });
//           // cmd.Parameters.Add(new MySqlParameter("@date_str", MySqlDbType.Date) { Value = DateOnly.FromDateTime(DateTime.Now) });
//            cmd.Parameters.Add(new MySqlParameter("@datetime_test", MySqlDbType.DateTime) { Value = DateTime.Now });
//           // cmd.Parameters.Add(new MySqlParameter("@datetime_str", MySqlDbType.DateTime) { Value = DateTime.Now });
//            cmd.ExecuteNonQuery();

        }

        #region 区域时间
        static void Test1()
        {
            ChineseDateTimeTest();
            Console.WriteLine("-------------");
            DateTimeTest();
            Console.WriteLine("-------------");
            DateTimeOffSetTest();
        }

        /// <summary>
        /// 中国农历
        /// </summary>
        static void ChineseDateTimeTest()
        {
            Console.WriteLine("天干地支：");
            string[] tiangan = { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
            string[] dizhi = { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };
            var calendar = new ChineseLunisolarCalendar();
            var i = calendar.GetSexagenaryYear(DateTime.Now);
            var t = calendar.GetCelestialStem(i) - 1;
            var d = calendar.GetTerrestrialBranch(i) - 1;
            Console.WriteLine($"{tiangan[t]}{dizhi[d]}年");
        }
        /// <summary>
        /// 普能时间格式
        /// </summary>
        static void DateTimeTest()
        {
            Console.WriteLine($"自定义时间格式：{DateTime.Now.ToString("yyyy年MM月dd日HH时mm分ss秒fff毫秒")}");
        }
        /// <summary>
        /// 带时区的格式
        /// </summary>
        static void DateTimeOffSetTest()
        {
            Console.WriteLine("带有时区的时间格式");
            var cnCulture = new CultureInfo("zh-CN");
            Console.WriteLine($"中国时间格式：{DateTimeOffset.Now.ToString(cnCulture)}");
            var jpCulture = new CultureInfo("ja-jp");
            Console.WriteLine($"日本时间格式：{DateTimeOffset.Now.ToString(jpCulture)}");
            var usCulture = new CultureInfo("en-us");
            Console.WriteLine($"日本时间格式：{DateTimeOffset.Now.ToString(usCulture)}");


            Console.WriteLine($"本地时区(毫秒)：{ DateTimeOffset.Now.ToString("o")}");
            Console.WriteLine($"UTC时区：{DateTimeOffset.UtcNow.ToString()}");
            Console.WriteLine($"东八区时间：{DateTimeOffset.Now.ToOffset(new TimeSpan(8, 0, 0))}");

        }
        #endregion
    }
}
