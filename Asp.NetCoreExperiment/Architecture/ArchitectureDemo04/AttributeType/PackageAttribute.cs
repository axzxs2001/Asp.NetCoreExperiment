using System;


namespace ArchitectureDemo04
{
    /// <summary>
    /// 报文中属性的顺序SN和长度Length
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PackageAttribute : Attribute
    {
        /// <summary>
        /// 序号，从1开始
        /// </summary>
        public int SN
        { get; private set; }
        /// <summary>
        /// 转成字符串后的长度
        /// </summary>
        public int Length
        { get; private set; }
        /// <summary>
        /// 发关报文实体类属性特性类
        /// </summary>
        /// <param name="SN">属性的序号，从1开始</param>
        /// <param name="Length">属性转成字符串后长度</param>
        public PackageAttribute(int SN, int Length)
        {
            this.SN = SN;
            this.Length = Length;
        }
        /// <summary>
        /// 是否是时间类型,因为时间类型是左对齐，右补空格
        /// </summary>
        public bool IsDateTime
        { get; set; }
    }
}
