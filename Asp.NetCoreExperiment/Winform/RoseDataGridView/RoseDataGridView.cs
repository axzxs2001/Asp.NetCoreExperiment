using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoseControles
{
    public class RoseDataGridView : DataGridView
    {
        /// <summary>
        /// 焦点序列
        /// </summary>
        public int[] FocusIndexs;

        /// <summary>
        /// 触发事件列的序列
        /// </summary>
        public int[] EditColumnIndexs;

        /// <summary>
        /// 重写OnCellEndEdit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCellEndEdit(DataGridViewCellEventArgs e)
        {
            if (EditColumnIndexs.Contains(this.CurrentCell.ColumnIndex) && EditValues != null)
            {
                foreach (var item in EditValues)
                {
                    this.Rows[this.CurrentCell.RowIndex].Cells[item.ColumnIndex].Value = item.CellValue;
                }
            }
            base.OnCellEndEdit(e);
        }

        public EditColumnValue[] EditValues { get; set; }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == Keys.Enter && this.IsCurrentCellInEditMode)
            {
                if (this.Columns.Count == this.CurrentCell.ColumnIndex + 1)
                {
                    (this.DataSource as DataTable).Rows.Add();
                    SendKeys.Send("{DOWN}");
                    SendKeys.Send("{HOME}");
                }
                else
                {
                    if (EditColumnIndexs.Contains(this.CurrentCell.ColumnIndex))
                    {
                        var cellValue = this.CurrentCell.EditedFormattedValue;
                        EditValues = ValueEdited(this.CurrentCell.RowIndex, this.CurrentCell.ColumnIndex, cellValue);

                    }
                    for (var i = 0; i < FocusIndexs.Length - 1; i++)
                    {
                        if (FocusIndexs[i] == (this.CurrentCell.ColumnIndex))
                        {
                            SendKeys.Send($"{{Tab {FocusIndexs[i + 1] - FocusIndexs[i]}}}");
                        }
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public event CellValueEventHanlder ValueEdited;
    }






}
