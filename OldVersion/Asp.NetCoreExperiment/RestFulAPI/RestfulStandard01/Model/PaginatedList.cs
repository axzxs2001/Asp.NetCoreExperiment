using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulStandard01.Model
{
    /// <summary>
    /// 分页集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedList<T> : List<T> where T : class
    {
        /// <summary>
        /// 分页父类
        /// </summary>
        public PaginationBase PaginationBase { get; }
        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalItemCount { get; set; }
        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount => TotalItemCount / PaginationBase.PageSize + (TotalItemCount % PaginationBase.PageSize > 0 ? 1 : 0);
        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPrevious => PaginationBase.PageIndex > 0;
        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNext => PaginationBase.PageIndex < PageCount - 1;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">一页记录数</param>
        /// <param name="totalItemCount">总页数</param>
        /// <param name="itmes">当页实体集合</param>
        public PaginatedList(int pageIndex, int pageSize, int totalItemCount, IEnumerable<T> itmes)
        {
            PaginationBase = new PaginationBase
            {
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            TotalItemCount = totalItemCount;
            AddRange(itmes);
        }

    
    }
}
