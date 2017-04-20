using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{

    /**************************************************************
     * 策略模式
     * 它定义了算法家族，分别封装起来，让它们之间可以互相替换，此模式让算法的变化，不会影响到使用算法的客户 
     **************************************************************/

    /// <summary>
    /// 执行计算类
    /// </summary>
    public class Context
    {
        Strategy _strategy;
        /// <summary>
        /// 原策略构造
        /// </summary>
        /// <param name="strategy"></param>
        public Context(Strategy strategy)
        {
            _strategy = strategy;
        }
        //简单工厂的构造
        public Context(string strategyMark)
        {
            switch (strategyMark)
            {
                case "1":
                    _strategy = new OneStrategy();
                    break;
                case "2":
                    _strategy = new TowStrategy();
                    break;
            }
        }

        public void GetCompute()
        {
            _strategy.Compute();
        }
    }

    /// <summary>
    /// 抽象计算类
    /// </summary>
    public abstract class Strategy
    {
        public abstract void Compute();
    }
    /// <summary>
    /// 计算类一
    /// </summary>
    public class OneStrategy : Strategy
    {
        public override void Compute()
        {
            Console.WriteLine("OneStrategy.Compute");
        }
    }
    /// <summary>
    /// 计算类二
    /// </summary>
    public class TowStrategy : Strategy
    {
        public override void Compute()
        {
            Console.WriteLine("TowStrategy.Compute");
        }
    }
}
