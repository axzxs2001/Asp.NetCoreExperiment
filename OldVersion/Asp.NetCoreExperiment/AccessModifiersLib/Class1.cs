using System;

namespace AccessModifiersLib
{
    public class Class1
    {
    }
    public class BaseClass
    {
        private protected int myValue = 0;
    }
    public class DerivedClass1 : BaseClass
    {
        void Access()
        {
            var baseObject = new BaseClass();       
            //下载代码错误，因为是私有，实例化看不到
             //baseObject.myValue = 5;  

            //同一个程序集内能访问
            myValue = 5;
        }
    }
}
