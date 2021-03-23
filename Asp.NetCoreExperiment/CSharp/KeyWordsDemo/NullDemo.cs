using System;
using static System.Console;

namespace KeyWordsDemo
{
    class NullDemo : Demo
    {
        public void Run()
        {
            var str = string.Empty;//和str=""等价
            var result = str switch
            {
                var s when s is null => "string.Empty is null",
                var s when s is "" => "string.Empty is \"\"",
                var _ => "none"
            };
            WriteLine(result);

            if (string.IsNullOrEmpty(str))
            {
                WriteLine("string is null or \"\"");
            }

            //------------------
            Nullable<int> i = 10;
            if (i.HasValue)
            {
                WriteLine(i.Value);
            }
            int? ii = null;
            if (ii.HasValue)
            {
                WriteLine(ii.Value);
            }
            //-------
            var c = new AppOrder();
            if (c == null)
            {
                Console.WriteLine("c is null");
            }


        }
    }
    class Order
    {
        public static bool operator ==(Order left, Order right)
        {
            return true;
        }
        public static bool operator !=(Order left, Order right)
        {
            return true;
        }

        public string OrderNo
        { get; set; }
    }

    class AppOrder : Order
    {

    }

}
