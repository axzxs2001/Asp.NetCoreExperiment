using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
    * 外观模式
    * 为子系统中的一组接口提供一个一致的界面，此模式定义了一个高层接口，这个接口使得这一子系统更加容易使用
    ****************************************************************************/
    /// <summary>
    /// 外观类
    /// </summary>
    public class Facade
    {
        SubSystemOne _subOne;
        SubSystemTow _subTow;
        SubSystemThree _subThree;
        public Facade()
        {
            _subOne = new SubSystemOne();
            _subTow = new SubSystemTow();
            _subThree = new SubSystemThree();
        }

        public void FacadeOne()
        {
            Console.WriteLine("Facade.FacadeOne");
            _subOne.MethodOne();
            _subTow.MethodTow();
        }
        public void FacadeTow()
        {
            Console.WriteLine("Facade.FacadeTow");
            _subOne.MethodOne();
            _subThree.MethodThree();
        }
    }

    /// <summary>
    /// 子功能一
    /// </summary>
    public class SubSystemOne
    {
        public void MethodOne()
        {
            Console.WriteLine("SubSystemOne.MethodOne");
        }
    }
    /// <summary>
    /// 子功能二
    /// </summary>
    public class SubSystemTow
    {
        public void MethodTow()
        {
            Console.WriteLine("SubSystemTow.MethodTow");
        }
    }
    /// <summary>
    /// 子功能三
    /// </summary>
    public class SubSystemThree
    {
        public void MethodThree()
        {
            Console.WriteLine("SubSystemThree.MethodThree");
        }
    }
}
