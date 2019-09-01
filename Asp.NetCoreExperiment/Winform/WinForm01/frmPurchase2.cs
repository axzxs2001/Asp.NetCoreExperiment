using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RoseControles;

namespace WinForm01
{
    public partial class frmPurchase2 : Form
    {



        public frmPurchase2()
        {
            InitializeComponent();

        }


        private void FrmPurchase2_Load(object sender, EventArgs e)
        {

            dgvData.AllowUserToAddRows = false;
            var table = new DataTable();
            table.Columns.Add("名称", typeof(string));
            table.Columns.Add("厂家", typeof(string));
            table.Columns.Add("规格", typeof(string));
            table.Columns.Add("数量", typeof(float));
            table.Columns.Add("进价", typeof(decimal));
            table.Columns.Add("日期", typeof(DateTime));
            table.Columns.Add("批号", typeof(string));
            table.Columns.Add("备注", typeof(string));
            table.Rows.Add();

            dgvData.DataSource = table;
            dgvData.FocusIndexs = new int[] { 0, 3, 4, 5, 6, 7 };
            dgvData.EditColumnIndexs = new int[] { 0, 5 };
            dgvData.ValueEdited += DgvData_ValueEdited; ;


        }

        private EditColumnValue[] DgvData_ValueEdited(int rowIndex, int columnIndex, dynamic cell)
        {
            switch (columnIndex)
            {
                case 0:
                    var frmGoodses = new frmSelectGoodes(cell);
                    frmGoodses.ShowDialog();

                    return new EditColumnValue[]{
                        new EditColumnValue(0,frmGoodses.SelectGoodes.Name),
                        new EditColumnValue(1,frmGoodses.SelectGoodes.CJ),
                        new EditColumnValue(2,frmGoodses.SelectGoodes.GG),
                    };
                case 5:
                    return new EditColumnValue[]{
                        new EditColumnValue(columnIndex,DateTime.Now.ToString())
                    };
                default:
                    return null;
            }
        }

        private void DgvData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show(e.Exception.Message);
            dgvData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DBNull.Value;

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            var table = (dgvData.DataSource as DataTable);
        }

        private void FrmPurchase2_Shown(object sender, EventArgs e)
        {

            dgvData.Focus();

        }
    }



}
