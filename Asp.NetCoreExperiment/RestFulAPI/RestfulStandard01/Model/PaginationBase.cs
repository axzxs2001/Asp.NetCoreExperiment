using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulStandard01.Model
{
    /// <summary>
    /// 分页父类
    /// </summary>
    public class PaginationBase
    {
        /// <summary>
        /// 最大页记录数
        /// </summary>
        int _maxPageSize = 100;
        /// <summary>
        /// 页记录数
        /// </summary>
        int _pageSize = 10;
        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex { get; set; } = 0;
        /// <summary>
        /// 页记录数
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > _maxPageSize ? _maxPageSize : value;
        }
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public PaginationBase Clone()
        {
            var type = GetType();
            var obj = Activator.CreateInstance(type);
            foreach (var pro in type.GetProperties())
            {
                pro.SetValue(obj, pro.GetValue(this));
            }
            return obj as PaginationBase;
        }
    }


}
