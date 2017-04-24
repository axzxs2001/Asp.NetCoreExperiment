using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
     * 职责链模式
     * 使多个对象都有机会处理请求，从而避免请求的发送者和接收者之间的耦合关系。将这个对象连成一条链，并沿着这条链传递该请求，直到有一个对象处理它为止。
    ****************************************************************************/

    /// <summary>
    /// 处理对象父类
    /// </summary>
    public abstract class Handler
    {
        protected Handler _successor;
        public void SetSuccessor(Handler successor)
        {
            _successor = successor;
        }
        public abstract void HandleRequest(int request);
    }
    /// <summary>
    /// 处理类1
    /// </summary>
    public class ConcreteHandler1 : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 0 && request < 10)
            {
                Console.WriteLine($"{this.GetType().Name}处理请求，request={request}");
            }
            else
            {
                if (_successor != null)
                {
                    _successor.HandleRequest(request);
                }
            }
        }
    }
    /// <summary>
    /// 处理类2
    /// </summary>
    public class ConcreteHandler2 : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 10 && request < 20)
            {
                Console.WriteLine($"{this.GetType().Name}处理请求，request={request}");
            }
            else
            {
                if (_successor != null)
                {
                    _successor.HandleRequest(request);
                }
            }
        }
    }
    /// <summary>
    /// 处理类3
    /// </summary>
    public class ConcreteHandler3 : Handler
    {
        public override void HandleRequest(int request)
        {
            if (request >= 20 && request < 30)
            {
                Console.WriteLine($"{this.GetType().Name}处理请求，request={request}");
            }
            else
            {
                if (_successor != null)
                {
                    _successor.HandleRequest(request);
                }
            }
        }
    }
}
