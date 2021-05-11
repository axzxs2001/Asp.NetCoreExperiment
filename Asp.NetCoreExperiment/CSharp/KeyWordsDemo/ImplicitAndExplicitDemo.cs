using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeyWordsDemo
{
    class ImplicitAndExplicitDemo : IDemo
    {
        public void Run()
        {
            Date date = DateTime.Now;
            Console.WriteLine(date);
            DateTime datetime = (DateTime)date;
            Console.WriteLine(datetime);
        }

        class Date
        {
            string _value;
            public Date(DateTime dateTime)
            {
                _value = dateTime.ToString("yyyy/MM/dd");
            }

            public static implicit operator Date(DateTime dateTime)
            {
                return new Date(dateTime);
            }
            public static explicit operator DateTime(Date date)
            {
                return DateTime.Parse(date._value);
            }
            public override string ToString()
            {
                return _value;
            }
        }

    }
}