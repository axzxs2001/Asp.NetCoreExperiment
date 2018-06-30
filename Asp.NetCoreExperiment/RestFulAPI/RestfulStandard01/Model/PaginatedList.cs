using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestfulStandard01.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PaginatedList<T> : List<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        public PaginationBase PaginationBase { get; }
        /// <summary>
        /// 
        /// </summary>
        public int TotalItemCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int PageCount => TotalItemCount / PaginationBase.PageSize + (TotalItemCount % PaginationBase.PageSize > 0 ? 1 : 0);
        /// <summary>
        /// 
        /// </summary>
        public bool HasPrevious => PaginationBase.PageIndex > 0;
        /// <summary>
        /// 
        /// </summary>
        public bool HasNext => PaginationBase.PageIndex < PageCount - 1;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalItemCount"></param>
        /// <param name="itmes"></param>
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
