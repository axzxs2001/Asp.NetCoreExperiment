using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinForm01
{
    public partial class frmSelectGoodes : Form
    {
        readonly string _findString;
        public frmSelectGoodes(string findString)
        {
            _findString = findString;
            InitializeComponent();
            this.Text = _findString;
        }

        private void FrmSelectGoodes_Load(object sender, EventArgs e)
        {
            var table = new DataTable();
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("CJ", typeof(string));
            table.Columns.Add("GG", typeof(string));
            table.Rows.Add("阿莫西林1", "桂林药厂", "1.25g");
            table.Rows.Add("阿莫西林2", "桂林药厂", "2.25g");
            table.Rows.Add("阿莫西林3", "桂林药厂", "3.25g");
            table.Rows.Add("阿莫西林4", "桂林药厂", "4.25g");
            dgvData.DataSource = table;
        }


        public SelectGoodes SelectGoodes
        {
            get; set;
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                var row = dgvData.SelectedRows[0];
                SelectGoodes = new SelectGoodes
                {
                    Name = row.Cells["Name"].Value.ToString(),
                    CJ = row.Cells["CJ"].Value.ToString(),
                    GG = row.Cells["GG"].Value.ToString()
                };
                //SendKeys.Send("{Up}");
                this.Close();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }

    public class SelectGoodes
    {
        public string Name { get; set; }
        public string CJ { get; set; }

        public string GG { get; set; }
    }

}
