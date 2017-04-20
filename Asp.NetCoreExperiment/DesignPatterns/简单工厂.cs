using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
     * 简单工厂
     ****************************************************************************/ 

    /// <summary>
    /// 工厂方法
    /// </summary>
    public class OperationFactory
    {
        public static Operation CreateOperate(int operate)
        {
            Operation operation = null;
            switch (operate)
            {
                case 1:
                    operation = new OneOperation();
                    break;
                case 2:
                    operation = new TowOperation();
                    break;
            }
            return operation;
        }
    }

    /// <summary>
    /// 父类
    /// </summary>
    public abstract class Operation
    {
        public abstract void GetResult();
    }
    /// <summary>
    /// 子类一
    /// </summary>
    public class OneOperation : Operation
    {
        public override void GetResult()
        {
            Console.WriteLine($"OneOperation.GetResult");
        }
    }
    /// <summary>
    /// 子类二
    /// </summary>
    public class TowOperation : Operation
    {
        public override void GetResult()
        {
            Console.WriteLine($"TowOperation.GetResult");
        }
    }
}
