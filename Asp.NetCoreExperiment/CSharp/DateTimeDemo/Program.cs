using System;
using System.Globalization;

namespace DateTimeDemo
{
    class Program
    {
        static void Main(string[] args)
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
    }
}
