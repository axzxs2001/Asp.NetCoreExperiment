using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseControles
{
    /// <summary>
    /// 编辑的单元格索引和值
    /// </summary>
    public class EditColumnValue
    {
        public EditColumnValue(int columnIndex, dynamic cellValue)
        {
            ColumnIndex = columnIndex;
            CellValue = cellValue;
        }
        /// <summary>
        /// 单元格索引
        /// </summary>
        public int ColumnIndex { get; set; }
        /// <summary>
        /// 单元格值
        /// </summary>
        public dynamic CellValue { get; set; }
    }
}
