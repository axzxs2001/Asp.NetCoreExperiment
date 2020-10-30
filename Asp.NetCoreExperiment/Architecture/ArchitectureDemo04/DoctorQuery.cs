
using System;


namespace ArchitectureDemo04
{

    /// <summary>
    /// 医师信息查询
    /// </summary>
    [PackageType(1009, 504, 85)]
    public class DoctorQuery : Entity
    {
        /// <summary>
        /// 医师编码
        /// </summary>
        [Package(1, 8)]
        public virtual String DoctorCode
        {
            get; set;
        }
        /// <summary>
        /// 医师姓名
        /// </summary>
        [Package(2, 20)]
        public virtual String DoctorName
        { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        [Package(3, 18)]
        public virtual string PersonID
        { get; set; }

        ///编号为0002的医院， 下属有编号为0113的定点， 在总院注册登记的医师可以在这样的2家医院出诊， 则“可出诊医院编号”为00020113，若长度不足20位则前补空格。
        /// <summary>
        /// 可出诊医院编号
        /// </summary>
        [Package(4, 20)]
        public virtual string CanVisitHospitalCode
        { get; set; }

        /// <summary>
        /// 终止日期
        /// </summary>
        [Package(5, 16, IsDateTime = true)]
        public virtual string TerminationTime
        { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        [Package(6, 1)]
        public virtual DLYBAvailableMarker DLYBAvailableMarker
        { get; set; }
    }

    /// <summary>
    /// 有效标志
    /// </summary>
    [EnumValeuNumber]
    public enum DLYBAvailableMarker
    {
        /// <summary>
        /// 无效
        /// </summary>
        nullity = 0,
        /// <summary>
        /// 有效
        /// </summary>
        Valid = 1
    }
}
