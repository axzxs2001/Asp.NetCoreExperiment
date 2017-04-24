using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
    * 抽象工厂
    * 给定一个语言，定义它的文法的一种表示，并定义一个解释器，这个解释器使用该表示来解释语言中的句子。
    ****************************************************************************/
    public class ExtContext
    {
        public string Input
        { get; set; }
        public string Output
        { get; set; }
    }

    /// <summary>
    /// 抽象语法树中所有节点
    /// </summary>
    public abstract class AbstractExperssion
    {
        public abstract void Interpret(ExtContext context);
    }

    public class TerminalExpression : AbstractExperssion
    {
        public override void Interpret(ExtContext context)
        {
            Console.WriteLine("终端解释器");
        }
    }

    public class NoterminalExpression : AbstractExperssion
    {
        public override void Interpret(ExtContext context)
        {
            Console.WriteLine("非终端解释器");
        }
    }
}
