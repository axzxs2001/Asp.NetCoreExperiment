using System;

namespace CalendarDemo
{
    class Program
    {
        private static readonly string[] _celestialStem = { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
        private static readonly string[] _terrestrialBranch = { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };
        private static readonly string[] _chineseZodiac = { "鼠", "牛", "虎", "免", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };
        private static readonly string[] _chineseMonth =
       {
            "正", "二", "三", "四", "五", "六", "七", "八", "九", "十", "冬", "腊"
        };
        private static readonly string[] _chineseDay =
        {
            "初一", "初二", "初三", "初四", "初五", "初六", "初七", "初八", "初九", "初十",
            "十一", "十二", "十三", "十四", "十五", "十六", "十七", "十八", "十九", "二十",
            "廿一", "廿二", "廿三", "廿四", "廿五", "廿六", "廿七", "廿八", "廿九", "三十"
        };
 
        static void Main(string[] args)
        {
            var dt = DateTime.Parse("2019/06/02");
            Console.WriteLine(dt);
            Console.WriteLine("----------------------------------");

            //日本日历
            var jpci = new System.Globalization.CultureInfo("ja-JP");
            var jpcal = new System.Globalization.JapaneseCalendar();
            jpci.DateTimeFormat.Calendar = jpcal;
            Console.WriteLine(dt.ToString("ggyy/MM/dd", jpci));

            Console.WriteLine("----------------------------------");
            //中国农历            
            var zhcal = new System.Globalization.ChineseLunisolarCalendar();          
            int yearIndex = zhcal.GetSexagenaryYear(dt);
            int yTG = zhcal.GetCelestialStem(yearIndex);
            int yDZ = zhcal.GetTerrestrialBranch(yearIndex);
            Console.WriteLine($"{_celestialStem[yTG-1]}{_terrestrialBranch[yDZ-1]}  {_chineseZodiac[yDZ - 1]}年  {_chineseMonth[zhcal.GetMonth(dt)-1]}月{_chineseDay[zhcal.GetDayOfMonth(dt)-1]}");



        }
    }
}
