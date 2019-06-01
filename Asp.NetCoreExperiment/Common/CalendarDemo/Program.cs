using System;

namespace CalendarDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //日本日历
            var jpci = new System.Globalization.CultureInfo("ja-JP");
            var jpcal = new System.Globalization.JapaneseCalendar();
            jpci.DateTimeFormat.Calendar = jpcal;
            var dt = DateTime.Parse("2019/04/30");
            Console.WriteLine(dt.ToString("ggyy/MM/dd", jpci));
            //中国农历
            var zhci = new System.Globalization.CultureInfo("zh-cn");
            var zhcal = new System.Globalization.ChineseLunisolarCalendar();
                   
            zhci.DateTimeFormat.Calendar = zhcal;
            Console.WriteLine(dt.ToString("yyyy/MM/dd", zhci));
        }
    }
}
