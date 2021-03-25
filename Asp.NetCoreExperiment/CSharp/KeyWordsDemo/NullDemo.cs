using System;
using static System.Console;

namespace KeyWordsDemo
{
    class NullDemo : Demo
    {
        public void Run()
        {
            var appOrder = new AppOrder();
            if (appOrder == null)
            {
                WriteLine("appOrder == null:appOrder is null");
            }
            if (appOrder is null)
            {
                WriteLine("appOrder is null:appOrder is null");
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
    }

    class AppOrder : Order
    {
    }

}
