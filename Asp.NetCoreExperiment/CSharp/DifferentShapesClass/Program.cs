using System;

namespace DifferentShapesClass
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");   
        }    
    }

    #region 普通类

    /// <summary>
    /// 普通类
    /// </summary>
    public class CommonClass
    {
        /// <summary>
        /// 自定义枚举类型
        /// </summary>

        enum MyEnum
        {
            enum1,
            enum2
        }
        /// <summary>
        /// 自定义委托类型
        /// </summary>
        public delegate void MyDeleaget();
        /// <summary>
        /// 构造函数
        /// </summary>
        public CommonClass()
        {
            _arr = new double[10];
        }
        /// <summary>
        /// 字段
        /// </summary>
        private string _myField;
        /// <summary>
        /// 属性
        /// </summary>
        public string MyProperty { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public void MyMethod()
        { }
        /// <summary>
        /// 事件
        /// </summary>
        public event MyDeleaget MyEvent;
        /// <summary>
        /// 运算符重载
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static CommonClass operator +(CommonClass a, CommonClass b)
        {
            return new CommonClass() { MyProperty = a.MyProperty + b.MyProperty };
        }
        /// <summary>
        /// 索引器集合
        /// </summary>
        double[] _arr;
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                return _arr[index];
            }
            set
            {
                _arr[index] = value;
            }
        }
    }
    #endregion

    #region 静态类
    public static class StaticClass
    {
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static StaticClass()
        {
            Console.WriteLine("静态类构造函数");
        }
        /// <summary>
        /// 静态方法
        /// </summary>
        public static void StaticMethod()
        {
            Console.WriteLine("静态类中静态方法");
        }
        /// <summary>
        /// 静态属性
        /// </summary>
        public static string StaticProperty { get; set; }

        /// <summary>
        /// 自定义委托类型
        /// </summary>
        public delegate void MyDeleaget();

        /// <summary>
        /// 字段
        /// </summary>
        private static string _myField;

        /// <summary>
        /// 事件
        /// </summary>
        public static event MyDeleaget MyEvent;


    }
    #endregion

    #region 抽像类
    /// <summary>
    /// 抽像类
    /// </summary>
    public abstract class AbstractClass
    {
        public AbstractClass()
        {
            Console.WriteLine("抽象类构造函数");
        }
        /// <summary>
        /// 自定义委托类型
        /// </summary>
        public delegate void MyDeleaget();

        /// <summary>
        /// 属性
        /// </summary>
        public string MyProperty { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public abstract void MyMethod();

        /// <summary>
        /// 事件
        /// </summary>
        public abstract event MyDeleaget MyEvent;


        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public abstract double this[int index] { get; set; }
    }
    #endregion

    #region 密封类
    /// <summary>
    /// 密封类
    /// </summary>
    public sealed class SealedClass
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SealedClass()
        {
            Console.WriteLine("密封类构造函数");
        }
        /// <summary>
        /// 自定义委托类型
        /// </summary>
        public delegate void MyDeleaget();

        /// <summary>
        /// 字段
        /// </summary>
        private string _myField;
        /// <summary>
        /// 属性
        /// </summary>
        public string MyProperty { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public void MyMethod()
        { }
        /// <summary>
        /// 事件
        /// </summary>
        public event MyDeleaget MyEvent;
        /// <summary>
        /// 运算符重载
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static SealedClass operator +(SealedClass a, SealedClass b)
        {
            return new SealedClass() { MyProperty = a.MyProperty + b.MyProperty };
        }
        /// <summary>
        /// 索引器集合
        /// </summary>
        double[] _arr;
        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                return _arr[index];
            }
            set
            {
                _arr[index] = value;
            }
        }
    }
    #endregion

    #region 分部类
    /// <summary>
    /// 分部类1
    /// </summary>
    public partial class PartialClass
    {
        public PartialClass()
        {
            Console.WriteLine("分部类构造函数");
        }

        /// <summary>
        /// 自定义委托类型
        /// </summary>
        public delegate void MyDeleaget();

        /// <summary>
        /// 字段
        /// </summary>
        private string _myField;
        /// <summary>
        /// 属性
        /// </summary>
        public string MyProperty { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public void MyMethod()
        { }

        /// <summary>
        /// 运算符重载
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static PartialClass operator +(PartialClass a, PartialClass b)
        {
            return new PartialClass() { MyProperty = a.MyProperty + b.MyProperty };
        }
        /// <summary>
        /// 索引器集合
        /// </summary>
        double[] _arr;
    }

    /// <summary>
    /// 分部类1
    /// </summary>
    public partial class PartialClass
    {
        /// <summary>
        /// 事件
        /// </summary>
        public event MyDeleaget MyEvent;

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double this[int index]
        {
            get
            {
                return _arr[index];
            }
            set
            {
                _arr[index] = value;
            }
        }

    }
    #endregion

}
