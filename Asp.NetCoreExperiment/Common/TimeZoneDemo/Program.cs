using System;

namespace TimeZoneDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var i = 1;
            foreach (var tz in TimeZoneInfo.GetSystemTimeZones())
            {
                Console.WriteLine(i + "、" + tz.DisplayName);
                i++;
            }
            var tzi = TimeZoneInfo.Utc;
            var dt = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.CreateCustomTimeZone("1121", TimeSpan.FromHours(8), "gsw", "gsw"));
            Console.WriteLine(dt);
           
          
        }
    }
}
