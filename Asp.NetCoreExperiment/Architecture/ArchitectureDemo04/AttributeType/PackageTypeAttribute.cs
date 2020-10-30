using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArchitectureDemo04
{
    /// <summary>
    /// 发送报文类型
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class PackageTypeAttribute : Attribute
    {

        /// <summary>
        /// 发关报文实体类属性特性类
        /// </summary>
        /// <param name="SN">属性的序号，从1开始</param>
        /// <param name="Length">属性转成字符串后长度</param>
        public PackageTypeAttribute(uint OperationType, uint DataFormaterType, uint MinLength)
        {
            this.OperationType = OperationType;
            this.DataFormaterType = DataFormaterType;
            this.MinLength = MinLength;
        }
        /// <summary>
        /// 业务请求类型
        /// </summary>
        public uint OperationType
        { get; private set; }
        /// <summary>
        /// 数据解析格式类型
        /// </summary>
        public uint DataFormaterType
        { get; private set; }
        /// <summary>
        /// 数据串最小长度
        /// </summary>
        public uint MinLength
        { get; private set; }
    }
}
