using System;

namespace HisMedical
{
    /// <summary>
    /// HIS
    /// </summary>
    public interface IHis
    {
        /// <summary>
        /// his登记号
        /// </summary>
        string RegisterID { set; }
        /// <summary>
        /// 住院登记
        /// </summary>
        /// <returns></returns>
        dynamic Register();
        /// <summary>
        /// 缴费
        /// </summary>
        /// <returns></returns>
        dynamic Fee();
    }
}
