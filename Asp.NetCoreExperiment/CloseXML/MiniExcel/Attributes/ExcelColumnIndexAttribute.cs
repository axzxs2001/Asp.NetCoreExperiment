﻿namespace MiniExcelLibs.Attributes
{
    using MiniExcelLibs.Utils;
    using System;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ExcelColumnIndexAttribute : Attribute
    {
        public int ExcelColumnIndex { get; set; }
        internal string ExcelXName { get; set; }
        public ExcelColumnIndexAttribute(string columnName) => Init(ColumnHelper
            .GetColumnIndex(columnName), columnName);
        public ExcelColumnIndexAttribute(int columnIndex) => Init(columnIndex);

        private void Init(int columnIndex,string columnName=null)
        {
            if (columnIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(columnIndex), columnIndex, $"Column index {columnIndex} must be greater or equal to zero.");
            }
            if (ExcelXName == null)
                if(columnName != null)
                    ExcelXName = columnName;
                else
                    ExcelXName = ColumnHelper.GetAlphabetColumnName(columnIndex);
            ExcelColumnIndex = columnIndex;
        }
    }
}
