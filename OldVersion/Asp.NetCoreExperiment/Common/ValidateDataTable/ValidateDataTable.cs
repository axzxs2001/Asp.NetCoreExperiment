using System;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace ValidateDataTableLib
{
    /// <summary>
    /// 一个验证Table，设置每列的正则，当调用Validate时，给附加验证后的错误信息
    /// </summary>
    public class ValidateDataTable : DataTable
    {

        private (string ColumnName, (string RegularExpression, string Error)[] RegularExpressions)[] _validateCollections;
        /// <summary>
        /// 验证表达式
        /// </summary>
        public (string ColumnName, (string RegularExpression, string Error)[] RegularExpressions)[] ValidateCollections
        {
            get
            {
                return _validateCollections;
            }
            set
            {
                _validateCollections = value;
                ValidateResult = Validate(ValidateCollections);
                CellChanged?.Invoke(this, null);
            }
        }
        /// <summary>
        /// 单元格改变事件
        /// </summary>
        public event DataRowChangeEventHandler CellChanged;
        protected override void OnRowChanged(DataRowChangeEventArgs e)
        {
            base.OnRowChanged(e);
            if (e.Action == DataRowAction.Change)
            {
                ValidateResult = Validate(ValidateCollections);
                CellChanged?.Invoke(this, e);
            }
        }
        /// <summary>
        /// 验证结果
        /// </summary>
        public (bool result, int error) ValidateResult;
        /// <summary>
        /// 开始验证
        /// </summary>
        /// <param name="ValidateCollections">验证列集合，每条验证用ColumnName,和正则集合</param>
        public (bool result, int error) Validate(params (string ColumnName, (string RegularExpression, string Error)[] RegularExpressions)[] ValidateCollections)
        {
            var result = true;
            var errorCount = 0;
            if (ValidateCollections != null && ValidateCollections.Length > 0)
            {
                foreach (var validateColumn in ValidateCollections)
                {
                    foreach (DataRow row in this.Rows)
                    {
                        var value = row[validateColumn.ColumnName]?.ToString();
                        var errors = new StringBuilder();
                        var sn = 1;
                        foreach (var regularItem in validateColumn.RegularExpressions)
                        {
                            var regex = new Regex(regularItem.RegularExpression);
                            if (!regex.IsMatch(value))
                            {
                                errors.AppendLine($"{sn++}、{regularItem.Error}");
                                errorCount++;
                            }
                        }
                        result = false;
                        row.SetColumnError(validateColumn.ColumnName, errors.ToString());
                    }
                }
            }
            return (result, errorCount);
        }

    }
}