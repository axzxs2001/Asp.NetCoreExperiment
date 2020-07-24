using System;

namespace EnumDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            FlagsEnumTest();
        }
        /// <summary>
        /// 测试位枚举
        /// </summary>
        static void FlagsEnumTest()
        {
            var operation = Operation.Add | Operation.QueryAll | Operation.Modify;
            Console.WriteLine(operation);
        }
    }

    /// <summary>
    /// 位枚兴
    /// </summary>
    [Flags]
    enum Operation
    {
        None = 0,
        Add = 1,
        Modify = 2,
        Remove = 4,
        Query = 8,
        QueryAll = 16
    }
}
