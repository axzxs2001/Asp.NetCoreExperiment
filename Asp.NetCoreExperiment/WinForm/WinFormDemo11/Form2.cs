using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace WinFormDemo11
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Focus();
        }
    }

    public class MyGrid : DataGridView
    {
        DataTable table;
        public MyGrid()
        {
            table = new();
        }

        protected override void OnCreateControl()
        {
            this.AllowUserToAddRows = false;
            this.AllowUserToOrderColumns = true;

            this.EditMode = DataGridViewEditMode.EditOnEnter;
            this.MultiSelect = false;
            table.Columns.Add("sn", typeof(int)).ReadOnly = true;
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("price", typeof(decimal));
            table.Columns.Add("quantity", typeof(double));
            table.Columns.Add("amount", typeof(decimal), "price*quantity");
            table.Columns.Add("memory", typeof(string));
            table.Rows.Add(table.NewRow()[0] = table.Rows.Count + 1);
            this.DataSource = table;

            foreach (DataGridViewColumn column in this.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            base.OnCreateControl();
        }
        protected override void OnDataBindingComplete(DataGridViewBindingCompleteEventArgs e)
        {
            base.OnDataBindingComplete(e);
            this.ClearSelection();
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.CurrentCell = this.Rows[0].Cells[1];
            this.ClearSelection();
        }
        bool IsDecimalType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
        bool IsIntType(Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                    return true;
                default:
                    return false;
            }
        }

        protected override void OnEditingControlShowing(DataGridViewEditingControlShowingEventArgs e)
        {

            base.OnEditingControlShowing(e);
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                var textBox = (DataGridViewTextBoxEditingControl)e.Control;
                var columnType = this.CurrentCell.ValueType;

                textBox.KeyPress -= DecimalTextBox_KeyPress;
                textBox.KeyPress -= IntTextBoxb_KeyPress;
                if (IsIntType(columnType))
                {
                    textBox.KeyPress += IntTextBoxb_KeyPress;
                }
                else
                {
                    if (IsDecimalType(columnType))
                    {
                        textBox.KeyPress += DecimalTextBox_KeyPress;
                    }
                }
            }
        }
        private void DecimalTextBox_KeyPress(object? sender, KeyPressEventArgs e)
        {
            var result = decimal.TryParse(((DataGridViewTextBoxEditingControl)sender!).Text + e.KeyChar, out decimal val);
            if (!result)
            {
                e.Handled = true;
            }
        }
        private void IntTextBoxb_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }


        LinkedList<int> forcelinked = new LinkedList<int>(new int[] { 1, 2, 3, 5 });
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (IsCurrentCellInEditMode && keyData == Keys.Tab)
            {
                MessageBox.Show("不要在Gid中按tab");
                this.CurrentCell = this.Rows[0].Cells[0];
                this.ClearSelection();
                return false;
            }

            if (IsCurrentCellInEditMode && keyData == Keys.Enter)
            {
                var next = forcelinked.Find(this.CurrentCell.ColumnIndex)!.Next;
                if (next != null)
                {
                    this.CurrentCell = this.Rows[this.CurrentCell.RowIndex].Cells[next.Value];
                }
                else
                {
                    table.Rows.Add(table.NewRow()[0] = table.Rows.Count + 1);
                    this.CurrentCell = this.Rows[this.CurrentCell.RowIndex + 1].Cells[forcelinked.First!.Value];
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }

}
