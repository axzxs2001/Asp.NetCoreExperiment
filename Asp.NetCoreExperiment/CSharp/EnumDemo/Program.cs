using System;

namespace EnumDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            EnumConvert();

            FlagsEnumTest();
        }
        /// <summary>
        /// 枚举类型转换
        /// </summary>
        static void EnumConvert()
        {
            //用枚举值转换
            var sex01 = (Sex)0;
            Console.WriteLine($"字符串male转换后的类型：{sex01},对应值：{(int)sex01}");
            //用Enum.Parse转换
            var sex02 = (Sex)Enum.Parse(typeof(Sex), "Male");
            Console.WriteLine($"字符串male转换后的类型：{sex02},对应值：{(int)sex02}");
            //列出所有sex的枚举名称
            foreach (var item in Enum.GetNames(typeof(Sex)))
            {
                Console.WriteLine($"Sex:{item} value:{(int)Enum.Parse(typeof(Sex), item)}");
            }
            //列出所有sex的枚举名称
            foreach (var item in Enum.GetValues(typeof(Sex)))
            {
                Console.WriteLine($"Sex:{item} value:{(int)item}");
            }
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

    enum Sex
    {
        Famale = 0,
        Male = 1,
        Other = 2
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
