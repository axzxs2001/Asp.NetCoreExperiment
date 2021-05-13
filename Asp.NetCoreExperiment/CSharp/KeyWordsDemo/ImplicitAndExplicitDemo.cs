using System;
using static System.Console;

namespace KeyWordsDemo
{
    class ImplicitAndExplicitDemo : IDemo
    {
        public void Run()
        {
            //把DateTime赋值 给Date
            Date date = DateTime.Parse("2030-01-01 12:12:12");
            WriteLine($"Date:{date}");
            WriteLine($"Year:{date.Year}");
            WriteLine($"Month:{date.Month}");
            WriteLine($"Day:{date.Day}");

            //把Date转成DateTime类型
            var datetime = (DateTime)date;
            WriteLine($"DateTime:{datetime}");
        }

        public struct Date
        {
            private DateTime _value;
            public int Year
            {
                get
                {
                    return _value.Year;
                }
            }
            public int Month
            {
                get
                {
                    return _value.Month;
                }
            }
            public int Day
            {
                get
                {
                    return _value.Day;
                }
            }
            public static implicit operator Date(DateTime dateTime)
            {
                var date = new Date();
                date._value = dateTime;
                return date;
            }
            public static explicit operator DateTime(Date date)
            {
                return date._value.Date;
            }
            public override string ToString()
            {
                return _value.ToString("yyyy/MM/dd");
            }
        }
    }
}