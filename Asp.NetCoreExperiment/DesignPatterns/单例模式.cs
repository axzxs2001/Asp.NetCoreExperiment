using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
    * 单例模式
    * 保证一个类仅有一个实例，并提供一个访问它的全局访问点。
    ****************************************************************************/
    public class Singleton
    {
        static Singleton instance;
        private Singleton()
        {

        }
        public static Singleton GetInstance()
        {
            if(instance==null)
            {
                instance = new Singleton();
            }
            return instance;
        }

    }
}
