using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaitForm
{
    public partial class LoadingForm : Form
    {
        public LoadingForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
        }

        public string Message
        {
            set
            {
                this.Invoke(() =>
                {
                    messageLab.Text = value;
                });
            }
        }

        public void CloseAll()
        {
            this.Invoke(() =>
            {
                this.Close();
                this.Dispose();
            });
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {

        }
    }
}
