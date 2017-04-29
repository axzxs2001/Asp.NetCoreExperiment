using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.装饰模式
{
    public class 装饰模式_Tax
    {
        public static void Start()
        {
            var personTax = new PersonTax();
            personTax.Sum = 5000;

            var medical = new ComputeTaxMedical();
            medical.ComputeTax = personTax;

            var headTax = new ComputeTaxLocal();
            headTax.ComputeTax = medical;
            headTax.Pay();
        }
    }
    public abstract class ComputeTax
    {
        public decimal Sum
        { get; set; }
        public abstract void Pay();
    }

    /// <summary>
    /// 计算税法
    /// </summary>
    public class PersonTax : ComputeTax
    {
        public override void Pay()
        {
            var tax = Sum * 0.01m;
            Sum = Sum - tax;
            Console.WriteLine($"这里要交1%的所得税:{tax} 你剩余：{Sum}");
        }
    }
    public class Decorator : ComputeTax
    {
        /// <summary>
        /// 用属性来传递
        /// </summary>
        public ComputeTax ComputeTax
        {
            set;
            protected get;
        }
        /// <summary>
        /// 用方法来扩展和调用父方法，实现串联   
        /// </summary>
        public override void Pay()
        {
            if (ComputeTax != null)
            {
                ComputeTax.Pay();
                Console.WriteLine("Decorator.Pay执行，没有扣钱");
            }
        }
    }

    public class ComputeTaxMedical : Decorator
    {
        public override void Pay()
        {
            base.Pay();
            var tax = 200;
            //这里的ComputeTax是PersonTax，所以他的Sum是有值的
            Sum = ComputeTax.Sum = ComputeTax.Sum - tax;
            Console.WriteLine($"这里要交200块的保险 你剩余：{ComputeTax.Sum}");

        }
    }
    public class ComputeTaxLocal : Decorator
    {
        public override void Pay()
        {
            base.Pay();
            //ComputeTaxLocal的ComputeTax是ComputeTaxMedical，所以他的Sum如果不赋值时为空
            var tax = ComputeTax.Sum * 0.02m;
            Sum = ComputeTax.Sum = ComputeTax.Sum - tax;
            Console.WriteLine($"这里要交2%的地方税:{tax} 你剩余：{ComputeTax.Sum }");
        }
    }
}
