using System.Collections.Generic;
using System.Data;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace WinFormDemo11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DataTable table = new();
        private void Form1_Load(object sender, EventArgs e)
        {

            table.Columns.Add("sn", typeof(int)).ReadOnly = true;
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("price", typeof(decimal));
            table.Columns.Add("quantity", typeof(double));
            table.Columns.Add("amount", typeof(decimal), "price*quantity");
            table.Columns.Add("memory", typeof(string));
            DataGrid.DataSource = table;


            DataGrid.EditingControlShowing += DataGrid_EditingControlShowing;
            DataGrid.RowsRemoved += DataGrid_RowsRemoved;

            table.Rows.Add(table.NewRow()[0] = table.Rows.Count + 1);
            DataGrid.CurrentCell = DataGrid.Rows[0].Cells[1];
            DataGrid.Focus();


        }

        private void DataGrid_RowsRemoved(object? sender, DataGridViewRowsRemovedEventArgs e)
        {
          // table.
        }

        LinkedList<int> forcelinked = new LinkedList<int>(new int[] { 1, 2, 3, 5 });


        private void DataGrid_EditingControlShowing(object? sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                var grid = (DataGridView)sender!;
                var textBox = (DataGridViewTextBoxEditingControl)e.Control;
                var columnType = grid.CurrentCell.ValueType;

                textBox.KeyPress -= Tb_KeyPress;
                if (IsNumericType(columnType))
                {
                    textBox.KeyPress += Tb_KeyPress;
                }
                textBox.KeyUp -= Tb_KeyUp;
                textBox.KeyUp += Tb_KeyUp;
            }
        }

        public bool IsNumericType(Type type)
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
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
        private void Tb_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void Tb_KeyUp(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var next = forcelinked.Find(DataGrid.CurrentCell.ColumnIndex)!.Next;
                if (next != null)
                {
                    DataGrid.CurrentCell = DataGrid.Rows[DataGrid.CurrentCell.RowIndex].Cells[next.Value];
                }
                else
                {
                    table.Rows.Add(table.NewRow()[0] = table.Rows.Count + 1);
                    DataGrid.CurrentCell = DataGrid.Rows[DataGrid.CurrentCell.RowIndex + 1].Cells[forcelinked.First!.Value];
                }
            }
        }











        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(table.Rows.Count.ToString());
        }
    }
}