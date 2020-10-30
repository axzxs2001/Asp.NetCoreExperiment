using System;

namespace ArchitectureDemo04
{
    /// <summary>
    /// 取枚举的值还是
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum)]
    public class EnumValeuNumberAttribute : Attribute
    {
        /// <summary>
        /// 是否把枚举类型属性的的值数转成Char类型
        /// </summary>
        public bool IsChar
        { get; set; }
    }
}
