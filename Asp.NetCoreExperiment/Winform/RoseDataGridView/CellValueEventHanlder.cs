using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseControles
{
    /// <summary>
    /// 单元格值处理委托
    /// </summary>
    /// <param name="rowIndex">行索引</param>
    /// <param name="columnIndex">列索引</param>
    /// <param name="cell">单元格</param>
    /// <returns></returns>
    public delegate EditColumnValue[] CellValueEventHanlder(int rowIndex, int columnIndex, dynamic cell);
}
