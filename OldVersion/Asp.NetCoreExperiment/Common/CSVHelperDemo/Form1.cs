using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSVHelperDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dia = new OpenFileDialog())
            {
                if (dia.ShowDialog() == DialogResult.OK)
                {
                    using (var reader = new StreamReader(dia.FileName))
                    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                    {
                        // Do any configuration to `CsvReader` before creating CsvDataReader.
                        using (var dr = new CsvDataReader(csv))
                        {
                            var dt = new DataTable();
                            dt.Load(dr);
                            var newdt = new DataTable();
                            foreach (DataColumn col in dt.Columns)
                            {
                                newdt.Columns.Add(col.ColumnName, col.DataType);
                            }
                            foreach (DataRow row in dt.Rows)
                            {
                                newdt.Rows.Add(row.ItemArray);
                            }
                            dataGridView1.DataSource = newdt;

                        }
                    }
                }
            }
        }
    }
}
