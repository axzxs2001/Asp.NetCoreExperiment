using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulStandard01.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class PaginationBase
    {
        int _maxPageSize = 100;
        int _pageSize = 10;
        /// <summary>
        /// 
        /// </summary>
        public int PageIndex { get; set; } = 0;
        /// <summary>
        /// 
        /// </summary>
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > _maxPageSize ? _maxPageSize : value;
        }
        /// <summary>
        /// 
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
