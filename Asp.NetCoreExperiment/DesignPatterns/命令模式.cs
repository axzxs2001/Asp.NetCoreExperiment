using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
    * 命令模式
    * 将一个请求封闭为一个对象,从而使你可用不同的请求对客户进行参数化;对请求排队或记录请求日志,以及支持可撤销的操作。
    ****************************************************************************/
    public class Receiver
    {
        public void Action()
        {
            Console.WriteLine("执行请求");
        }
    }
    /// <summary>
    /// 命令类
    /// </summary>
    public abstract class Command
    {
        protected Receiver _receiver;
        public Command(Receiver receiver)
        {
            _receiver = receiver;
        }
        public abstract void Execute();
    }
    public class ConcreteCommand:Command
    {
        public ConcreteCommand(Receiver receiver):base(receiver)
        { }
        public override void Execute()
        {
            _receiver.Action();
        }
    }
    /// <summary>
    /// 调用命令类
    /// </summary>
    public class Invoker
    {
        Command _command;
        public void SetCommand(Command command)
        {
            _command = command;
        }
        public void ExecuteCommand()
        {
            _command.Execute();
        }
    }
}
